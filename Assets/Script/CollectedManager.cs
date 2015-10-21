using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CollectedManager : MonoBehaviour
{
    static public CollectedManager Instance = null;
    public float MaxCollects { get; set; }
    public float Collected = 0;
    private RectTransform rectTransform = null;
    public Image imagePoints = null;
    public bool AlienDiscovered { get; set; }
    
    public void Awake()
    {
        if (CollectedManager.Instance == null)
        {
            CollectedManager.Instance = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            GameObject.Destroy(this.gameObject);
            return;
        }
        if (imagePoints != null)
        {
            rectTransform = imagePoints.GetComponent<RectTransform>();
        }
    }

    public void AddCollected(float value)
    {
        this.Collected += value;
        rectTransform.sizeDelta = rectTransform.sizeDelta + new Vector2(value, 0);

        if (this.MaxCollects <= this.Collected)
        {
            GameManager.Instance.FinishGame();
        }
    }
}
