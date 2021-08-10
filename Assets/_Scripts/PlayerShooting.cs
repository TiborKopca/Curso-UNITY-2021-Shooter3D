using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PlayerShooting : MonoBehaviour
{
    //Obsolete by class Weapon
    // [Tooltip("Add PlayerShooting point from the prefab.")]
    // public GameObject ShootingPoint;

    private Animator _animator;     //UNITY animator manages the animations
    
    // public ParticleSystem fireEffect; //For the Shooting Gun effect //OBSOLETE HERE
    // public AudioSource shootSound;  //this holds the Shot sound audio //OBSOLETE HERE
    public AudioSource noAmmoSound;

    //Animation corrections, OBSOLETE by class Weapon
    // public float fireRate = 0.25f;   //animation duration from Start to End
    // private float lastShootTime;    //time from the last shot

    //Limited ammo
    public int bulletsAmount;

    public Weapon weapon;





    //We access to the all the animation before start of the game
    private void Awake() {
        _animator = GetComponent<Animator>();
    }



    void Update(){

    //If we press key shoot with mouse, it will create GameObject Pelota, which we set in Inspector for prefab Player
    //The second parameter will Not shoot when we are in pause menu(Time = 0)
       if(Input.GetKey(KeyCode.Mouse0) && Time.timeScale > 0){
            
            _animator.SetBool(name: "Shot Bullet Bool", true);
            //We dont create instance of the Animator Component, that is in Awake() only once.
            //_animator.SetTrigger(name: "Shot Bullet"); //we set trigger to animation change!!
            //we NEED to put the False for the enemy STOP shooting at GoToBase/Chase player

            //BULLETS MANAGEMENT AND INVOKING WEAPON TO SHOOT
            //THERE IS AMMO and The player shot the bullet, if player have bullets, it is able to fire weapon     
            if (bulletsAmount > 0){

                weapon.ShootBullet("Player Bullet", 0.01f);

                bulletsAmount--;            //substract the bullet shot from the pool
                if(bulletsAmount < 0){
                    bulletsAmount = 0;
                }   
                //Invoke(methodName: "FireBullet" ,time:0.2f); //OBSOLETE by class Weapon
            }else{
                Invoke(methodName: "UnableFire", time:0.3f);
            }
           
        }else{
            //We set the shooting animation to false => animation will go to Idle
            _animator.SetBool("Shot Bullet Bool", false);
        }
    }

    void UnableFire(){
        //Click Click sound
        Instantiate(noAmmoSound, transform.position, transform.rotation).GetComponent<AudioSource>().Play();
    }

//OBSOLETE
    // void FireBullet(){
    
    //Instantiate(GameObject, position, rotation) == we get those from the parent which is player who is shooting
           //Instantiate(prefab,parent:transform.position, parent:transform.rotation);  //this will instantiate too many bullets, its unusable

    //We create instance of GameObject == literaly GameObject bullet = Instantiate(prefab);
            //GameObject bullet = Instantiate(prefab); //OBSOLETE, we now use ObjectPool
            //GameObject bullet = GetFirstPooledObject(); //OBSOLETE, we now use SharedInstance SINGLETON

    //SharedInstance == static variable accessed by everything
    //We call object/class in ObjectPool.cs 

            
            //GameObject bullet = ObjectPool.SharedInstance.GetFirstPooledObject(); //OBSOLETE by class Weapon

    //For Layer Collision Matrix == we add layer mask to the object
            //bullet.layer = LayerMask.NameToLayer("Player Bullet"); //OBSOLETE by class Weapon

    //ShootingPoint is a point/empty object set on the Player gun at player prefab
            // bullet.transform.position = ShootingPoint.transform.position;  //OBSOLETE by class Weapon
            // bullet.transform.rotation = ShootingPoint.transform.rotation;  //OBSOLETE by class Weapon
            // bullet.SetActive(true);             //set the object to be active //OBSOLETE by class Weapon
            
    //This will play particle effect of the shooting gun, now in class Weapon
            // if(fireEffect != null){  //OBSOLETE by class Weapon
            //     fireEffect.Play();   //OBSOLETE by class Weapon
            // }                        
            
    //This plays the ShootSFX sound
            /* This is a trick that we can add sound to the bullet alone, so everytime the bullet is
            instanciated, it plays sound */
            //Instantiate(shootSound, transform.position, transform.rotation).GetComponent<AudioSource>().Play();  //OBSOLETE by class Weapon
            //shootSound.Play(); this doesnt work seem so 


           //Temporary Destroying bullets after time 2?
           //Destroy(bullet, t:2); //OBSOLETE
    //}
}