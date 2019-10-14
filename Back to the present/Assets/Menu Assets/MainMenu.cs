using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;

public class MainMenu : MonoBehaviour
{

    public GameObject optionsMenu;
    public GameObject mainMenu;
    public GameObject creditsMenu;

    public AudioMixer audioMixer;
    public Toggle FullscreenToggle;
    public Toggle MuteAudioToggle;
    public TMP_Dropdown ScreenResolutionOptions;
    public Slider VolumeSlider;
    public TMP_InputField VolumeInputField;

    //start code
    public void StartGame ()
    {
        SceneManager.LoadScene(1);
    }

    //options menu code
    public void OpenOptions ()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
        ScreenResolutionOptions.options.Clear();
        foreach (Resolution resolution in Screen.resolutions)
        {
            ScreenResolutionOptions.options.Add(new TMP_Dropdown.OptionData(resolution.ToString()));
        }
        ScreenResolutionOptions.captionText.text = ScreenResolutionOptions.options[ScreenResolutionOptions.value].text;
    }

    //quit code
    public void QuitGame ()
    {
        Application.Quit();
    }

    //all of this is options menu stuff

    //go back
    public void BackOptions ()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    //full screen toggle
    public void ToggleFullScreen ()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
    //volume slider
    //public void 
    public void ChangeVolume(string volume)
    {
        var amount = float.Parse(volume);
        amount = Mathf.Clamp(amount, 0f, 100f);
        VolumeSlider.value = amount;
        ChangeVolume(amount);
    }
    public void ChangeVolume (float volume)
    {
        if (!MuteAudioToggle.isOn) AudioListener.volume = volume/100;
        VolumeInputField.text = Mathf.RoundToInt(volume).ToString();
    }
    public void MuteVolume(bool mute)
    {
        AudioListener.volume = mute ? 0 : VolumeSlider.value / 100;
    }
    public void RollCredits()
    {
        creditsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }
    public void BackCredits()
    {
        creditsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void SetResolution(int index)
    {
        Resolution desiredResolution = Screen.resolutions[index];
        Screen.SetResolution(desiredResolution.width, desiredResolution.height, FullscreenToggle.isOn, desiredResolution.refreshRate);
    }
}
