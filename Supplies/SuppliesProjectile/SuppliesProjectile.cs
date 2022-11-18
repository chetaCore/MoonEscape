using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuppliesProjectile : Projectile
{
    [SerializeField] protected Sprite effectSprite;

    [Range(-1, 60)]
    [SerializeField] protected int effectDuration;

    protected void AddProjectileToInventory(GameObject gameObject)
    {
        gameObject.GetComponent<Player>().GetWeapon.AddProjectile(this.gameObject);
    }



}
