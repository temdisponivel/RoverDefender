using UnityEngine;
using System.Collections;

public class TitleMenuControl : MonoBehaviour
{
    private bool story = false;
    public GameObject panelStory = null;
    public GameObject blackBoard = null;

    public void PlayAction()
    {
        this.StartStoryTelling();
    }

    public void QuitAction()
    {
        Application.Quit();
    }

    void Update()
    {
        if (this.story && Input.anyKeyDown)
        {
            Application.LoadLevel("GamePlay");
            GameManager.Instance.StartGame();
        }
    }

    public void StartStoryTelling()
    {
        this.story = true;
        this.panelStory.SetActive(true);
        this.blackBoard.SetActive(true);
    }
}
