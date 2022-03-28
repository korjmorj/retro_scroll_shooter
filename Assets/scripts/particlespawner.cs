using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particlespawner : MonoBehaviour
{
    public GameObject particleResource;
 public int maxnumberOfParticles;
 public int numberOfParticles;


    public float cooldown;
    public float timer;
    public int round;
    float x_pos;

    
    float[] speedx;
    float[] speedy;
    void Start()
    {
        timer = cooldown;
        speedx = new float[numberOfParticles];
        speedy = new float[numberOfParticles];
        //Randomspeed();
    }
        
    // Update is called once per frame
    void Update()
    {
        if (round<=maxnumberOfParticles){
            numberOfParticles=round;
            speedx = new float[numberOfParticles];
            speedy = new float[numberOfParticles];
        }
        if (timer <= 0)
        {   round+=1;
            Randomspeedx();
            Randomspeedy();
            SpawnParticles();
            timer = cooldown-1;
            if (cooldown==1){
                timer=1;
            }

        }
        timer -= Time.deltaTime;

    }

    // Select a random rotation from min to max for each bullet

    public float[] Randomspeedx()
    {
        for (int i = 0; i < numberOfParticles; i++)
        {

            speedx[i] = Random.Range(-2, 4);
        }
        return speedx;
    }
    public float[] Randomspeedy()
    {
        for (int i = 0; i < numberOfParticles; i++)
        {

            speedy[i] = Random.Range(1, 4);
        }
        return speedy;

    }
        

    public GameObject[] SpawnParticles()
    {


        // Spawn Bullets
        GameObject[] spawnedParticles = new GameObject[numberOfParticles];
        for (int i = 0; i < numberOfParticles; i++)
        {
            spawnedParticles[i] = Instantiate(particleResource);
            
            var b = spawnedParticles[i].GetComponent<particlemove>();
            b.speedx= speedx[i];
            b.speedy = speedy[i];

 
        }
        return spawnedParticles;
    }
    public void newnumber()
    {
        numberOfParticles++;
    }

}