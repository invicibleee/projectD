using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlaskGUI : GUIController
{
    public override Image[] GetImages()
    {
        return GetComponentsInChildren<Image>(true);
    }
    public override void Start()
    {
        images = GetImages();
        Activate();
    }
}
