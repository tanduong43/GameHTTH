package activities;

import java.io.IOException;
import client.Player;
import core.Service;
import io.Message;
/**
 *
 * @author Truongbk
 */
public class Chat {
	public static void process(Player p, Message m2, int type) throws IOException {
		if (type == 0) {
			String tab_name = m2.reader().readUTF();
			String text = m2.reader().readUTF();
			switch (tab_name) {
				case "Nhóm": {
					if (p.party != null) {
						for (int i = 0; i < p.party.list.size(); i++) {
							Player p0 = p.party.list.get(i);
							if (p0.index_map != p.index_map) {
								send_chat(p0, tab_name, "@" + p.name + " : " + text, false);
							}
						}
					} else {
						Service.send_box_ThongBao_OK(p, "Nhóm không tồn tại");
					}
					break;
				}
			}
		}
	}

	public static void send_chat(Player p, String tab_name, String text, boolean cache)
			throws IOException {
		if (p.conn != null) {
			Message m = new Message(18);
			m.writer().writeUTF(tab_name);
			m.writer().writeUTF(text);
			if (cache) {
				p.list_msg_cache.add(m);
			} else {
				p.conn.addmsg(m);
			}
			m.cleanup();
		}
	}
}
//                } else if (listGet.cat[i] == 105) {
//                    ItemFashion itf = ItemFashion.get_item(listGet.id[i]);
//                    GiftBox gb_beri4 = new GiftBox();
//                    ItemFashionP2 temp2 = new ItemFashionP2();
//                    temp2.id = itf.ID;
//                    p.fashion.add(temp2);
//                    p.update_fashionP2(temp2);
//                    for (int j = 0; j < p.map.players.size(); j++) {
//                        Player p0 = p.map.players.get(j);
//                        Service.charWearing(p, p0, false);
//                    }
//                    Service.UpdateInfoMaincharInfo(p);
//                    gb_beri4.id = itf.ID;
//                    gb_beri4.type = 105;
//                    gb_beri4.name = itf.name;
//                    gb_beri4.icon = itf.idIcon;
//                    gb_beri4.num = 1;
//                    gb_beri4.color = 0;
//                    list.add(gb_beri4);
//                } else {
//                    Service.send_box_ThongBao_OK(p, "Có lỗi xảy ra, hãy thử lại sau!");
//                    return;
//                }