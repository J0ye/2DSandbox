using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBall : Thing
{
    public override void ResetPosition()
    {
        transform.position = startPos;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0;
    }
}
