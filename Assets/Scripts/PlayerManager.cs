using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Player player;
    public static PlayerManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(instance.gameObject);
        } else
        {
            instance = this;
        }
    }
}
