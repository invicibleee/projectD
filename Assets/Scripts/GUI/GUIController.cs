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
    public void Awake()
    {
        images = GetImages();

    }
    public virtual void Start()
    {
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
 
            img.enabled = false;
        }
    }

    public virtual void FillImage(int index)
    {
        images[index].sprite = fullImage;
    }

    public virtual void ClearImage(int index)
    {
        Debug.Log(index);
        images[index].sprite = emptyImage;
    }
}
