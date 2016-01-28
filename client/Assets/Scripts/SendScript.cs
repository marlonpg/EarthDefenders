using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Analytics;
using System.Collections.Generic;
using UnityEngine.Advertisements;

public class SendScript : MonoBehaviour
{


    public Canvas inputCanvas;
    public Text textName;
    public Text textScore;
	private bool sendScoreEnabled = true;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PostScore()
    {
		if (sendScoreEnabled) {
			sendScoreEnabled = false;
			Advertisement.Initialize ("1029906", true);

			//Example how to use Analytics
			//Analytics.Transaction("12345abcde", 0.99m, "USD", null, null);
			//Gender gender = Gender.Female;
			//Analytics.SetUserGender(gender);
			//int birthYear = 2014;
			//Analytics.SetUserBirthYear(birthYear);

			StartCoroutine (ShowAdWhenReady ());

			int totalPoints = int.Parse (textScore.text);
			string name = textName.text;
			Analytics.CustomEvent ("gameOver", new Dictionary<string, object> {
				{ "totalPoints", totalPoints },
				{ "name", name }
			});
			inputCanvas.gameObject.GetComponent<HSController> ().callPostScores (textName.text, int.Parse (textScore.text));
		}
    }
    IEnumerator ShowAdWhenReady()
    {
		while (!Advertisement.IsReady())
            yield return null;

		Advertisement.Show();
    }
}
