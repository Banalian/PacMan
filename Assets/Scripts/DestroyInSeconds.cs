using UnityEngine;

public class DestroyInSeconds : MonoBehaviour
{
    public int seconds = 5;
    void Start()
    {
        Destroy(gameObject, seconds);
    }
}
