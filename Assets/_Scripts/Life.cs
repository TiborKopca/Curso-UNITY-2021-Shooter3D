using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Life : MonoBehaviour
{
  public UnityEvent onDeath; //This is a reference to the Event and it can be reached by anyone

  [SerializeField]
  [Tooltip("This represents current Health Points, if enemy will shoot you, this will drop.")]
  private float amount;

  [Tooltip("Maximum possible HP, this will at start define current amount of HP.")]
  public float maximumLife = 100.0f; //this replace the 'amount' of life

  public float Amount{
    get => amount;
    set{
      amount = value;
      //code will run only when value will be changes|we set the new value of the 'Life'
      if (amount <= 0) 
      {
        //Destroy(gameObject); //OBSOLETE, this will destroy the object and we wont be able to play its particle effect animation
        onDeath.Invoke();  //we need to invoke function for call animation in it or something?
      }
    }
  }

  //we set the actual life to match the max life before start of the game
  private void Awake() {
    amount = maximumLife;  
  }
}
