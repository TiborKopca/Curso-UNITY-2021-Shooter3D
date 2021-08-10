using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]   //we require that if rigidbody is not set in the object, it will be added by UNITY

public class PlayerMovement : MonoBehaviour
{
    //F = m*a
    //[Tooltip("Velocity of the movement of the player in m/s.")] //OBSOLETE
    [Tooltip("Force of the movement of the player in N/s.")]
    [Range(0,10000)]
    public float speed = 1000; //default speed

    //[Tooltip("Velocity of the rotation of the player is in grades/sec")] //OBSOLETE
    [Tooltip("Force of the rotation of the player is in N/sec")]
    [Range(0,360)]
    public float rotationSpeed;

    private Rigidbody _rb;           //for adjusting of forces

    private Animator _animator;     //the animator properties to change

    void Start(){
        //HIDING THE CURSOR WHEN IN GAMEMODE
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        //caching the rigid body == instance only once!
        _rb = GetComponent<Rigidbody>(); //this gets to the Start(), NEVER NEVER to Update()!!!!
        _animator = GetComponent<Animator>(); 
    }




    void Update(){
        //this == this object on which is this script placed
        //  this.transform.Translate(x:0, y:0, z:0);
        //  this.transform.Translate(x:0, y:0, z:speed); 

        //S(Speed) = V(Velocity) * t(increment time)
        //delta time => time that passed from last frame, time is DYNAMIC!!
        //speed == m/s  OBSOLETE
        //incr S = V*incr t
        float space = speed * Time.deltaTime;  
        
    //CONTROLS 
        //KEYS W || UpArrow
        /*
        if((Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.UpArrow))){
            this.transform.Translate(x:0,y:0,z:space);  //Z axis will be modified upon keypress
        }
        if(Input.GetKey(KeyCode.S) || (Input.GetKey(KeyCode.DownArrow))){
             this.transform.Translate(0,0,-space);
        }
        if(Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.LeftArrow))){
             this.transform.Translate(-space,0,0);
        }
        if(Input.GetKey(KeyCode.D) || (Input.GetKey(KeyCode.RightArrow))){
             this.transform.Translate(space,0,0);
        }
        */

        //THESE ARE THE MOVEMENT SETTINGS VIA INPUT MANAGER DEFININTION OF KEYS
        float horizontal = Input.GetAxis("Horizontal");     //-1 to 1 
        float vertical = Input.GetAxis("Vertical");         //-1 to 1 
        //transform.Translate(x:speed*horizontal, y:0, z:speed*vertical); //OBSOLETE, we put it to normalization Vector

        //Normalization, Vector3.normalized Returns vector with a magnitude of 1, this is for not moving faster diagonally because of sums of vectors 
        Vector3 direction = new Vector3(x:horizontal, y:0, z:vertical);
        //new vector normalized, it is replaced by Translation force down below 
        //transform.Translate(translation: direction.normalized * space); //OBSOLETE by Translation forces
    
    //TRANSLATION FORCES
        _rb.AddRelativeForce(direction.normalized*space);    //in newtons!?



    //Rotation(grades/s) of the mouse| gamepad | joystick
        float angle = rotationSpeed * Time.deltaTime;
        
        //X - left right rotation
        float mouseX = Input.GetAxis("Mouse X"); //-1 to 1
        //Debug.Log("MouseX:" +mouseX);

        // transform.Rotate(xAngle:0, yAngle:mouseX*rotationSpeed, zAngle:0); //OBSOLETE
        //transform.Rotate(xAngle:0, yAngle:mouseX*angle, zAngle:0); //OBSOLETE by Torque force down bellow
    
    //ROTATION == TORQUE force
        _rb.AddRelativeTorque(0, y:mouseX*angle, 0);

        //Y - up down rotation
        // float mouseY = Input.GetAxis("Mouse Y");
        //Debug.Log("MouseY:" +mouseY);
        // transform.Rotate(xAngle:mouseY*angle, yAngle:0, zAngle:0);

    //ANIMATION DEPENDING ON PHYSICS
        //SetFloat - will set to the Float variable something that Rigid Body Physics have under its values as velocity
        _animator.SetFloat("Velocity", _rb.velocity.magnitude);
        //TODO: Animations velocities for Animator Blend tree for walking and running
        // _animator.SetFloat("Move X", horizontal);
        // _animator.SetFloat("Move Y", vertical);

        /*
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _animator.SetFloat("Velocity", _rb.velocity.magnitude);
        }
        else
        {
            if (Mathf.Abs(horizontal)<0.01f && Mathf.Abs(vertical)<0.01f)
            {
                _animator.SetFloat("Velocity", 0);
            }
            else
            {
                _animator.SetFloat("Velocity", 0.15f);
            }
        }
        */
    }
}
