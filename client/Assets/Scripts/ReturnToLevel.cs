using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
namespace ReturnToLevel {
	public class ReturnToLevel : MonoBehaviour {

		public string levelToReturn;
		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		public void Return(){
			SceneManager.LoadScene(levelToReturn);
		}
	}
}