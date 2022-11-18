using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoxSupplies : MonoBehaviour
{

    [SerializeField] GameObject boxPrefab;
    [SerializeField] float createRadius;
    [Range(5,100)]
    [SerializeField] int creationBreakTime;

    Stack<GameObject> boxStack = new();
    void Start()
    {
        Create(creationBreakTime * 2);
        StartCoroutine(ReleaseCor());
    }

    void Create(int numberEntities = 1)
    {
        for (int i = 0; i < numberEntities; i++)
        {
            var box = Instantiate(boxPrefab, new Vector3(0, 100, 0), Quaternion.identity, transform);
            box.SetActive(false);
            boxStack.Push(box);

        }  
    }

    void Release()
    {

        if (boxStack.Count != 0)
        {
            var x = Random.Range(-createRadius, createRadius);
            var z = Random.Range(-createRadius, createRadius);

            boxStack.Peek().transform.position = new Vector3(x, 100, z);
            boxStack.Pop().SetActive(true);
        }
        else
        {
            Create();
            Release();
        }
            

    }

    public void ReturnInStack(GameObject returnableBox)
    {

        returnableBox.SetActive(false);
        boxStack.Push(returnableBox);
    }

    IEnumerator ReleaseCor()
    {
        while (true)
        {
            Release();
            yield return new WaitForSeconds(creationBreakTime);
        }
   
    }

}
