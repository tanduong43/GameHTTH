package activities;

import java.io.IOException;
import client.Player;
import core.Service;
import core.Util;

/**
 * Quản lý tính năng Tặng Ruby giữa người chơi:
 * - Khi tương tác với người chơi khác và chọn "Tặng Ruby"
 * - 1 Vé tặng ruby (ID: 360, category: 4) = 10 Ruby
 * - Kiểm tra đủ số lượng vé và số lượng Ruby tương ứng trước khi tặng
 *
 * @author HTTH Dev
 */
public class GiftRuby {

    public static final short INPUT_ID_GIFT_RUBY = 3601;
    public static final short ITEM_TICKET_ID = 360; // Vé tặng 10 ruby (category 4)
    public static final int RUBY_PER_TICKET = 10;   // 1 vé = 10 ruby

    /**
     * Xử lý yêu cầu tặng ruby khi người chơi click "Tặng Ruby" trên người chơi khác
     *
     * @param p        Người chơi gửi tặng
     * @param targetId ID index_map của người chơi được tặng
     */
    public static void handleGiftRubyRequest(Player p, short targetId) throws IOException {
        if (p == null || p.map == null) {
            return;
        }

        Player target = p.map.get_player_by_id_inmap(targetId);
        if (target == null) {
            Service.send_box_ThongBao_OK(p, "Đối phương không ở gần hoặc đã rời khỏi khu vực!");
            return;
        }

        if (target.id == p.id || target.name.equalsIgnoreCase(p.name)) {
            Service.send_box_ThongBao_OK(p, "Không thể tự tặng ruby cho chính mình!");
            return;
        }

        int currentTickets = p.item.total_item_bag_by_id(4, ITEM_TICKET_ID);
        int currentRuby = p.get_ngoc();

        if (currentTickets <= 0) {
            Service.send_box_ThongBao_OK(p, "Bạn không có vé tặng!");
            return;
        }

        if (currentRuby < RUBY_PER_TICKET) {
            Service.send_box_ThongBao_OK(p, "Bạn không đủ Ruby để tặng!\nCần ít nhất " + RUBY_PER_TICKET
                    + " Ruby cho 1 vé (Bạn đang có " + Util.number_format(currentRuby) + " Ruby).");
            return;
        }

        // Lưu mục tiêu tặng
        p.tang_ruby_target = target;

        int maxTicketsByRuby = currentRuby / RUBY_PER_TICKET;
        int maxGiftable = Math.min(currentTickets, maxTicketsByRuby);

        Service.input_text(p, INPUT_ID_GIFT_RUBY,
                "Tặng Ruby cho " + target.name,
                new String[] { "Nhập số lượng vé:" });
    }

    /**
     * Xử lý khi người chơi nhập số lượng vé muốn tặng
     *
     * @param p    Người chơi gửi tặng
     * @param name Mảng input từ client (name[0] là số vé nhập vào)
     */
    public static void processGiftRubyInput(Player p, String[] name) throws IOException {
        if (p == null) {
            return;
        }

        if (name == null || name.length == 0 || name[0] == null || name[0].trim().isEmpty()) {
            p.tang_ruby_target = null;
            return;
        }

        String inputStr = name[0].trim();
        if (!Util.isnumber(inputStr)) {
            Service.send_box_ThongBao_OK(p, "Số lượng vé không hợp lệ! Vui lòng nhập số nguyên dương.");
            p.tang_ruby_target = null;
            return;
        }

        int tickets;
        try {
            tickets = Integer.parseInt(inputStr);
        } catch (Exception e) {
            Service.send_box_ThongBao_OK(p, "Số lượng vé không hợp lệ!");
            p.tang_ruby_target = null;
            return;
        }

        if (tickets <= 0) {
            Service.send_box_ThongBao_OK(p, "Số lượng vé muốn tặng phải lớn hơn 0!");
            p.tang_ruby_target = null;
            return;
        }

        Player target = p.tang_ruby_target;
        p.tang_ruby_target = null;

        if (target == null || target.conn == null || !target.conn.connected) {
            Service.send_box_ThongBao_OK(p, "Đối phương hiện không online hoặc đã ngắt kết nối!");
            return;
        }

        if (target.id == p.id || target.name.equalsIgnoreCase(p.name)) {
            Service.send_box_ThongBao_OK(p, "Không thể tự tặng ruby cho chính mình!");
            return;
        }

        int rubyRequired = tickets * RUBY_PER_TICKET;
        int currentTickets = p.item.total_item_bag_by_id(4, ITEM_TICKET_ID);
        int currentRuby = p.get_ngoc();

        // Kiểm tra số vé
        if (currentTickets < tickets) {
            Service.send_box_ThongBao_OK(p, "Bạn không đủ vé tặng ruby!\n• Cần: " + Util.number_format(tickets)
                    + " vé\n• Hiện có: " + Util.number_format(currentTickets) + " vé.");
            return;
        }

        // Kiểm tra số Ruby
        if (currentRuby < rubyRequired) {
            Service.send_box_ThongBao_OK(p, "Bạn không đủ Ruby!\n• Tặng " + Util.number_format(tickets)
                    + " vé cần: " + Util.number_format(rubyRequired) + " Ruby\n• Hiện có: "
                    + Util.number_format(currentRuby) + " Ruby.");
            return;
        }

        // Thực hiện trừ vé và Ruby của người gửi, cộng Ruby cho người nhận
        synchronized (p) {
            synchronized (target) {
                // Kiểm tra lại trong khối đồng bộ
                if (p.item.total_item_bag_by_id(4, ITEM_TICKET_ID) < tickets || p.get_ngoc() < rubyRequired) {
                    Service.send_box_ThongBao_OK(p, "Số lượng vé hoặc Ruby không đủ để thực hiện giao dịch!");
                    return;
                }

                // Trừ vé và Ruby của người gửi
                p.item.remove_item47(4, ITEM_TICKET_ID, tickets);
                p.update_ngoc(-rubyRequired);
                p.update_money();
                p.item.update_Inventory(-1, false);

                // Cộng Ruby cho người nhận
                target.update_ngoc(rubyRequired);
                target.update_money();
            }
        }

        // Thông báo kết quả cho cả 2 bên
        Service.send_box_ThongBao_OK(p, "Bạn đã tặng thành công " + Util.number_format(rubyRequired)
                + " Ruby cho người chơi " + target.name + "!");
        Service.send_box_ThongBao_OK(target, "Người chơi " + p.name + " đã tặng bạn "
                + Util.number_format(rubyRequired) + " Ruby!");
    }
}
