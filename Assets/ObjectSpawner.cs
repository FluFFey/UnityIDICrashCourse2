using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

    [SerializeField] private float bulletPickupCooldown;
    [SerializeField] private float zombieSpawnCooldown;
    private float timeSinceBulletSpawn;
    private float timeSinceZombieSpawn;
    [SerializeField] private GameObject[] bullets;
    [SerializeField] private GameObject[] zombies;
    // Use this for initialization
    void Start ()
    {
        timeSinceBulletSpawn = 0;
        timeSinceZombieSpawn = 0;
    }
	
	// Update is called once per frame
	void Update () {
        timeSinceBulletSpawn += Time.deltaTime;
        timeSinceZombieSpawn += Time.deltaTime;
        if (timeSinceBulletSpawn > bulletPickupCooldown)
        {
            Vector2 spawnPosition = new Vector2(
                Random.Range(-7, 7),
                Random.Range(-2, 4)
                );
            Instantiate(bullets[Random.Range(0, bullets.Length)], spawnPosition,Quaternion.identity);
            timeSinceZombieSpawn = 0;
        }

        if (timeSinceBulletSpawn > bulletPickupCooldown)
        {
            Vector2 spawnPosition = new Vector2(
                Random.Range(-6, 6),
                Random.Range(-1, 3)
                );
            Instantiate(zombies[Random.Range(0, zombies.Length)], spawnPosition, Quaternion.identity);
            timeSinceBulletSpawn = 0;
        }
    }
}
