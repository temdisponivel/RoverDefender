using UnityEngine;
using System.Collections;

public class AudioPlayAndDestroy : MonoBehaviour
{
    void Start()
    {
        GameObject.Destroy(this.gameObject, this.GetComponent<AudioSource>().clip.length);
    }
}
