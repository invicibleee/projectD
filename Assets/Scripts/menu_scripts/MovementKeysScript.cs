using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InputSettings : MonoBehaviour
{
    public static InputSettings Instance { get; private set; }

    [SerializeField] public Button Button_ult;
    [SerializeField] public Button Button_s;
    [SerializeField] public Button Button_jump;
    [SerializeField] public Button Button_ability;
    [SerializeField] public Button Button_attack;
    [SerializeField] public Button Button_dash;
    [SerializeField] public Button Button_heal;
    [SerializeField] public Button Button_use;

    Dictionary<Button, KeyCode> buttonKeyMap = new Dictionary<Button, KeyCode>();
    private KeyCode[] reservedKeys = { KeyCode.A, KeyCode.D, KeyCode.M, KeyCode.Escape };

    private const string saveKey = "KeyBindings";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Load();
        //SetupButton(Button_ult, "R");
        //SetupButton(Button_s, "S");
        //SetupButton(Button_jump, "Space");
        //SetupButton(Button_ability, "F");
        //SetupButton(Button_attack, "LM");
        //SetupButton(Button_dash, "LeftShift");
        //SetupButton(Button_heal, "C");
        //SetupButton(Button_use, "E");
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
                    Save();
                    break;
                }
            }
        }
    }

    bool IsKeyAssigned(KeyCode newKeyCode)
    {
        if (System.Array.Exists(reservedKeys, key => key == newKeyCode))
        {
            return true;
        }
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

    public KeyCode GetKeyForAction(Button button)
    {
        if (buttonKeyMap.ContainsKey(button))
        {
            return buttonKeyMap[button];
        }
        else
        {
            Debug.LogError("Button not found in buttonKeyMap: " + button);
            return KeyCode.None;
        }
    }

    public void Save()
    {
        SaveManager.Save(saveKey, GetData());
    }


    private void Load()
    {
        var data = SaveManager.Load<SaveData.ButtonsSave>(saveKey);
        SetupButton(Button_ult, data._keyCode[0]);
        SetupButton(Button_s, data._keyCode[1]);
        SetupButton(Button_jump, data._keyCode[2]);
        SetupButton(Button_ability, data._keyCode[3]);
        SetupButton(Button_attack, data._keyCode[4]);
        SetupButton(Button_dash, data._keyCode[5]);
        SetupButton(Button_heal, data._keyCode[6]);
        SetupButton(Button_use, data._keyCode[7]);

    }

    private SaveData.ButtonsSave GetData()
    {
        var data = new SaveData.ButtonsSave
        {
            _keyCode = new string[]
            {
                GetKeyText(buttonKeyMap[Button_ult]),
                GetKeyText(buttonKeyMap[Button_s]),
                GetKeyText(buttonKeyMap[Button_jump]),
                GetKeyText(buttonKeyMap[Button_ability]),
                GetKeyText(buttonKeyMap[Button_attack]),
                GetKeyText(buttonKeyMap[Button_dash]),
                GetKeyText(buttonKeyMap[Button_heal]),
                GetKeyText(buttonKeyMap[Button_use])
            }
        };

        return data;
    }
}
