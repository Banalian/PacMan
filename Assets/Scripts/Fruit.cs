using UnityEngine;

public class Fruit : MonoBehaviour
{
    public int points = 200;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            FindObjectOfType<GameManager>().FruitEaten(this);
        }
    }

}
