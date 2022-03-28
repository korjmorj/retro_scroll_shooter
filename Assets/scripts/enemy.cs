using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yarn.Unity.Example {
[RequireComponent(typeof(Rigidbody2D))]
public class enemy : MonoBehaviour
{
    public int lives;
    public bool shield=true;
    public SpriteRenderer spriteRenderer { get; private set; }
    public Sprite enemy_sp;
    public Sprite enemy_shield_sp;
    // Start is called before the first frame update
    void Start()
    {
        
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.GetComponent<spawner>().ready_to_shoot==false|this.gameObject.GetComponent<spawner>().rest==true){
                shield=true;
            }
        else{shield=false;}
        //if (this.gameObject.GetComponent<spawner>().rest==true){shield=true;}
        if (shield==true){spriteRenderer.sprite = enemy_shield_sp;}
        else{spriteRenderer.sprite = enemy_sp;}
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Bullet"){
            Debug.Log("ПОПАЛИ");
            if (shield==false){
                lives-=1;
            }
            Destroy(collision.gameObject);
            if (lives==0){
                Destroy(this.gameObject);
            }
        }
    }
}
}