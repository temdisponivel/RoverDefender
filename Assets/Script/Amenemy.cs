using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor;

public class Amenemy : MonoBehaviour
{
    static public LinkedList<Amenemy> Amenemys { get; set; }
    static private LinkedList<Amenemy> AmenemyRemove { get; set; }
    protected Rover rover = null;
    public GameObject dirtyParticle = null;
    public GameObject projectile = null;
    private ParticleSystem particleDirty = null;
    public float velocity = 2.5f;
    public float force = 1;
    private float lastFireTime = 0;
    public float coolDown = 0.3f;
    
    public void Start()
    {
        if (Amenemy.Amenemys == null)
        {
            Amenemy.Amenemys = new LinkedList<Amenemy>();
            Amenemy.AmenemyRemove = new LinkedList<Amenemy>();
        }

        Amenemy.Amenemys.AddLast(this);
        rover = Rover.Instance;
        particleDirty = dirtyParticle.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (rover == null && Rover.Instance != null) 
        { 
            rover = Rover.Instance; 
        }
        
        if (Vector3.Distance(this.transform.position, rover.transform.position) <= GameManager.Instance.distancyToCollision)
        {
            this.transform.LookAt(rover.transform);
            if (Time.realtimeSinceStartup - lastFireTime >= coolDown / 2)
            {
                lastFireTime = Time.realtimeSinceStartup;
                rover.RemoveLife(this.force);
            }
            if (particleDirty.isPlaying)
            {
                particleDirty.Stop();
            }
        }
        else if (Vector3.Distance(this.transform.position, rover.transform.position) < GameManager.Instance.distancyToFollow)
        {
            this.transform.LookAt(rover.transform);
            this.transform.position = Vector3.Lerp(this.transform.position, rover.transform.position, (velocity * Time.deltaTime) / Vector3.Distance(this.transform.position, rover.transform.position));

            if (!particleDirty.isPlaying)
            {
                particleDirty.Play();
            }
        }
        else if (Vector3.Distance(this.transform.position, rover.transform.position) < GameManager.Instance.distancyToAttack)
        {
            this.transform.LookAt(rover.transform);
            if (Time.realtimeSinceStartup - lastFireTime >= coolDown)
            {
                lastFireTime = Time.realtimeSinceStartup;
                GameObject.Instantiate(projectile, this.transform.position, this.transform.rotation);
            }
        }

    }

    void LateUpdate()
    {
        foreach (var instance in Amenemy.AmenemyRemove)
        {
            Amenemy.Amenemys.Remove(instance);
            GameObject.Destroy(instance.gameObject);
        }
        Amenemy.AmenemyRemove.Clear();
    }

    static public void KillAmenemy(Amenemy instance)
    {
        Amenemy.AmenemyRemove.AddLast(instance);
    }
}
