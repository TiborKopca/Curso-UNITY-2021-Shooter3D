using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager SharedInstance; //singleton, this is shared across the project

    //AMOUNT WILL HOLD THE POINTS - SCORE THAT WILL SHOW ON THE DISPLAY
    [SerializeField]
    [Tooltip("Candidad de puntos de la partida actual.")]
    private int amount;

    public int Amount{
        get => amount;
        set => amount = value;
    }
    


    private void Awake(){
        if(SharedInstance == null){
            SharedInstance = this;
        }else{
            Debug.LogWarning(message: "ScoreManager duplicado, debe ser destruido.", gameObject);
            Destroy(gameObject);
        }
    }
}
