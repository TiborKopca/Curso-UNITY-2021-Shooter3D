using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnContact : MonoBehaviour
{
    public float damage; //the damage is set in UNITY
    
    //We need to check when the collider of other object is affected
    private void OnTriggerEnter(Collider other){

        //Debug.Log("Bullet collided with " +other.name);
        //Destroy(gameObject);          //Prohibido, we have object pooling
        gameObject.SetActive(false);    //Desactivate the bullet
        
        /*
        //the layer and tag is not the same, we should have TAG to the object i suppose
        if(other.CompareTag("Enemy") || other.CompareTag("Player")){      
            Destroy(other.gameObject); //destroys the other object(player or enemy)
        }
        */

        //Life has ammount, we access the other object's component of class Life
        Life life = other.GetComponent<Life>();
        if(life != null){ //if exists that component a.k.a doesnt return null
            life.Amount -= damage; //is the same as> life.amount = life.amount - damage;
        }
        //life ? life.Amount -= damage; //this is shortened
    }
}
