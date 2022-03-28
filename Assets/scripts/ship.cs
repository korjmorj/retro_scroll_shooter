using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Yarn.Unity.Example {
[RequireComponent(typeof(Rigidbody2D))]
public class ship : MonoBehaviour

{   public Sprite Ship;
    public Sprite ShipShield;
    public float armor = 100f;
    public float energy = 100f;
    public bool shield = false;
    public float moveSpeed = 6f;
    public float HorizontalBorder = 4.0f;
    public float VerticalBorder = 4.0f;
    public SpriteRenderer spriteRenderer { get; private set; }
    public new Rigidbody2D rigidbody { get; private set; }
    public Bullet bulletPrefab;
    public float interactionRadius = 2.0f;
    public Text counterText;
    public Text enText;
    public float energy_reload = 4.0f;
    public float shield_cost = 1.0f;
    float shield_cost_time;
    float reload_time;
    float init_en;
    
    // Start is called before the first frame update
    void Start()
    {
        reload_time=energy_reload;
        init_en=energy;
        shield_cost_time=shield_cost;
    }
    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.rigidbody = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        //get the Input from Horizontal axis
        counterText.text = armor.ToString();
        enText.text = energy.ToString();
        float horizontalInput = Input.GetAxis("Horizontal");
        //get the Input from Vertical axis
        float verticalInput = Input.GetAxis("Vertical");

        //update the position
        transform.position = transform.position + new Vector3(horizontalInput * moveSpeed * Time.deltaTime, verticalInput * moveSpeed * Time.deltaTime, 0);
        if (transform.position.x<-HorizontalBorder){transform.position = new Vector3(-HorizontalBorder, transform.position.y, transform.position.z);}
        if (transform.position.x>HorizontalBorder){transform.position = new Vector3(HorizontalBorder, transform.position.y, transform.position.z);}
        if (transform.position.y<-VerticalBorder){transform.position = new Vector3(transform.position.x, -VerticalBorder, transform.position.z);}
        if (transform.position.y<-VerticalBorder){transform.position = new Vector3(transform.position.x, VerticalBorder, transform.position.z);}


        if (shield==true){
            spriteRenderer.sprite = ShipShield;
            if(shield_cost_time<=0){
                energy-=2.0f;
                shield_cost_time=shield_cost;
            }
            if (Input.GetKeyUp(KeyCode.Z)) {
            shield=false;
            }
            shield_cost_time-=Time.deltaTime;
        }
        if (shield==false){
            spriteRenderer.sprite = Ship;
            if (Input.GetKeyDown(KeyCode.X)) {
            if(energy>1.0f){Shoot();}
            }
            if (energy>2.0f){
            if (Input.GetKeyDown(KeyCode.Z)) {
            shield=true;
            }

            }

        }

        // if (Input.GetKeyDown(KeyCode.C)) {
        //         CheckForNearbyNPC();
        //     }
        if (reload_time<=0){
            if(energy+2.0f<init_en){
                energy+=2.0f;
            }
            reload_time=energy_reload;
        }
        reload_time-=Time.deltaTime;

    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        //bullet.Project(this.transform.up);
        energy-=1;
    }

        private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "astro" || collision.gameObject.tag == "enemy" )
        {
            // Check if the asteroid is large enough to split in half
            // (both parts must be greater than the minimum size)
            if (collision.gameObject.tag == "enemy"){
                if (shield==false){armor-=0.5f;}
                }
            
            //}
            

            //FindObjectOfType<GameManager>().AsteroidDestroyed(this);

            // Destroy the current asteroid since it is either replaced by two
            // new asteroids or small enough to be destroyed by the bullet
            
            Destroy(collision.gameObject);
        }
    }

    // public void CheckForNearbyNPC ()
    //     {
    //         var allParticipants = new List<Contact> (FindObjectsOfType<Contact> ());
    //         var target = allParticipants.Find (delegate (Contact p) {
    //             return string.IsNullOrEmpty (p.talkToNode) == false && // has a conversation node?
    //             (p.transform.position - this.transform.position)// is in range?
    //             .magnitude <= interactionRadius;
    //         });
    //         if (target != null) {
    //             // Kick off the dialogue at this node.
    //             FindObjectOfType<DialogueRunner> ().StartDialogue (target.talkToNode);
    //         }
    //     }
}
}