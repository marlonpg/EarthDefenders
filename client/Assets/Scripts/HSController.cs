using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using SimpleJSON;
using UnityEngine.SceneManagement;

public class HSController : MonoBehaviour
{
	private string secretKey = "PutYouRSecurityCode";
	private string game = "earthdefenders";
	public string addScoreURL = "http://127.0.0.1/RankSystem/api/score/";
	public string highscoreURL = "http://127.0.0.1/RankSystem/api/rank/10";
	public Text globalScoreText;
	public GameObject globalGameObject;
	public GameObject groupLayout;
		
	private string COUNTRYCODE = "countrycode";

	public void callGetRank()
	{
		StartCoroutine(GetRank());    
	}

	public void callPostScores(string name, int score)
	{
		StartCoroutine(PostScores(name, score));    
	}

	IEnumerator PostScores(string name, int score)
	{
		System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
		byte[] bytes = ue.GetBytes(name + score + secretKey);

		System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
		byte[] hash = md5.ComputeHash(bytes);
		
		string hashString = "";

		for (int i = 0; i < hash.Length; i++)
		{
			hashString += System.Convert.ToString(hash[i], 16).PadLeft(2, '0');
		}

		string hashString2 = hashString.PadLeft(32, '0');
		var countrycode = PlayerPrefs.GetString (COUNTRYCODE); //WWW.EscapeURL(name) 
		string post_url = addScoreURL + game + "?name=" + WWW.EscapeURL(name)  + "&score=" + score + "&hash=" + hashString2 + "&countrycode=" + countrycode;

		WWW hs_post = new WWW(post_url);
		yield return hs_post; // Wait until the download is done
		
		if (hs_post.error != null) {
			print ("There was an error posting the high score: " + hs_post.error);
		} else {
            Debug.Log("Loading MainMenu");
			SceneManager.LoadScene ("MainMenu");
		}
	}

	IEnumerator GetRank()
	{
		WWW hs_get = new WWW(highscoreURL);
		yield return hs_get;
		if (hs_get.error != null) {
			print ("There was an error getting the high score: " + hs_get.error);
		} else {
			for (int i = 0; i < groupLayout.transform.childCount; i++) {
				var child = groupLayout.transform.GetChild (i).gameObject;
				Destroy (child);
			}
			JSONNode jsonResult;
			jsonResult = JSON.Parse (hs_get.text);

			if (jsonResult.Count != 0) {
				foreach (JSONNode item in jsonResult.AsArray) {

					GameObject newRowRankScore = Instantiate (globalGameObject, transform.position, transform.rotation) as GameObject;


					Texture2D countryTexture;
					foreach (Transform child in newRowRankScore.transform) {
						if (child.tag == "RankName") {
							child.gameObject.GetComponent<Text> ().text = item [0].Value;
						}
						if (child.tag == "RankCountryFlag") {
                            countryTexture = Resources.Load ("Flags/" + item [2].Value) as Texture2D;
							child.gameObject.GetComponent<RawImage> ().texture = countryTexture;
						}
						if (child.tag == "RankScore") {
							child.gameObject.GetComponent<Text> ().text = item [1].Value;
						}
					}
					newRowRankScore.transform.SetParent (groupLayout.transform);
				}
			}
		}
	}
}