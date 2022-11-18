using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEggs : MonoBehaviour
{
    [SerializeField] private GameControl _gameControl;
    [SerializeField] private GameObject _eggsPrefab;
    [SerializeField] private int amountEggs = 5;
    [SerializeField] private int eggSpawnRadius = 50;

    private List<GameObject> eggList = new();

    public GameControl GetGameControl => _gameControl;
    public List<GameObject> GetEggList => eggList;
    public int GetAmountEggs => amountEggs;



    private void Start()
    {
        for (int i = 0; i < amountEggs; i++)
        {
            float circlePosition = (float)i / (float)amountEggs * 2f;
            float x = Mathf.Sin(circlePosition * Mathf.PI) * eggSpawnRadius;
            float z = Mathf.Cos(circlePosition * Mathf.PI) * eggSpawnRadius;
            eggList.Add(Instantiate(_eggsPrefab, new Vector3(x, transform.position.y, z), Quaternion.identity, transform));
            

        }
    }
}
