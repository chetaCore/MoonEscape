using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    [SerializeField] private GameObject _nest;
    [SerializeField] private GameObject _player;

    [SerializeField] private int amountAllEnemy;
    public int GetamountAllEnemy => amountAllEnemy;



    public GameObject GetNest => _nest;
    public GameObject GetPlayer => _player;


    private int spawnedEnemy = 0;
    private int takedEggs = 0;
    private int deliveredEggs = 0;

    private void Awake()
    {
        EventsSet.EnemyIsSpawned.AddListener(AddSpawnedEnemy);
        EventsSet.EggTaken.AddListener(AddTakedEgg);
        EventsSet.EggDelivered.AddListener(AddDeliveredEggs);
        EventsSet.EggLost.AddListener(ReduceTakedEgg);
        EventsSet.EnemyDied.AddListener(ReduceCountLiveEnemy);
    }

    private void AddSpawnedEnemy()
    {
        spawnedEnemy++;
        if (spawnedEnemy > amountAllEnemy) 
            EventsSet.AllEnemysIsSpawned.Invoke();
    }

    private void AddTakedEgg()
    {
        
        takedEggs++; 
        if (takedEggs == _nest.GetComponent<CreateEggs>().GetAmountEggs)
        {
            EventsSet.AllEggTaken.Invoke();
            print("AllEggTaken");
        }
            
    }

    private void ReduceTakedEgg()
    {
        takedEggs--;
    }

    private void AddDeliveredEggs()
    {
        deliveredEggs++;
        if (deliveredEggs == _nest.GetComponent<CreateEggs>().GetAmountEggs)
            print("youLose");
    }

   private void ReduceCountLiveEnemy()
   {
        amountAllEnemy--;
        if (amountAllEnemy == -1)
        {
            EventsSet.AllEnemyKilled.Invoke();
        }
   }
}
