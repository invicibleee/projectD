using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCursor : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private CursorMode cursorMode = CursorMode.Auto;
    [SerializeField] private Vector2 hotSpot = Vector2.zero;
    [SerializeField] private Texture2D customCursor;

    void Start()
    {
        Cursor.SetCursor(customCursor, hotSpot, cursorMode);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
