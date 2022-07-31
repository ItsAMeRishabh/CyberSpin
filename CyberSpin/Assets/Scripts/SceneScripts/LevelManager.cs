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

    public bool lvl4CheckpointTaken;

    [SerializeField] private GameObject checkpointLevel4;

    private void Awake()
    {
        instanceLevelManager = this;
    }

    private void Start()
    {
        lvl4CheckpointTaken = false;
    }

    private void Update()
    {
        if(currentLevel == null)
        {
            toNextPos();
        }
    }


    //Updating Pos of Position
    public void toNextPos()
    {
        Debug.Log(currentLevel);
        ButtonScript.instanceButtonScript.isActivated = false;
        ButtonScript.instanceButtonScript.canActivate = false;
        ButtonScript.instanceButtonScript.countdownTimer = 0f;

        if (currentLevel == 4 && lvl4CheckpointTaken)
        {
            rbChar.transform.position = checkpointLevel4.transform.position;
        }
        else
        {
            rbChar.transform.position = levelPos[currentLevel - 1].position;
        }
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
