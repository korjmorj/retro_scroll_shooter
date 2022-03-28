using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particlemove : MonoBehaviour
{
public float speedx;
public float speedy;
float x_pos;


//private Vector3 initialVelocity;
    // Start is called before the first frame update
    void Start()
    { 
      x_pos = Random.Range(-6, 6);


      transform.position=new Vector2(x_pos, 7); 
    }

    // Update is called once per frame
    void Update()
    {

        transform.position=new Vector2(transform.position.x-speedx*Time.deltaTime, transform.position.y-speedy*Time.deltaTime);
        if(transform.position.x>7f|transform.position.x<-7f|transform.position.y<-5.4f){
            Destroy(gameObject);

        }
    }

    void OnTriggerEnter(Collider other)
    {
        speedx = speedx * -1;
        Debug.Log("СТОЛКНОВЕНИЕ");
    }



} 