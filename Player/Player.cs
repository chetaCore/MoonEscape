using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Animator animator;
    [SerializeField] PlayerUI _playerUI;
    [SerializeField] Rigidbody _rigidbody;


    [SerializeField] Weapon weapon;
    public Weapon GetWeapon => weapon;


    [SerializeField] private int speed = 5;
    public int GetSpeed => speed; 
    public void SetSpeed(int speed)
    {
        this.speed = speed;
    }


    int maxHP;
    public int GetMaxHP => maxHP;
    public void SetMaxHP(int value)
    {
        maxHP = value;
    }
   

    [SerializeField] int hp = 100;
    public int GetHP => hp;
    public void SetHP(int value)
    { 
        if (value < 0) 
            value = 0; 
        hp = value; 
        _playerUI.SetUIHP(value); 
    }


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    private void Start()
    {
        maxHP = hp;
    }


    private void LateUpdate()
    {
        Movement();
        Jump();
    }

    private void Movement()
    {

       
        float h = Input.GetAxisRaw("Horizontal"); 
        float v = Input.GetAxisRaw("Vertical");    

        if (h != 0 || v != 0)
        {
            animator.SetBool("IsRunning", true);

            Vector3 velocity = new Vector3(h, 0, v) * speed;
            velocity.y = _rigidbody.velocity.y;

            Vector3 worldVelocity = transform.TransformVector(velocity);
            _rigidbody.velocity = worldVelocity;
        }else
        {
            animator.SetBool("IsRunning", false);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(transform.up * 300);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Egg" && other.GetComponent<Egg>().isTaken == false)
        {
           
            other.GetComponent<Egg>().BackToNest();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Egg")
        {
            other.GetComponent<Egg>().StopBackToNest();
        }
    }





}
