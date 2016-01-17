package com.gambasoft.rs;

import java.io.InputStream;
import java.security.NoSuchAlgorithmException;

import javax.ws.rs.GET;
import javax.ws.rs.Path;
import javax.ws.rs.PathParam;
import javax.ws.rs.Produces;
import javax.ws.rs.QueryParam;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;

import org.apache.log4j.Logger;

import com.gambasoft.service.RankSystemService;

@Path("/")
public class RankSystemRest {
	static final Logger LOGGER = Logger.getLogger(RankSystemService.class);
	String logs = "[CLASS] - % ==== [METHOD] - % ==== [PARAMETERS] - %,%";
 
	@GET
	@Path("/verify")
	@Produces(MediaType.TEXT_PLAIN)
	public Response verifyRESTService(InputStream incomingData) {
		String result = "RESTService Successfully started..";
		// return HTTP response 200 in case of success
		return Response.status(200).entity(result).build();
	}
	
	@GET
	@Path("/score/{game}")
	@Produces(MediaType.APPLICATION_JSON)
	public Response add(@PathParam("game") String game, @QueryParam("name") final String name, @QueryParam("score") final String score, @QueryParam("countrycode") final String countrycode, @QueryParam("hash") final String hash) throws NoSuchAlgorithmException{
		final String METHOD = "add";
		LOGGER.info(String.format("[CLASS] ==== [METHOD] - %s ==== [PARAMETERS] - (academic) = %s,%s,%s,%s", METHOD, game, name, score, countrycode, hash));
		
		Response response = RankSystemService.addScore(game, name, score, countrycode, hash);
		
		return response;
	}
	@GET
	@Path("/rank/top/{numberOfBestPlayers}")
	@Produces(MediaType.TEXT_HTML)
	public Response getRankTopX(@PathParam("numberOfBestPlayers") final int numberOfBestPlayers){
		final String METHOD = "get";
		LOGGER.info(String.format("[CLASS] ==== [METHOD] - %s ==== [PARAMETERS]", METHOD));
		
		return RankSystemService.getRankTopX(numberOfBestPlayers);
	}
	
	@GET
	@Path("/rank/{numberOfBestPlayers}")
	@Produces(MediaType.APPLICATION_JSON)
	public Response getRank(@PathParam("numberOfBestPlayers") final int numberOfBestPlayers){
		final String METHOD = "get";
		LOGGER.info(String.format("[CLASS] ==== [METHOD] - %s ==== [PARAMETERS]", METHOD));
		
		return RankSystemService.getRankJSON(numberOfBestPlayers);
	}
}
