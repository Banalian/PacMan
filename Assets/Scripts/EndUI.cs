using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class EndUI : MonoBehaviour
{
    private Label scoreLabel;

    private Button restartButton;
    private Button mainMenuButton;
    private Button quitButton;

    public void Start()
    {
        UIDocument ui = GetComponent<UIDocument>();
        VisualElement document = ui.rootVisualElement;

        scoreLabel = document.Q<Label>("Score");

        restartButton = document.Q<Button>("Restart");
        mainMenuButton = document.Q<Button>("Menu");
        quitButton = document.Q<Button>("Quit");

        restartButton.clicked += () => FindObjectOfType<GameManager>().NewGame();
        mainMenuButton.clicked += () => SceneManager.LoadScene("StartMenu");
        quitButton.clicked += () => Application.Quit();

        document.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        scoreLabel.text = "Final Score: " + FindObjectOfType<GameManager>().score;
    }
}
