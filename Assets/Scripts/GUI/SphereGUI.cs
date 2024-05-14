using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class SphereGUI : MonoBehaviour
{
    [SerializeField] public Sprite emptyImage;
    [SerializeField] public Sprite fullImage;
    public Image[] images;
    

    private void Start()
    {
        // Get all Image components, including inactive ones
        Image[] allImages = GetComponentsInChildren<Image>(true);

        // Filter out null references
        List<Image> validImages = new List<Image>();
        foreach (Image img in allImages)
        {
            if (img != null)
            {
                validImages.Add(img);
            }
        }

        // Convert to array
        images = validImages.ToArray();
        Deactivate();
    }

    public void Activate()
    {
        foreach (Image img in images)
        {
            img.enabled = true; 
        }
    }
    public void Deactivate()
    {
        foreach (Image img in images)
        {
            img.sprite = emptyImage; 
            img.enabled = false;
        }
    }
}
