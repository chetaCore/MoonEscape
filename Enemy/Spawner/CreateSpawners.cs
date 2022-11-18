using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSpawners : MonoBehaviour
{

    [SerializeField] private GameObject spawnerPrefab;

    [SerializeField] private float createRadius;

    [SerializeField] private float forbiddingSpawnRadius;

    [SerializeField] private int spawnersCount;


    private void Start()
    {
       // CreateSpawner();
    }

    private void CreateSpawner()
    {
        for (int i = 0; i < spawnersCount; i++)
        {
            

            float x = Random.Range(Random.Range(-createRadius, -forbiddingSpawnRadius), Random.Range(forbiddingSpawnRadius, createRadius));
            float z = Random.Range(Random.Range(-createRadius, -forbiddingSpawnRadius), Random.Range(forbiddingSpawnRadius, createRadius));
            print(x + " + " + z);
            Instantiate(spawnerPrefab, new Vector3(x, 0, z), Quaternion.identity, transform);
        } 
    }


  

    //for (int i = 0; i<amountEggs; i++)
    //    {
    //        float circlePosition = (float)i / (float)amountEggs * 2f;
    //float x = Mathf.Sin(circlePosition * Mathf.PI) * eggSpawnRadius;
    //float z = Mathf.Cos(circlePosition * Mathf.PI) * eggSpawnRadius;
    //eggList.Add(Instantiate(_eggsPrefab, new Vector3(x, transform.position.y, z), Quaternion.identity, transform));


}
