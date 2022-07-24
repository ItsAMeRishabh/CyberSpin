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

    private void Start()
    {
        instanceLevelManager = this;
    }

    //Updating Pos of Player
    public void toNextPos()
    {
        rbChar.transform.position = levelPos[currentLevel - 1].position;
    }

}
