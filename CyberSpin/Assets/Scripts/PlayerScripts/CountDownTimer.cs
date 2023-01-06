using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountDownTimer : MonoBehaviour
{
    public static CountDownTimer instanceCountdownTimer;

    [SerializeField] private TMP_Text timertext;

    private TimeSpan timePlaying;
    private bool timerGoing;

    private float elapedTime;

    private void Awake()
    {
        instanceCountdownTimer = this;
    }

    private void Start()
    {
        timertext.text = "00:00.00";
        timerGoing = false;

        BeginTimer();
    }

    public void BeginTimer()
    {
        timerGoing = true;
        elapedTime = 0f;

        StartCoroutine(UpdateTimer());
    }

    public void EndTimer()
    {
        timerGoing = false;
    }

    private IEnumerator UpdateTimer()
    {
        while(timerGoing)
        {
            elapedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapedTime);
            string timePlayingStr = /*"Time: " +*/ timePlaying.ToString("mm':'ss'.'ff");
            timertext.text = timePlayingStr;

            yield return null;
        }
    }

}
