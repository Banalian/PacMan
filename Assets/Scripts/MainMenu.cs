using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    private Button startButton;
    private Button quitButton;

    public void Awake()
    {
        VisualElement document = GetComponentInChildren<UIDocument>().rootVisualElement;

        startButton = document.Q<Button>("Start");
        quitButton = document.Q<Button>("Quit");

        startButton.clicked += () => SceneManager.LoadScene("Pacman");
        quitButton.clicked += () => Application.Quit();
        
    }
}
