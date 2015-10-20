using UnityEngine;
using System.Collections;

public class LifeBar : MonoBehaviour
{
    static public LifeBar Instance = null;
    public Light light = null;

    void Start()
    {
        LifeBar.Instance = this;
    }
}
