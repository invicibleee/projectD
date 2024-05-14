using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class GUIController : MonoBehaviour
{

    [SerializeField] public Sprite emptyImage;
    [SerializeField] public Sprite fullImage;
    public Image[] images;

    public abstract Image[] GetImages();

    public virtual void Start()
    {
        images = GetImages();
        Deactivate();
    }

    public virtual void Activate()
    {
        foreach (Image img in images)
        {
            img.enabled = true;
        }
    }

    public virtual void Deactivate()
    {
        foreach (Image img in images)
        {
            img.sprite = emptyImage;
            img.enabled = false;
        }
    }

    public virtual void FillImage(int index)
    {
        images[index].sprite = fullImage;
    }

    public virtual void ClearImage(int index)
    {
        images[index].sprite = emptyImage;
    }
}
