using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTarget : Target
{
    public override void OnTriggerEnter2D(Collider2D other)
    {  
        if(other.gameObject.GetComponent<PlayBall>() != null && !reached)
        {
            OnGoal.Invoke();
            GameManager.Instance.player.GetComponent<ThingController>().bounds = transform.parent.gameObject.GetComponent<Collider2D>();
            GameManager.Instance.RaiseLevel();            
            reached = true;
        }
    }
}
