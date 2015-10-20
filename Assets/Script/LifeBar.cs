using UnityEngine;
using System.Collections;

public class LifeBar : MonoBehaviour
{
    static public LifeBar Instance = null;

    void Start()
    {
        LifeBar.Instance = this;
    }
}
