using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Button startGameButton;
    public InputField s;

    void Start()
    {
        startGameButton.onClick.AddListener(StartLevel);
        s.onValueChanged.AddListener(SS);
    }

    private void StartLevel()
    {
        SceneManager.LoadScene(1);
    }    

    public void QuitGame()
    {
        Application.Quit();
    }

    private void SS(string str)
    {

    }
}
