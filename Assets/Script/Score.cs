using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class Score : MonoBehaviour
{
    private TextMesh tex;
    private int score = 0;
    void Start()
    {
        tex = GetComponent<TextMesh>();
        tex.text = score.ToString();
    }

    public void RaiseScore()
    {
        score++;
        tex.text = score.ToString();
    }
}
