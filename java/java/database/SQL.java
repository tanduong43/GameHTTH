package database;

import java.sql.Connection;
import java.sql.SQLException;
import com.zaxxer.hikari.HikariDataSource;
import core.Manager;
/**
 *
 * @author Truongbk
 */
public class SQL {
	private static SQL instance = null;
	private HikariDataSource dataSource;
	private final String url;
	private final String user;
	private final String pass;

	public SQL() {
		url = "jdbc:mysql://" + Manager.gI().mysql_host + ":3306/" + Manager.gI().mysql_database
				+ "?autoReconnect=true&useUnicode=yes&characterEncoding=UTF-8";
		// System.out.println(url);
		user = Manager.gI().mysql_user;
		pass = Manager.gI().mysql_pass;
		HikariDataSource config = new HikariDataSource();
		config.setDriverClassName("com.mysql.cj.jdbc.Driver");
		config.setJdbcUrl(url);
		config.setUsername(user);
		config.setPassword(pass);
		//
		config.setConnectionTimeout(30_000L);
		config.setIdleTimeout(600_000);
		config.setMaximumPoolSize(21);
		config.setPoolName("HTTH_pool");
		dataSource = new HikariDataSource(config);
		System.out.println("OPEN DataBase connect");
	}

	public static SQL gI() {
		if (instance == null) {
			instance = new SQL();
		}
		return instance;
	}

	public synchronized Connection getCon() {
		Connection conn = null;
		try {
			conn = dataSource.getConnection();
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return conn;
	}

	public void close() {
		dataSource.close();
	}
}
