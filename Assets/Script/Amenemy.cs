using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor;

public class Amenemy : MonoBehaviour
{
    static public LinkedList<Amenemy> Amenemys = new LinkedList<Amenemy>();
    static private LinkedList<Amenemy> AmenemyRemove = new LinkedList<Amenemy>();
    protected Rover rover = null;
    public GameObject dirtyParticle = null;
    public GameObject projectile = null;
    private ParticleSystem particleDirty = null;
    public float velocity = 2.5f;
    public float force = 1;
    private float lastFireTime = 0;
    public float coolDown = 0.3f;
    public bool battle = false;
    private AudioSource audioRobot = null;

    static public void Restart()
    {
        Amenemy.Amenemys.Clear();
        Amenemy.AmenemyRemove.Clear();
    }
    
    public void Awake()
    {
        Amenemy.Amenemys.AddLast(this);
        rover = Rover.Instance;
        particleDirty = dirtyParticle.GetComponent<ParticleSystem>();
        audioRobot = this.GetComponent<AudioSource>();
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

            if (audioRobot.isPlaying)
            {
                audioRobot.Stop();
            }

            if (!battle)
            {
                battle = true;
                GameManager.Instance.SetInBattle(battle);
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

            if (!battle)
            {
                battle = true;
                GameManager.Instance.SetInBattle(battle);
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

            if (!audioRobot.isPlaying)
            {
                audioRobot.Play();
            }

            if (!battle)
            {
                battle = true;
                GameManager.Instance.SetInBattle(battle);
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
        GameManager.Instance.SetInBattle(false);
    }
}
