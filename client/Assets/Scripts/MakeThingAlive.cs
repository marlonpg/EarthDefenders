using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MakeThingAlive : MonoBehaviour {
	public bool isTimer;
	public float startValue;
	private float variableToIncrement;
	// Use this for initialization
	void Start () {
		variableToIncrement = startValue;
		if (!isTimer) {
			InvokeRepeating ("IncrementPoints", 0, 1.0F);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (isTimer) {
			if (variableToIncrement < 0) {
				variableToIncrement = startValue;
			}
			variableToIncrement -= Time.deltaTime;
			GetComponent<Text> ().text = variableToIncrement.ToString ("0.00");	
		}
	}

	void IncrementPoints(){
		variableToIncrement += Random.Range (1,100);
		GetComponent<Text> ().text = variableToIncrement.ToString ();
	}
}
