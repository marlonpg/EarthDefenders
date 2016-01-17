package com.gambasoft.service;

import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.List;

import javax.ws.rs.core.Response;

import org.apache.commons.lang3.StringUtils;
import org.apache.log4j.Logger;

import com.gambasoft.dao.RankSystemDAO;
import com.gambasoft.vo.Score;

public class RankSystemService {
	static final Logger LOGGER = Logger.getLogger(RankSystemService.class);

	private final static String SECURITY = "PutYouRSecurityCode";

	public static Response addScore(final String game, final String name, final String score, final String countrycode, final String hash) throws NoSuchAlgorithmException {
		final String METHOD = "addScore";
		LOGGER.info(String.format("[CLASS] ==== [METHOD] - %s ==== [PARAMETERS] - (academic) = %s,%s,%s,%s,%s", METHOD, game, name, score, countrycode, hash));
		Response response = paramValidation(game, name, score, countrycode, hash);

		if (response == null) {
			String securityString = name + score + SECURITY;

			MessageDigest md = MessageDigest.getInstance("MD5");
			md.update(securityString.getBytes());

			byte byteData[] = md.digest();

			// convert the byte to hex format method 1
			StringBuffer sb = new StringBuffer();
			for (int i = 0; i < byteData.length; i++) {
				sb.append(Integer.toString((byteData[i] & 0xff) + 0x100, 16).substring(1));
			}

			System.out.println("Digest(in hex format):: " + sb.toString());
			if (sb.toString().equals(hash)) {
				RankSystemDAO.getRankSystemDAO().addScore(game, name, score, countrycode);
				response = Response.status(200).header("Access-Control-Allow-Origin", "*").build();
			} else {
				response = Response.status(401).header("Access-Control-Allow-Origin", "*").build();
			}
		}
		return response;
	}

	private static Response paramValidation(final String game, final String name, final String score, final String countrycode, final String hash) {
		final String METHOD = "paramValidation";
		LOGGER.info(String.format("[CLASS] ==== [METHOD] - %s ==== [PARAMETERS] - (academic) = %s,%s,%s,%s,%s", METHOD, game, name, score, countrycode, hash));

		if (StringUtils.isBlank(game)) {
			return Response.status(400).header("Access-Control-Allow-Origin", "*").entity("Parameter game is required").build();
		} else if (StringUtils.isBlank(name)) {
			return Response.status(400).header("Access-Control-Allow-Origin", "*").entity("Parameter name is required").build();
		} else if (StringUtils.isBlank(score)) {
			return Response.status(400).header("Access-Control-Allow-Origin", "*").entity("Parameter score is required").build();
		} else if (StringUtils.isBlank(countrycode)) {
			return Response.status(400).header("Access-Control-Allow-Origin", "*").entity("Parameter countrycode is required").build();
		} else if (StringUtils.isBlank(hash)) {
			return Response.status(400).header("Access-Control-Allow-Origin", "*").entity("Parameter hash is required").build();
		}
		return null;
	}

	public static Response getRankTopX(final int numberOfBestPlayers) {
		final String METHOD = "getScore";
		LOGGER.info(String.format("[CLASS] ==== [METHOD] - %s ==== [PARAMETERS]", METHOD));

		String rank = RankSystemDAO.getRankSystemDAO().getRankTopX(numberOfBestPlayers);
		
		return Response.status(200).header("Access-Control-Allow-Origin", "*").entity(rank).build();
		//return rank;
	}
	
	public static Response getRankJSON(final int numberOfBestPlayers) {
		final String METHOD = "getScore";
		LOGGER.info(String.format("[CLASS] ==== [METHOD] - %s ==== [PARAMETERS]", METHOD));

		List<Score> scores = RankSystemDAO.getRankSystemDAO().getRankJSON(numberOfBestPlayers);
		
		return Response.status(200).header("Access-Control-Allow-Origin", "*").entity(scores).build();
		//return rank;
	}
}
