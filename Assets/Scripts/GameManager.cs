using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacMan;
    public Transform pellets;
    public Fruit fruit;

    public int ghostMultiplier { get; private set; } = 1;
    public int score { get; private set; }
    public int lives { get; private set; }


    private Label scoreLabel;
    private Label livesLabel;

    public void OnEnable()
    {
        VisualElement document = GetComponentInChildren<UIDocument>().rootVisualElement;

        scoreLabel = document.Q<Label>("Score");
        livesLabel = document.Q<Label>("Lives");
    }


    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (this.lives <=0 && Input.anyKeyDown)
        {
            NewGame();
        }
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
        
    }

    private void NewRound()
    {
        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }

        ResetState();
    }

    private void ResetState()
    {
        ResetGhostMultiplier();
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].ResetState();
        }

        this.pacMan.ResetState();
        this.fruit.ResetState();
    }

    private void GameOver()
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);
        }

        this.pacMan.gameObject.SetActive(false);
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreLabel.text = $"Score : {this.score}";
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        livesLabel.text = $"Lives : {this.lives}";
    }

    public void GhostEaten(Ghost ghost)
    {
        int points = ghost.points * ghostMultiplier;
        SetScore(this.score + points);
        this.ghostMultiplier++;
    }

    public void FruitEaten(Fruit fruit)
    {
        SetScore(this.score + fruit.points);
        fruit.gameObject.SetActive(false);
        
    }

    public void PacManEaten()
    {
        this.pacMan.gameObject.SetActive(false);

        SetLives(this.lives - 1);

        if (this.lives > 0)
        {
            Invoke(nameof(ResetState), 3.0f);
        } else
        {
            GameOver();
        }
    }

    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);
        
        SetScore(this.score + pellet.points);

        if (!HasRemainingPellets())
        {
            this.pacMan.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3.0f);
        }
    }

    public void PowerPelletEaten(PowerPellet powerPellet)
    {

        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].frightened.Enable(powerPellet.duration);
        }

        PelletEaten(powerPellet);

        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), powerPellet.duration);
    }

    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }

        return false;
    }

    private void ResetGhostMultiplier()
    {
        this.ghostMultiplier = 1;
    }
}
