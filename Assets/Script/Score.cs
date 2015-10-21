using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text text = null;

    void Start()
    {
        this.text = this.GetComponent<Text>();
        string message = "You have collect: " + CollectedManager.Instance.Collected + " amount of material. We already receive the information about it. \n";
        if (CollectedManager.Instance.AlienDiscovered)
        {
            message += "And you have confirmed the existence of extraterrestrial life. Mulder would be proud! \n";
        }
        else
        {
            message += "You have not found a alien. I guess Scully was right.";
        }
        this.text.text += message;
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            Application.LoadLevel("Title");
        }
    }
}
