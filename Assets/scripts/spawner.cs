using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Yarn.Unity;

namespace Yarn.Unity.Example{
public class spawner : MonoBehaviour
{   
    public BulletSpawnedData[] spawnData;
    public int index=0;
    BulletSpawnedData GetSpawnData(){
        return spawnData[index];
    }

    float timer;
    float lifetime;
    public bool isSeqRandom;
    public bool auto;
    float angle = 0;
    float timer_for_data;
    public bool ready_to_shoot=true;
    float [] rotations;
    public bool rest=true;
    public Vector3 [] spawnersCoords;
    public bool move_again=false;

    void Start()
    {

        lifetime= GetSpawnData().lifetime;
        timer = GetSpawnData().cooldown;
        timer_for_data = GetSpawnData().timer_for_level;
        rotations = new float[GetSpawnData().numberOfBullets];
        Array.Resize(ref rotations, GetSpawnData().numberOfBullets);
    }

    // Update is called once per frame
    void Update()
    {
    if(GetSpawnData().rest==true){rest=true;}
    else{rest=false;}
    if(GetSpawnData().is_moving_again==true){this.gameObject.GetComponent<StationMove>().pointA=this.gameObject.transform.position;
        this.gameObject.GetComponent<StationMove>().speed=GetSpawnData().spawner_speed;
        this.gameObject.GetComponent<StationMove>().pointB=GetSpawnData().spawnersCoords[0];
        StationMove moving = this.gameObject.GetComponent<StationMove>();
        moving.GetStart();
        }


    
    
    if (auto)
        {
        if (timer <= 0)
        {
            
            if ((ready_to_shoot==true)&(rest==false)){
                if(GetSpawnData().create_spawners==false){
                    Array.Resize(ref rotations, GetSpawnData().numberOfBullets);
                    SpawnBullets();}
                else{SpawnSpawners();}
                if(GetSpawnData().destroytourself==true){
                    if (lifetime<=0){Destroy(this.gameObject);}
                    }
                timer = GetSpawnData().cooldown;
            
            }
        if ((ready_to_shoot==true)&(timer_for_data<=0)){
        if (isSeqRandom){
            index=UnityEngine.Random.Range(0, spawnData.Length);
            timer_for_data = GetSpawnData().timer_for_level;
            }
        else{
            index+=1;
            if (index>= spawnData.Length){
                index=0;
                timer_for_data = GetSpawnData().timer_for_level;
                }
            timer_for_data = GetSpawnData().timer_for_level; 
            }
            Array.Resize(ref rotations, GetSpawnData().numberOfBullets);
            }


        }
        }
        timer -= Time.deltaTime;
        timer_for_data-=Time.deltaTime;
        lifetime -= Time.deltaTime;
    }

        


    public float[] RandomRotations()
    {
        for (int i = 0; i < GetSpawnData().numberOfBullets; i++)
        {
            rotations[i] = UnityEngine.Random.Range(GetSpawnData().minRotation, GetSpawnData().maxRotation);
        }
        return rotations;

    }
  
    public float[] DistributedRotations()
    {
        for (int i = 0; i < GetSpawnData().numberOfBullets; i++)
        {
            var fraction = (float)i / ((float)GetSpawnData().numberOfBullets - 1);
            var difference = GetSpawnData().maxRotation - GetSpawnData().minRotation;
            var fractionOfDifference = fraction * difference;
            rotations[i] = fractionOfDifference + GetSpawnData().minRotation; 
        }
        return rotations;
    }
    public GameObject[] SpawnBullets()
    {
        if (GetSpawnData().isDistrib)
        {
            DistributedRotations();
        }
        if (GetSpawnData().isRandom)
        {
            RandomRotations();
        }
        if (GetSpawnData().spiral)
        {
            SpiralRotations();
        }

        if (GetSpawnData().loh)
        {
            LOHRotations();
        }


        GameObject[] spawnedBullets = new GameObject[GetSpawnData().numberOfBullets];
        for (int i = 0; i < GetSpawnData().numberOfBullets; i+=1)
        {
            spawnedBullets[i] = Instantiate(GetSpawnData().bulletResource, this.transform.position, this.transform.rotation);
            this.gameObject.transform.DetachChildren();
            spawnedBullets[i].transform.localScale = new Vector3(0, 0, 0);
            spawnedBullets[i].transform.localScale+=GetSpawnData().bullet_size;
            if (this.gameObject.transform.position.y>0){spawnedBullets[i].transform.position -= new Vector3 (0, this.gameObject.GetComponent<Renderer>().bounds.min.y/2, 0);}
            else {spawnedBullets[i].transform.position += new Vector3 (0, this.gameObject.GetComponent<Renderer>().bounds.min.y/2, 0);}
            if (this.gameObject.transform.position.y==0){spawnedBullets[i].transform.position += new Vector3 (0, this.gameObject.GetComponent<Renderer>().bounds.min.y/2, 0);}
            //spawnedBullets[i].transform.position = this.gameObject.GetComponent<Renderer>().bounds.center;
            //spawnedBullets[i].transform.position -= new Vector3 (0, this.gameObject.GetComponent<Renderer>().bounds.center.y/2, 0);
            var s = spawnedBullets[i].GetComponent<SpriteRenderer>();
            s.sprite = GetSpawnData().bullet_sprite;
            spawnedBullets[i].GetComponent<EnergyBullet>().invert=GetSpawnData().invert;
            spawnedBullets[i].GetComponent<EnergyBullet>().slow_down=GetSpawnData().slow_down;
            spawnedBullets[i].GetComponent<EnergyBullet>().random=GetSpawnData().random;
            var b = spawnedBullets[i].GetComponent<EnergyBullet>();
            //b.invert=GetSpawnData().invert;
            b.rotation = rotations[i];
            b.speed = GetSpawnData().bulletSpeed;
            b.velocity = GetSpawnData().bulletVelocity;

            


            
        }
        Array.Resize(ref rotations, GetSpawnData().numberOfBullets);
        return spawnedBullets;
    }
    public GameObject[] SpawnSpawners()
    { GameObject[] spawnedSpawners = new GameObject[GetSpawnData().spawnerResource.Length];

        //GameObject[] spawnedSpawners = new GameObject[GetSpawnData().spawnerResource.Length];
        for (int i = 0; i < spawnedSpawners.Length; i+=1)
        {
            spawnedSpawners[i] = Instantiate(GetSpawnData().spawnerResource[i], transform);
            this.gameObject.transform.DetachChildren();
            spawnedSpawners[i].transform.localScale = new Vector3(0, 0, 0);
            spawnedSpawners[i].transform.localScale+=GetSpawnData().spawner_size;
            spawnedSpawners[i].GetComponent<spawner>().ready_to_shoot=GetSpawnData().ready_to_shoot;
            spawnedSpawners[i].GetComponent<spawner>().auto=true;
            spawnedSpawners[i].GetComponent<spawner>().rest=false;
            spawnedSpawners[i].GetComponent<StationMove>().pointA=this.transform.position;
            spawnedSpawners[i].GetComponent<StationMove>().pointB=GetSpawnData().spawnersCoords[i];
            spawnedSpawners[i].GetComponent<StationMove>().spawner=true;
            spawnedSpawners[i].GetComponent<StationMove>().speed=GetSpawnData().spawner_speed;
            this.gameObject.transform.DetachChildren();
            //var b = spawnedBullets[i].GetComponent<EnergyBullet>();
            //b.invert=GetSpawnData().invert;
            //b.rotation = rotations[i];
            //b.speed = GetSpawnData().bulletSpeed;
            //b.velocity = GetSpawnData().bulletVelocity;
        }
        //if (GetSpawnData().circle_spawn==true){return spawnedSpawners, pointBcreated;}
        return spawnedSpawners;
    }

    public Vector3 [] SpawnInCircle(){
        spawnersCoords =new Vector3[GetSpawnData().numberOfBullets];
        for (int i = 0; i < GetSpawnData().numberOfBullets; i++)
        {
            var fraction = (float)i / ((float)GetSpawnData().numberOfBullets - 1);
            var difference = GetSpawnData().maxRotation - GetSpawnData().minRotation;
            var fractionOfDifference = fraction * difference;
            var angle_inside = fractionOfDifference + GetSpawnData().minRotation;
            spawnersCoords[i] = new Vector3(Mathf.Sin(angle_inside* Mathf.Deg2Rad)*3, Mathf.Cos(angle_inside* Mathf.Deg2Rad)*3, 0); 
        }
        foreach (var r in spawnersCoords) print(r);
        return spawnersCoords;
        
        }
    
    public float[] SpiralRotations()
    {
        
        for (int i = 0; i < GetSpawnData().numberOfBullets; i++)
        {
            
            var fraction = (float)i / ((float)GetSpawnData().numberOfBullets - 1);
            var difference = GetSpawnData().maxRotation - GetSpawnData().minRotation;
            var fractionOfDifference = fraction * difference;
            rotations[i] = fractionOfDifference + GetSpawnData().minRotation - angle; 
        }
        angle = angle+GetSpawnData().deltaangle;

        return rotations;
    }

    public float[] LOHRotations()
    {
        
        for (int i = 0; i < GetSpawnData().numberOfBullets; i++)
        {
            
            var fraction = (float)i / ((float)GetSpawnData().numberOfBullets - 1);
            var difference = GetSpawnData().maxRotation - GetSpawnData().minRotation;
            var fractionOfDifference = fraction * difference;
            if (i%2==0){rotations[i] = fractionOfDifference + GetSpawnData().minRotation + angle;}
            else {rotations[i] = fractionOfDifference + GetSpawnData().minRotation - angle;} 
        }
        angle = angle+GetSpawnData().deltaangle;

        return rotations;
    }
}
}