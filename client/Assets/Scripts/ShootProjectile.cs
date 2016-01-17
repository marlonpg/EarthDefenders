using UnityEngine;
using System.Collections;

public class ShootProjectile : MonoBehaviour
{
    public GameObject projectile;
    public float power = 10.0f;

    public AudioClip shootSFX;

    public string Name;

    public void SetDownState()
    {

        if (projectile)
        {
            GameObject newProjectile = Instantiate(projectile, transform.position + transform.forward, transform.rotation) as GameObject;

            if (!newProjectile.GetComponent<Rigidbody>())
            {
                newProjectile.AddComponent<Rigidbody>();
            }

            newProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * power, ForceMode.VelocityChange);

            if (shootSFX)
            {
                if (newProjectile.GetComponent<AudioSource>())
                { 
                    newProjectile.GetComponent<AudioSource>().PlayOneShot(shootSFX);
                }
                else {
                    AudioSource.PlayClipAtPoint(shootSFX, newProjectile.transform.position);
                }
            }
        }
    }
}