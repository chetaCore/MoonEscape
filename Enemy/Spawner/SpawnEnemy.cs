using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] GameControl _gameControl;

    [SerializeField] private float enemySpawnRadius;
    [Range (1,10)]
    [SerializeField] private int SpawnTimeMin;
    [Range(1, 10)]
    [SerializeField] private int SpawnTimeMax;


    private List<GameObject> enemyList = new();

    public List<GameObject> GetEggList => _gameControl.GetNest.GetComponent<CreateEggs>().GetEggList;
    public GameObject GetPlayer => _gameControl.GetPlayer;

    private void Awake()
    {
        EventsSet.AllEnemysIsSpawned.AddListener(StopSpawnProcess);
    }

    private void Start()
    {
        StartCoroutine("SpawnProcess");
    }

    IEnumerator SpawnProcess()
    {
        while (true) {
            yield return new WaitForSeconds(Random.Range(SpawnTimeMin, SpawnTimeMax + 1));
            float x = Mathf.Sin(4 * Mathf.PI) * enemySpawnRadius;
            float z = Mathf.Cos(4 * Mathf.PI) * enemySpawnRadius;
            enemyList.Add(Instantiate(enemyPrefab, new Vector3(x + transform.position.x, transform.position.y, z + transform.position.z), Quaternion.identity, transform));
            EventsSet.EnemyIsSpawned.Invoke();
          
        }
    }

    private void StopSpawnProcess()
    {
        StopCoroutine("SpawnProcess");
    }
}
