using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuppliesRestoreHP : Supplies
{
    void RestoreHPEffect(GameObject obj)
    {
        if (obj.TryGetComponent(out Player player))
        {
            player.SetHP(player.GetMaxHP);
            Destroy(gameObject);
        }
        else
            Debug.LogError(obj.name + "has no Player component");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            RestoreHPEffect(other.gameObject);
    }

}
