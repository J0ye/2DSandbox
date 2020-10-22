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
    [Tooltip("Determines how far left or right a gametarget can spawn on the X axis.")]
    public Vector2 targetRange = Vector2.zero;
    [Tooltip("This is added to the targets y size.")]
    public Vector2 targetIncrease = Vector2.one;
    public GameObject wallPrefab;

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

    public void RaiseLevel()
    {
        float cameraY = Camera.main.transform.position.y + 7; // Height camera will move to
        Camera.main.transform.DOMoveY(cameraY, 0.3f, true);
        GameObject nextEnv = Instantiate(wallPrefab, new Vector3(0, cameraY, 0), Quaternion.identity);
        SetUpNextEnviroment(nextEnv.GetComponent<GameEnviroment>());
        score++;
        scoreMesh.text = score.ToString();
        startedClimb = true;
    }

    private void SetUpNextEnviroment(GameEnviroment ge)
    {
        float newX = 0;
        if(targetRange.x < targetRange.y) newX = UnityEngine.Random.Range(targetRange.x, targetRange.y);
        if(targetRange.y < targetRange.x) newX = UnityEngine.Random.Range(targetRange.y, targetRange.x);
        Vector2 newSize = new Vector2(player.GetComponent<ThingController>().bounds.transform.lossyScale.x +targetIncrease.x , player.GetComponent<ThingController>().bounds.transform.lossyScale.y + targetIncrease.y);
        player.GetComponent<ThingController>().bounds.transform.DOScale(newSize, 0.5f);
        ge.gbb.transform.position = new Vector3(newX, ge.gbb.transform.position.y, 0);
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
        SceneManager.LoadScene(activeScene.name);
    }
}
