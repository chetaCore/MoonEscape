using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuppliesIncreaseSpeed : Supplies
{
   [Range(1, 10)]
   [SerializeField] int speedCoefficient;



    void IncreaseSpeedEffect(GameObject obj)
   {
        if (obj.TryGetComponent(out Player player))
        {
            StartCoroutine(IncreaseSpeedEffectCor(player));
            TurnOffActivity();
        }
        else
            Debug.LogError(obj.name + "has no Player component");
   }
   
   IEnumerator IncreaseSpeedEffectCor(Player player)
   {
        player.SetSpeed(player.GetSpeed * speedCoefficient);
        yield return new WaitForSeconds(effectDuration);
        if (effectDuration > -1)
            player.SetSpeed(player.GetSpeed / speedCoefficient);
        Destroy(gameObject);
   }



   private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            IncreaseSpeedEffect(other.gameObject);
   }
}
