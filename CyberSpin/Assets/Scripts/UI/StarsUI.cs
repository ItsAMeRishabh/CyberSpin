using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsUI : MonoBehaviour
{
    public static StarsUI instanceStarsUI;

    [SerializeField] private GameObject star1;
    [SerializeField] private GameObject star2;
    [SerializeField] private GameObject star3;

    public int currentStars;

    private void Awake()
    {
        instanceStarsUI = this;
        currentStars = 0;
    }

    void Start()
    {
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
    }

    public void SetStars()
    {
        switch (currentStars)
        {
            case 0:
                star1.SetActive(false);
                star2.SetActive(false);
                star3.SetActive(false);
                break;

            case 1:
                star1.SetActive(true);
                star2.SetActive(false);
                star3.SetActive(false);
                break;

            case 2:
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(false);
                break;

            case 3:
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                break;
        }
    }
    
    public void ResetStars()
    {
        currentStars = 0;

        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
    }

    private void Update()
    {
        Debug.Log(currentStars);
    }
}
