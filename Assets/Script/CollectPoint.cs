using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectPoint : MonoBehaviour
{
    public Rover rover = null;
    public float point = 100;
    public float pointPerSecond = 50;
    public GameObject emmiterObj = null;
    public GameObject collectingObj = null;
    private ParticleSystem emmiter = null;
    private ParticleSystem collecting = null;

    void Start()
    {
        emmiter = emmiterObj.GetComponent<ParticleSystem>();
        collecting = collectingObj.GetComponent<ParticleSystem>();
        CollectedManager.Instance.MaxCollects += this.point;
    }

    void Update()
    {
        if (rover == null && Rover.Instance != null)
        {
            rover = Rover.Instance;
        }
        if (rover == null)
        {
            return;
        }

        if (Vector3.Distance(this.transform.position, rover.transform.position) < GameManager.Instance.distancyToCollision)
        {
            if (emmiter.isPlaying)
            {
                emmiter.Stop();
            }
            if (!collecting.isPlaying)
            {
                collecting.Play();
            }

            if (point > 0)
            {
                point -= (pointPerSecond * Time.deltaTime);
                CollectedManager.Instance.AddCollected((pointPerSecond * Time.deltaTime));
            }
            else
            {
                GameObject.Destroy(this.gameObject);
            }
        }
        else
        {
            if (!emmiter.isPlaying)
            {
                emmiter.Play();
            }
            if (collecting.isPlaying)
            {
                collecting.Stop();
            }
        }
    }
}
