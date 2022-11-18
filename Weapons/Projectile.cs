using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Sprite sprite;
    public Sprite GetSprite => sprite; 

    [SerializeField] protected int damage;
    [SerializeField] int quantityProjectiles = 10;
    [SerializeField] private float lifetime = 1;
   

    private void Start()
    {
        StartCoroutine(DestroyCor());
    }

    protected void OnTriggerEnter(Collider other)
    {
        DestroyProjectile(other);
    }

    IEnumerator DestroyCor()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

    protected void DestroyProjectile(Collider objCollider)
    {
        if (objCollider.gameObject.tag == "Enemy")
        {
            objCollider.gameObject.GetComponent<EnemyUI>().TakeDamage(damage);
        }

        if (objCollider.gameObject.tag != "Player" && objCollider.gameObject.tag != "Weapon")
            Destroy(gameObject);
        
    }

}
