using UnityEngine;
using System.Collections;

public class Alien : MonoBehaviour
{
    public AudioPlayAndDestroy audioDiscovery = null;
    void Update()
    {
        if (Vector3.Distance(this.transform.position, Rover.Instance.transform.position) < GameManager.Instance.distancyToCollision)
        {
            CollectedManager.Instance.AlienDiscovered = true;
            GameObject.Instantiate(audioDiscovery);
        }
    }
}
