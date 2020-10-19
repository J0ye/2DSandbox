using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Target : MonoBehaviour
{
    public UnityEvent OnGoal;

    protected bool reached = false;

    void Start()
    {
        StartEvent(OnGoal);
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {  
        if(other.gameObject.GetComponent<PlayBall>() != null && !reached)
        {
            OnGoal.Invoke();
            reached = true;
        }
    }
    
    private void StartEvent(UnityEvent eve)
    {
        if (eve == null)
        {
            eve = new UnityEvent();
        }
    }
}
