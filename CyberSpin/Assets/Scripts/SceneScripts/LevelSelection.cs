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
        buttonName = EventSystem.current.currentSelectedGameObject.name;
        levelNumber = buttonName[6];
        selectedLevel = levelNumber - '0';

        Debug.Log(selectedLevel);

        LevelManager.currentLevel = selectedLevel;
        LevelManager.hasToUpdate = true;

        SceneManager.LoadScene("Level 1");

    }


}
