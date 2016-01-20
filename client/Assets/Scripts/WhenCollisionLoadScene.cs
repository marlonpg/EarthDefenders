using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class WhenCollisionLoadScene : MonoBehaviour {
	public string sceneToLoad;
	private Ray ray;//this will be the ray that we cast from our touch into the scene
	private RaycastHit rayHitInfo = new RaycastHit();//return the info of the object that was hit by the ray

	void OnCollisionEnter(Collision newCollision)
	{
		if (newCollision.gameObject.tag == "Projectile") {
			newCollision.gameObject.transform.DetachChildren();
			Destroy(newCollision.gameObject);
			SceneManager.LoadScene(sceneToLoad);
		}
		Debug.Log ("TEST");
	}

//	void OnMouseDown(){
//		Debug.Log ("TEST");
//		Debug.Log ("TOUCH TEST "+Input.touchCount);
//	}
	void Update(){
		foreach(Touch touch in Input.touches)
		{
			ray = Camera.main.ScreenPointToRay(touch.position);//creates ray from screen point position
			//if the ray hit something, only if it hit something
			if(Physics.Raycast(ray, out rayHitInfo))
			{
				//if the thing that was hit implements ITouchable3D
				//touchedObject = rayHitInfo.transform.gameObject;//(typeof(ITouchable3D)) as ITouchable3D;
				Debug.Log(rayHitInfo.transform.gameObject);
					//Debug.Log (touchedObject);
				//If the ray hit something and it implements ITouchable3D
				//if (touchedObject != null) {
				
				//}
			}
		}
	}
//
//	public void LoadScene(){
//		SceneManager.LoadScene(sceneToLoad);
//		Debug.Log ("TOUCH TEST "+Input.touchCount);
//	}
}
