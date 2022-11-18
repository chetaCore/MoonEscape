using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBox : Supplies
{
    [SerializeField] private GameObject projectile;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Player>().GetWeapon.AddProjectile(projectile);
            Destroy(gameObject);
        }
    }

}
