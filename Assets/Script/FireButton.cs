using UnityEngine;
using System.Collections;

public class FireButton : MonoBehaviour
{
    private Vector3 initialScale = default(Vector3);

    void Start()
    {
        initialScale = this.transform.localScale;
    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x, initialScale.y * 0.1f, this.transform.localScale.z);
        }
        else
        {
            this.transform.localScale = initialScale;
        }
    }
}
