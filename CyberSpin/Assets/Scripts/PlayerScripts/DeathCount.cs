using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DeathCount : MonoBehaviour
{
    public static DeathCount instanceDeathCount;

    [SerializeField] private TMP_Text deathText;
    private int deathCounter;
    public int currentDeaths;

    private void Awake()
    {
        instanceDeathCount = this;
    }

    private void Start()
    {
        deathCounter = 0;
        currentDeaths = 0;

        deathText.text = "DEATHS: " + deathCounter;
    }

    public void UpdateDeaths()
    {
        deathCounter = currentDeaths;
        deathText.text = "DEATHS: " + deathCounter;
    }
}
