using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InputSettings : MonoBehaviour
{
    [SerializeField] private Button Button_w;
    [SerializeField] private Button Button_a;
    [SerializeField] private Button Button_s;
    [SerializeField] private Button Button_d;
    [SerializeField] private Button Button_jump;
    [SerializeField] private Button Button_ability;
    [SerializeField] private Button Button_attack;
    [SerializeField] private Button Button_strongAttack;
    [SerializeField] private Button Button_dash;
    [SerializeField] private Button Button_heal;
    [SerializeField] private Button Button_use;

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
