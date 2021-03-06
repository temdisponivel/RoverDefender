﻿using UnityEngine;
using System.Collections;
using System;

public class Rover : MonoBehaviour
{
    static public Rover Instance { get; set; }
    public GameObject projectile = null;
    public float life = 100f;
    protected float initialLife = 0f;
    protected Vector3 initialScale = default(Vector3);
    public GameObject lifeBar = null;
    public GameObject dirtyParticle = null;
    public float velocity = 5f;
    public float anglePerMove = 30f;
    public float coolDown = 0.3f;
    private ParticleSystem particleDirty = null;
    private float lastFireTime = 0;
    private AudioSource audioRobot = null;
    private Light lightVisibility = null;
    public bool Visible { get; set; }

    void Start()
    {
        Rover.Instance = this;
        initialLife = life;
        initialScale = lifeBar.transform.localScale;
        particleDirty = dirtyParticle.GetComponent<ParticleSystem>();
        audioRobot = this.GetComponent<AudioSource>();
        lightVisibility = this.GetComponent<Light>();
        Visible = lightVisibility.enabled;
    }
        
    void Update()
    {
        this.ValidateDeath();

        if (Input.GetButton("Fire1") && Time.realtimeSinceStartup - lastFireTime >= coolDown)
        {
            lastFireTime = Time.realtimeSinceStartup;
            GameObject.Instantiate(projectile, this.transform.position, this.transform.rotation);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            lightVisibility.enabled = !lightVisibility.enabled;
            Visible = lightVisibility.enabled;
        }
    }

    public void LateUpdate()
    {
        float multiplier = velocity * Time.deltaTime;
        float angleMultiplier = anglePerMove * Time.deltaTime;
        float horizontalMoviment = Input.GetAxis("Horizontal");
        float verticallMoviment = Input.GetAxis("Vertical");
        this.transform.Rotate(Vector3.up, horizontalMoviment * angleMultiplier * 4, Space.World);
        bool soundRobotPlay = horizontalMoviment != 0;

        if (verticallMoviment != 0 && Terrain.activeTerrain.SampleHeight((this.transform.position + (this.transform.forward * multiplier * verticallMoviment))) < 0.1)
        {
            this.transform.position += this.transform.forward * multiplier * verticallMoviment;
            
            if (!particleDirty.isPlaying)
            {
                particleDirty.Play();
            }

            soundRobotPlay = true;
        }
        else if (verticallMoviment == 0)
        {
            if (particleDirty.isPlaying)
            {
                particleDirty.Stop();
            }

            soundRobotPlay = soundRobotPlay || false;
        }

        if (soundRobotPlay)
        {
            if (!audioRobot.isPlaying)
            {
                audioRobot.Play();
            }
        }
        else
        {
            if (audioRobot.isPlaying)
            {
                audioRobot.Stop();
            }
        }
    }

   protected void ValidateDeath()
    {
       if (this.life <= 0)
       {
           GameManager.Instance.GameOver();
           GameObject.Destroy(this.gameObject);
       }
    }

    public void RemoveLife(float quantity)
    {
        if (this.life < 0)
        {
            return;
        }

        this.life -= quantity;
        float percentLife = this.life / 100;
        LifeBar.Instance.transform.localScale = new Vector3((this.initialScale.x * percentLife), LifeBar.Instance.transform.localScale.y, LifeBar.Instance.transform.localScale.z);
    }
}
