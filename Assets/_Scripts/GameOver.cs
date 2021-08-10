using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //for having access to the scene management!
using TMPro;
using UnityEngine.UI;
using System.Text;                  //This is needed for StringBuilder!

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI actualScore, actualTime, bestScore, bestTime;
    public bool playerHasWon; //check this at GameScene Victory and left unchecked in GameScene Lose

    private void Start() {
        //Enable Cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;



        if (playerHasWon)
        {
            int score = PlayerPrefs.GetInt("Last Score");
            float time = PlayerPrefs.GetFloat("Last Time");
            
            actualScore.text = string.Format("Your Score: {0}", score);
            actualTime.text = string.Format("Your Time: {0}", time);
            
            //STRINGBUILDER
            StringBuilder builder = new StringBuilder();
            builder.Append("Best Score: ");                               //we add first line "Best:" to the String
            builder.Append(PlayerPrefs.GetInt("High Score"));       //2nd line
            bestScore.text = builder.ToString();                    //stringify the string inside builder
            
            //string s = "Mi" + "nombre" + "es" + "Juan" + "Gabriel";

            builder.Clear();                                        //Delete - clear the Stringbuilder
            builder.Append("Best Time: ");
            builder.Append(PlayerPrefs.GetFloat("Low Time"));
            bestTime.text = builder.ToString(); 
            
            print($"La partida ha terminado con {score} puntos en {time} segundos");

            string msg = $"Esto es un mensaje de {score} puntos";

        // actualScore.text = "Actual Score: "+PlayerPrefs.GetInt("Last Score");
        // actualTime.text = "Time: "+PlayerPrefs.GetFloat("Last Time");
        // bestScore.text = "Best Score: "+PlayerPrefs.GetInt("Best Score");
        // bestTime.text = "Best Time: "+PlayerPrefs.GetFloat("Low Time");
        }
    }
//PLAY AGAIN
    public void ReloadLevel(){
        SceneManager.LoadScene("Level1_2");
    }

//EXIT GAME OPTION
    public void ExitGame(){
        print(message: "Quitting the game.");
        //Application.Quit();  //this finalizes the program, now this quits the UNITY.
    }
}
