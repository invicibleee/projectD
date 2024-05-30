using UnityEngine;
using DialogueEditor;

public class HideCursor : MonoBehaviour
{
    private Player player;
    private bool cursorVisible = false;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Texture2D customCursor;
    [SerializeField] private Vector2 hotSpot = Vector2.zero;
    [SerializeField] private CursorMode cursorMode = CursorMode.Auto;

    void Start()
    {
        // Скрыть курсор при старте игры
        SetCursorState(cursorVisible);
        player = FindAnyObjectByType<Player>();
    }

    [System.Obsolete]
    void Update()
    {
        if (player.stats.isDead || pauseMenu.activeSelf || ConversationManager.Instance.IsConversationActive)
        {
            if (!cursorVisible)
            {
                cursorVisible = true;
                SetCursorState(cursorVisible);
            }
        }
        else if (!pauseMenu.activeSelf && !ConversationManager.Instance.IsConversationActive && !Input.GetKey(KeyCode.R))
        {
            if (cursorVisible)
            {
                cursorVisible = false;
                SetCursorState(cursorVisible);
            }
        }

        if (Input.GetKey(KeyCode.R))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
        }
    }

    void SetCursorState(bool isVisible)
    {
        if (isVisible)
        {
            Cursor.SetCursor(customCursor, hotSpot, cursorMode);
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
        Cursor.visible = isVisible;
        Cursor.lockState = isVisible ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
