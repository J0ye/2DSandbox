using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Goal : MonoBehaviour
{
    public UnityEvent OnGoal;
    // Start is called before the first frame update
    void Start()
    {
        StartEvent(OnGoal);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<Thing>() != null)
        {
            other.gameObject.GetComponent<Thing>().ResetPosition();

            if(other.gameObject.GetComponent<PlayBall>() != null)
            {
                OnGoal.Invoke();
            }
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
