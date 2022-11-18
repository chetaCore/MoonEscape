using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    [SerializeField] Image defaultImage;
    [SerializeField] Image inventoryFrame;
    [SerializeField] List<Image> projectileImage;

    private int activeImage = 0;

    private void Awake()
    {
        EventsSet.NewProjectileAdded.AddListener(IncreaseProjectileSprites);
        EventsSet.ProjectileChanged.AddListener(SelectFramePosition);
    }

    //Костыль, исправить!!!!
    private void IncreaseProjectileSprites(Sprite sprite)
    {
        var image = Instantiate<Image>(defaultImage, transform);
        projectileImage.Add(image);
        image.sprite = sprite;
        Invoke(nameof(SelectFramePosition), 0.02f);
    }

    private void SelectFramePosition(int num)
    {
        activeImage = num;
        inventoryFrame.transform.position = projectileImage[activeImage].transform.position;   
    }
    private void SelectFramePosition()
    {
        inventoryFrame.transform.position = projectileImage[activeImage].transform.position;
    }


}
