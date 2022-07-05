using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    public int nextSceneLoad;
    public Button[] lvlButtons;

    // Start is called before the first frame update
    void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1; 
        int levelAt = PlayerPrefs.GetInt("levelAt", 2);
        Debug.Log(lvlButtons[0].name);
        for(int i = 0; i < lvlButtons.Length ; i++) 
        {
            if(i+2 > levelAt)
            {
                lvlButtons[i].interactable = false;
            }
        }
    }
}
