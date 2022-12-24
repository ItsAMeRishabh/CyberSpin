using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelSelection : MonoBehaviour
{
    public Animator transition;

    public Animator musicAnim;

    public float transitionTime = 1f;

    private string buttonName;
    private char levelNumber;
    private int selectedLevel;

    public void ChangeScene(int sceneIndex)
    {
        StartCoroutine(LoadLevel(sceneIndex));
    }

    IEnumerator LoadLevel(int sceneIndex)
    {
        Time.timeScale = 1f;
        transition.SetTrigger("Start");

        musicAnim.SetTrigger("FadeOut");

        buttonName = EventSystem.current.currentSelectedGameObject.name;  //Check Name of level pressed
        levelNumber = buttonName[6];                                      //Checking Number from level string
        selectedLevel = levelNumber - '0';                                //String -> int

        LevelManager.currentLevel = selectedLevel;                        //Setting to Level to load
        LevelManager.hasToUpdate = true;    

        yield return new WaitForSeconds(transitionTime);                              //Has to (?) update variables on level load

        SceneManager.LoadScene(sceneIndex);                                //Loading Levels Scene
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
