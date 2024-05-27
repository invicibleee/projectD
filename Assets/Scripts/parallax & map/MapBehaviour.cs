using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject localMap;
    [SerializeField] private GameObject fullforestMap;
    [SerializeField] private GameObject fullMap;
    [SerializeField] private GameObject cursor;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject prompt;

    private bool isObjectActive = false;
    private bool isPaused = false;
    [SerializeField] public bool isForestMapOpened;
    [SerializeField] public bool isFullMapOpened;
    private string saveKey = "mapSave";
    private Player player;
    // Start is called before the first frame update
    void Awake()
    {
        Load();
    }
    private void Start()
    {
       player= FindAnyObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.stats.currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            isObjectActive = !isObjectActive;

            mainCamera.SetActive(isObjectActive);

            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if (isObjectActive && isForestMapOpened && !isFullMapOpened)
        {
            prompt.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                localMap.SetActive(true);
                cursor.SetActive(true);
                background.SetActive(false);
                fullforestMap.SetActive(false);
                fullMap.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                localMap.SetActive(false);
                cursor.SetActive(false);
                background.SetActive(true);
                fullforestMap.SetActive(true);
                fullMap.SetActive(false);
            }
        } else if (isObjectActive && isFullMapOpened)
        {
            prompt.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                localMap.SetActive(true);
                cursor.SetActive(true);
                background.SetActive(false);
                fullforestMap.SetActive(false);
                fullMap.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                localMap.SetActive(false);
                cursor.SetActive(false);
                background.SetActive(true);
                fullforestMap.SetActive(false);
                fullMap.SetActive(true);
            }
        }
        else
        {
            prompt.SetActive(false);
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
    }

    private void Load()
    {
        var data = SaveManager.Load<SaveData.MapSave>(saveKey);

        isFullMapOpened = data._isCastleOpen;
        isForestMapOpened = data._isForestOpen;
    }

}
