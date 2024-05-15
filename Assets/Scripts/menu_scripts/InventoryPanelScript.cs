using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Item
{
    public Sprite sprite;
    public bool isOwned;
    public string description;
    public string name;
}

public class InventoryPanelScript : MonoBehaviour
{

    [SerializeField] private GridLayoutGroup layoutInventory;
    public Item[] items = new Item[10];
    [SerializeField] private Vector2 imageSize = new(100, 100);
    [SerializeField] private Vector3 scale = new(1, 1, 1);
    public static InventoryPanelScript instance;
    private int selecteditemIndex = -1;
    [SerializeField] private Text descriptionText;
    [SerializeField] private Text nameText;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        FindItems();
        descriptionText.text = "select item";
        nameText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FindItems();
            descriptionText.text = "select item";
            nameText.text = "";
        }
    }

    private void FindItems()
    {

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].isOwned && !ItemExistsInInventory(i))
            {
                GameObject newItemObject = new GameObject("Item_" + i);
                Image newItemImage = newItemObject.AddComponent<Image>();
                Button newItemButton = newItemObject.AddComponent<Button>();

                newItemImage.overrideSprite = items[i].sprite;
                newItemImage.rectTransform.localScale = scale;

                newItemImage.rectTransform.sizeDelta = imageSize;

                newItemObject.transform.SetParent(layoutInventory.transform);
                newItemImage.rectTransform.localScale = scale;
                newItemImage.rectTransform.localPosition = new Vector3(newItemImage.rectTransform.localPosition.x,
                newItemImage.rectTransform.localPosition.y, 0f);

                int index = i;
                newItemButton.onClick.AddListener(() => OnItemClick(index));
            } else if (!items[i].isOwned)
            {
                RemoveItemFromInventory(i);
            }
        }
    }

    bool ItemExistsInInventory(int itemId)
    {
        
        for (int i = 0; i < layoutInventory.transform.childCount; i++)
        {
            Transform child = layoutInventory.transform.GetChild(i);
            if (child.name == "Item_" + itemId.ToString())
            {
                return true;
            }
        }

        return false;
    }

    private void RemoveItemFromInventory(int itemId)
    {
        for (int i = 0; i < layoutInventory.transform.childCount; i++)
        {
            Transform child = layoutInventory.transform.GetChild(i);
            if (child.name == "Item_" + itemId.ToString())
            {
                Destroy(child.gameObject);
                return;
            }
        }
    }

    private void OnItemClick(int itemsIndex)
    {
        Debug.Log(itemsIndex);
        UpdateDescriptionText(itemsIndex);
        nameText.text = items[itemsIndex].name;

        if (selecteditemIndex != -1 && selecteditemIndex <= layoutInventory.transform.childCount)
        {
            int prevIndex = FindObjectId(selecteditemIndex);
            layoutInventory.transform.GetChild(prevIndex).localScale = new Vector3(1f, 1f, 1f);
        }

        int currentIndex = FindObjectId(itemsIndex);
        if (currentIndex != -1 && currentIndex <= layoutInventory.transform.childCount)
        {
            layoutInventory.transform.GetChild(currentIndex).localScale = new Vector3(1.1f, 1.1f, 1.1f);
        }

        selecteditemIndex = itemsIndex;
    }

    private void UpdateDescriptionText(int itemsIndex)
    {
        int descriptionIndex = itemsIndex;

        if (descriptionIndex >= 0)
        {
            descriptionText.text = items[descriptionIndex].description;
            nameText.text = items[descriptionIndex].name;
        }
        else
        {
            Debug.LogError("Invalid description index: " + descriptionIndex);
        }
    }

    private int FindObjectId(int index)
    {
        string itemName = "Item_" + index;

        for (int i = 0; i < layoutInventory.transform.childCount; i++)
        {
            Transform child = layoutInventory.transform.GetChild(i);
            if (child.name == itemName)
            {
                return i;
            }
        }

        return -1;
    }

    public void SetItemOwned(int index)
    {
        items[index].isOwned = true;
        Debug.Log("You found item Index: " + index);
        FindItems();

    }
}
