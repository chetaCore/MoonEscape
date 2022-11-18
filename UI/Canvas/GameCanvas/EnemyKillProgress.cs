using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyKillProgress : MonoBehaviour
{
    [SerializeField] private GameControl gameControl;


    private TMP_Text tMP_Text;
    [SerializeField]private Image scaleProgressBar;
    private int amountAllEnemy;

    private int killedEnemy = 0;

    private void Awake()
    {
        EventsSet.EnemyDied.AddListener(IncreaseKilledEnemys);
    }

    private void Start()
    {
        amountAllEnemy = gameControl.GetamountAllEnemy + 1;
        tMP_Text = GetComponentInChildren<TMP_Text>();
        tMP_Text.text = amountAllEnemy.ToString();
        
    }


    private void IncreaseKilledEnemys()
    {

        killedEnemy++;
        tMP_Text.text = (amountAllEnemy - killedEnemy).ToString(); ;
        scaleProgressBar.fillAmount = amountAllEnemy / 100f * (float)killedEnemy;
    }
}
