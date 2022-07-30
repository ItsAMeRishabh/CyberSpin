using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //Level Manager Instance
    public static LevelManager instanceLevelManager;

    //Char Variables
    [SerializeField] private Rigidbody2D rbChar;

    public Transform[] levelPos;
    public static int currentLevel;

    public static bool hasToUpdate;

    private void Awake()
    {
        instanceLevelManager = this;
    }

    //Updating Pos of Position
    public void toNextPos()
    {
        Debug.Log(currentLevel);
        rbChar.transform.position = levelPos[currentLevel - 1].position;
    }

    //Retry level
    public void RetryLevel()
    {
        CinemachineSwitch.instanceCineSwitch.victoryScreen.SetActive(false);
        ButtonScript.instanceButtonScript.isActivated = false;
        ButtonScript.instanceButtonScript.canActivate = false;
        toNextPos();
    }
    
}
