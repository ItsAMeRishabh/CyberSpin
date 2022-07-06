using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class EndLevelScript : MonoBehaviour
{
    public int nextSceneLoad;

   // public PlayableDirector pd;
    private void Start() 
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;    
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player") 
        {
           // pd.Play();
            
           if(SceneManager.GetActiveScene().buildIndex == 5)
            {
                //SceneManager.LoadScene(nextSceneLoad);
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
        }

    }
}