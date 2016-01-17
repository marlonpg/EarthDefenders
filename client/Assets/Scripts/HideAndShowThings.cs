using UnityEngine;
using System.Collections;

namespace HideAndShowThings{
	public class HideAndShowThings : MonoBehaviour {
		public GameObject canvas;
		public GameObject menuObject;
		public GameObject playAgain;
		public GameObject mainMenu;
		public GameObject quitGame;
		public GameObject crossHair;

		public void hideGameObject(){
			PlayerPrefs.SetString ("whereIAm", "Inicial");
			if (menuObject != null) {
				menuObject.SetActive (true);
			}
			if (playAgain != null) {
				playAgain.SetActive (true);
			}
			if (mainMenu != null) {
				mainMenu.SetActive (true);
			}
			if (quitGame != null) {
				quitGame.SetActive (true);
			}
			if (crossHair != null) {
				crossHair.SetActive (true);
			}
			canvas.SetActive (false);

		}
	}
}
