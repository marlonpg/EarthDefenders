using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using HideAndShowThings;

public class Scoreboard : MonoBehaviour {

	public Text globalScoreText;
	public Canvas scoreboardCanvas;

	public GameObject hideMenuObjects;


	void OnCollisionEnter(Collision newCollision)
	{
		if (newCollision.gameObject.tag == "Projectile") {
			PlayerPrefs.SetString ("whereIAm", "Scoreboard");
			newCollision.gameObject.transform.DetachChildren();
			Destroy(newCollision.gameObject);

			hideMenuObjects.gameObject.SetActive (false);

			scoreboardCanvas.gameObject.SetActive(true);
			scoreboardCanvas.gameObject.GetComponent<HSController> ().callGetRank();

		}
	}
}
