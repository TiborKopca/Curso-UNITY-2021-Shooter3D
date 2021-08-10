using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [Tooltip("Speed of this object in m/s.")]
    public float speed;
   
    // Update is called once per frame
    void Update(){
        transform.Translate(x:0,y:0,z:speed*Time.deltaTime); //this moves the object from the player.
        //transform.position = transform.position + transform.forward * speed * Time.deltaTime;
    }
}
