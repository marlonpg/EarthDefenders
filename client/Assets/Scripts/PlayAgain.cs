using UnityEngine;
using System.Collections;

public class PlayAgain : MonoBehaviour {

	// respond on collisions
	void OnCollisionEnter(Collision newCollision)
	{
		// only do stuff if hit by a projectile
		if (newCollision.gameObject.tag == "Projectile") {
			PlayerPrefs.SetString ("whereIAm", "Playing");
			GameManager.gm.PlayAgain();
		}
	}
}
