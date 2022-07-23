using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class EndLevelScript : MonoBehaviour
{
    public GameObject victoryScreen;

    private void OnTriggerEnter2D(Collider2D other) 
    {   
        if (other.gameObject.tag == "Player") 
        {
            victoryScreen.SetActive(true);
        }
    }
}

#region BackUpScript
//nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;    //1st

//Backup Script
/*private void NextUnlock()                                       //2nd
{
    if(SceneManager.GetActiveScene().buildIndex == 5)
        {
            Debug.Log("End of level");
        }
        else
        {
            SceneManager.LoadScene(nextSceneLoad);

            if(nextSceneLoad>PlayerPrefs.GetInt("levelAt"))
            {
                PlayerPrefs.SetInt("levelAt", nextSceneLoad);
            }
        }
}*/
#endregion