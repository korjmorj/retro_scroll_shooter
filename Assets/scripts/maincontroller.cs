using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yarn.Unity.Example{
public class maincontroller : MonoBehaviour
{
    public ParticleSystem explosionEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        this.explosionEffect.transform.position = asteroid.transform.position;
        this.explosionEffect.Play();
        Debug.Log("ПЫЩЬ");

    }

}
}
