using UnityEngine;

public class HideCursor : MonoBehaviour
{
    //private Player player;
    //private bool cursorVisible = false;

    //void Start()
    //{
    //    // ������ ������ ��� ������ ����
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
    //        // ������������ ��������� ������� ��� ������� ������� Escape
    //        if (Input.GetKeyDown(KeyCode.Escape))
    //        {
    //            cursorVisible = !cursorVisible;
    //            SetCursorState(cursorVisible);
    //        }

    //        // ������������� �������, �� ���������� ��� ��������� ��� ������� ������� R
    //        if (Input.GetKey(KeyCode.R))
    //        {
    //            Cursor.lockState = CursorLockMode.None;
    //            Cursor.visible = false;
    //        }
    //        else if (Input.GetKeyDown(KeyCode.E))
    //        {
    //            // ����������� ������� ��� ������� ������� E
    //            cursorVisible = true;
    //            SetCursorState(cursorVisible);
    //        }
    //        else
    //        {
    //            // ���������� ������ � �������� ���������, ���� R �� ������ � E �� ������
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
