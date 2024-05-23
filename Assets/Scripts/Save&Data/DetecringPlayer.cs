using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DetecringPlayer : MonoBehaviour
{
    private string saveKey = "playerPosition";
    private void Awake()
    {
        Load();
    }
    private void Update()
    {
        Save();
    }
    public void Save()
    {
        SaveManager.Save(saveKey, GetData());

    }


    private void Load()
    {
        var data = SaveManager.Load<SaveData.PlayerPos>(saveKey);

        // якщо ви хочете використовувати завантажен≥ дан≥, наприклад, дл€ перем≥щенн€ гравц€
        transform.position = new Vector2(data._playerPos.x, data._playerPos.y);
        Debug.Log("Scene = "+data._sceneIndex);
    }

    private SaveData.PlayerPos GetData()
    {
        var data = new SaveData.PlayerPos()
        {
            _playerPos = new Vector2(transform.position.x, transform.position.y),
            _sceneIndex = SceneManager.GetActiveScene().buildIndex // ќтримуЇмо ≥ндекс поточноњ сцени
        };
        return data;
    }
}
