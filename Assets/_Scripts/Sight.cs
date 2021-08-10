using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    [Tooltip("Distance of the vision")]
    public float distance; //in sphere

    [Tooltip("Angle of the vision - vision cone.")]
    public float angle; 

    [Tooltip("Set target layers -- player, base, etc, They need to have a layer set.")]
    public LayerMask targetLayers; //this represents what objects AI can to detect(search for things)
    
    [Tooltip("Set Default - this is what the enemy wants to avoid.")]
    public LayerMask obstacleLayers; //this represents what obstacle AI can to detect(avoid things)

    [Tooltip("Here will show the target that was detected.")]
    public Collider detectedTarget; //this would hold the referece to the detected object

    private Collider[] colliders; //here we store collider data when each collision occurs


    private void Update() {
        //if there is no colliding of the objects, dont go to next code
        if(Physics.OverlapSphereNonAlloc(transform.position, distance, this.colliders, targetLayers) == 0){
            return;
        }
    //we create the sphere where are to be the sensors(vision of the enemy)
    //the enemy should have at least 1 collider to be detected! 
    //we store the result to the array
    //OverlapSphere is where the positions of the objects are crossed - 
    //defined by the positon, distance and what target should be resolved to collide
        colliders = Physics.OverlapSphere(transform.position, distance, targetLayers); 

        //this resets the variable which holds the last detected object
        /* Each frame (we are in update) will scan all its colliders, and all detected targets will be next
        frame set to null again. so the last target only will be current target */
        detectedTarget = null;

        //FILTER DISTANCE
        foreach (Collider collider in colliders){
        //this way we can obtain center of the pivot to the collider of the object that is overlapping with the collider
            //NOT normalized
            Vector3 directionToCollider = collider.bounds.center - transform.position;
            //Normalized -- will have longtitude 1
            directionToCollider = Vector3.Normalize(directionToCollider);

        //FILTER ANGLE (to target) 
            float angleToCollider = Vector3.Angle(transform.forward, directionToCollider);
            //Scalar angle formula(we are not using this) => cos(angle) = u.v / ||u||*||v|| 

            //DETECTION VALIDATION = The angle of vision we set to 45degrees
            if(angleToCollider < angle){
                //object inside of the angle cone of the vision == the enemy will detect target

                //FILTER CLEAR LINE OF SIGHT
                //Physics.Linecast = line to the target exists == there are no obstacles
                if(!Physics.Linecast(transform.position, collider.bounds.center, out RaycastHit hit, obstacleLayers)){
                   //Debug to see the GREEN line when enemy has free line of sight, the RaycastHit == null
                   Debug.DrawLine(transform.position, collider.bounds.center, Color.green);
                   //Reference to the object detected
                   detectedTarget = collider; 
                   break;

                }else{
                    //object outside of the angle cone of the vision, the RaycastHit != null
                    //We draw a line where exists a RaycastHit hit
                    //Debug to see the RED line when enemy has free line of sight
                    Debug.DrawLine(transform.position, hit.point, Color.red);
                }
            }
        }
    }

    //TO SHOW THE VISION CONE OF THE ENEMY
    private void OnDrawGizmos() {
        //We can draw different helping lines, spheres for us
        //First change color and then DRAW IT
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, distance);    


        //RIGHT RAY
        //Vectors that will be line drawn to the right and left
        //Quaternion is a matrix that can be multiplied by vectors
        Vector3 rightDir = Quaternion.Euler(0,angle,0)*transform.forward;
        //This will draw the line with the respect of the forward vector, to the distance
        Gizmos.DrawRay(transform.position, rightDir*distance);

        //LEFT RAY
        Vector3 leftDir = Quaternion.Euler(0,-angle,0)*transform.forward;
        Gizmos.DrawRay(transform.position, leftDir*distance);
    }
}
