using UnityEngine;

public class Fruit : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public int points = 200;
    public float respawnDelay = 10f;

    public Sprite[] availableFruits;

    public void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        ResetState();
    }

    public void Enable()
    {
        this.gameObject.SetActive(true);
        // choose random fruit in list
        int index = Random.Range(0, availableFruits.Length);
        this.spriteRenderer.sprite = availableFruits[index];
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            Invoke(nameof(Enable), respawnDelay);
            FindObjectOfType<GameManager>().FruitEaten(this);
        }
    }

    public void ResetState()
    {
        this.gameObject.SetActive(false);
        Invoke(nameof(Enable), respawnDelay);
    }
}
