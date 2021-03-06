﻿using UnityEngine;
using System.Collections;

public class TargetBehavior : MonoBehaviour
{
	
	public int scoreAmountMin = 0;
	public int scoreAmountMax = 0;
	public float timeAmount = 0.0f;
	
	public GameObject explosionPrefab;
	
	void OnCollisionEnter (Collision newCollision)
	{
		if (GameManager.gm) {
			if (GameManager.gm.gameIsOver)
				return;
		}
		
		if (newCollision.gameObject.tag == "Projectile") {
			if (explosionPrefab) {
				Instantiate (explosionPrefab, transform.position, transform.rotation);
			}
			int scoreAmount = Random.Range (scoreAmountMin, scoreAmountMax);
			if (GameManager.gm) {
				GameManager.gm.targetHit (scoreAmount, timeAmount);
			}

			if(!this.gameObject.tag.Equals("TargetBulb")){
				// destroy the projectile
				Destroy (newCollision.gameObject);
				
				// destroy self
				Destroy (gameObject);
			}
		}
	}
}
