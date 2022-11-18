using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCanvas : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject gameControl;
    [SerializeField] private GameObject spawners;
    [SerializeField] private GameObject boxSuppliesSpawner;
    [SerializeField] private Camera canvasCamera;
    [SerializeField] private GameObject gameCanvas; 

    public void StartGame()
    {
        canvasCamera.enabled = false;
        player.SetActive(true);
        gameControl.SetActive(true);
        spawners.SetActive(true);
        boxSuppliesSpawner.SetActive(true);
        gameCanvas.SetActive(true);
        gameObject.SetActive(false);
    }
}
