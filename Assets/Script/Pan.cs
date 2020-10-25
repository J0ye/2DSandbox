using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Pan : MonoBehaviour
{
    public Vector2 target;
    public float duration = 1f;

    private Vector2 start;

    void Start()
    {
        start = transform.localPosition;
        transform.DOLocalMove(target, duration, false);
    }

    void Update()
    {
        if((Vector2)transform.localPosition == target)
        {
            transform.DOLocalMove(start, duration, false);
        } else if((Vector2)transform.localPosition == start)
        {
            transform.DOLocalMove(target, duration, false);
        }
    }
}
