using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSupplies : MonoBehaviour
{
    [SerializeField] List<GameObject> suppliesPrefabs;
    SpawnBoxSupplies spawnBoxSupplies;
    Stack<StandartPositionsSide> standartPositionsSides = new();

    struct StandartPositionsSide 
    {
        public StandartPositionsSide(GameObject side, Vector3 defaultPosition, Quaternion defaultRotation)
        {
            this.side = side;
            this.defaultPosition = defaultPosition;
            this.defaultRotation = defaultRotation;
        }
        public GameObject side;
        public Vector3 defaultPosition;
        public Quaternion defaultRotation;
    }

    bool suplesIsCreated = false;

    private void Start()
    {
        spawnBoxSupplies = transform.parent.parent.GetComponent<SpawnBoxSupplies>();
        
        SaveDefauoltPosition();
    }

    IEnumerator CreateSuppliesCor()
    {
        if (suplesIsCreated) yield break;
        suplesIsCreated = true;


        foreach (Transform item in transform.parent)
        {
            item.GetComponent<Rigidbody>().freezeRotation = false;
            item.GetComponent<Rigidbody>().AddForce(-item.forward * 10f, ForceMode.Impulse);
        }


        Instantiate(ChoiceSupplies(), transform.position, Quaternion.identity, transform.parent.transform.parent);


        yield return new WaitForSeconds(3);


        spawnBoxSupplies.ReturnInStack(transform.parent.gameObject);


        foreach (var item in standartPositionsSides)
        {   
            item.side.transform.position = item.defaultPosition;
            item.side.transform.rotation = item.defaultRotation;

            item.side.GetComponent<Rigidbody>().freezeRotation = true;
        }
        suplesIsCreated = false;
    }

    void SaveDefauoltPosition()
    {
        
        foreach (Transform side in transform.parent)
        {
            standartPositionsSides.Push(new StandartPositionsSide(side.gameObject, side.transform.position, side.rotation));
        }
    }

    GameObject ChoiceSupplies()
    {
        while (true)
        {
            var supplies = suppliesPrefabs[Random.Range(0, suppliesPrefabs.Count)].GetComponent<Supplies>();
            if (Random.Range(1, 11) <= supplies.GetPriority)
               return supplies.gameObject;
        }
       
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Supplies")
           StartCoroutine(CreateSuppliesCor());
    }
}
