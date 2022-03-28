using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="Data",menuName="Script/BulletSpawnedData", order=1)]

public class BulletSpawnedData : ScriptableObject
{
    public bool rest = false;
    public GameObject bulletResource;
    public GameObject[] spawnerResource;
    public float minRotation;
    public float maxRotation;
    public int numberOfBullets;
    public bool isRandom;
    public bool isDistrib=true;

    public float cooldown;
    public float bulletSpeed;
    public Vector2 bulletVelocity;
    public bool isNotParent=true;


    public int deltaangle;
    public float timer_for_level;
    public float line_time;
    public bool spiral=false;
    public bool loh=false;
    public bool line=false;
    public bool create_spawners = false;
    public bool circle_spawn = false;
    public bool destroytourself = false;
    public float lifetime;
    public bool is_moving_again;
    public bool ready_to_shoot = false;
    public float spawner_speed = 5f;
    public Vector3 [] spawnersCoords;
    public Vector3 spawner_size;
    public Sprite bullet_sprite;
    public Vector3 bullet_size;
    public bool invert = false;
    public bool slow_down = false;
    public bool random = false;




}