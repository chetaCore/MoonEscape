using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{

    [SerializeField] private Image uiHPImage;
    [SerializeField] private TMP_Text uiHPText;

    public void SetUIHP(int hp)
    {
        uiHPImage.fillAmount = (float)hp / 100;
        uiHPText.text = hp.ToString();
    }


}
