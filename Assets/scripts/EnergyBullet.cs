using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

namespace Yarn.Unity.Example{
public class EnergyBullet : MonoBehaviour
{
    public Vector2 velocity;
    public float speed;
    public float rotation;
    public float lifetime;
    public float timer;
    public bool invert;
    public bool slow_down;
    public bool random;

    // Start is called before the first frame update
    void Start()
    {  
        timer=lifetime;
        transform.rotation=Quaternion.Euler(0,0,rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if(invert==true){
            if (timer<=(lifetime/4*3)){
                velocity=-velocity;
            invert=false;}
        }
        if(slow_down==true){
            if (timer<=(lifetime/4*3)){
                speed = 0.5f;
            slow_down=false;}
        }
        if(random==true){
            if (timer<=(lifetime/4*3)){
                rotation= UnityEngine.Random.Range(0, 360);
                transform.rotation=Quaternion.Euler(0,0,rotation);
            random=false;}
        }
        transform.Translate(velocity*speed*Time.deltaTime);
        timer-=Time.deltaTime;
        if (timer<=0){
            Destroy(gameObject);
        }

    }
        public void ResetTimer(){
        timer=lifetime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player"){
            ship spaceship=FindObjectOfType<ship>();
            if (spaceship.shield==false){spaceship.armor-=1f;}
            else {spaceship.energy-=2f;}
            Destroy(this.gameObject);
            
            
        }

        
    }

}
}
