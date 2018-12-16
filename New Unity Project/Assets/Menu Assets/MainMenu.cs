using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{

    public GameObject optionsMenu;
    public GameObject mainMenu;

    public AudioMixer audioMixer;

    //start code
    public void startGame ()
    {
        SceneManager.LoadScene(1);
    }

    //options menu code
    public void openOptions ()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    //quit code
    public void quitGame ()
    {
        Application.Quit();
    }

    //all of this is options menu stuff

    //go back
    public void backOptions ()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    //full screen toggle
    public void toggleFullScreen ()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    //volume slider
    public void changeVolume (float volume)
    {
        audioMixer.SetFloat("PrimeVolume", volume);
    }
}
