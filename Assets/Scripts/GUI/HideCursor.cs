using UnityEngine;

public class HideCursor : MonoBehaviour
{
    //private Player player;
    //private bool cursorVisible = false;

    //void Start()
    //{
    //    // —крыть курсор при старте игры
    //    SetCursorState(cursorVisible);
    //    player = FindAnyObjectByType<Player>();
    //}

    //void Update()
    //{
    //    if (player.stats.isDead)
    //    {
    //        cursorVisible = true;
    //        SetCursorState(cursorVisible);
    //    }
    //    else
    //    {
    //        // ѕереключение видимости курсора при нажатии клавиши Escape
    //        if (Input.GetKeyDown(KeyCode.Escape))
    //        {
    //            cursorVisible = !cursorVisible;
    //            SetCursorState(cursorVisible);
    //        }

    //        // –азблокировка курсора, но оставление его невидимым при зажатии клавиши R
    //        if (Input.GetKey(KeyCode.R))
    //        {
    //            Cursor.lockState = CursorLockMode.None;
    //            Cursor.visible = false;
    //        }
    //        else if (Input.GetKeyDown(KeyCode.E))
    //        {
    //            // ќтображение курсора при нажатии клавиши E
    //            cursorVisible = true;
    //            SetCursorState(cursorVisible);
    //        }
    //        else
    //        {
    //            // ¬озвращаем курсор в исходное состо€ние, если R не зажата и E не нажата
    //            SetCursorState(cursorVisible);
    //        }
    //    }
    //}

    //void SetCursorState(bool isVisible)
    //{
    //    Cursor.visible = isVisible;
    //    Cursor.lockState = isVisible ? CursorLockMode.None : CursorLockMode.Locked;
    //}
}
