using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    private Button startButton;
    private Button quitButton;

    private Label madeByLabel;

    public void Awake()
    {
        VisualElement document = GetComponentInChildren<UIDocument>().rootVisualElement;

        startButton = document.Q<Button>("Start");
        quitButton = document.Q<Button>("Quit");
        madeByLabel = document.Q<Label>("MadeBy");

        startButton.clicked += () => SceneManager.LoadScene("Pacman");
        quitButton.clicked += () => Application.Quit();

        madeByLabel.text = "Made by: \n" +
            "Lilian Pouvreau POUL06120000\n" +
            "Benoît Caudron CAUB22020107\n" +
            "Mathis Ronzon RONM16090000\n" +
            "Tristan Guichard GUIT13020005";
    }
}
