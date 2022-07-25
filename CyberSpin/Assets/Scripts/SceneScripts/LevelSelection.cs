using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelSelection : MonoBehaviour
{
    private string buttonName;
    private char levelNumber;
    private int selectedLevel;

    public void LoadLevel()
    {
        buttonName = EventSystem.current.currentSelectedGameObject.name;  //Check Name of level pressed
        levelNumber = buttonName[6];                                      //Checking Number from level string
        selectedLevel = levelNumber - '0';                                //String -> int

        LevelManager.currentLevel = selectedLevel;                        //Setting to Level to load
        LevelManager.hasToUpdate = true;                                  //Has to (?) update variables on level load

        SceneManager.LoadScene("Level 1");                                //Loading Levels Scene
    }

}
