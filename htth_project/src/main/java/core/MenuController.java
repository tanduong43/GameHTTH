package core;

import java.io.IOException;
import java.util.ArrayList;
import java.util.Comparator;
import java.util.HashMap;
import java.util.List;
import org.joda.time.LocalTime;
import activities.*;
import client.*;
import database.SQL;
import event.Doriki;
import event.EventSpecial;
import event.LucThuc;
import event.SucManhVatLy;
import io.Message;
import io.Session;
import io.SessionManager;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.SQLException;
import map.*;
import org.json.simple.JSONObject;
import template.*;
/**
 *
 * @author Truongbk
 */
public class MenuController {
  public static int[] ID_MAP_LANG =
      new int[] {1, 9, 17, 25, 33, 41, 49, 66, 69, 79, 83, 93, 107, 113, 191};

  public static void send_menu(Player p, Message m) throws IOException {
    if (!p.isdie) {
      short type = m.reader().readShort();
      // System.out.println("npc id " + type);
      boolean in_map = false;
      for (int i = 0; i < p.map.template.npcs.size(); i++) {
        if (type == p.map.template.npcs.get(i).iditem) {
          in_map = true;
          break;
        }
      }
      if (!in_map) {
        return;
      }
      switch (type) {
        case -140: {
          send_dynamic_menu(p, type, "WIPPER", new String[] {"Chế tạo DIAL", "Thử thách vệ thần"},
              null);
          break;
        }
        case -86: {
          send_dynamic_menu(p, type, "Phó bản",
              new String[] {"Đá đít Mr3", "Phó bản khổng lồ", "Hướng dẫn Phó bản khổng lồ"},
              new short[] {150, 142, 148});
          break;
        }
        case -77: {
          send_dynamic_menu(p, type, "Ms Gym", new String[] {"BXH Đấu trường", "Hướng dẫn"}, null);
          break;
        }
        case -73: {
          send_dynamic_menu(p, type, "Croket", new String[] {"Hướng dẫn"}, null);
          break;
        }
        case -84: {
          if (p.clan != null) {
            if (p.clan.members.get(0).name.equals(p.name)) {
              if (p.clan.allowRequest == 1) {
                send_dynamic_menu(p, type, "Băng hải tặc",
                    new String[] {"Nhiệm vụ băng", "Huy hiệu hành trình", "Phó bản băng",
                        "Cửa hàng biểu tượng", "Cửa hàng vật phẩm", "Khóa xin vào băng"},
                    new short[] {141, 171, 146, 143, 144, 118});
              } else {
                send_dynamic_menu(p, type, "Băng hải tặc",
                    new String[] {"Nhiệm vụ băng", "Huy hiệu hành trình", "Phó bản băng",
                        "Cửa hàng biểu tượng", "Cửa hàng vật phẩm", "Mở xin vào băng"},
                    new short[] {141, 171, 146, 143, 144, 118});
              }
            } else {
              send_dynamic_menu(p, type, "Băng hải tặc",
                  new String[] {"Nhiệm vụ băng", "Huy hiệu hành trình", "Phó bản băng"},
                  new short[] {141, 171, 146});
            }
          } else {
            send_dynamic_menu(p, type, "Băng hải tặc",
                new String[] {"Đăng ký băng hải tặc (2000 Ruby)", "Hướng dẫn"}, null);
          }
          break;
        }
        case -138: { // npc law
          send_dynamic_menu(p, type, "Cường hóa máu", new String[] {"Phẫu thuật", "Hướng dẫn"},
              null);
          break;
        }
        case -106:
        case -91:
        case -71:
        case -48: {
          switch (p.map.template.id) {
            case 9: {
              send_dynamic_menu(p, type, "Zosaku", new String[] {"Săn trùm", "Thách đấu",
                  "Vượt liên ải", "Trận chiến lớn", "Thợ săn hải tặc"},
                  new short[] {136, 137, 138, 146, 111});
              break;
            }
            case 191:
            case 189:
            case 113:
            case 93:
            case 69:
            case 33:
            case 17: {
              send_dynamic_menu(p, type, "Zosaku",
                  new String[] {"Săn trùm", "Thách đấu", "Vượt liên ải", "Trận chiến lớn"},
                  new short[] {136, 137, 138, 146});
              break;
            }
            case 25: {
              send_dynamic_menu(p, type, "Zosaku", new String[] {"Săn trùm", "Thách đấu",
                  "Vượt liên ải", "Trận chiến lớn", "Vượt ải đơn"},
                  new short[] {136, 137, 138, 146, 138});
              break;
            }
            case 41: {
              send_dynamic_menu(
                  p, type, "Zosaku", new String[] {"Săn trùm", "Thách đấu", "Vượt liên ải",
                      "Trận chiến lớn", "Bảo vệ kho báu Namie"},
                  new short[] {136, 137, 138, 146, 139});
              break;
            }
            case 49: {
              send_dynamic_menu(p, type, "Zosaku", new String[] {"Săn trùm", "Thách đấu",
                  "Vượt liên ải", "Trận chiến lớn", "Lệnh truy nã"},
                  new short[] {136, 137, 138, 146, 160});
              break;
            }
            case 83: {
              send_dynamic_menu(p, type, "Zosaku",
                  new String[] {"Săn trùm", "Thách đấu", "Vượt siêu liên ải", "Trận chiến lớn"},
                  new short[] {136, 137, 159, 146});
              break;
            }
          }
          break;
        }
        case -72: { // npc nami
          if (p.map.template.id == 17) {
            send_dynamic_menu(p, type, "Nami",
                new String[] {"Đổi Ruby", "Thành tích hằng ngày", "Tích lũy nạp thẻ", "Đấu giá",
                    "Điểm nạp tích lũy", "Chợ mua bán"},
                new short[] {132, 134, 110, 169, 170,170, 152});
          } else {
            send_dynamic_menu(
                p, type, "Nami", new String[] {"Đổi Ruby", "Thành tích hằng ngày",
                    "Tích lũy nạp thẻ", "Đấu giá", "Điểm nạp tích lũy",},
                new short[] {132, 134, 110, 169, 170, 170});
          }
          break;
        }
        case -997: {
          switch (p.map.template.id) {
            case 1: { // lang coi xay gio
              send_dynamic_menu(p, type, "Hướng dẫn", new String[] {"Đăng ký tài khoản",
                  "Nhiệm vụ tân thủ", "Vật phẩm", "Vận buôn", "Trang bị", "Kỹ năng"}, null);
              break;
            }
            case 9: { // thi tran vo so
              send_dynamic_menu(p, type, "Hướng dẫn",
                  new String[] {"Bảng xếp hạng", "Nhiệm vụ hàng ngày", "Cường hóa trang bị",
                      "Khảm đá", "Chuyển hóa", "Săn trùm", "Phó bản liên tầng", "Phó bản PvP",
                      "Khóa bảo vệ", "Nạp tiền"},
                  null);
              break;
            }
            case 17: { // thi tran orange
              send_dynamic_menu(p, type, "Hướng dẫn",
                  new String[] {"Chợ mua bán", "Vòng xoay kho báu", "Hoàn mỹ", "Kích ẩn",
                      "Thuộc tính kích ẩn (1-4)", "Thuộc tính kích ẩn (5-8)",
                      "Thuộc tính kích ẩn (9-13)"},
                  null);
              break;
            }
            case 25: { // sirup
              send_dynamic_menu(p, type, "Hướng dẫn", new String[] {"Cường hóa ác quỷ"}, null);
              break;
            }
            case 33: { // barati
              send_dynamic_menu(p, type, "Hướng dẫn", new String[] {"Băng hải tặc", "Phó bản băng",
                  "Phó bản khổng lồ", "Bảo vệ pháo đài"}, null);
              break;
            }
            case 41: { // hat de
              send_dynamic_menu(p, type, "Hướng dẫn",
                  new String[] {"Bảo vệ kho báu Namie", "Siêu boss"}, null);
              break;
            }
            case 49: { // khoi dau
              send_dynamic_menu(p, type, "Hướng dẫn", new String[] {"Lệnh truy nã", "Siêu boss"},
                  null);
              break;
            }
            case 66: { // mom sinh doi
              send_dynamic_menu(p, type, "Hướng dẫn", new String[] {"Vượt Redline"}, null);
              break;
            }
            case 69: { // whiskey
              send_dynamic_menu(p, type, "Hướng dẫn",
                  new String[] {"Trái ác quỷ", "Đấu trường tự do", "Siêu boss"}, null);
              break;
            }
            case 79: { // little grand
              send_dynamic_menu(p, type, "Hướng dẫn",
                  new String[] {"Đá đít Mr.3", "Phó bản khổng lồ"}, null);
              break;
            }
          }
          break;
        }
        case -100: {
          send_dynamic_menu(p, type, "Sự kiện",
              new String[] {"T/g x2 kỹ năng EXP", "T/g khóa exp", "Hủy t/g khóa exp", "Tài xỉu"},
              null);
          break;
        }
        case -37: {
          send_dynamic_menu(p, type, "Bếp trưởng", new String[] {"Học Skill", "Tẩy tiềm năng",
              "Xóa nội tại", "Người giới thiệu", "Đá hành trình"},
              new short[] {123, 124, 125, 138, 127});
          break;
        }
        case -4: {
          if (p.level >= 100) {
            send_dynamic_menu(p, type, "Gap", new String[] {"Học Skill", "Tẩy tiềm năng",
                "Xóa nội tại", "Người giới thiệu", "Thông thạo"},
                new short[] {123, 124, 125, 138, 138});
          } else {
            send_dynamic_menu(p, type, "Gap",
                new String[] {"Học Skill", "Tẩy tiềm năng", "Xóa nội tại", "Người giới thiệu"},
                new short[] {123, 124, 125, 138});
          }
          break;
        }
        // case -144: // kinh do nuoc
        case -153:
        case -152:
        case -151:
        case -150:
        case -149:
        case -148: {
          Show_List_Map_Tele(p, 0, -144);
          break;
        }
        // case -124: // thi tran thien su
        case -131:
        case -130:
        case -129:
        case -128:
        case -127:
        case -126:
        case -125:
        case -123: {
          Show_List_Map_Tele(p, 0, -124);
          break;
        }
        case -115:
        case -114:
        case -113:
        case -112:
        case -111:
        case -110:
        case -109:
        case -108: {
          Show_List_Map_Tele(p, 0, -107);
          break;
        }
        case -96:
        case -94:
        case -93:
        case -92: {
          Show_List_Map_Tele(p, 0, -85);
          break;
        }
        case -83:
        case -81:
        case -80:
        case -79: {
          Show_List_Map_Tele(p, 0, 0);
          break;
        }
        case -59:
        case -63:
        case -62:
        case -61: {
          Show_List_Map_Tele(p, 0, -60);
          break;
        }
        case -58:
        case -51:
        case -50:
        case -49: {
          Show_List_Map_Tele(p, 0, -44);
          break;
        }
        case -57:
        case -42:
        case -41:
        case -40: {
          Show_List_Map_Tele(p, 0, -36);
          break;
        }
        case -56:
        case -34:
        case -33:
        case -32: {
          Show_List_Map_Tele(p, 0, -28);
          break;
        }
        case -55:
        case -26:
        case -25:
        case -24: {
          Show_List_Map_Tele(p, 0, -20);
          break;
        }
        case -54:
        case -18:
        case -17:
        case -16: {
          Show_List_Map_Tele(p, 0, -12);
          break;
        }
        case -53:
        case -10:
        case -9:
        case -8: {
          // send_dynamic_menu(p, type, "Nhiệm vụ", new String[] {"Nhiệm vụ chính", "Nhiệm
          // vụ lặp"}, null);
          Show_List_Map_Tele(p, 0, -5);
          break;
        }
        case -6: {
          Service.Send_UI_Shop(p, 99);
          break;
        }
        case -145:
        case -122:
        case -118:
        case -103:
        case -87:
        case -74:
        case -67:
        case -45:
        case -31:
        case -21:
        case -13:
        case -1: {
          if (p.conn.status != 1) {
            send_dynamic_menu(
                p, type, get_name_npc(type), new String[] {"Kích Hoạt Tài Khoản", "Thách đấu",
                    "Cao thủ", "Băng hải tặc", "Truy nã", "Đá hành trình","Điểm Danh","Điểm Danh Vip "+p.conn.vip},
                null);
          } else {
            send_dynamic_menu(p, type, get_name_npc(type), new String[] {"Thách đấu", "Cao thủ",
                "Băng hải tặc", "Truy nã", "Đá hành trình","Điểm Danh","Điểm Danh Vip "+p.conn.vip}, null);
          }
          break;
        }
        case -133: {
          send_dynamic_menu(p, type, "Kho Báu",
              new String[] {"Vòng quay kho báu", "Hoàn mỹ - Kích ẩn", "Vòng quay ốc sên","Lục Thức","Sức Mạnh Vật Lý","Doriki"}, null);
          break;
        }
        case -105:
        case -90:
        case -70:
        case -47: {
          if (p.map.template.id == 25) { // cuong hoa ac quy
            send_dynamic_menu(p, type, "Johny",
                new String[] {"Cường Hóa", "Khảm đá", "Chuyển hóa", "Ghép mảnh trang bị",
                    "Cường hóa thời trang", "Cường hóa ác quỷ"},
                new short[] {126, 127, 128, 126, 126, 154});
          } else {
            send_dynamic_menu(
                p, type, "Johny", new String[] {"Cường Hóa", "Khảm đá", "Chuyển hóa",
                    "Ghép mảnh trang bị", "Cường hóa thời trang"},
                new short[] {126, 127, 128, 126, 126});
          }
          break;
        }
        case -147:
        case -120:
        case -116:
        case -102:
        case -89:
        case -76:
        case -68:
        case -46:
        case -39:
        case -29:
        case -22:
        case -14:
        case -2: {
          send_dynamic_menu(
              p, type, get_name_npc(type), new String[] {"Quán ăn", "Vận Chuyển Hàng", "Tiệm tóc",
                  "Đóng thuyền", "Thời trang", "Thẩm mỹ viện"},
              new short[] {104, 107, 106, 105, 108, 158});
          break;
        }
        case -144: // kinh do nuoc
        case -124: // thi tran thien su
        case -132: // dao jaza
        case -107: // thi tran nanohano
        case -85: // thi tran horn
        case -97: // dao little grand
        case 0: // thi tran whiskey
        case -82: // mom sinh doi
        case -60: // thi tran khoi dau
        case -44: // lang hat de
        case -36: // nha hang barati
        case -28: // lang sirup
        case -20: // thi tran orang
        case -12: // thi tran vo so
        case -5: { // lang coi xay gio
          send_dynamic_menu(p, type, "", new String[] {"Trong làng", "Thế giới"});
          break;
        }
        case -7: {
          Menu_Change_Zone(p);
          break;
        }
        case -146:
        case -121:
        case -117:
        case -101:
        case -88:
        case -75:
        case -69:
        case -38:
        case -30:
        case -23:
        case -15:
        case -3: {
          send_dynamic_menu(p, type, get_name_npc(type),
              new String[] {Clazz.NAME[p.clazz - 1], "Hệ khác",
                  (!p.is_show_hat ? "Bật hiển thị nón" : "Tắt hiển thị nón"), "Khóa bảo vệ",
                  "Thùng rác"},
              new short[] {Clazz.ICON[p.clazz - 1], 116, 117, 118, 113});
          break;
        }
        case -119: {
          break;
        }
        default: {
          send_dynamic_menu(p, type, (get_name_npc(type) + " id " + type), new String[] {"Chưa có"},
              new short[] {117});
          break;
        }
      }
    }
  }

  private static String get_name_npc(int type) {
    switch (type) {
      case -145:
        return "Icebug";
      case -122:
        return "Gan";
      case -118:
        return "Cricket";
      case -103:
        return "Cobran";
      case -87:
        return "Daltont";
      case -74:
        return "Mr Opera";
      case -67:
        return "Mastersun";
      case -45:
        return "Genzo";
      case -31:
        return "Băng hải tặc nhí";
      case -1:
        return "Trưởng làng";
      case -146:
        return "Paule";
      case -147:
        return "kookoroo";
      case -120:
        return "Conic";
      case -121:
        return "Pagada";
      case -117:
        return "Spect";
      case -116:
        return "Terri";
      case -101:
        return "Kohzak";
      case -102:
        return "Yoshi moto";
      case -89:
        return "Dr Kure";
      case -88:
        return "Stook";
      case -75:
        return "Ms Vivi";
      case -76:
        return "Mr Acrobatic";
      case -68:
        return "Sapie";
      case -46:
        return "Noziko";
      case -39:
        return "Cami";
      case -29:
        return "Kaiya";
      case -21:
        return "Thị Trưởng";
      case -13:
        return "Cobi";
      case -69:
        return "Masu";
      case -3:
        return "Guru";
      case -15:
        return "Mẹ Rita";
      case -23:
        return "Poroy";
      case -30:
        return "Merri";
      case -38:
        return "Partty";
      case -2:
        return "Machiko";
      case -14:
        return "Rita";
      case -22:
        return "Cho Cho";
    }
    return "NPC";
  }

  public static void process_menu(Player p, Message m2) throws IOException {
    if (!p.isdie) {
      short idNPC = m2.reader().readShort();
      // byte idMenu =
      m2.reader().readByte();
      byte index = m2.reader().readByte();
      // System.out.println("idNPC " + idNPC);
      // System.out.println("idMenu " + idMenu);
      // System.out.println("index " + index);
      switch (idNPC) {
          case 1003: {
                Menu_SucManhVatLy(p, index);
                break;
            }
            case 1001: {
                Menu_Doriki(p, index);
                break;
            }
            case 1002: {
                Menu_LucThuc(p, index);
                break;
            }
        case 978: {
          if (index == 0) {
            HanhTrinh.show_table(p, 1);
          } else if (index == 1) {
            HanhTrinh.show_table(p, 0);
          } else if (index == 2) {
            Service.send_box_ThongBao_OK(p,
                "Mỗi khi bạn tiêu diệt boss cuối mỗi làng sẽ có 10% cơ hội nhận được đá hành trình. "
                    + "Gặp trưởng làng xem đá kiếm được");
          }
          break;
        }
        case 979: {
          if (index == 0) {
            Message m = new Message(-19); // show table select icon
            m.writer().writeByte(97);
            m.writer().writeUTF("Cửa hàng biểu tượng thường");
            m.writer().writeByte(107);
            m.writer().writeShort(278);
            for (int i = 0; i < 278; i++) {
              m.writer().writeShort(i);
              m.writer().writeShort(i);
              m.writer().writeUTF("Huy hiệu " + (i + 1));
              m.writer().writeUTF("Được làm từ gì đấy không biết nữa, mua đeo vào rất đẹp");
              if (i < 10) {
                m.writer().writeShort(0);
              } else {
                m.writer().writeShort(50);
              }
            }
            p.conn.addmsg(m);
            m.cleanup();
          } else if (index == 1) {
            Message m = new Message(-19); // show table select icon
            m.writer().writeByte(97);
            m.writer().writeUTF("Cửa hàng biểu tượng cao cấp");
            m.writer().writeByte(107);
            m.writer().writeShort(76);
            for (int i = 293; i < 370; i++) {
              if (i == 303) {
                continue;
              }
              m.writer().writeShort(i);
              m.writer().writeShort(i);
              m.writer().writeUTF("Huy hiệu " + (i + 1));
              m.writer().writeUTF("Được làm từ gì đấy không biết nữa, mua đeo vào rất đẹp");
              if (i < 10) {
                m.writer().writeShort(0);
              } else {
                m.writer().writeShort(200);
              }
            }
            p.conn.addmsg(m);
            m.cleanup();
          }
          break;
        }
        case 980: {
          switch (index) {
            case 0:
            case 1:
            case 2: {
              UpgradeDevil.show_table(p, index + 2);
              break;
            }
            case 3: {
              UpgradeDial.show_table(p);
              break;
            }
            case 4: {
              Rebuild_Item.show_table(p, 10);
              break;
            }
          }
          break;
        }
        case 981: {
          if (index == 0) {
          }
          break;
        }
        case 982: {
          if (p.ship_pet != null && p.name_ThoSanHaiTac != null
              && index < p.name_ThoSanHaiTac.length) {
            Player p0 = Map.get_player_by_name_allmap(p.name_ThoSanHaiTac[index]);
            if (p0 != null && p0.name_ThoSanHaiTac == null) {
              p0.name_ThoSanHaiTac = new String[] {p.name};
              Service.send_box_yesno(p0, 53, "Thông báo",
                  p.name + " muốn mời bạn bảo vệ hàng, bạn hãy trả lời?",
                  new String[] {"Đồng ý", "Hủy"}, new byte[] {2, 1});
              Service.send_box_ThongBao_OK(p, "Gửi yêu cầu thành công, đợi đối phương xác nhận");
            } else {
              Service.send_box_ThongBao_OK(p, "Đối phương đã rời đi, hãy thử lại");
            }
          } else {
            Service.send_box_ThongBao_OK(p, "Hãy nhận hàng trước");
          }
          break;
        }
        case 984: {
          // pho ban bang select
          if (p.clan != null) {
            switch (index) {
              case 0: {
                break;
              }
              case 1: { // dang ky pho ban khong lo
                if (p.clan.map_create != null) { // enter map
                  //
                  if (p.clan.map_create.map_little_garden.clan1.equals(p.clan)) {
                    p.type_pk = 4;
                  } else {
                    p.type_pk = 5;
                  }
                  //
                  Vgo vgo = new Vgo();
                  vgo.map_go = new Map[1];
                  vgo.map_go[0] = p.clan.map_create;
                  vgo.xnew = 350;
                  vgo.ynew = 260;
                  p.goto_map(vgo);
                } else {
                  boolean check_tt_tp = false;
                  for (int i = 0; i < p.clan.members.size(); i++) {
                    if (p.clan.members.get(i).name.equals(p.name)
                        && (p.clan.members.get(i).levelInclan == 0
                            || p.clan.members.get(i).levelInclan == 1)) {
                      check_tt_tp = true;
                      break;
                    }
                  }
                  if (check_tt_tp) {
                    if (p.tableTickOption == null) {
                      int time_h = LocalTime.now().getHourOfDay();
                      if ((Util.is_DayofWeek(2) || Util.is_DayofWeek(4) || Util.is_DayofWeek(6))
                          && time_h == 21) {
                        p.tableTickOption = new TableTickOption();
                        p.tableTickOption.listP = new ArrayList<>();
                        p.tableTickOption.idDialog = 0;
                        p.tableTickOption.listP.add(p);
                        for (int i = 0; i < p.clan.members.size(); i++) {
                          Player p0 = Map.get_player_by_name_allmap(p.clan.members.get(i).name);
                          if (p0 != null && p0.index_map != p.index_map && p0.map.equals(p.map)) {
                            p.tableTickOption.listP.add(p0);
                          }
                        }
                        p.tableTickOption.list_check = new byte[p.tableTickOption.listP.size()];
                        p.tableTickOption.list_check[0] = 1;
                        for (int i = 1; i < p.tableTickOption.list_check.length; i++) {
                          p.tableTickOption.list_check[i] = 0;
                        }
                        TableTickOption.show_table(p, "Phó bản khổng lồ");
                      } else {
                        Service.send_box_ThongBao_OK(p,
                            "Phó bản hoạt động vào 21h tối thứ 3, 5, 7");
                      }
                    } else {
                      Service.send_box_ThongBao_OK(p, "Băng đã đăng ký, đang chờ ghép đội!");
                    }
                  } else {
                    Service.send_box_ThongBao_OK(p, "Bạn không phải thuyền trưởng");
                  }
                }
                break;
              }
            }
          }
          break;
        }
        case 985: {
          if (p.ship_pet == null) {
            if (index == 0 || index == 1 || index == 2) {
              p.typePirate = index;
            } else {
              p.typePirate = -1;
            }
            p.update_info_to_all();
          }
          break;
        }
        case 986: {
          Menu_VanChuyenHang(p, index);
          break;
        }
        case 987: {
          if (index == 0) {
            Message m = new Message(-19);
            m.writer().writeByte(118);
            m.writer().writeUTF("Cửa hàng tích lũy nạp");
            m.writer().writeByte(11);
            m.writer().writeShort(ShopTichLuy.ENTRY.size());
            for (int i = 0; i < ShopTichLuy.ENTRY.size(); i++) {
              ShopTichLuy temp = ShopTichLuy.ENTRY.get(i);
              m.writer().writeShort(temp.id);
              m.writer().writeByte(temp.type);
              switch (temp.type) {
                case 4: {
                  ItemTemplate4 temp4 = ItemTemplate4.get_it_by_id(temp.id);
                  m.writer().writeUTF(temp4.name);
                  m.writer().writeShort(temp4.icon);
                  break;
                }
                case 7: {
                  ItemTemplate7 temp7 = ItemTemplate7.get_it_by_id(temp.id);
                  m.writer().writeUTF(temp7.name);
                  m.writer().writeShort(temp7.icon);
                  break;
                }
                case 105: { // thoi trang
                  ItemFashion temp105 = ItemFashion.get_item(temp.id);
                  m.writer().writeUTF(temp105.name);
                  m.writer().writeShort(temp105.idIcon);
                  break;
                }
              }
              String info = temp.info + "\n" + temp.point + " điểm tích lũy.";
              if (temp.limit > 0) {
                int time = 0;
                if (temp.limit_data.containsKey(p.name)) {
                  time = temp.limit_data.get(p.name);
                }
                info += "\nĐổi tối đa: " + temp.limit + " lần.\nHiện tại đã đổi: " + time + "/"
                    + temp.limit + ".";
              }
              m.writer().writeUTF(info);
            }
            p.conn.addmsg(m);
            m.cleanup();
          }
          break;
        }
        case 988: {
          if (index >= 0 && index <= 12 && p.dungeon == null) {
            int save = index;
            p.data_yesno = new int[] {save};
            if (save < 7) {
              Service.send_box_yesno(p, 52, "Thông báo",
                  ("Vào phó bản đơn cấp độ " + (index + 3) + " cần 1 chìa khóa phó bản"),
                  new String[] {"Đồng ý", "Hủy"}, new byte[] {2, 1});
            } else {
              Service.send_box_yesno(p, 52, "Thông báo",
                  ("Vào phó bản đơn cấp độ " + (index + 3) + " cần 2 chìa khóa phó bản"),
                  new String[] {"Đồng ý", "Hủy"}, new byte[] {2, 1});
            }
          } else {
            p.dungeon.mobs.clear();
            for (int i = 0; i < p.dungeon.maps.size(); i++) {
              p.dungeon.maps.get(i).stop_map();
            }
            p.dungeon = null;
            Service.send_box_ThongBao_OK(p, "Có lỗi xảy ra");
          }
          break;
        }
        case 989: { // taixiu
          if (index == 0) {
            EventSpecial.show_table(p, 0);
          } else if (index == 1) {
            TaiXiuInfo t = Manager.gI().TaiXiu().get_my_result(p);
            if (t != null) {
              if (t.isReceive == 0) {
                t.isReceive = 1;
                p.update_vang(t.money);
                p.update_money();
                Service.send_box_ThongBao_OK(p, "Nhận " + t.money + " beri");
                Manager.gI().TaiXiu().remove_result(p);
              } else {
                Service.send_box_ThongBao_OK(p, "Đã nhận rồi!");
              }
            } else {
              Service.send_box_ThongBao_OK(p, "Không thấy thông tin");
            }
          }
          break;
        }
        case 990: {
          break;
        }
        
        case -140: { // wipper
          if (index == 0) {
            send_dynamic_menu(p, 980, "WIPPER", new String[] {"Ghép sách công thức", "Ghép vỏ ốc",
                "Chế tạo dial", "Cường hóa dial", "Đục lỗ dial"}, null);
          } else if (index == 1) {
            if (p.party == null || p.party.list.size() != 2
                || !p.party.list.get(0).name.equals(p.name)) {
              Service.send_box_ThongBao_OK(p, "Hãy tạo nhóm 2 người để vào phó bản");
              return;
            }
            Service.send_box_yesno(p, 56, "Thông báo",
                "Phó bản thử thách vệ thần mỗi lần đi tốn 1 chìa khóa, xác nhận vào?",
                new String[] {"Đồng ý", "Hủy"}, new byte[] {2, 1});
          }
          break;
        }
        case -86: { // miss pho ban
          break;
        }
        case -77: { // ms gym
          break;
        }
        case -73: { // croket
          break;
        }
        case -84: { // npc mihaw
          if (p.clan != null) {
            Menu_Clan(p, index);
          } else {
            if (index == 0) {
              Service.input_text(p, 10, "Đăng ký băng (2000 Ruby)", new String[] {"Tên băng"});
            } else if (index == 1) {
              String txt = "Băng Hải Tặc\n"
                  + " Giờ đây bạn có thể tập trung các người bạn của mình lại để tạo thành 1 nhóm cùng nhau luyện tập "
                  + "và tham gia các hoạt động dành riêng cho nhóm rồi. \bNếu là thuyền Trưởng bạn sẽ có mọi quyền hành trong "
                  + "Băng của mình. Từ thay đổi Icon Băng cho tới quản lý thành viên, mua sử dụng vật phẩm, cộng điểm tiềm năng , "
                  + "hay thông báo toàn Băng.\bThuyền phó bạn sẽ được Thông báo toàn Clan và quản lý thành viên. Còn Hoa tiêu thì "
                  + "Quản lý thành viên là việc bạn có thể làm.\bNgoài ra thì tặng quà, làm nhiệm vụ, đóng góp hay nhận các lợi ích "
                  + "từ các vật phẩm Băng thì tất cả mọi người đều có.\bHãy tham gia vào Băng ngay sẽ có rất nhiều hoạt động hay và "
                  + "thú vị mà chỉ dành cho Băng Hải tặc thôi nhé. ";
              Service.Help_From_Server(p, -84, txt);
            }
          }
          break;
        }
        case -138: {
          Menu_Law(p, index);
          break;
        }
        case 991: { // hoan my kich an
          switch (index) {
            case 0: {
              Rebuild_Item.show_table(p, 6);
              break;
            }
            case 1: {
              Rebuild_Item.show_table(p, 7);
              break;
            }
            case 2: {
              Rebuild_Item.show_table(p, 8);
              break;
            }
          }
          break;
        }
        case -106:
        case -91:
        case -71:
        case -48: {
          Menu_Zosaku(p, index);
          break;
        }
        case 992: {
          Join_Item.show_table(p, index);
          break;
        }
        case 993: {
          switch (index) {
            case 0: {
              Service.input_text(p, 4, "Đổi Extol Sang Ruby", new String[] {"Ruby muốn đổi"});
              break;
            }
            case 1: {
              Service.send_box_yesno(p, 10, "Thông báo",
                  "Bạn muốn đổi 1000 ruby sang 750.000 extol?", new String[] {"Đồng ý", "Hủy"},
                  new byte[] {2, 1});
              break;
            }
            case 2: {
              Service.send_box_ThongBao_OK(p, "Đang bảo trì, anh em lên web nạp nha");
              break;
            }
            case 3: {
              Service.input_text(p, 1, "Quà tặng máy chủ", new String[] {"Nhập giftcode"});
              break;
            }
            case 4: {
                if (p.conn.status != 1){
                    Service.send_box_ThongBao_OK(p,
                                "Chưa Kích hoạt không thể đổi coin");
                    return;
                }
              Service.input_text(p, 8, "Đổi Coin Sang Ruby", new String[] {"10 coin = 2 ruby"});
              break;
            }
            case 5: {
                if (p.conn.status != 1){
                    Service.send_box_ThongBao_OK(p,
                                "Chưa Kích hoạt không thể đổi Beri");
                    return;
                }
              Service.input_text(p, 9, "Đổi Coin Sang Beri", new String[] {"1 coin = 5000 beri"});
              break;
            }
            case 6: {
              Service.send_box_ThongBao_OK(p, "Bạn đang sở hữu "+Util.number_format(p.conn.coin)+" Coin.\n"
                      + "Nếu bạn vừa đổi coin vui lòng thoát game vào lại để xem chính xác coin");
              break;
            }
          }
          break;
        }
        case -72: { // npc nami
          Menu_Nami(p, index);
          break;
        }
        case 994: {
          if (index == 0) {
          } else if (index == 1) {
            String txt =
                "Khóa bảo vệ\nAi cũng có những món đồ mình rất quý trọng và không muốn mất nó.\nChức năng khóa bảo vệ sẽ giúp "
                    + "bạn làm điều đó.\b"
                    + "Sau khi đăng ký đặt khóa thì các thao tác có ảnh hưởng đến tài khoản của bạn sẽ phải nhập đúng mã để xác nhận đó "
                    + "chính là bạn chứ không phải ai khác.\b"
                    + "Trong một lần đăng nhập bạn chỉ cần mở khóa duy nhất 1 lần. Nếu cần biết thêm chi tiết vui lòng liên hệ admin"
                    + " đẹp trai.";
            switch (p.map.template.id) {
              case 1: {
                Service.Help_From_Server(p, -3, txt);
                break;
              }
              case 9: {
                Service.Help_From_Server(p, -15, txt);
                break;
              }
              case 17: {
                Service.Help_From_Server(p, -23, txt);
                break;
              }
              case 25: {
                Service.Help_From_Server(p, -30, txt);
                break;
              }
              case 33: {
                Service.Help_From_Server(p, -38, txt);
                break;
              }
              case 49: {
                Service.Help_From_Server(p, -69, txt);
                break;
              }
              case 69: {
                Service.Help_From_Server(p, -75, txt);
                break;
              }
              case 83: {
                Service.Help_From_Server(p, -88, txt);
                break;
              }
            }
          } else if (index == 2) {
          }
          break;
        }
        case 995: {
          Select_Map_Tele_world(p, index);
          break;
        }
        case 996: {
          if (p.ship_pet != null) {
            Service.send_box_ThongBao_OK(p, "Không thể chuyển map khi đang chuyển hàng");
          } else {
            Select_Map_Tele(p, index);
          }
          break;
        }
        case -997: {
          Menu_HuongDan(p, index);
          break;
        }
        case -100: {
          Menu_Robin(p, index);
          break;
        }
        case 997: {
          Menu_Remove_Skill(p, index);
          break;
        }
        case 998: {
          Menu_Learn_Skill(p, index);
          break;
        }
        case -37: {
          if (index < 4) {
            Menu_Gap(p, index);
          } else if (index == 4) {
            send_dynamic_menu(p, 978, "Đá hành trình",
                new String[] {"Kho hành trình", "Bản đồ hành trình", "Hướng dẫn"}, null);
          }
          break;
        }
        case -4: {
          Menu_Gap(p, index);
          break;
        }
        case -145:
        case -122:
        case -118:
        case -103:
        case -87:
        case -74:
        case -67:
        case -45:
        case -31:
        case -21:
        case -13:
        case -1: {
          Menu_TruongLang(p, index);
          break;
        }
        case 120: { // bhx
          break;
        }
        case -133: {
          Menu_Buggi(p, index);
          break;
        }
        case 9999: {
          Menu_Admin(p, index);
          break;
        }
        case 32003: {
          int[] shop_pos = new int[4];
          int pos = 0;
          for (int i = 0; i < 5; i++) {
            if ((i + 1) == p.clazz) {
              continue;
            }
            shop_pos[pos++] = (int) i;
          }
          Service.Send_UI_Shop(p, shop_pos[index]);
          break;
        }
        case 32002: {
          switch (index) {
            case 0: {
              Service.Send_UI_Shop(p, 6);
              break;
            }
            case 1:
            case 2: {
              UpgradeDevil.show_table(p, index);
              break;
            }
          }
          break;
        }
        case 32001: {
          Menu_KhamNgoc(p, index);
          break;
        }
        case 32000: {
          Menu_Rebuilt_Item(p, index);
          break;
        }
        case -105:
        case -90:
        case -70:
        case -47: {
          Menu_Johny(p, index);
          break;
        }
        case -147:
        case -120:
        case -116:
        case -102:
        case -89:
        case -76:
        case -68:
        case -46:
        case -39:
        case -29:
        case -22: // menu cho cho
        case -14: // menu rita
        case -2: {
          Menu_Machiko(p, index);
          break;
        }
        case -146:
        case -121:
        case -117:
        case -101:
        case -88:
        case -75:
        case -69: // masu
        case -38: // menu partty
        case -30: // menu merri
        case -23: // menu poroy
        case -15: // Menu_MomRiTa
        case -3: {
          Menu_Guru(p, index);
          break;
        }
        case -144: // kinh do nuoc
        case -124: // thi tran thien su
        case -132: // dao jaza
        case -107: // thi tran nanohano
        case -85: // thi tran horn
        case -97: // dao little grand
        case 0: // thi tran whiskey
        case -82: // mom sinh doi
        case -60: // thi tran khoi dau
        case -44: // lang hat de
        case -36: // nha hang barati
        case -28: // lang sirup
        case -20: // thi tran orang
        case -12: // thi tran vo so
        case -5: { // lang coi xay gio
          Show_List_Map_Tele(p, index, idNPC);
          break;
        }
      }
    }
  }
private static void Menu_LucThuc(Player p, byte index) throws IOException {
        switch (index) {
            case 0: {
                String notice = "Điều kiện cần để có thể luyện Lục Thức:\r\n" + "• Đạt 8000 điểm Doriki\r\n" + "• Ít nhất là lv 70\r\n"
                        + "• Sức Mạnh Vật Lý đạt tối đa \r\n" + "- Khi tích đủ kinh nghiệm đến npc: Buggi để tăng cấp.\r\n"
                        + "- Phí mỗi cấp là 10 triệu beri và 3.000 ruby, 500 exp Lục Thức, 100% tỷ lệ thành công.\r\n"
                        + "- Khi cảnh giới đạt đến Cao Cấp, đến npc: Buggi để đột phá.\r\n"
                        + "Phí đột phá là 20 triệu beri và 5.000 ruby, 1000 exp Lục Thức  10% tỷ lệ thành công.";
                Service.send_box_ThongBao_OK(p, notice);
                break;
            }
            case 1: {
                if (p.level >= 110 && p.doriki[0] >= 8 && p.sucmanhvatly == 12) {
                    LucThuc.start(p);
                } else {
                    Service.send_box_ThongBao_OK(p, "Chưa đủ điều kiện để có thể luyện lục thức");
                }
                break;
            }
            case 2: {
                LucThuc.send_info(p);
                break;
            }
            default: {
                Service.send_box_ThongBao_OK(p, "Chưa có chức năng");
                break;
            }
        }
    }

    private static void Menu_Doriki(Player p, byte index) throws IOException {
        switch (index) {
            case 0: {
                String notice = " Mỗi Doriki gồm có 5 cấp\r\n" + "- Tỷ lệ nâng cấp thành công là 30% \r\n"
                        + "- Khi nâng cấp cần  beri và ruby. \r\n"
                        + "- Mỗi cấp cần  5 triệu beri, 2.000 ruby.\r\n"
                        + "- Khi nâng cao mỗi cấp người chơi sẽ nhận đc:\r\n" + "+ 1% sát thương \r\n" + "+ 1% máu\r\n"
                        + "+ 1% mana\r\n" + "+ 1% phòng thủ.";
                Service.send_box_ThongBao_OK(p, notice);
                break;
            }
            case 1: {
                Doriki.start(p);
                break;
            }
            case 2: {
                Doriki.send_info(p);
                break;
            }
            default: {
                Service.send_box_ThongBao_OK(p, "Chưa có chức năng");
                break;
            }
        }
    }

    private static void Menu_SucManhVatLy(Player p, byte index) throws IOException {
        switch (index) {
            case 0: {
                String notice = "Chức năng Sức Mạnh Vật Lý:\r\n" + "+Tổng cộng có 12 tầng \r\n" + "Điều kiện để đột phá:\r\n"
                        + "• Các hải tặc phải trên lv 60 \r\n" + "• 5 triệu beri • 2.000 ruby \r\n"
                        + "- Tỷ lệ thành công 30% \r\n" + "Mỗi cấp sẽ đạt đc:\r\n" + "+ 200 sát thương \r\n"
                        + "+ 200 phòng thủ\r\n" + "+ 2% Chí Mạng\r\n" + "+ 2% Xuyên giáp\r\n" + "+ 2000 máu\r\n"
                        + "+ 2000 mana";
                Service.send_box_ThongBao_OK(p, notice);
                break;
            }
            case 1: {
                if (p.level < 60) {
                    Service.send_box_ThongBao_OK(p, "Level chưa đủ, cần cấp 60 trở lên");
                } else {
                    SucManhVatLy.start(p);
                }
                break;
            }
            case 2: {
                SucManhVatLy.send_info(p);
                break;
            }
            default: {
                Service.send_box_ThongBao_OK(p, "Chưa có chức năng");
                break;
            }
        }
    }
  private static void Menu_VanChuyenHang(Player p, byte index) throws IOException {
    switch (index) {
      case 0: {
        if (p.map.template.id == 1) {
          if (p.typePirate == 0) {
            if (p.time_ship >= 5) {
              Service.send_box_ThongBao_OK(p, "Hôm nay đã vận chuyến tối đa!");
              return;
            }
            if (p.ship_pet == null) {
              Ship.show_table(p);
            } else {
              Service.send_box_ThongBao_OK(p, "Bạn đang có gói hàng!");
            }
          } else {
            Service.send_box_ThongBao_OK(p, "Bạn phải đăng ký là lái buôn!");
          }
        } else {
          Service.send_box_ThongBao_OK(p,
              "Chỉ có thể bắt đầu vận chuyển hàng từ làng cối xay gió!");
        }
        break;
      }
      case 1: {
        if (p.id_ship_packet != -1 && p.ship_pet != null
            && (p.map.template.id == 9 || p.map.template.id == 17 || p.map.template.id == 25)) {
          if (p.id_ship_packet != -1) {
            if (!p.ship_pet.map.equals(p.map)
                || !(Math.abs(p.ship_pet.x - p.x) < 150 && Math.abs(p.ship_pet.y - p.y) < 150)) {
              Service.send_box_ThongBao_OK(p, "Ta không thấy vật phẩm buôn của ngươi!");
              return;
            }
            if (p.typePirate == 0 && p.time_ship >= 5) {
              Service.send_box_ThongBao_OK(p, "Hôm nay đã vận chuyến tối đa!");
              return;
            }
            int beri_total = 0;
            switch (p.id_ship_packet) {
              case 36: {
                beri_total = 30_000;
                break;
              }
              case 37: {
                beri_total = 100_000;
                break;
              }
              case 38: {
                beri_total = 200_000;
                break;
              }
              case 39: {
                beri_total = 400_000;
                break;
              }
            }
            if (p.map.template.id == 17) {
              beri_total = (beri_total * 15) / 10;
            } else if (p.map.template.id == 25) {
              beri_total *= 2;
            }
            if (p.typePirate == 2) {
              beri_total = (beri_total * 3) / 10;
            }
            p.update_vang(beri_total);
            Message m = new Message(3);
            m.writer().writeShort(p.ship_pet.index_map);
            m.writer().writeByte(2);
            for (int i = 0; i < p.map.players.size(); i++) {
              Player p0 = p.map.players.get(i);
              p0.conn.addmsg(m);
            }
            m.cleanup();
            Ship_pet.remv(p.ship_pet);
            String name_BaoVe = p.ship_pet.mainBaoVe;
            p.ship_pet = null;
            p.id_ship_packet = -1;
            if (!name_BaoVe.isEmpty()) {
              Player p0 = Map.get_player_by_name_allmap(name_BaoVe);
              if (p0 != null && p0.name_ThoSanHaiTac.length == 1
                  && p0.name_ThoSanHaiTac[0].equals(p.name)) {
                beri_total /= 3;
                p0.update_vang(beri_total);
                //
                p.update_vang(-beri_total);
                //
                p0.update_money();
                Service.send_box_ThongBao_OK(p0,
                    "Bảo vệ hàng thành công nhận " + beri_total + " beri");
                p0.name_ThoSanHaiTac = null;
                Service.send_box_ThongBao_OK(p, "Trả hàng thành công nhận " + beri_total + " beri");
              } else {
                Service.send_box_ThongBao_OK(p, "Trả hàng thành công nhận " + beri_total + " beri");
              }
            } else {
              Service.send_box_ThongBao_OK(p, "Trả hàng thành công nhận " + beri_total + " beri");
            }
            //
            if (p.typePirate == 0) {
              p.time_ship++;
            }
            p.name_ThoSanHaiTac = null;
            p.update_money();
          } else {
            Service.send_box_ThongBao_OK(p, "Bạn không có gói hàng nào cả!");
          }
        } else {
          Service.send_box_ThongBao_OK(p, "Không thể trả hàng tại đây!");
        }
        break;
      }
      case 2: {
        Service.send_box_ThongBao_OK(p,
            "Hãy chọn chức năng thợ săn hải tặc và đứng trong map để lái buôn"
                + " thuê sẽ có thông báo");
        break;
      }
      case 3: { // thue bao ve
        if (p.ship_pet != null) {
          List<String> name = new ArrayList<>();
          for (int i = 0; i < p.map.players.size(); i++) {
            if (p.map.players.get(i).typePirate == 1) {
              name.add(p.map.players.get(i).name);
            }
          }
          if (name.size() > 0) {
            p.name_ThoSanHaiTac = new String[name.size()];
            for (int i = 0; i < name.size(); i++) {
              p.name_ThoSanHaiTac[i] = name.get(i);
            }
            send_dynamic_menu(p, 982, "Chọn thợ săn hải tặc", p.name_ThoSanHaiTac, null);
          } else {
            send_dynamic_menu(p, 982, "Chọn thợ săn hải tặc", new String[] {"Trống"}, null);
          }
        } else {
          Service.send_box_ThongBao_OK(p, "Hãy nhận hàng trước");
        }
        break;
      }
      case 4: {
        if (p.ship_pet == null) {
          Message m = new Message(-20);
          m.writer().writeByte(1);
          m.writer().writeShort(985); // id npc
          m.writer().writeByte(0); // id menu
          m.writer().writeUTF("Đăng ký chức năng");
          m.writer().writeByte(4);
          String[] name = new String[] {"Lái buôn", "Thợ săn Hải Tặc", "Hải Tặc", "Không đăng ký"};
          for (int i = 0; i < 4; i++) {
            m.writer().writeUTF(name[i]);
            m.writer().writeByte(0);
            if (p.typePirate == i) {
              m.writer().writeByte(3);
            } else {
              m.writer().writeByte(7);
            }
          }
          p.conn.addmsg(m);
          m.cleanup();
        } else {
          Service.send_box_ThongBao_OK(p,
              "Hủy chuyến hàng hiện tại trước khi thực hiện chức năng này");
        }
        break;
      }
      case 5: {
        if (p.ship_pet != null) {
          if (p.ship_pet.map != null) {
            p.ship_pet.map.remove_obj(p.ship_pet.index_map, 0);
          }
          Ship_pet.remv(p.ship_pet);
          p.ship_pet = null;
          p.id_ship_packet = -1;
          Service.send_box_ThongBao_OK(p, "Hủy chuyến buôn thành công");
        }
        break;
      }
      case 6: {
        if (p.ship_pet != null) {
          if (p.map.template.id > p.ship_pet.id_map_save) {
            Service.send_box_ThongBao_OK(p, "Hiện tại chỉ có thể gọi lạc đà từ "
                + Map.get_map_by_id(p.ship_pet.id_map_save)[0].template.name + " trở lại");
            return;
          }
          if (p.ship_pet.map != null) {
            p.ship_pet.map.remove_obj(p.ship_pet.index_map, 0);
          }
          short index_map_new = -3;
          p.ship_pet.index_map = index_map_new;
          p.ship_pet.map = p.map;
          p.ship_pet.x = p.x;
          p.ship_pet.y = p.y;
          //
          Message m_local = new Message(1);
          m_local.writer().writeByte(0);
          m_local.writer().writeShort(p.ship_pet.index_map);
          m_local.writer().writeShort(p.ship_pet.x);
          m_local.writer().writeShort(p.ship_pet.y);
          for (int j = 0; j < p.map.players.size(); j++) {
            Player p0 = p.map.players.get(j);
            p0.conn.addmsg(m_local);
          }
          m_local.cleanup();
        } else {
          Service.send_box_ThongBao_OK(p, "Không tìm thấy chuyến hàng");
        }
        break;
      }
      case 7: {
        if (p.ship_pet != null && p.ship_pet.map != null) {
          Service.send_box_ThongBao_OK(p, "Vị trí: " + p.ship_pet.map.template.name + " khu "
              + (p.ship_pet.map.zone_id + 1) + " tọa độ " + p.ship_pet.x + " " + p.ship_pet.y);
        } else {
          p.ship_pet = null;
          Service.send_box_ThongBao_OK(p, "Không tìm thấy chuyến hàng");
        }
        break;
      }
      case 8: {
        Service.send_box_ThongBao_OK(p, "Hôm nay đã hoàn thành " + p.time_ship + " chuyến");
        break;
      }
    }
  }

  private static void Menu_Clan(Player p, byte index) throws IOException {
    if (p.clan == null || index > 2 && !p.clan.members.get(0).name.equals(p.name)) {
      return;
    }
    switch (index) {
      case 0: {
        Clan_member clan_mem = null;
        for (int i = 0; i < p.clan.members.size(); i++) {
          if (p.clan.members.get(i).name.equals(p.name)) {
            clan_mem = p.clan.members.get(i);
            break;
          }
        }
        if (clan_mem != null) {
          if (clan_mem.numquest >= 3) {
            Service.send_box_ThongBao_OK(p, "Hôm nay đã hết nhiệm vụ, hãy quay lại vào ngày mai");
            return;
          }
          //
          QuestP questP = null;
          for (int i = 0; i < p.list_quest.size(); i++) {
            if (p.list_quest.get(i).template.id < -2000) {
              questP = p.list_quest.get(i);
              break;
            }
          }
          if (questP == null) {
            Service.send_box_yesno(p, 42, "Thông báo",
                ("Bạn muốn nhận nhiệm vụ Băng hải tặc cấp " + (clan_mem.numquest + 1)),
                new String[] {"Đồng ý", "Hủy"}, new byte[] {-1, -1});
          } else {
            Service.send_box_ThongBao_OK(p, "Nhiệm vụ hiện tại chưa hoàn thành");
          }
        }
        break;
      }
      case 2: {
        send_dynamic_menu(p, 984, "Phó bản băng", new String[] {"Phó bản PVP", "Phó bản khổng lồ"},
            null);
        break;
      }
      case 3: {
        send_dynamic_menu(p, 979, "Icon băng", new String[] {"Cửa hàng thường", "Cửa hàng cao cấp"},
            null);
        break;
      }
      case 4: {
        Message m = new Message(-19);
        m.writer().writeByte(110);
        m.writer().writeUTF("Cửa hàng vật phẩm băng");
        m.writer().writeByte(8);
        m.writer().writeShort(ItemTemplate8.ENTRYS.size());
        for (int i = 0; i < ItemTemplate8.ENTRYS.size(); i++) {
          m.writer().writeShort(i);
          m.writer().writeShort(1);
        }
        p.conn.addmsg(m);
        m.cleanup();
        break;
      }
      case 5: {
        if (p.clan.allowRequest == 1) {
          p.clan.allowRequest = 0;
          Service.send_box_ThongBao_OK(p, "Khóa mọi người xin vào băng thành công");
        } else {
          p.clan.allowRequest = 1;
          Service.send_box_ThongBao_OK(p, "Cho phép mọi người xin vào băng thành công");
        }
        break;
      }
    }
  }

  private static void Menu_Law(Player p, byte index) throws IOException {
    switch (index) {
      case 0: {
        if (p.item.it_heart == null) {
          String text = "Để có thể tách tim được bạn sẽ mất %s bột vàng, "
              + "%s đá ác quỷ, %s đá hải thạch cấp 1, %s.000.000 beri. Bạn thật sự muốn tách?";
          Service.send_box_yesno(p, 27, "Thông báo",
              String.format(text, ((p.level < 40) ? 120 : 100), ((p.level < 40) ? 120 : 100),
                  ((p.level < 40) ? 120 : 100), ((p.level < 40) ? 12 : 10)),
              new String[] {"Đồng ý", "Hủy"}, new byte[] {-1, -1});
        } else {
          if (p.item.it_heart.levelup > 98) {
            Service.send_box_ThongBao_OK(p, "Đã nâng cấp tối đa");
          } else {
            UpgradeItem.show_table_upgrade_heart(p);
          }
        }
        break;
      }
      case 1: {
        String txt = "CHỨC NĂNG TÁCH TRÁI TIM\b"
            + "Được mệnh danh là bác sĩ tử thần\nLaw chứa năng lực cực kỳ quái dị\n"
            + "Với năng lực đó, anh ta có thể biến bất kỳ ai trở nên mạnh hơn bao giờ hết\b"
            + "Hãy chuẩn bị trước bột vàng, đá hải thạch, đá ác quỷ và beri để đến gặp Law và tiến hành tách trái tim\b"
            + "Sau khi tách trái tim sẽ tăng 10% HP cuối\b"
            + "Bạn có thể đến gặp Law để phẫu thuật, giúp tăng cấp cho quả tim máu được mạnh mẽ hơn";
        Service.Help_From_Server(p, -138, txt);
        break;
      }
    }
  }

  private static void Menu_Nami(Player p, byte index) throws IOException {
    if (p.map.template.id == 17 && index == 5) {
      Market.show_table(p);
    } else {
      switch (index) {
        case 0: {
          send_dynamic_menu(p, 993, "Nami",
              new String[] {"Đổi Ruby", "Đổi extol", "Nạp tiền", "GiftCode", "Đổi Ruby",
                  "Đổi Beri", "Xem Coin", "Mã quà tặng"},
              new short[] {128, 128, 132, 161, 127, 162, 140, 140});
          break;
        }
        case 1: {
          break;
        }
        case 2: {
          break;
        }
        case 3: {
          send_dynamic_menu(p, 981, "Đấu giá", new String[] {"Đấu giá"}, null);
          break;
        }
        case 4: {
          send_dynamic_menu(p, 987, "Điểm tích lũy", new String[] {"Vào cửa hàng"}, null);
          break;
        }
        
      }
    }
  }

  private static void Menu_Zosaku(Player p, byte index) throws IOException {
    switch (index) {
      case 3:
      case 0: {
        break;
      }
      case 1: { // map pvp
        Vgo vgo = new Vgo();
        vgo.map_go = Map.get_map_by_id(1000);
        if (vgo.map_go != null) {
          boolean full = false;
          if (!full) {
            vgo.xnew = (short) Util.random(150, 300);
            vgo.ynew = (short) Util.random(200, 300);
            p.goto_map(vgo);
          } else {
            Service.send_box_ThongBao_OK(p, "Map đầy hãy quay lại sau");
          }
        }
        break;
      }
      case 2: { // pho ban lien tang
        break;
      }
      case 4: {
        if (p.map.template.id == 25) {
          String[] name = new String[12];
          for (int i = 0; i < name.length; i++) {
            name[i] = ("Cấp độ " + (i + 3));
          }
          short[] icon = new short[12];
          for (int i = 0; i < icon.length; i++) {
            if (i < 2) {
              icon[i] = 164;
            } else if (i < 5) {
              icon[i] = 165;
            } else if (i < 7) {
              icon[i] = 166;
            } else {
              icon[i] = 167;
            }
          }
          send_dynamic_menu(p, 988, "Vượt ải đơn", name, icon);
        } else if (p.map.template.id == 49) { // lenh truy na
          Vgo vgo = new Vgo();
          vgo.map_go = Map.get_map_by_id(119);
          if (vgo.map_go != null) {
            boolean full = false;
            if (!full) {
              vgo.xnew = (short) Util.random(120, 380);
              vgo.ynew = (short) Util.random(230, 330);
              p.goto_map(vgo);
            } else {
              Service.send_box_ThongBao_OK(p, "Map đầy hãy quay lại sau");
            }
          }
        }
      }
    }
  }

  private static void Select_Map_Tele(Player p, byte index) throws IOException {
    if (p.map_tele != null) {
      Map[] map_go = Map.get_map_by_id(p.map_tele[index]);
      if (p.map.template.id == map_go[0].template.id) {
        Service.send_box_ThongBao_OK(p, "Đang ở map này rồi!");
      } else {
        Vgo vgo = new Vgo();
        vgo.map_go = map_go;
        for (int i = 0; i < vgo.map_go[0].template.npcs.size(); i++) {
          Npc npc_temp = vgo.map_go[0].template.npcs.get(i);
          if (npc_temp.namegt.equals("Bản đồ")) {
            vgo.xnew = npc_temp.x;
            if (npc_temp.y < 250) {
              vgo.ynew = (short) (npc_temp.y + 20);
            } else {
              vgo.ynew = (short) (npc_temp.y - 20);
            }
            break;
          }
        }
        if (vgo.xnew == 0 || vgo.ynew == 0) {
          vgo.xnew = (short) (vgo.map_go[0].template.maxW / 2);
          vgo.ynew = (short) (vgo.map_go[0].template.maxH / 2);
        }
        p.goto_map(vgo);
      }
    }
    p.map_tele = null;
  }

  private static void Select_Map_Tele_world(Player p, byte index) throws IOException {
    if (p.map_tele != null && index < p.map_tele.length) {
      p.data_yesno = new int[] {index};
      Service.send_box_yesno(
          p, 5, "", "Bạn có muốn dịch chuyển qua "
              + Map.get_map_by_id(p.map_tele[index])[0].template.name + " ?",
          new String[] {"20", "Hủy"}, new byte[] {6, -1});
    }
  }

  private static void Menu_HuongDan(Player p, byte index) throws IOException {
    switch (p.map.template.id) {
      case 1: {
        HelpDialog.show_LangCoiXayGio(p, index);
        break;
      }
      case 9: {
        HelpDialog.show_ThiTranVoSo(p, index);
        break;
      }
      case 17: {
        HelpDialog.show_ThiTranOrange(p, index);
        break;
      }
      case 25: {
        HelpDialog.show_LangSiRup(p, index);
        break;
      }
      case 33: {
        HelpDialog.show_ThuyenBarati(p, index);
        break;
      }
      case 41: {
        HelpDialog.show_LangHatDe(p, index);
        break;
      }
      case 49: {
        HelpDialog.show_ThiTranKhoiDau(p, index);
        break;
      }
      case 66: {
        HelpDialog.show_MomSinhDoi(p, index);
        break;
      }
      case 69: {
        HelpDialog.show_ThiTranWhiskey(p, index);
        break;
      }
      case 79: {
        HelpDialog.show_DaoLittleGrand(p, index);
        break;
      }
    }
  }

  private static void Menu_Remove_Skill(Player p, byte index) throws IOException {
    for (int i = 0; i < p.skill_point.size(); i++) {
      Skill_info temp = p.skill_point.get(i);
      if (temp.temp.ID >= 1000 && temp.temp.ID < 2000 && temp.temp.Lv_RQ > 0) {
        index--;
        if (index == -1) {
          p.data_yesno = new int[] {i};
          Service.send_box_yesno(p, 4, "Thông báo",
              ("Bạn có chắc muốn xóa kỹ năng " + temp.temp.name
                  + " này không? Phí xóa kỹ năng này là 2 ruby"),
              new String[] {"2", "Không"}, new byte[] {7, -1});
          break;
        }
      }
    }
  }

  private static void Menu_Robin(Player p, byte index) throws IOException {
    switch (index) {
      case 0: {
        EffTemplate eff = p.get_eff(3);
        if (eff != null) {
          Service.send_box_ThongBao_OK(p,
              "Thời gian x2 kỹ năng EXP còn lại "
                  + Util.get_time_str_by_sec2(eff.time - System.currentTimeMillis())
                  + "\nLưu ý cộng dồn tối đa 7 ngày");
        } else {
          Service.send_box_ThongBao_OK(p, "Thời gian x2 kỹ năng EXP còn lại 0 s.");
        }
        break;
      }
      case 1: {
        EffTemplate eff = p.get_eff(8);
        if (eff != null) {
          Service.send_box_ThongBao_OK(p,
              "Thời gian khóa EXP còn lại "
                  + Util.get_time_str_by_sec2(eff.time - System.currentTimeMillis())
                  + "\nLưu ý cộng dồn tối đa 30 ngày");
        } else {
          Service.send_box_ThongBao_OK(p, "Thời gian khóa EXP còn lại 0 s.");
        }
        break;
      }
      case 2: {
        EffTemplate effTemplate = p.get_eff(8);
        if (effTemplate != null) {
          effTemplate.time = 0;
          Service.send_box_ThongBao_OK(p, "Hủy thành công");
        }
        break;
      }
      case 3: {
          if(p.conn.status !=1){
             Service.send_box_ThongBao_OK(p, "Chưa kích hoạt không thể tham gia"); 
             return;
          }
        send_dynamic_menu(p, 989, "Tài xỉu", new String[] {"Tham gia", "Nhận thưởng"}, null);
        break;
      }
    }
  }

  private static void Menu_Learn_Skill(Player p, byte index) throws IOException {
    for (int i = 0; i < p.skill_point.size(); i++) {
      Skill_info temp = p.skill_point.get(i);
      if ((temp.temp.ID < 4 && temp.temp.Lv_RQ == -1)
          || (temp.temp.ID > 3 && temp.temp.ID < 2000 && temp.temp.Lv_RQ < 5)) {
        index--;
        if (index == -1) {
          p.data_yesno = new int[] {i};
          if (temp.temp.ID < 4) {
            Service.send_box_yesno(p, 3, "Thông báo",
                ("Bạn có muốn học kỹ năng " + temp.temp.name + "?"),
                new String[] {"10.000", "Không"}, new byte[] {6, -1});
          } else {
            Skill_Template sk_temp = null;
            if (temp.temp.ID > 3 && temp.temp.ID < 2000 && temp.temp.Lv_RQ > -1) {
              sk_temp = Skill_Template.get_temp((temp.temp.indexSkillInServer + 1), 0);
            }
            Service.send_box_yesno(p, 3, "Thông báo",
                ("Bạn có muốn học chiêu nội tại "
                    + (sk_temp != null ? sk_temp.name : temp.temp.name) + "?"),
                new String[] {"10.000", "Không"}, new byte[] {6, -1});
          }
          break;
        }
      }
    }
  }

  private static void Menu_Gap(Player p, byte index) throws IOException {
    switch (index) {
      case 0: {
        List<String> str_ = new ArrayList<>();
        List<Integer> icon_ = new ArrayList<>();
        for (int i = 0; i < p.skill_point.size(); i++) {
          Skill_info temp = p.skill_point.get(i);
          if ((temp.temp.ID < 4 && temp.temp.Lv_RQ == -1)
              || (temp.temp.ID > 3 && temp.temp.ID < 2000 && temp.temp.Lv_RQ < 5)) {
            if (temp.temp.ID > 3 && temp.temp.ID < 2000 && temp.temp.Lv_RQ > -1) {
              Skill_Template sk_temp =
                  Skill_Template.get_temp((temp.temp.indexSkillInServer + 1), 0);
              str_.add(sk_temp.name);
              icon_.add((int) (sk_temp.idIcon));
            } else {
              str_.add(temp.temp.name);
              icon_.add((int) (temp.temp.idIcon));
            }
          }
        }
        if (str_.size() == 0) {
          str_.add("Hiện tại không có kỹ năng thích hợp để học");
          icon_ = null;
        }
        send_dynamic_menu(p, 998, "Học kỹ năng - Gap", str_, icon_);
        break;
      }
      case 1: {
        if (p.get_ngoc() < 5) {
          Service.send_box_ThongBao_OK(p, "Bạn không đủ tiền, hồi điểm tiềm năng phải cần 5 Ruby.");
          return;
        }
        Service.send_box_yesno(p, 2, "",
            "Bạn có thật sự muốn hồi điểm tiềm năng?\nMức phí là 5 Ruby.",
            new String[] {"5", "Hủy"}, new byte[] {7, -1});
        break;
      }
      case 2: {
        List<String> str_ = new ArrayList<>();
        List<Integer> icon_ = new ArrayList<>();
        for (int i = 0; i < p.skill_point.size(); i++) {
          Skill_info temp = p.skill_point.get(i);
          if (temp.temp.ID >= 1000 && temp.temp.ID < 2000 && temp.temp.Lv_RQ > 0) {
            str_.add(temp.temp.name);
            icon_.add((int) (temp.temp.idIcon));
          }
        }
        if (str_.size() == 0) {
          str_ = null;
          icon_ = null;
        }
        if (icon_ == null) {
          send_dynamic_menu(p, 997, "Xóa kỹ năng - Gap",
              new String[] {"Hiện tại chưa học nội tại gì"}, null);
        } else {
          send_dynamic_menu(p, 997, "Xóa kỹ năng - Gap", str_, icon_);
        }
        break;
      }
      case 4: {
        if (p.level >= 100) {
          Max_Level.show_table(p);
        }
        break;
      }
    }
  }

  private static void Menu_TruongLang(Player p, byte index) throws IOException {
    if (!(p.conn.status != 1)) {
      index++;
    }
    switch (index) {
      case 0: {
//        if (p.level > 3) {
//          Service.input_text(p, 2, "Đăng ký",
//              new String[] {"Tên tài khoản (Email hoặc SĐT)", "Mật khẩu (6 đến 10 ký tự)"});
//        } else {
//          Service.send_box_ThongBao_OK(p, "Hãy luyện tập đến khi level 4 hãy quay lại đây!");
//        }
          if (p.conn.coin < 10000) {
              Service.send_box_ThongBao_OK(p, "Bạn Không đủ 10000 Coin để kích hoạt!");
              return;
          }
          p.update_coin(-10000);
          p.conn.status = 1;
          p.update_status(1);
          Service.send_box_ThongBao_OK(p, "Bạn đã kích hoạt thành công!");
         break;
      }
      case 1: {
        BXH.send(p, 7, 0);
        break;
      }
      case 2: {
        BXH.send(p, 4, 0);
        break;
      }
      case 3: {
        BXH.send(p, 6, 0);
        break;
      }
      case 4: {
        BXH.send(p, 9, 0);
        break;
      }
      case 5: {
        send_dynamic_menu(p, 978, "Đá hành trình",
            new String[] {"Kho hành trình", "Bản đồ hành trình", "Hướng dẫn"}, null);
        break;
      }
      case 6: {
                if (p.diemdanh == 0) {
                    p.diemdanh = 1;
                    int ruby = Util.random(1, 200);
                    int beri = Util.random(1, 1000000);
                    p.update_ngoc(ruby);
                    p.update_vang(beri);
                    p.update_money();
                    Service.send_box_ThongBao_OK(p, "Bạn đã điểm danh thành công, được " + ruby + " ruby "+ beri + " Beri");
                } else {
                    Service.send_box_ThongBao_OK(p, "Bạn đã điểm danh hôm nay rồi");
                }
                break;
      }
      case 7: {
                if (p.diemdanhvip == 0) {
                    p.diemdanhvip = 1;
                    if (p.conn.vip == 0){
                    int ruby = 100;
                    p.update_ngoc(ruby);
                    p.update_money();
                    Service.send_box_ThongBao_OK(p, "Bạn đã điểm danh vip 0 thành công, được " + ruby + " ruby ");
                    return;
                    }
                    if (p.conn.vip == 1){
                    int ruby = 215;
                    p.update_ngoc(ruby);
                    p.update_money();
                    Service.send_box_ThongBao_OK(p, "Bạn đã điểm danh vip 1 thành công, được " + ruby + " ruby ");
                    return;
                    }
                    if (p.conn.vip == 2){
                    int ruby = 1000;
                    p.update_ngoc(ruby);
                    p.update_money();
                    Service.send_box_ThongBao_OK(p, "Bạn đã điểm danh vip 2 thành công, được " + ruby + " ruby ");
                    return;
                    }
                    if (p.conn.vip == 3){
                    int ruby = 2000;
                    p.update_ngoc(ruby);
                    p.update_money();
                    Service.send_box_ThongBao_OK(p, "Bạn đã điểm danh vip 3 thành công, được " + ruby + " ruby ");
                    return;
                    }
                    if (p.conn.vip == 4){
                    int ruby = 3000;
                    p.update_ngoc(ruby);
                    p.update_money();
                    Service.send_box_ThongBao_OK(p, "Bạn đã điểm danh vip 4 thành công, được " + ruby + " ruby ");
                    return;
                    }
                    if (p.conn.vip == 5){
                    int ruby = 7000;
                    p.update_ngoc(ruby);
                    p.update_money();
                    Service.send_box_ThongBao_OK(p, "Bạn đã điểm danh vip 5 thành công, được " + ruby + " ruby ");
                    return;
                    }
                    if (p.conn.vip == 6){
                    int ruby = 20000;
                    p.update_ngoc(ruby);
                    p.update_money();
                    Service.send_box_ThongBao_OK(p, "Bạn đã điểm danh vip 6 thành công, được " + ruby + " ruby ");
                    return;
                    }
                    if (p.conn.vip == 7){
                    int ruby = 50000;
                    p.update_ngoc(ruby);
                    p.update_money();
                    Service.send_box_ThongBao_OK(p, "Bạn đã điểm danh vip thành công, được " + ruby + " ruby ");
                    return;
                    }
                } else {
                    Service.send_box_ThongBao_OK(p, "Bạn đã điểm danh vip hôm nay rồi");
                }
                break;
      }
    }
  }

  private static void Menu_Buggi(Player p, byte index) throws IOException {
    switch (index) {
      case 0: {
        VongQuay.show_table(p);
        break;
      }
      case 1: {
        send_dynamic_menu(p, 991, "Hoàn mỹ - Kích ẩn",
            new String[] {"Hoàn mỹ", "Kích ẩn", "Phục hồi chế tác"}, null);
        break;
      }
      
      case 2: {
        Service.send_box_ThongBao_OK(p, "Chức năng chưa ra mắt");
        break;
      }
      case 3: { // Lục Thức
                send_dynamic_menu(p, 1002,"Lục Thức", new String[]{"Hướng dẫn", "Luyện Lục Thức", "Cảnh Giới"}, null);
                break;
            }
      case 4: { // Sức Mạnh Vật Lý
                send_dynamic_menu(p, 1003,"Sức Mạnh Vật Lý", new String[]{"Hướng dẫn", "Tăng Sức Mạnh", "Thể Trạng"}, null);
                break;
            }
      case 5: { // Doriki
                send_dynamic_menu(p, 1001,"Doriki", new String[]{"Hướng dẫn", "Nâng Cấp", "Doriki"}, null);
                break;
            }
    }
  }

  private static void Menu_KhamNgoc(Player p, byte index) throws IOException {
    switch (index) {
      case 0: {
        Service.Send_UI_Shop(p, 111);
        break;
      }
      case 1:
      case 2:
      case 3:
      case 4:
      case 5: {
        Rebuild_Item.show_table(p, index);
        break;
      }
      case 6: { // ngoc than thoai
        Message m = new Message(-19);
        m.writer().write(DaThanThoai.data_shop);
        p.conn.addmsg(m);
        m.cleanup();
        break;
      }
      case 7: {
        String txt = "Khảm vật phẩm gồm 2 chức năng chính:\n" + "Khảm đá vào vật phẩm\n"
            + "Ghép đá\b" + "Khảm đá vào vật phẩm:\n"
            + "Mỗi vật phẩm mới khi mở rương sẽ có ngẫu nhiên các Lỗ Khảm dể bạn có thể gắn những viênd đá đặc biệt vào giúp "
            + "tăng sức mạnh cho bản thân.\b"
            + "Bạn có thể đục lỗ để có thể gắn được nhiều đá hơn (tối đa 2 lần).\n"
            + "Ngoài ra còn có chức năng lấy đá từ vật phẩm đã khảm để gắn vào vật phẩm mới.\b"
            + "Ghép đá:\n" + "Đá khảm rẩt đa dạng và mỗi loại có 6 cấp độ khác nhau.\b"
            + "Bạn có thể dùng 3 viên đá cấp thấp để ghép thành viên đá cấp cao hơn cùng loại.";
        Service.Help_From_Server(p, -47, txt);
        break;
      }
    }
  }

  private static void Menu_Admin(Player p, byte index) throws IOException {
    if (p.conn.user.equals("admin")) {
      switch (index) {
        case 0: {
          new Thread(() -> {
            synchronized (SessionManager.CLIENT_ENTRYS) {
              System.out.println("START CLOSE SERVER");
              SaveData.process();
              try {
                ServerManager.gI().close();
              } catch (IOException e) {
                e.printStackTrace();
                System.exit(0);
              }
              synchronized (SessionManager.CLIENT_ENTRYS) {
                for (int i = SessionManager.CLIENT_ENTRYS.size() - 1; i >= 0; i--) {
                  try {
                    SessionManager.CLIENT_ENTRYS.get(i).p = null;
                    SessionManager.CLIENT_ENTRYS.get(i).disconnect();
                  } catch (Exception e) {
                    e.printStackTrace();
                    System.err.println("err 1 menu admin");
                  }
                }
              }
              System.out.println("WAIT TO SHUTDOWN SERVICE");
              try {
                Thread.sleep(5000L);
              } catch (InterruptedException e) {
                e.printStackTrace();
                System.exit(0);
              }
              Manager.gI().close();
              SQL.gI().close();
              System.out.println("CLOSE SERVER");
            }
          }).start();
          break;
        }
        case 1: {
          p.update_vang(1_000_000_000);
          p.update_ngoc(1_000_000_000);
          p.update_money();
          break;
        }
        case 2: {
          Service.input_text(p, 32000, "Uplevel", new String[] {"Nhập level"});
          break;
        }
        case 3: {
          Service.input_text(p, 32001, "SetXP", new String[] {"Nhập mức"});
          break;
        }
        case 4: {
          Service.input_text(p, 32002, "Get Item",
              new String[] {"Type item", "Id item", "Số lượng"});
          break;
        }
        case 5: {
          SaveData.process();
          Service.send_box_ThongBao_OK(p, "Thành công");
          break;
        }
      }
    }
  }

  private static void Menu_Rebuilt_Item(Player p, byte index) throws IOException {
    switch (index) {
      case 0: {
        Service.Send_UI_Shop(p, 6);
        break;
      }
      case 1: {
        int[] id = new int[] {1, 3, 9, 8, 11};
        String[] name = new String[id.length];
        byte[] icon = new byte[id.length];
        for (int i = 0; i < id.length; i++) {
          ItemTemplate7 temp = ItemTemplate7.get_it_by_id(id[i]);
          name[i] = temp.name;
          icon[i] = temp.icon;
        }
        send_dynamic_menu(p, 992, "Ghép nguyên liệu", name, icon, 7);
        break;
      }
      case 2: {
        Split_Item.show_table(p);
        break;
      }
      case 3: {
        UpgradeItem.show_table_upgrade(p);
        break;
      }
      case 4: {
        UpgradeSuperItem.show_table(p);
        break;
      }
    }
  }

  private static void Menu_Johny(Player p, byte index) throws IOException {
    if (p.map.template.id == 25 && index == 5) { // cuong hoa ac quy
      send_dynamic_menu(p, 32002, "Cường hóa ác quỷ",
          new String[] {"Cửa hàng đá khảm", "Cường hóa Rương ác quỷ", "Cường hóa Kỹ năng"},
          new short[] {129, 155, 156});
    } else {
      switch (index) {
        case 0: {
          send_dynamic_menu(
              p, 32000, "Cường Hóa", new String[] {"Cửa hàng Nguyên liệu", "Ghép nguyên liệu",
                  "Tách nguyên liệu", "Cường hóa đồ", "Cường hóa cao cấp"},
              new short[] {129, 127, 130, 131, 163});
          break;
        }
        case 1: {
          send_dynamic_menu(p, 32001, "Khảm Đá",
              new String[] {"Cửa Hàng Đá Khảm", "Ghép Đá", "Đục lỗ khảm", "Khảm Vật Phẩm",
                  "Tách Đá", "Đá siêu cấp", "Đá thần thoại", "Hướng dẫn"},
              new short[] {129, 127, 133, 126, 130, 141, 132, 148});
          break;
        }
        case 2: {
          ChuyenHoa.show_table(p);
          break;
        }
        case 3: {
          Rebuild_Item.show_table(p, 9);
          break;
        }
        case 4: {
          int ver_ = Integer.parseInt(p.conn.version.replace(".", ""));
          if (ver_ >= 115) {
            Upgrade_Skin.show_table(p);
          } else {
            Service.send_box_ThongBao_OK(p, "Hãy sử dụng phiên bản từ 1.1.5 trở lên");
          }
          break;
        }
      }
    }
  }

  private static void Menu_Machiko(Player p, byte index) throws IOException {
    switch (index) {
      case 0: {
        Service.Send_UI_Shop(p, 20);
        break;
      }
      case 1: {
        Message m = new Message(-20);
        m.writer().writeByte(6);
        m.writer().writeShort(986); // idnpc
        m.writer().writeByte(0); // idmenu
        m.writer().writeUTF("Vận chuyển hàng");
        m.writer().writeByte(10);
        String[] name = new String[] {"Lấy Hàng", "Trả hàng", "Đăng ký bảo vệ hàng",
            "Thuê bảo vệ hàng", "Đăng ký chức năng", "Hủy vận buôn", "Gọi Lạc đà trở về",
            "Xem vị trí lạc đà", "Xem số lần vận buôn", "Hướng dẫn"};
        short[] icon = new short[] {107, 109, 110, 111, 110, 111, 151, -1, 114, 114};
        byte[] b7 = new byte[] {3, 3, 3, 3, 7, 3, 3, 7, 7, 7};
        for (int i = 0; i < 10; i++) {
          m.writer().writeUTF(name[i]);
          m.writer().writeShort(icon[i]);
          m.writer().writeByte(b7[i]);
        }
        p.conn.addmsg(m);
        m.cleanup();
        break;
      }
      case 2: {
        ItemFashionP.show_table(p, 103);
        break;
      }
      case 3: {
        ItemFashionP.show_table(p, 102);
        break;
      }
      case 4: {
        ItemFashionP.show_table(p, 105);
        break;
      }
      case 5: {
        ItemFashionP.show_table(p, 108);
        break;
      }
    }
  }

  private static void Menu_Guru(Player p, int index) throws IOException {
    switch (index) {
      case 0: {
        Service.Send_UI_Shop(p, (p.clazz - 1));
        break;
      }
      case 1: {
        String[] other_clazz_name = new String[4];
        short[] other_clazz_icon = new short[4];
        int pos = 0;
        for (int i = 0; i < 5; i++) {
          if ((i + 1) == p.clazz) {
            continue;
          }
          other_clazz_name[pos] = Clazz.NAME[i];
          other_clazz_icon[pos++] = Clazz.ICON[i];
        }
        send_dynamic_menu(p, 32003, "Hệ khác", other_clazz_name, other_clazz_icon);
        break;
      }
      case 2: {
        p.is_show_hat = !p.is_show_hat;
        Service.charWearing(p, p, false);
        Service.update_PK(p, p, false);
        p.update_info_to_all();
        Service.send_box_ThongBao_OK(p,
            p.is_show_hat ? "Đã bật hiển thị nón" : "Đã tắt hiển thị nón");
        break;
      }
      case 3: {
        send_dynamic_menu(p, 994, "Khóa Bảo Vệ",
            new String[] {"Đăng ký khóa bảo vệ", "Hướng dẫn", "Hủy mã khóa"},
            new short[] {118, 148, 118});
        break;
      }
      case 4: {
        Service.Send_UI_Shop(p, 119);
        break;
      }
    }
  }

  private static void Menu_Change_Zone(Player p) throws IOException {
    Message m = new Message(23);
    m.writer().writeByte(p.map.template.max_zone);
    Map[] map_ = Map.get_map_by_id(p.map.template.id);
    for (int i = 0; i < map_.length; i++) {
      int s = map_[i].players.size();
      int max = map_[i].template.max_player;
      // 0
      // green, 1 orange, 2 red, 3 violet, other green
      m.writer().writeByte((s >= max) ? 2 : ((s > (max / 2)) ? 1 : 0));
    }
    p.conn.addmsg(m);
    m.cleanup();
  }

  private static void Show_List_Map_Tele(Player p, int index, int idNPC) throws IOException {
    if (index == 1) {
      p.map_tele = MenuController.ID_MAP_LANG;
      send_dynamic_menu(p, 995, "Dịch chuyển", p.map_tele);
    } else if (index == 0) { // trong lang
      switch (idNPC) {
        case -5: {
          p.map_tele = new int[] {1, 2, 3, 4, 6};
          break;
        }
        case -12: {
          p.map_tele = new int[] {9, 10, 11, 12, 14};
          break;
        }
        case -144: {
          p.map_tele = new int[] {191, 192, 193, 194, 195, 196, 197};
          break;
        }
        case -124: {
          p.map_tele = new int[] {113, 112, 115, 116, 117, 118, 124, 125, 126};
          break;
        }
        case -107: {
          p.map_tele = new int[] {93, 94, 95, 96, 97, 98, 99, 100, 101, 103};
          break;
        }
        case -85: {
          p.map_tele = new int[] {83, 84, 85, 86, 88};
          break;
        }
        case -132: // dao jaza
        case -97: // dao little grand
        case -82: // mom sinh doi
          return;
        case 0: {
          p.map_tele = new int[] {69, 70, 71, 72, 74};
          break;
        }
        case -60: {
          p.map_tele = new int[] {49, 50, 51, 52, 54};
          break;
        }
        case -44: {
          p.map_tele = new int[] {41, 42, 43, 44, 46};
          break;
        }
        case -36: {
          p.map_tele = new int[] {33, 34, 35, 36, 38};
          break;
        }
        case -28: {
          p.map_tele = new int[] {25, 26, 27, 28, 30};
          break;
        }
        case -20: { // thi tran orang
          p.map_tele = new int[] {17, 18, 19, 20, 22};
          break;
        }
      }
      send_dynamic_menu(p, 996, "Dịch chuyển", p.map_tele);
    }
  }

  public static void send_dynamic_menu(Player p, int id_npc, String name_npc, String[] list_menu,
      short[] list_icon) throws IOException {
    if (!p.isdie) {
      Message m = new Message(-20);
      if (list_icon == null) {
        m.writer().writeByte(0);
      } else {
        m.writer().writeByte(5);
      }
      m.writer().writeShort(id_npc);
      m.writer().writeByte(0);
      m.writer().writeUTF(name_npc);
      m.writer().writeByte(list_menu.length);
      for (int i = 0; i < list_menu.length; i++) {
        m.writer().writeUTF(list_menu[i]);
        if (list_icon != null) {
          m.writer().writeShort(list_icon[i]);
        }
      }
      p.conn.addmsg(m);
      m.cleanup();
    }
  }

  private static void send_dynamic_menu(Player p, int id_npc, String name_npc, String[] list_menu,
      byte[] list_icon, int b) throws IOException {
    if (!p.isdie) {
      Message m = new Message(-20);
      m.writer().writeByte(3);
      m.writer().writeShort(id_npc);
      m.writer().writeByte(1);
      m.writer().writeUTF(name_npc);
      m.writer().writeByte(list_menu.length);
      for (int i = 0; i < list_menu.length; i++) {
        m.writer().writeUTF(list_menu[i]);
        m.writer().writeShort(list_icon[i]);
        m.writer().writeByte(b);
      }
      p.conn.addmsg(m);
      m.cleanup();
    }
  }

  private static void send_dynamic_menu(Player p, int id_npc, String name_npc,
      List<String> list_menu, List<Integer> list_icon) throws IOException {
    if (!p.isdie) {
      Message m = new Message(-20);
      m.writer().writeByte(4);
      m.writer().writeShort(id_npc);
      m.writer().writeByte(0);
      m.writer().writeUTF(name_npc);
      m.writer().writeByte(list_menu.size());
      for (int i = 0; i < list_menu.size(); i++) {
        m.writer().writeUTF(list_menu.get(i));
        m.writer().writeShort(list_icon.get(i));
      }
      p.conn.addmsg(m);
      m.cleanup();
    }
  }

  private static void send_dynamic_menu(Player p, int idNPC, String title, String[] name)
      throws IOException {
    if (!p.isdie) {
      Message m = new Message(-20);
      m.writer().writeByte(2);
      m.writer().writeShort(idNPC);
      m.writer().writeByte(0);
      m.writer().writeUTF(title);
      m.writer().writeByte(name.length);
      for (int i = 0; i < name.length; i++) {
        m.writer().writeUTF(name[i]);
      }
      p.conn.addmsg(m);
      m.cleanup();
    }
  }

  private static void send_dynamic_menu(Player p, int idNPC, String title, int[] name)
      throws IOException {
    if (!p.isdie) {
      int idMap = MapCanGoTo.idMap[MapCanGoTo.idMap.length - 1];
      //
      QuestP quest_select = p.list_quest.get(0);
      if (quest_select != null) {
        for (int i = 0; i < MapCanGoTo.idQuest.length; i++) {
          if (MapCanGoTo.idQuest[i] > quest_select.template.id) {
            idMap = MapCanGoTo.idMap[i - 1];
            break;
          }
        }
      }
      // System.out.println(idMap);
      //
      Message m = new Message(-20);
      m.writer().writeByte(1);
      m.writer().writeShort(idNPC);
      m.writer().writeByte(0);
      m.writer().writeUTF(title);
      m.writer().writeByte(name.length);
      for (int i = 0; i < name.length; i++) {
        Map map = Map.get_map_by_id(name[i])[0];
        m.writer().writeUTF(map.template.name);
        if (name[i] <= idMap) {
          m.writer().writeByte(map.template.id == p.map.template.id ? 4 : 2);
        } else {
          m.writer().writeByte(1);
        }
        m.writer().writeByte(7);
      }
      p.conn.addmsg(m);
      m.cleanup();
    }
  }
}
