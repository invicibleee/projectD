using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InputSettings : MonoBehaviour
{
    public Button Button_w;
    public Button Button_a;
    public Button Button_s;
    public Button Button_d;
    public Button Button_jump;
    public Button Button_ability;
    public Button Button_attack;
    public Button Button_strongAttack;
    public Button Button_dash;
    public Button Button_heal;
    public Button Button_use;

    Dictionary<Button, KeyCode> buttonKeyMap = new Dictionary<Button, KeyCode>();

    void Start()
    {
        SetupButton(Button_w, "W");
        SetupButton(Button_a, "A");
        SetupButton(Button_s, "S");
        SetupButton(Button_d, "D");
        SetupButton(Button_jump, "Space");
        SetupButton(Button_ability, "R");
        SetupButton(Button_attack, "RM");
        SetupButton(Button_strongAttack, "LM");
        SetupButton(Button_dash, "LeftCtrl");
        SetupButton(Button_heal, "F");
        SetupButton(Button_use, "E");
    }

    void SetupButton(Button button, string defaultKey)
    {
        button.onClick.AddListener(() => StartKeyAssignment(button));
        UpdateButtonText(button, defaultKey);
        buttonKeyMap.Add(button, GetKeyCodeFromString(defaultKey));
    }

    void StartKeyAssignment(Button button)
    {
        StartCoroutine(WaitForKeyPress(button));
    }

    IEnumerator WaitForKeyPress(Button button)
    {
        KeyCode currentKey = KeyCode.None;

        while (currentKey == KeyCode.None)
        {
            yield return null;

            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode) && !IsKeyAssigned(keyCode))
                {
                    currentKey = keyCode;
                    UpdateKey(button, currentKey);
                    break;
                }
            }
        }
    }

    bool IsKeyAssigned(KeyCode newKeyCode)
    {
        return buttonKeyMap.ContainsValue(newKeyCode);
    }

    void UpdateKey(Button button, KeyCode newKeyCode)
    {
        buttonKeyMap[button] = newKeyCode;
        UpdateButtonText(button, GetKeyText(newKeyCode));
    }

    void UpdateButtonText(Button button, string keyText)
    {
        button.GetComponentInChildren<Text>().text = keyText;
    }

    string GetKeyText(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.Mouse0:
                return "LM";
            case KeyCode.Mouse1:
                return "RM";
            case KeyCode.LeftControl:
                return "LeftCtrl";
            default:
                return keyCode.ToString();
        }
    }

    KeyCode GetKeyCodeFromString(string keyString)
    {
        if (keyString == "LM") return KeyCode.Mouse0;
        if (keyString == "RM") return KeyCode.Mouse1;
        if (keyString == "LeftCtrl") return KeyCode.LeftControl;

        KeyCode keyCode;
        if (System.Enum.TryParse(keyString, out keyCode))
        {
            return keyCode;
        }
        else
        {
            Debug.LogError("Invalid KeyCode: " + keyString);
            return KeyCode.None;
        }
    }
}
