using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance = null;
    public float distancyToCollision = 1f;
    public CollectedManager collectedManager = null;
    public float distancyToFollow = 3f;
    public float distancyToAttack = 5f;
    public float TimeGamePlay { get; set; }
    public GameObject pausePanel = null;
    public AudioSource audioBattle = null;
    public AudioSource audioEnviroment = null;
    private LinkedList<bool> inBattle = new LinkedList<bool>();
    
    void Awake()
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

        inBattle = new LinkedList<bool>();
    }

    void Update()
    {
        this.TimeGamePlay += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape) && pausePanel != null)
        {
            Time.timeScale = .00000001f;
            pausePanel.SetActive(true);
        }
    }

    public void StartGame()
    {
        Amenemy.Restart();
        this.collectedManager.MaxCollects = 0;
        this.collectedManager.Collected = 0;
        inBattle.Clear();
    }

    public void FinishGame()
    {
        Application.LoadLevel("FinishGame");
    }

    public void GameOver()
    {
        Application.LoadLevel("GameOver");
    }

    public void SetInBattle(bool battle)
    {
        if (battle)
        {
            inBattle.AddLast(battle);
        }
        else
        {
            inBattle.RemoveLast();
        }

        if (inBattle.Count > 0)
        {
            if (this.audioEnviroment.isPlaying)
            {
                this.audioEnviroment.Stop();
            }

            if (!this.audioBattle.isPlaying)
            {
                this.audioBattle.Play();
            }
        }
        else
        {
            if (this.audioBattle.isPlaying)
            {
                this.audioBattle.Stop();
            }

            if (!this.audioEnviroment.isPlaying)
            {
                this.audioEnviroment.Play();
            }
        }
    }
}