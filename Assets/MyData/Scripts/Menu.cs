using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Button startGameButton;
    public InputField s;
    public Slider slider;

    void Start()
    {
        startGameButton.onClick.AddListener(StartLevel);
        //s.onValueChanged.AddListener(SS);
        slider.onValueChanged.AddListener(SliderValue);
    }

    private void StartLevel()
    {
        SceneManager.LoadScene(1);
    }    

    public void QuitGame()
    {
        Application.Quit();
    }

    private void SliderValue(float val)
    {
        Settings.volume = val;
    }
}
