using UnityEngine;
using System.Collections;

public class PauseManager : MonoBehaviour
{
    public void Continue()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }

    public void Exit()
    {
        Application.LoadLevel("Title");
    }
}
