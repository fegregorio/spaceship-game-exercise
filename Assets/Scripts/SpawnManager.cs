using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] asteroids;
    [SerializeField]
    private float areaLimit = 100f;
    [SerializeField]
    private int asteroidLimit = 50;
    
    void Start()
    {
        SpawnAsteroids();
    }

    void SpawnAsteroids()
    {
        for (int i = 0; i < asteroidLimit; i++)
        {
            GameObject obj = asteroids[Random.Range(0, asteroids.Length)];

            Instantiate(obj,
                new Vector3(RandomFloat(), RandomFloat(), RandomFloat()),
                new Quaternion(RandomFloat(), RandomFloat(), RandomFloat(), RandomFloat())
                );
        }
    }

    float RandomFloat()
    {
        return Random.Range(-areaLimit, areaLimit);
    }
}
