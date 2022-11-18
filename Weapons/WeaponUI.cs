using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private TMP_Text uiAmunationText;
    public void SetUIAmunationText(int value)
    {
        if (value == 0) 
            uiAmunationText.text = "...";
        else
            uiAmunationText.text = value.ToString();
    }
    
    private void Start()
    {
        uiAmunationText.text = _weapon.GetAmunation.ToString();
    }
}
