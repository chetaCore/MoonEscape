using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

    [SerializeField] private Image uiHP;
    [SerializeField] private Image uiLabel;


    private void Update()
    {
        GetComponentInChildren<Canvas>().transform.LookAt(_enemy.GetSpawn.GetPlayer.transform);
    }

    public void TakeDamage(int damage)
    {
        _enemy.SetHP(_enemy.GetHP - damage);
        uiHP.fillAmount = (float)_enemy.GetHP / 100;
        if (_enemy.GetHP < 1)
        {
            _enemy.Died();
            GetComponentInChildren<Canvas>().enabled = false;
        }
           
    }

    public void ChangeStatelabel()
    {
        if (uiLabel.IsActive())
            uiLabel.gameObject.SetActive(false);
        else
            uiLabel.gameObject.SetActive(true);
    }

}
