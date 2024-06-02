using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mapTrigger : MonoBehaviour
{
    private MapBehaviour map;
    // Start is called before the first frame update
    private string saveKey = "mapSave";
    void Start()
    {
        map = FindAnyObjectByType<MapBehaviour>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !map.isForestMapOpened && SceneManager.GetActiveScene().buildIndex == 1)
        {
            map.isForestMapOpened = true;
            Save();
        } else if (other.CompareTag("Player") && !map.isFullMapOpened && SceneManager.GetActiveScene().buildIndex == 2)
        {
            map.isFullMapOpened = true;
            Save();
        }
    }
    public void Save()
    {
        SaveManager.Save(saveKey, GetData());
    }

    private SaveData.MapSave GetData()
    {
        var data = new SaveData.MapSave()
        {
            _isForestOpen = map.isForestMapOpened,
            _isCastleOpen = map.isFullMapOpened,
        };
        return data;
    }
}
