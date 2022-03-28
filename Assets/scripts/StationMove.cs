using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

namespace Yarn.Unity.Example{
public class StationMove : MonoBehaviour
{
    // Start is called before the first frame update
 public Vector3 pointA;
 public Vector3 pointB;
 public float speed = 5f;
 private IEnumerator coroutine;
 public bool spawner = false;
 public bool move_again;
     void Start()
     {
        GetStart();
     }
     private void Update() {
                if(spawner==true){
                 if (transform.localPosition == pointB){
                    spawner sp = this.gameObject.GetComponent<spawner>();
                    sp.ready_to_shoot=true;
                    //spawner=false;

                }
             }
     }
     
     
    public IEnumerator moveObject() {
        float totalMovementTime = 5f; //the amount of time you want the movement to take
        float currentMovementTime = 0f;//The amount of time that has passed
    while (transform.localPosition != pointB) {
        currentMovementTime += Time.deltaTime;
        transform.localPosition = Vector3.Lerp(pointA, pointB, currentMovementTime / totalMovementTime);
        yield return null;
    }
}
     private IEnumerator move()
         {
             while (transform.position != pointB)
             {
                 Vector3.Lerp(transform.position, pointB, speed);
                 yield return new WaitForEndOfFrame();
             }

             
         }
    public void GetStart(){
        transform.position = pointA;
         coroutine = moveObject();
         StartCoroutine(coroutine);
    }
}
}
