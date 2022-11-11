using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    public AudioMixer volMixer;

    public Slider volSlider;

    public TMP_Dropdown qualityDropDown;

    public TMP_Dropdown resolutionDropDown;

    public Toggle fullscreenToggle;

    private int screenInt;

    Resolution[] resolutions;

    private bool isFullScreen = false;

    const string prefName = "optionvalue";
    const string resName = "resolutionoption";

    private void Awake()
    {
        screenInt = PlayerPrefs.GetInt("togglestate");

        if(screenInt == 1)
        {
            isFullScreen = true;
            fullscreenToggle.isOn = true;
        }
        else
        {
            fullscreenToggle.isOn = false;
        }

        //Sets Playerprefs for Resolution Dropdown
        resolutionDropDown.onValueChanged.AddListener(new UnityAction<int>(index =>
        {
            PlayerPrefs.SetInt(resName, resolutionDropDown.value);
            PlayerPrefs.Save();
        }));

        //Sets Playerprefs for Quality Dropdown
        qualityDropDown.onValueChanged.AddListener(new UnityAction<int>(index =>
        {
            PlayerPrefs.SetInt(prefName, qualityDropDown.value);
            PlayerPrefs.Save();
        }));
    }

    private void Start()
    {
        //Volume Slider
        volSlider.value = PlayerPrefs.GetFloat("MVolume", 1f);
        volMixer.SetFloat("volume", PlayerPrefs.GetFloat("MVolume"));

        qualityDropDown.value = PlayerPrefs.GetInt(prefName, 4);

        resolutions = Screen.resolutions;

        resolutionDropDown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        //Displaying resolutions
        for(int i=0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = PlayerPrefs.GetInt(resName, currentResolutionIndex);
        resolutionDropDown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void ChangeVol(float volume)
    {
        PlayerPrefs.SetFloat("MVolume", volume);
        volMixer.SetFloat("volume", PlayerPrefs.GetFloat("MVolume"));
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;

        if (isFullScreen == false)
            PlayerPrefs.SetInt("togglestate", 0);
        else
        {
            isFullScreen = true;
            PlayerPrefs.SetInt("togglestate", 1);
        }
    }
}
