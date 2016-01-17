using UnityEngine;
using System.Collections;

public class RestartGame : MonoBehaviour {

	void OnCollisionEnter(Collision newCollision)
	{
		// only do stuff if hit by a projectile
		if (newCollision.gameObject.tag == "Projectile") {
			PlayerPrefs.SetString ("whereIAm", "Playing");

			newCollision.gameObject.transform.DetachChildren();
			Destroy(newCollision.gameObject);

			GameManager.gm.RestartGame();
		}
	}
}