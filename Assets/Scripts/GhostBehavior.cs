using UnityEngine;

[RequireComponent(typeof(Ghost))]
public abstract class GhostBehavior : MonoBehaviour
{

    public Ghost ghost { get; private set; }
    public float duration;

    public void Awake()
    {
        ghost = GetComponent<Ghost>();
    }

    public void Enable()
    {
        Enable(this.duration);
    }

    public virtual void Enable(float duration)
    {
        this.enabled = true;

        CancelInvoke();
        if(duration != -1f)
        {
            Invoke(nameof(Disable), duration);
        }
    }

    public virtual void Disable()
    {
        this.enabled = false;
        CancelInvoke();
    }

}
