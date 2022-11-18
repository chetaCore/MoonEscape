using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetOfActiveEffects : MonoBehaviour
{
    [SerializeField]private List<Image> imageEffect;
    [SerializeField] private List<Sprite> allEffect;

    public List<Image> GetImageEffect => imageEffect;

    public void AddEffect(IEnumerator effectCor)
    {

        StartCoroutine(effectCor);
    }


    [SerializeField] Image defaultImage;

    public void IncreaseEffectSprites(Sprite sprite)
    {

        int effect = 0;
        foreach (var spr in allEffect)
            if (spr.Equals(sprite))
                effect++;



        if (effect == 0)
        {
            var image = Instantiate<Image>(defaultImage, transform);
            imageEffect.Add(image);

            image.sprite = sprite;
                                        

            allEffect.Add(sprite);

        }else
            allEffect.Add(sprite);
    }

    public void DecreaseEffectSprites(Sprite sprite)
    {
        
        int effect = 0;
        foreach (var spr in allEffect)
            if (spr.Equals(sprite))
                effect++;


        if (effect > 1)
        {
            foreach (var spr in allEffect)
                if (spr.Equals(sprite))
                {
                    allEffect.Remove(spr);
                    return;
                }
        }else
        {
            foreach (var spr in allEffect)
                if (spr.Equals(sprite))
                {
                    allEffect.Remove(spr);
                    break;
                }
            foreach (var img in imageEffect)
                if (img.sprite.Equals(sprite))
                {
                    Destroy(img.gameObject);
                    imageEffect.Remove(img);
                    return;
                }


        }
             
    }
}
