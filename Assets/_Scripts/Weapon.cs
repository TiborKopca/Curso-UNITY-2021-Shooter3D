
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //variable to hold time when the shot occured last time
    private float lastShootTime;

    //how fast the weapon can fire
    public float shootRate;
    //physical object from where is weapon shooting

    public AudioSource shootSound;  //this holds the Shot sound audio
    public AudioSource noAmmoSound;

    public GameObject shootingPoint;
    public ParticleSystem fireEffect;   //For the Shooting Gun effect


    //this can be "Enemy Bullet" or "Player Bullet"
    private string layerName;
    

    //This Class exist because there is the same logic used for Player and Enemy shooting
    //Returns true/false
    //Delay we set when invoking function, because the animation needs a bit time
    public bool ShootBullet(string layerName, float delay)
    {
        //enemy can shoot only if NOT in pause, time==normal
        if (Time.timeScale > 0)
        {
            //the same as in player = if the last shot is less than rate of fire == wont shoot
            //This class is the only place where this code appears now!
            //TO HOLD THE FIRE/STOP SHOOTING WHEN THE PAUSE IS PRESSED
            /* time from the last shot, this means that if the time that passed from last shot
                is less than 0.5s as defined in firerate, there will be no fire animation - the function
                will end */
            var timeSinceLastShoot = Time.time - lastShootTime;
            if (timeSinceLastShoot < shootRate)
            {
                return false;
            } 
            //last shot is done NOW
            lastShootTime = Time.time;

            //ASIGN the bullet is Enemy Bullet
            //Override of the variables
            this.layerName = layerName;
            Invoke("FireBullet", delay);

            return true;
        }
        //here false, because if timescale is !> 0, we are in pause and nobody cant shoot
        return false;
    }

    //Handles setting the Layer(enemy|player), SFX and spawning from the pool
    void FireBullet(){
            
        var bullet = ObjectPool.SharedInstance.GetFirstPooledObject();
        bullet.layer = LayerMask.NameToLayer(layerName); //depends who is shooting
        
        //this will set the bullet shooting point to the point shooting point on the prefab
        bullet.transform.position = shootingPoint.transform.position;
        bullet.transform.rotation = shootingPoint.transform.rotation;
        bullet.SetActive(true);

        //This will play particle effect of the shooting gun, now in class Weapon
        if (fireEffect!=null){
            fireEffect.Play();
        }

        if (shootSound != null)
        {
            //This plays the ShootSFX sound
            /* This is a trick that we can add sound to the bullet alone, so everytime the bullet is
            instanciated, it plays sound */
            Instantiate(shootSound, transform.position, transform.rotation).GetComponent<AudioSource>().Play();
            //shootSound.Play(); this doesnt work seem so 
        }
        
    }
}