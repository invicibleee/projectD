using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class SphereGUI : GUIController
{
    public override Image[] GetImages()
    {
        return GetComponentsInChildren<Image>(true);
    }
}
