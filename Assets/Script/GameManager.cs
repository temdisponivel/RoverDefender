using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance = null;
    public float distancyToCollision = 1f;
    public CollectedManager collectedManager = null;
    public float distancyToFollow = 3f;
    public float distancyToAttack = 5f;
    public float TimeGamePlay { get; set; }
    
    void Start()
    {
        if (GameManager.Instance == null)
        {
            GameManager.Instance = this;
        }
        else
        {
            GameObject.Destroy(this.gameObject);
            return;
        }

        if (collectedManager == null && CollectedManager.Instance != null)
        {
            collectedManager = CollectedManager.Instance;
        }
    }

    void Update()
    {
        this.TimeGamePlay += Time.deltaTime;
    }

    public void StartGame()
    {
        this.collectedManager.MaxCollects = 0;
        this.collectedManager.Collected = 0;
    }



    public void FinishGame()
    {
        Application.LoadLevel("FinishGame");
    }

    public void GameOver()
    {
        Application.LoadLevel("GameOver");
    }
}
