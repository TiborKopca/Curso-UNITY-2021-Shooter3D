using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Tooltip("Candidad de puntos que se obtienen al derrotar al enemigo.")]
    public int pointsAmount = 10;
   




    private void Awake() {
        //before start we load Life Component of the enemy to open a EventListener on it
        var life = GetComponent<Life>(); //var is a local variable here
        //we set listener which will obtain points of enemy?
        life.onDeath.AddListener(DestroyEnemy); 
    }

    private void Start() {
        //EnemyManager prepares condition(creates list of enemies) for this to happen
        EnemyManager.SharedInstance.AddEnemy(this); 
    }



    private void DestroyEnemy() {  //we changed name from OnDestroy()

        //Obtain animator component and play the animation that is set by trigger
        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("Play Die");
        
        Invoke("PlayDestruction", 0.2f);
        
        var life = GetComponent<Life>();

    //Remove the listerer for not taking memory!
        life.onDeath.RemoveListener(DestroyEnemy); 

    //ENEMY MANAGER REMOVING ENEMIES
        EnemyManager.SharedInstance.RemoveEnemy(this); //we remove the enemy from the list of enemies
    
    //SCORE MANAGER POINTS ADDING - increment of the points    
        ScoreManager.SharedInstance.Amount += pointsAmount; //Amount because we have in SharedInstance static variable "Amount"

    //NOW WE DESTROY THE OBJECT, AFTER ALL CHANGES!
        Destroy(gameObject, 1); //number is time in seconds
    }

    //we "search" for the child component of type particle, 
    //if there will be more, we add them to array and we would need to filter
    void PlayDestruction() {
        //this code will play FIRST animation that returns!! If the explosions is 2nd it wont work!
        ParticleSystem explosion = GetComponentInChildren<ParticleSystem>(); 
        explosion.Play();
    }
}
