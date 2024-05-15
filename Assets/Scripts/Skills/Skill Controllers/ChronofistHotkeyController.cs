using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChronofistHotkeyController : MonoBehaviour
{
    private SpriteRenderer sr;
    private KeyCode myHotKey;
    private TextMeshProUGUI myText;

    private Transform myEnemy;
    private ChronofistSkillController chrono;

    public void SetupHotKey(KeyCode _myNewHotKey, Transform _myEnemy, ChronofistSkillController _myChrono)
    {
        sr = GetComponent<SpriteRenderer>();
        myText = GetComponentInChildren<TextMeshProUGUI>();

        myEnemy = _myEnemy;
        chrono= _myChrono;

        myHotKey = _myNewHotKey;
        myText.text = _myNewHotKey.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(myHotKey))
        {
            chrono.AddEnemyToList(myEnemy);

            myText.color = Color.clear;
            sr.color = Color.clear;
        }
    }
}
