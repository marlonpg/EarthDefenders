package com.gambasoft.dao;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

import org.apache.log4j.Logger;

import com.gambasoft.db.ConnectionFactory;
import com.gambasoft.vo.Score;

public class RankSystemDAO {
	static final Logger LOGGER = Logger.getLogger(RankSystemDAO.class);
    private static RankSystemDAO instance = new RankSystemDAO();
    
    public static RankSystemDAO getRankSystemDAO() {
        return instance;
    }
    
	public void addScore(final String game, final String name, final String score, final String countrycode) {
		final String METHOD = "addScore";
		LOGGER.info(String.format("[CLASS] ==== [METHOD] - %s ==== [PARAMETERS] - (academic) = %s,%s,%s,%s", METHOD, game, name, score, countrycode));

		String sql = "INSERT INTO scores VALUES (null,?,?,?,?) ";
		Connection connection = ConnectionFactory.getConnection();
		PreparedStatement pStatement;
		
		try {
			pStatement = connection.prepareStatement(sql);
			pStatement.setString(1, game);
			pStatement.setString(2, name);
			pStatement.setString(3, score);
			pStatement.setString(4, countrycode);
			
			pStatement.executeUpdate();
			
			connection.close();
		} catch (SQLException e) {
			e.printStackTrace();
		}
	}

	public String getRankTopX(final int numberOfBestPlayers) {
		final String METHOD = "getScore";
		LOGGER.info(String.format("[CLASS] ==== [METHOD] - %s ==== [PARAMETERS]", METHOD));

		String sql = "SELECT * FROM scores ORDER BY score DESC LIMIT ? ";
		Connection connection = ConnectionFactory.getConnection();
		PreparedStatement pStatement;
		StringBuilder sb = new StringBuilder();
		try {
			pStatement = connection.prepareStatement(sql);
			pStatement.setInt(1, numberOfBestPlayers);
			ResultSet rs = pStatement.executeQuery();
			
			while(rs.next()){
				sb.append(rs.getString("name") + "\t" + rs.getString("score") +"\n");
			}
			
			connection.close();
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return sb.toString();
	}
	
	public List<Score> getRankJSON(final int numberOfBestPlayers) {
		final String METHOD = "getScore";
		LOGGER.info(String.format("[CLASS] ==== [METHOD] - %s ==== [PARAMETERS]", METHOD));

		String sql = "SELECT * FROM scores ORDER BY score DESC LIMIT ? ";
		Connection connection = ConnectionFactory.getConnection();
		PreparedStatement pStatement;
		StringBuilder sb = new StringBuilder();
		List<Score> scores = new ArrayList<Score>();
		try {
			pStatement = connection.prepareStatement(sql);
			pStatement.setInt(1, numberOfBestPlayers);
			ResultSet rs = pStatement.executeQuery();
			
			while(rs.next()){
				Score score = new Score();
				score.setName(rs.getString("name"));
				score.setScore(rs.getInt("score"));
				score.setCountrycode(rs.getString("countrycode"));
				scores.add(score);
			}
			
			connection.close();
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return scores;
	}
}
