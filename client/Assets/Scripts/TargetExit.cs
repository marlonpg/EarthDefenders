using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class TargetExit : MonoBehaviour
{
	public float exitAfterSeconds = 10f;
	public float exitAnimationSeconds = 1f;

	private bool startDestroy = false;
	private float targetTime;

	void Start ()
	{
		targetTime = Time.time + exitAfterSeconds;
	}
	
	void Update ()
	{
		if (Time.time >= targetTime) {
			if (this.GetComponent<Animator> () == null)
				Destroy (gameObject);
			else if (!startDestroy) {
				startDestroy = true;

				this.GetComponent<Animator> ().SetTrigger ("Exit");

				Invoke ("KillTarget", exitAnimationSeconds);
			}
		}
		if (GameManager.gm.gameIsOver) {
			Destroy (gameObject);
		}
	}

	void KillTarget ()
	{
		Destroy (gameObject);
	}
}
