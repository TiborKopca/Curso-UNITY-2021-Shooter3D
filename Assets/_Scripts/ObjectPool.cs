using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    //to define this for it functions for all, it needs to be static
    //and be of the same Type as the Class
    public static ObjectPool SharedInstance; //SINGLETON, with value NULL

    //public GameObject prefab; //access to the bullet object --> this we add in the Prefab editor of the player
    [Tooltip("Add Bullet prefab here.")]
    public GameObject prefab;
    
    //list of our pooled objects
    public List<GameObject> pooledObjects; 

    //how many we want to pool
    public int amountToPool;                

    //Before the start we make sure that only 1 SharedInstance == our pool exists.
    private void Awake(){
        if(SharedInstance == null){ //the first existing pool
            SharedInstance = this;  
        }else{
            Debug.LogError(message: "Ya hay otro pool en pantalla.");
            Destroy(gameObject);        //if there are more objects with pool, destroy it
        }
    }

    private void Start() {
        pooledObjects = new List<GameObject>();     //inicializate List of objects
        GameObject tmp;                             //we create Object Temp
        for(int i = 0; i < amountToPool; i++){      //for everything we have set for pooled objects
            tmp = Instantiate(prefab);              //create that object in temporarily
            tmp.SetActive(false);                   //temp object will not be active
            pooledObjects.Add(tmp);                 //add temp object to the list of pooled objects
        }
    }

    //returns the first object that is available in pool
    public GameObject GetFirstPooledObject(){
        for(int i = 0; i < amountToPool; i++){  //for all objects in the pool 

            if(!pooledObjects[i].activeInHierarchy){    //take first object that is not active and return it
                return pooledObjects[i];
            }
        }
        return null;                        //if there is no free object to return, return NADA!
    } 
}
