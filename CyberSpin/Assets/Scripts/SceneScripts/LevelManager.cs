using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instanceLevelManager;

    [SerializeField] private Rigidbody2D rbChar;

    public static int currentLevel;

    public static bool hasToUpdate;

    public Transform[] levelPos;

    private void Awake()
    {
        instanceLevelManager = this;
    }

    //Updating Pos of Player
    public void toNextPos()
    {
        Debug.Log(currentLevel);
        rbChar.transform.position = levelPos[currentLevel - 1].position;
    }

    public void RetryLevel()
    {
        CinemachineSwitch.instanceCineSwitch.victoryScreen.SetActive(false);
        ButtonScript.instanceButtonScript.isActivated = false;
        ButtonScript.instanceButtonScript.canActivate = false;
        toNextPos();
    }
    
}
