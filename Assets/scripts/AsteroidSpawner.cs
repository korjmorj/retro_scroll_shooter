using UnityEngine;
namespace Yarn.Unity.Example{
public class AsteroidSpawner : MonoBehaviour
{
    public StationMove stationPrefab;
    public Asteroid asteroidPrefab;
    //public Pirate piratePrefab;
    public float spawnDistance = 6.0f;
    public float spawnRate = 1.0f;
    public int amountPerSpawn = 1;
    [Range(0.0f, 45.0f)]
    public float timer;
    public float trajectoryVariance = 6.0f;
    public int lowWave;
    public int highWave;
    public float piratecool;
    float piratetime;
    public bool waves = true;
    public bool ready = false;
    int initspavnnumb;
    bool endOfLevel = false;
    private Asteroid [] allAstro; 

    private void Start()
    {
        initspavnnumb = this.amountPerSpawn;
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
     
        
    }


    void Update() {
        //InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
        if (this.amountPerSpawn!=0){
        if (timer<=0){
            if (waves == true){
            //var initspavnnumb = this.amountPerSpawn;
            var wavenumber = Random.Range(lowWave, highWave);
            this.amountPerSpawn = wavenumber;
            Spawn();
            this.amountPerSpawn = initspavnnumb;
            
            initspavnnumb +=1;}
            
            if (this.amountPerSpawn>=lowWave){
                waves = false;
            }
            if (waves == false){
                this.amountPerSpawn -=1;
            }
            timer = Random.Range(5.0f, 15.0f);
        }
        }
        if (this.amountPerSpawn==0){
            allAstro = FindObjectsOfType<Asteroid> ();
            Debug.Log(allAstro.Length);

            if (allAstro.Length==0){
                if(endOfLevel == false){
                StationMove station = Instantiate(stationPrefab);
                endOfLevel=true;
                }
            }
            
        }


       timer -= Time.deltaTime;
       piratetime -= Time.deltaTime;
        }
    


    public void Spawn()
    {
        for (int i = 0; i < this.amountPerSpawn; i++)
        {
            // Choose a random direction from the center of the spawner and
            // spawn the asteroid a distance away
            var x_sp = Random.Range(-1.0f, 1.0f);
            var y_sp = Random.Range(0.1f, 1.0f);
            Vector2 spawnDirection = new Vector2(x_sp, y_sp);
            var x_p = Random.Range(-5, 5);
            Vector3 spawnPoint = new Vector3(x_p, 7.0f, 0);

            // Offset the spawn point by the position of the spawner so its
            // relative to the spawner location
            //spawnPoint += this.transform.position;

            // Calculate a random variance in the asteroid's rotation which will
            // cause its trajectory to change
            float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            // Create the new asteroid by cloning the prefab and set a random
            // size within the range
            Asteroid asteroid = Instantiate(this.asteroidPrefab, spawnPoint, rotation);
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);

            // Set the trajectory to move in the direction of the spawner
            Vector2 trajectory = rotation * -spawnDirection;
            asteroid.SetTrajectory(trajectory);
            
        }
    }

}

}