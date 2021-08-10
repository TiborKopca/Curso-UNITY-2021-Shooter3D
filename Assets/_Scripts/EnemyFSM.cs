using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

[RequireComponent(typeof(Sight))]
public class EnemyFSM : MonoBehaviour
{
    //The base states that enemy can do
    public enum EnemyState { GoToBase, AttackBase, ChasePlayer, AttackPlayer}

    [Tooltip("ENUM states ")]
    public EnemyState currentState;         //this holds the ENUM valuated by the code

    private Sight _sight;                   //Sight - the other script that Enemy has, this contains sences 
    
    private Transform baseTransform;        //Position of the base

    public float baseAttackDistance, playerAttackDistance; //variables for calculate stuff

    private NavMeshAgent agent;             //For the Nav Mesh Collider

    private Animator animator;              //this we need to get the enemy shoot

    [Tooltip("The weapon from which the enemy | player can shoot.")]
    public Weapon weapon;
    
    
    private void Awake()
    {
        //We get the sense of the Enemy so the code can use it and decide
        _sight = GetComponent<Sight>();

        //position of the base
        //Localize object by name = GameObject.Find(), FindWithTag() is by TAG
        baseTransform = GameObject.FindWithTag("Base").transform;

        //We add to the script the component NavMeshAgent- but its in its parent!
        agent = GetComponentInParent<NavMeshAgent>();

        //this we need to get the enemy shoot
        animator = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.GoToBase:
                GoToBase();
                break;
            
            case EnemyState.AttackBase:
                AttackBase();
                break;
            
            case EnemyState.ChasePlayer:
                ChasePlayer();
                break;
            
            case EnemyState.AttackPlayer:
                AttackPlayer();
                break;

            default:
                //TODO: caso por defecto
            break;
        }
    }



    void GoToBase()
    {
        //print("Go To Base");

        //this we need to get the enemy shoot
        animator.SetBool("Shot Bullet Bool", false);

        //Shooting Animation stopped
        agent.isStopped = false;

        //Destination for the Nav Mesh
        agent.SetDestination(baseTransform.position);
        
        //No target in sight == CHASE PLAYER
        if (_sight.detectedTarget != null)
        {
            currentState = EnemyState.ChasePlayer;
        }

        //Base in in shooting range == ATTACK BASE
        float distanceToBase = Vector3.Distance(transform.position, baseTransform.position);
        if (distanceToBase < baseAttackDistance)
        {
            currentState = EnemyState.AttackBase;
        }
    }



    void AttackBase()
    {
        //print("Attack the Base");

        //Nav Mesh --> when they reach the base, they attack but they also STOP MOVING
        agent.isStopped = true;

        //This will tell the directions to the target
        LookAt(baseTransform.position);
        ShootTarget();
    }



    void ChasePlayer()
    {
        //print("Chase player");

        //this we need to get the enemy shoot
        animator.SetBool("Shot Bullet Bool", false);

        //No target detected == GO TO BASE
        if (_sight.detectedTarget == null)
        {
            currentState = EnemyState.GoToBase;
            //because if there is no target, we dont have its position, and there would be error?
            return;   //This prioritize the "go after base", skipped all the other code
        }

        //Nav Mesh
        agent.isStopped = false;
        agent.SetDestination(_sight.detectedTarget.transform.position);

        //Player is in shooting range == ATTACK PLAYER
        float distanceToPlayer = Vector3.Distance(transform.position,
            _sight.detectedTarget.transform.position);
        if (distanceToPlayer < playerAttackDistance)
        {
            currentState = EnemyState.AttackPlayer;
        }

    }




    void AttackPlayer()
    {
        //print("Attack the player");

        //no target detected == GO TO BASE
        if (_sight.detectedTarget == null)
        {
            currentState = EnemyState.GoToBase;
            return;
        }
        
        //Player is far, out of range == CHASE PLAYER
        float distanceToPlayer = Vector3.Distance(transform.position,
            _sight.detectedTarget.transform.position);
        //tolerance number to the end -- when the enemy stop attack
        if (distanceToPlayer > (playerAttackDistance * 1.2f))
        {
            currentState = EnemyState.ChasePlayer;
        }

        //Nav Mesh, enemy will stop and shoot player/base
        agent.isStopped = true;

        //This will tell the directions to the target
        LookAt(_sight.detectedTarget.transform.position);
        //self explanatory
        ShootTarget();



    }


    //the logic of shooting to player/base is very simillar to the player's
    void ShootTarget()
    {   
        //only if the function returns true the animator can visualize it
        //the property we pass in is the layer for enemy "Enemy Bullet" 
        //The second parameter is delay in s
        if (weapon.ShootBullet("Enemy Bullet", 0))
        {
            animator.SetBool("Shot Bullet Bool", true); //we NEED to put the False for the enemy STOP shooting at GoToBase/Chase player
        }
    }



    //The Enemy will look at the target WHEN STOPPED
    private void LookAt(Vector3 targetPos)
    {
        //angle to target
        var directionToLook = Vector3.Normalize(targetPos - transform.position);
        //when the target is at different height, the Enemy shouldnt rotate/pivot to the ground or up
        directionToLook.y = 0;

        //because this script is child, we need to tell the parent-the enemy where to look
        transform.parent.forward = directionToLook;
    }
    
    
    
    private void OnDrawGizmos()
    {
        //Player Attack Range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playerAttackDistance);

        //Base Attack Range    
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, baseAttackDistance);
    }
}