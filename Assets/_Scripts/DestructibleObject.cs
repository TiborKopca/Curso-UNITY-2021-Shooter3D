using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    
    private void Awake() {
        var life = GetComponent<Life>();
        life.onDeath.AddListener(DestroyObject); 
    }

    private void DestroyObject() {  
    //Remove the listerer for not taking memory!
        var life = GetComponent<Life>();
        life.onDeath.RemoveListener(DestroyObject); 
         Invoke("PlayDestruction", 0.2f);

    //NOW WE DESTROY THE OBJECT, AFTER ALL CHANGES!
        Destroy(gameObject, 0.3f); //number is time in seconds
    }

    void PlayDestruction() {
        //this code will play FIRST animation that returns!! If the explosions is 2nd it wont work!
        ParticleSystem explosion = GetComponentInChildren<ParticleSystem>(); 
        explosion.Play();
    }
}
