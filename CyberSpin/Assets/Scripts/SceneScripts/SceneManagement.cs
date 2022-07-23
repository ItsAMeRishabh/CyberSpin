using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagement : MonoBehaviour
{
  public Animator transition;

  public float transitionTime = 1f;

  public void ChangeScene(int sceneIndex)
  {
      StartCoroutine(LoadLevel(sceneIndex));
  }

  public void Quit()
  {
    Application.Quit();
  }

  IEnumerator LoadLevel(int sceneIndex)
  {
    transition.SetTrigger("Start");

    yield return new WaitForSeconds(transitionTime);

    SceneManager.LoadScene(sceneIndex);
  }

    
}
