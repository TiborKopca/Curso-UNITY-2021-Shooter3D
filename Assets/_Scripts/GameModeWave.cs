using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //This handles scenes loading

public class GameModeWave : MonoBehaviour
{
    [SerializeField]
    private Life playerLife; //uses Class Life with its amount value

    [SerializeField]
    private Life baseLife;

    private void Start() {

        //when games starts, we start to check for WIN/LOSE conditions
        playerLife.onDeath.AddListener(CheckLoseCondition);
        baseLife.onDeath.AddListener(CheckLoseCondition);

        //AddListeners will stay on until the game ends => they check if something changed
        EnemyManager.SharedInstance.onEnemyChanged.AddListener(CheckWinCondition);
        WaveManager.SharedInstance.onWaveChanged.AddListener(CheckWinCondition);
    }

//PERDER
    void CheckLoseCondition(){
        if(playerLife.Amount <= 0 || baseLife.Amount <= 0){
            Debug.Log(message:"HEMOS PERDIDO!");

            RegisterScore();

            //LoadSceneMode.Single will close all the previos scenes and load this selected
            SceneManager.LoadScene("LoseScene", LoadSceneMode.Single);
        }
    }
//GANAR
    void CheckWinCondition(){
        if(EnemyManager.SharedInstance.EnemyCount <= 0 && WaveManager.SharedInstance.WavesCount <= 0){
            Debug.Log(message:"HEMOS GANADO!");
            //TODO> SET TIMEOUT 
            //new WaitForSeconds(5);
            
            //Invoke Time and Score Validation
            RegisterScore();
            RegisterTime();

            //second parameter can be ADDITIVE MODE(in addiction, we can another scene with it)
            SceneManager.LoadScene("WinScene", LoadSceneMode.Single); 
        }
    }

    void RegisterScore(){
        var actualScore = ScoreManager.SharedInstance.Amount;
        PlayerPrefs.SetInt("Last Score", actualScore);
        
        //the first time it is needed to set default value = 0
        var highScore = PlayerPrefs.GetInt("High Score", 0);
        if(actualScore > highScore){
            PlayerPrefs.SetInt("High Score", actualScore);
        }
    }

    void RegisterTime(){
        var actualTime = Time.time;
        PlayerPrefs.SetFloat("Last Time", actualTime);

        var lowTime = PlayerPrefs.GetFloat("Low Time", 999999999.0f);
        if(actualTime < lowTime){
            PlayerPrefs.SetFloat("Low Time", actualTime);
        }
    }

}
