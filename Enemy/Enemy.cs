using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    Animator animator;

    EnemyUI enemyUI;
    EnemyAnimate enemyAnimate;


    [SerializeField] private float speedIncreaseFactor = 1;

    private float maxSpeed;
    public float GetMaxSpeed => maxSpeed;

    public float GetSpeed => goal.speed;
    public void SetSpeed(float value) { goal.speed = value; }

    [SerializeField] private int hp = 100;
    public int GetHP => hp;
    public void SetHP(int value) { hp = value; }


    [SerializeField] private int damage;
    [SerializeField] private float attackRecharge;
    [SerializeField] private float protection;

    private bool nearbyPlayer = false;
    private bool isAttacking = false;
    private bool isBusy = false;
    public bool GetIsBusy => isBusy;


    private SpawnEnemy spawn;
    public SpawnEnemy GetSpawn => spawn;


    private GameObject enemyTarget;
    public GameObject GetEnemyTarget => enemyTarget;


    private UnityEngine.AI.NavMeshAgent goal;



    private void Awake()
    {
        enemyAnimate = GetComponent<EnemyAnimate>();
        animator = GetComponent<Animator>();
        enemyUI = GetComponent<EnemyUI>();

        EventsSet.EggTaken.AddListener(TargetSelection);
        EventsSet.AllEggTaken.AddListener(StartPlayerTargetCorotines);
        EventsSet.EggLost.AddListener(StopPlayerTargetCorotines);
        EventsSet.EggLost.AddListener(TargetSelection);
        EventsSet.UpdateTarget.AddListener(TargetSelection);

       
    }

    private void Start()
    {
 
        enemyAnimate.ActivateRunAnimation();

        spawn = transform.parent.GetComponent<SpawnEnemy>();
        goal = GetComponent<UnityEngine.AI.NavMeshAgent>();
        TargetSelection();


        maxSpeed = goal.speed;

    }

    private void TargetSelection()
    {
        if (isBusy) return;

        var eggList = GetComponentInParent<SpawnEnemy>().GetEggList;
        
        float maxDistance = float.MaxValue;

        foreach (var egg in eggList)
        {
            if ((transform.position - egg.transform.position).sqrMagnitude < maxDistance && !egg.GetComponent<Egg>().isTaken)
            {
                maxDistance = (transform.position - egg.transform.position).sqrMagnitude;
                enemyTarget = egg;
            }
        }
        MovementToTarget(enemyTarget);
    }

    private void MovementToTarget(GameObject target)
    {
        if(goal.enabled)
            goal.destination = target.transform.position;   
    }

    private void StartPlayerTargetCorotines()
    {
        if (!isBusy)
            StartCoroutine("SetThePlayerAsATarget");
    }

    public void StopPlayerTargetCorotines()
    {
        StopCoroutine("SetThePlayerAsATarget");
        isAttacking = false;
    }

    IEnumerator SetThePlayerAsATarget()
    {
        yield return new WaitForSeconds(1f);
        isAttacking = true;
        while (true)
        {
            MovementToTarget(spawn.GetPlayer);
            yield return new WaitForSeconds(0.1f);
        }


    }
    IEnumerator Attack(Collider target)
    {

        while (nearbyPlayer)
        {
            target.GetComponent<Player>().SetHP(target.GetComponent<Player>().GetHP - damage);
            yield return new WaitForSeconds(attackRecharge);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Egg")
        {
            if (isBusy) return;

            other.transform.parent = gameObject.transform;


            goal.speed *= speedIncreaseFactor;
            maxSpeed = goal.speed;


            enemyUI.ChangeStatelabel();

            isBusy = true;

            EventsSet.EggTaken.Invoke();
            MovementToTarget(spawn.gameObject);
        }

        if (other.gameObject.tag == "Spawn")
        {
            if (!isBusy) return;

            GetComponentInParent<SpawnEnemy>().GetEggList.Remove(enemyTarget);
            EventsSet.EggDelivered.Invoke();


                
            isBusy = false;
            enemyUI.ChangeStatelabel();


            StartPlayerTargetCorotines();

        }

        if (other.gameObject.tag == "Player" && isAttacking )
        {
            nearbyPlayer = true;
            enemyAnimate.ActivateAttackAnimation(); 
            StartCoroutine(Attack(other));
        } 
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            MovementToTarget(gameObject);
            enemyAnimate.DeactivateAttackAnimation();
            nearbyPlayer = false;
        }
            
    }

    public void Died()
    {
        foreach (var capsuleCollider in GetComponents<CapsuleCollider>())
        {
            capsuleCollider.enabled = false;
        }
        
        enemyAnimate.ActivateDiedAnimation();

        StopPlayerTargetCorotines();

        GetComponent<Rigidbody>().freezeRotation = true;
        goal.enabled = false;


        if (isBusy) 
        {
            enemyTarget.transform.parent = null;
            enemyTarget.GetComponent<Egg>().isTaken = false;
            enemyTarget.GetComponent<CapsuleCollider>().enabled = true;
            enemyTarget.GetComponent<MeshRenderer>().enabled = true;
            EventsSet.EggLost.Invoke();
        }
        EventsSet.EnemyDied.Invoke();

    }




}
