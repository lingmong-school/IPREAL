using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject cubeSpawn;
    
    [SerializeField] private Transform spawnPoint;
    
    // Start is called before the first frame update
    public void Spawn()
    {
        Instantiate(cubeSpawn, spawnPoint.position, Quaternion.identity);
    }

    
}
