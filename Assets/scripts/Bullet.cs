using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; }
    public float speed = 500.0f;
    public float maxLifetime = 10.0f;

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
        this.rigidbody.AddForce(new Vector2(0, 1) * this.speed);
    }

    //public void Project(Vector2 ondirecti)
    //{
        // The bullet only needs a force to be added once since they have no
        // drag to make them stop moving
        //this.rigidbody.AddForce(new Vector2(0, 1) * this.speed);

        // Destroy the bullet after it reaches it max lifetime
        //Destroy(this.gameObject, this.maxLifetime);
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag!="Player"){
            Destroy(this.gameObject);
        }
        // Destroy the bullet as soon as it collides with anything
        
    }

}