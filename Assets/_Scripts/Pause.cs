using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Pause : MonoBehaviour
{
    [Tooltip("Select the Canvas Pause Menu.")]
    public GameObject pauseMenu;    //Canvas Image

    [Tooltip("Select the Canvas Exit Button.")]
    public Button exitButton;       //we can have this to set it in PauseManager in Editor

    //[Tooltip("Select the Canvas Resume Button.")]
    //public Button resumeButton;     //If we use with Component directly added to the button, we dont need variable

    public AudioMixerSnapshot pauseSnp, gameSnp;

    private void Awake()
    {
        pauseMenu.SetActive(false);                 //we hide the Pause menu when the game is started!!
        exitButton.onClick.AddListener(ExitGame);   //Invoke ExitGame()
        //ResumeGame we did via Editor --> look in Inspector for onClick()
    }

    private void Update() {
        //On cancel pressed on gamepad? or Escape key on keyboard -- check input manager
        if(Input.GetKeyDown(KeyCode.Escape)){
            //UNBLOCK THE CURSOR!
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            //SHOW THE MENU to the screen
            pauseMenu.SetActive(true); 
            //TimeScale will make that the Time isnt flowing
            Time.timeScale = 0; 

            pauseSnp.TransitionTo(0.1f);  //transition to sound set on pauseSnp = pause snapshot
        }
    }


    //HIDE THE PAUSE MENU  again
    public void ResumeGame(){
        //HIDE THE MENU PAUSE
        pauseMenu.SetActive(false);
        Time.timeScale = 1;

        //BLOCK THE CURSOR!
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        gameSnp.TransitionTo(0.1f); //transition to sound
    }

    //EXIT GAME OPTION
    public void ExitGame(){
        print(message: "Quitting the game.");
        //Application.Quit();  //this finalizes the program, now this quits the UNITY.
    }
}
