using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreCanvas : MonoBehaviour
{

    [SerializeField] private Button projectilesButton;
    [SerializeField] private Button bonusesButton;
    [SerializeField] private Button statsButton;

    [SerializeField] private GameObject projectilesVewport;
    [SerializeField] private GameObject bonusesVewport;
    [SerializeField] private GameObject statsVewport;

    [SerializeField] private ScrollRect scrollRect;

    private bool isActive = false;

    private void Start()
    {
        projectilesButton.onClick.AddListener(() => StartCoroutine(ChangeWindow(projectilesButton, projectilesVewport, bonusesVewport, statsVewport)));
        bonusesButton.onClick.AddListener(() => StartCoroutine(ChangeWindow(bonusesButton, bonusesVewport, projectilesVewport, statsVewport)));
        statsButton.onClick.AddListener(() => StartCoroutine(ChangeWindow(statsButton, statsVewport, projectilesVewport, bonusesVewport)));
    }

    private IEnumerator ChangeWindow(Button clickedButton, GameObject selectedContent, params GameObject[] otherContents)
    {
        if (isActive) yield break;

        isActive = true;

        foreach (var content in otherContents)
        {
            //через enum!
            content.GetComponentInChildren<DOTweenAnimation>().DORestartById(1.ToString());
        }


        selectedContent.SetActive(true);
        selectedContent.GetComponentInChildren<DOTweenAnimation>().DORestartById(0.ToString());

        clickedButton.GetComponent<DOTweenAnimation>().DORestartById(0.ToString());


        scrollRect.content = selectedContent.transform.GetComponentsInChildren<RectTransform>()[1];
        scrollRect.viewport = selectedContent.GetComponent<RectTransform>();

        yield return new WaitForSeconds(0.5f);

        isActive = false;
    }

   
}
