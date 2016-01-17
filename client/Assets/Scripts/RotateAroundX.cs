using UnityEngine;
using System.Collections;

public class RotateAroundX : MonoBehaviour {

	public Transform target;

	void Start() {
		if (target == null) 
		{
			target = this.gameObject.transform;
		}
	}
	void Update () {
		transform.RotateAround(target.transform.position,target.transform.up,Random.Range (0, 101) * Time.deltaTime);
	}
}
