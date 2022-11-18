using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventsSet 
{

    //Enemy Events
    public static UnityEvent EnemyIsSpawned = new();
    public static UnityEvent AllEnemysIsSpawned = new();
    public static UnityEvent EggTaken = new();
    public static UnityEvent AllEggTaken = new();
    public static UnityEvent EggDelivered = new();
    public static UnityEvent EggLost = new();
    public static UnityEvent UpdateTarget = new();
    public static UnityEvent EnemyDied = new();

    //inventory events
    public static UnityEvent<int> ProjectileChanged = new();    
    public static UnityEvent<Sprite> NewProjectileAdded = new();

    //GameControll events
    public static UnityEvent AllEnemyKilled = new();




}
