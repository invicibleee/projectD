using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStyleManager : MonoBehaviour
{
    public static WeaponStyleManager instance;

    public RedWeaponStyle redWeaponStyle { get; set; }
    public GreenWeaponStyle greenWeaponStyle { get; set; }
    public BlueWeaponStyle blueWeaponStyle { get; set; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        redWeaponStyle = GetComponent<RedWeaponStyle>();
        greenWeaponStyle = GetComponent<GreenWeaponStyle>();
        blueWeaponStyle = GetComponent<BlueWeaponStyle>();
    }
}
