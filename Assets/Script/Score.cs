using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text text = null;

    void Start()
    {
        this.text = this.GetComponent<Text>();
        this.text.text += CollectedManager.Instance.Collected + " units of marsian mineral in: " + GameManager.Instance.TimeGamePlay + " seconds.";
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            Application.LoadLevel("Title");
        }
    }
}
