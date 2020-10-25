using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Standard", menuName = "Game/Difficulty", order = 1)]
public class Difficulty : ScriptableObject
{
    public List<Vector2> goalSize = new List<Vector2>();
    public List<Vector2> gbbSize = new List<Vector2>();
    public List<float> panDuration = new List<float>();
    [Tooltip("Raises the difficulty every time a multiple of this score is reached")]
    public int raiseDifficultyAt = 5;
    [Tooltip("Adds moving targets after reaching this score")]
    public int addPanAt = 5;

    public Vector2 GetGoalSize(int score)
    {
        int level = CalculateLevel(score); // 0 for the first 4 level, 1 for the next five, etc.

        if(level < goalSize.Count)
        {
            // Return relative to level
            return goalSize[level];
        }else
        {
            // Return the highest difficulty
            return goalSize[goalSize.Count - 1];
        }
    }

    public Vector2 GetGbbSize(int score)
    {
        int level = CalculateLevel(score); // 0 for the first 4 level, 1 for the next five, etc.

        if (level < gbbSize.Count)
        {
            // Return relative to level
            return gbbSize[level];
        }
        else
        {
            // Return the highest difficulty
            return gbbSize[goalSize.Count - 1];
        }
    }

    public float GetPanDuration(int score)
    {
        int level = CalculateLevel(score); // 0 for the first 4 level, 1 for the next five, etc.

        if (level < panDuration.Count)
        {
            // Return relative to level
            return panDuration[level];
        }
        else
        {
            // Return the highest difficulty
            return panDuration[goalSize.Count - 1];
        }
    }

    private int  CalculateLevel(int score)
    {
        return score / raiseDifficultyAt;
    }
}
