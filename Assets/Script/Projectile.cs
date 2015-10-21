using UnityEngine;
using System.Collections;
using System;

public class Projectile : MonoBehaviour
{
    public GameObject particle = null;
    public float velocity = 10f;
    public float distanceRate = 1000;
    private float sumDistances = 0;
    public bool targerRover = false;
    public GameObject audioExplosion = null;

    void Update()
    {
        if (Math.Abs(Terrain.activeTerrain.SampleHeight(this.transform.position) - this.transform.position.y) < 0.3)
        {
            this.Die();
        }
        if (sumDistances >= this.distanceRate)
        {
            this.Die();
        }
        Vector3 current = this.transform.position;
        this.transform.position += this.transform.forward * velocity * Time.deltaTime;
        sumDistances += Vector3.Distance(this.transform.position, current);
    }

    void LateUpdate()
    {
        if (!targerRover)
        {
            foreach (var enemy in Amenemy.Amenemys)
            {
                if (Vector3.Distance(this.transform.position, enemy.transform.position) < GameManager.Instance.distancyToCollision)
                {
                    this.Die();
                    Amenemy.KillAmenemy(enemy);
                }
            }
        }
        else
        {
            if (Rover.Instance != null)
            {
                if (Vector3.Distance(this.transform.position, Rover.Instance.transform.position) < GameManager.Instance.distancyToCollision)
                {
                    this.Die();
                    Rover.Instance.RemoveLife(10);
                }
            }
        }
    }

    protected void Die()
    {
        GameObject.Instantiate(particle, this.transform.position, this.transform.rotation);
        GameObject.Instantiate(audioExplosion, this.transform.position, this.transform.rotation);
        GameObject.Destroy(this.gameObject);
    }
}
