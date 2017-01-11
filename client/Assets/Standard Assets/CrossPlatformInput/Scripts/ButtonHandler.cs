using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace UnityStandardAssets.CrossPlatformInput
{
    public class ButtonHandler : MonoBehaviour
    {
		// Reference to projectile prefab to shoot
		public GameObject projectile;
		public float power = 10.0f;
		
		// Reference to AudioClip to play
		public GameObject shootSFX;
        public string Name;
		AudioSource audioSource; 
		public Text shotButton;

		void Start(){
			audioSource = shootSFX.GetComponents<AudioSource>()[0];
		}
        void OnEnable()
        {

        }

        public void SetDownState()
        {
			shotButton.color = Color.black;
            //CrossPlatformInputManager.SetButtonDown(Name);
			// Detect if fire button is pressed
			//if (CrossPlatformInputManager.GetButtonDown ("FireButton"))
			//{	
				// if projectile is specified
				if (projectile)
				{
					// Instantiante projectile at the camera + 1 meter forward with camera rotation
					GameObject newProjectile = Instantiate(projectile, transform.position + transform.forward, transform.rotation) as GameObject;
					
					// if the projectile does not have a rigidbody component, add one
					if (!newProjectile.GetComponent<Rigidbody>()) 
					{
						newProjectile.AddComponent<Rigidbody>();
					}
					// Apply force to the newProjectile's Rigidbody component if it has one
					newProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * power, ForceMode.VelocityChange);
					
					// play sound effect if set
				if (shootSFX)
					{
						if (newProjectile.GetComponent<AudioSource> ()) { // the projectile has an AudioSource component
							// play the sound clip through the AudioSource component on the gameobject.
							// note: The audio will travel with the gameobject.
							
							newProjectile.GetComponent<AudioSource> ().PlayOneShot (audioSource.clip);
						} else {
							// dynamically create a new gameObject with an AudioSource
							// this automatically destroys itself once the audio is done
						AudioSource.PlayClipAtPoint(audioSource.clip, newProjectile.transform.position);
						}
					}
				}
			}
        //}


        public void SetUpState()
        {
            CrossPlatformInputManager.SetButtonUp(Name);
			shotButton.color = new Color (1f,1f,1f,0.27f);
        }


        public void SetAxisPositiveState()
        {
            CrossPlatformInputManager.SetAxisPositive(Name);
        }


        public void SetAxisNeutralState()
        {
            CrossPlatformInputManager.SetAxisZero(Name);
        }


        public void SetAxisNegativeState()
        {
            CrossPlatformInputManager.SetAxisNegative(Name);
        }

        public void Update()
        {


        }
    }
}
