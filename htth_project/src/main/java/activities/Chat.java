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
				default: {
					if (tab_name.equals("Thế giới") || tab_name.equals("Bang") || tab_name.equals("Băng")
							|| tab_name.equals("Hệ thống") || tab_name.equals("Đồng minh") || tab_name.equals(p.name)) {
						break;
					}
					Player p0 = map.Map.get_player_by_name_allmap(tab_name);
					if (p0 != null && p0.conn != null) {
						send_chat(p0, p.name, text, false);
					} else {
						send_chat(p, tab_name, "Đối phương đang offline", false);
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
