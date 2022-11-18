using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Egg : MonoBehaviour
{
    MeshRenderer meshRenderer;


    public Vector3 startPosition;
    [SerializeField] private Image returnImage;
    [SerializeField] private float returnTime = 3f;
    private GameObject player;
    public bool isTaken = false;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        player = GetComponentInParent<CreateEggs>().GetGameControl.GetPlayer;
        startPosition = transform.position;
    }

    private void Update()
    {
        returnImage.transform.LookAt(player.transform);
    }
    public void BackToNest()
    {
        if (transform.position == startPosition) return;
        StartCoroutine("FillingPicture");
    }

    public void StopBackToNest()
    {
        StopCoroutine("FillingPicture");
        returnImage.fillAmount = 0;
    }

    IEnumerator FillingPicture()
    {
        while (true)
        {

            returnImage.fillAmount += returnTime / 20f;
            yield return new WaitForSecondsRealtime(returnTime / 20f);

            if (returnImage.fillAmount == 1)
            {
                transform.position = startPosition;
                returnImage.fillAmount = 0;
                EventsSet.UpdateTarget.Invoke();
                StopCoroutine("FillingPicture");
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {

            isTaken = true;
            transform.position = new Vector3(other.transform.position.x, other.transform.position.y + 1, other.transform.position.z);

            meshRenderer.enabled = false;

        }
        if (other.gameObject.tag == "Spawn")
        {
            transform.parent = other.transform;
            foreach (var col in GetComponents<CapsuleCollider>())
            {
                col.enabled = false;
            } 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            
            meshRenderer.enabled = true;

        }
    }


}
