using UnityEngine;
using System.Collections;

public class QuitGame : MonoBehaviour {

	void OnCollisionEnter(Collision newCollision)
	{
		if (newCollision.gameObject.tag == "Projectile") {
			
			newCollision.gameObject.transform.DetachChildren();
			Destroy(newCollision.gameObject);

			Application.Quit();
		}
	}
}
