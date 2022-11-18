using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supplies : MonoBehaviour
{
    [Range(1,10)]
    [SerializeField] protected int priority;
    public int GetPriority => priority;

    [Range(-1, 60)]
    [SerializeField] protected int effectDuration;

    MeshRenderer meshRenderer;
    BoxCollider boxCollider;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
    }

    protected void TurnOffActivity()
    {
        meshRenderer.enabled = false;
        boxCollider.enabled = false;
    }

}
