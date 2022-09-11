
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacMan;
    public Transform pellets;
    public FruitSpawner fruitSpawner;
    public float[] speedModifierByRoundPacman;
    public float[] speedModifierByRoundPacmanPellet;
    public float[] speedModifierByRoundGhost;
    public int[] bonusPointByRound;
    public int ghostMultiplier { get; private set; } = 1;
    public int score { get; private set; }
    public int lives { get; private set; }
    public int nbRounds { get; private set; } = -1;


    private Label scoreLabel;
    private Label livesLabel;
    private Label roundLabel;

    public void OnEnable()
    {
        VisualElement document = GetComponentInChildren<UIDocument>().rootVisualElement;

        scoreLabel = document.Q<Label>("Score");
        livesLabel = document.Q<Label>("Lives");
        roundLabel = document.Q<Label>("Round");
    }


    private void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
        FindObjectOfType<EndUI>().GetComponent<UIDocument>().rootVisualElement.visible = false;
    }

    private void NewRound()
    {
        this.nbRounds++;
        this.roundLabel.text = $"Round : {this.nbRounds + 1}";
        SetScore(this.score + 
                 (this.nbRounds > this.bonusPointByRound.Length
                    ? this.bonusPointByRound.Last()
                    : this.bonusPointByRound[this.nbRounds]));
        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }

        ResetState();
        //Modify the speed of the pacman and ghost
        SetPacmanSpeed();
        for(int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].movement.speedMultiplier = this.nbRounds > this.speedModifierByRoundGhost.Length
                        ? this.speedModifierByRoundGhost.Last()
                        : this.speedModifierByRoundGhost[this.nbRounds];
        }
        
    }

    private void ResetState()
    {
        ResetGhostMultiplier();
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].ResetState();
        }

        this.pacMan.ResetState();
        this.fruitSpawner.ResetState();
    }

    private void GameOver()
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);
        }
        
        this.pacMan.gameObject.SetActive(false);
        FindObjectOfType<EndUI>().GetComponent<UIDocument>().rootVisualElement.visible = true;
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
        fruitSpawner.NewFruit();
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
        else
        {
            this.pacMan.Movement.speedMultiplier = this.nbRounds > this.speedModifierByRoundPacmanPellet.Length
                ? this.speedModifierByRoundPacmanPellet.Last()
                : this.speedModifierByRoundPacmanPellet[this.nbRounds];
            CancelInvoke(nameof(SetPacmanSpeed));
            Invoke(nameof(SetPacmanSpeed), 0.5f);
        }
        
        
    }

    public void PowerPelletEaten(PowerPellet powerPellet)
    {

        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].frightened.Enable(powerPellet.duration);
        }

        PelletEaten(powerPellet);

        CancelInvoke(nameof(ResetGhostMultiplier));
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

    private void SetPacmanSpeed()
    {
        this.pacMan.Movement.speedMultiplier = this.nbRounds > this.speedModifierByRoundPacman.Length
            ? this.speedModifierByRoundPacman.Last()
            : this.speedModifierByRoundPacman[this.nbRounds];
    }
}
