using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject player;
    public TextMesh scoreMesh;
    public PlayBall playball;
    [Range(0.1f, 5f)]
    public float cameraTransitionDuration = 0.5f;

    [Header("Level Settings")]
    public Difficulty difficulty;
    [Tooltip("Determines how far left or right a gametarget can spawn on the X axis.")]
    public Vector2 targetRange = Vector2.zero;
    [Tooltip("This is added to the targets y size.")]
    public Vector2 targetIncrease = Vector2.one;
    [Tooltip("This is added to the size of the bounds the player is in.")]
    public Vector2 boundsIncrease = Vector2.one;
    [Range(0f, 5f)]
    public float animationDuration = 0.5f;
    [Tooltip("Randomises spined position between x and y")]
    public Vector2 flairStrength = Vector2.zero;
    [Range(0f, 5f)]
    public float flairDuration = 1f;
    public GameObject wallPrefab;

    [Header("Extra Settings")]
    public string scene;

    private Scene activeScene;
    private bool startedClimb = false;
    private int score = 0;

    void Start()
    {
        if(Instance) Destroy(this);
        if(!Instance) Instance = this;

        if(player == null)
        {
            if(GameObject.Find("Player")) player = GameObject.Find("Player");
            if(GameObject.FindWithTag("Player")) player = GameObject.FindWithTag("Player");
        }

        activeScene = SceneManager.GetActiveScene();
    }

    void LateUpdate()
    {
        if(!playball.gameObject.GetComponent<Renderer>().isVisible && startedClimb)
        {
            GameOver();
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(scene);
    }

    public void CallGameOver()
    {
        GameOver();
    }

    public void RaiseLevel()
    {
        float cameraY = Camera.main.transform.position.y + 7; // Height camera will move to
        Camera.main.transform.DOMoveY(cameraY, cameraTransitionDuration, false);
        GameObject nextEnv = Instantiate(wallPrefab, new Vector3(0, cameraY, 0), Quaternion.identity);
        SetUpNextEnviroment(nextEnv.GetComponent<GameEnviroment>());
        score++;
        scoreMesh.text = score.ToString();
        startedClimb = true;
    }

    private void SetUpNextEnviroment(GameEnviroment ge)
    {
        float newX = 0;
        //Randomise the position of the next gbb
        if(targetRange.x < targetRange.y) newX = UnityEngine.Random.Range(targetRange.x, targetRange.y);
        if(targetRange.y < targetRange.x) newX = UnityEngine.Random.Range(targetRange.y, targetRange.x);
        ge.gbb.transform.position = new Vector3(newX, ge.gbb.transform.position.y, 0);
        // Calculate game target size based on score and difficulty
        float xDiff = 1;
        float yDiff = 1;
        if(difficulty)
        {
            Vector2 diff = difficulty.GetGoalSize(score);
            xDiff = diff.x;
            yDiff = diff.y;
        }
        // Determine size of target
        Vector2 newSize = new Vector2((ge.gbb.transform.localScale.x + targetIncrease.x) * xDiff, 
                            (ge.gbb.transform.transform.localScale.y + targetIncrease.y) * yDiff);
        // Increase size of game target 
        ge.gbb.transform.localScale = newSize;
        // Add spin/flair
        float randZ = UnityEngine.Random.Range(flairStrength.x, flairStrength.y);
        Vector3 newRot = new Vector3(0, 0, randZ);
        ge.gbb.transform.DORotate(newRot, flairDuration, RotateMode.FastBeyond360);
        // Add Pan
        if(score >= difficulty.addPanAt)
        {
            Pan newPan = ge.gbb.gameObject.AddComponent<Pan>();
            newPan.target = new Vector2(-ge.gbb.transform.position.x, ge.gbb.transform.localPosition.y);
            newPan.duration = difficulty.GetPanDuration(score);
        }

        // Calculate players gbb size based on score and difficulty
        if(difficulty)
        {
            Vector2 diff = difficulty.GetGbbSize(score);
            xDiff = diff.x;
            yDiff = diff.y;
        }
        // Determine size of the players gbb
        newSize = new Vector2((player.GetComponent<ThingController>().bounds.transform.lossyScale.x + boundsIncrease.x) * xDiff, (player.GetComponent<ThingController>().bounds.transform.lossyScale.y + boundsIncrease.y) * yDiff);
        //Increase size of players gbb 
        player.GetComponent<ThingController>().bounds.transform.DOScale(newSize, animationDuration);
        if(player.GetComponent<ThingController>().bounds.GetComponent<Pan>())
        {
            Destroy(player.GetComponent<ThingController>().bounds.GetComponent<Pan>());
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
        SceneManager.LoadScene(activeScene.name);
    }
}
