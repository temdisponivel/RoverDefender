using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CollectedManager : MonoBehaviour
{
    static public CollectedManager Instance = null;
    public float MaxCollects { get; set; }
    public float Collected { get; set; }
    private RectTransform rectTransform = null;
    
    public void Awake()
    {
        if (CollectedManager.Instance == null)
        {
            CollectedManager.Instance = this;
        }
        else
        {
            GameObject.Destroy(this.gameObject);
            return;
        }
        rectTransform = this.GetComponent<Image>().GetComponent<RectTransform>();
    }

    public void AddCollected(float value)
    {
        this.Collected += value;
        rectTransform.sizeDelta = rectTransform.sizeDelta + new Vector2(value, 0);

        if (MaxCollects <= this.Collected)
        {
            GameManager.Instance.FinishGame();
        }
    }
}
