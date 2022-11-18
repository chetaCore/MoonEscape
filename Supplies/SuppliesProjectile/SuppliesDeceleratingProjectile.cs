using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuppliesDeceleratingProjectile : SuppliesProjectile
{

    [SerializeField] private float speedDecreaseFactor = 2f;

    IEnumerator DeceleratingProjectile(GameObject obj)
    {
        obj.GetComponentInChildren<Transform>().GetComponentInChildren<SetOfActiveEffects>().IncreaseEffectSprites(effectSprite);

        Enemy enemy = obj.GetComponent<Enemy>();
        var startSpeed = enemy.GetMaxSpeed;

        enemy.SetSpeed(startSpeed / speedDecreaseFactor);

        yield return new WaitForSeconds(effectDuration);

        enemy.SetSpeed(startSpeed);

        obj.GetComponentInChildren<Transform>().GetComponentInChildren<SetOfActiveEffects>().DecreaseEffectSprites(effectSprite);

    }

    protected new void DestroyProjectile(Collider objCollider)
    {
        if (objCollider.gameObject.tag == "Enemy")
        {
            objCollider.GetComponentInChildren<Transform>().GetComponentInChildren<SetOfActiveEffects>().AddEffect(DeceleratingProjectile(objCollider.gameObject));

            objCollider.gameObject.GetComponent<EnemyUI>().TakeDamage(damage);
           
            Destroy(gameObject);
        }

        if (objCollider.gameObject.tag != "Player" && objCollider.gameObject.tag != "Weapon" && objCollider.gameObject.tag != "Enemy")
            Destroy(gameObject);
    }

    private new void OnTriggerEnter(Collider other)
    {
        DestroyProjectile(other);
    }
}
