using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UiInventoryHandle : MonoBehaviour
{
    public ItemDataBase dataBase;
    public Inventory inventory;
    public GameObject item;
    public List<GameObject> list = new();
    public ScrollRect scrollRect;

    private GameObject searchedButton;
    private int selectedItemId;

    [SerializeField] CanvasGroup previewPanel;
    [SerializeField] Image iconPreviewImage;
    [SerializeField] TextMeshProUGUI descriptionTextPreview;
    [SerializeField] TextMeshProUGUI nameTextPreview;
    private void Start()
    {
        instaciateButtons();
        ShowItems();
        inventory.ItemAdded += ShowItems;
        inventory.ItemRemoved += ShowItems;
    }
    public void instaciateButtons()
    {
        for (int i = 0; i < 9; i++)
        {
            GameObject theButtons = Instantiate(item, scrollRect.content);

            theButtons.SetActive(false);
            list.Add(theButtons);
        }
    }
    public void ShowItems()
    {
        foreach (var item in inventory.Items)
        {
            ItemData itemsData = dataBase.SearchItemByID(item.Key);
            searchedButton = list.Find(x => x.activeSelf == false);
            searchedButton.SetActive(true);
            searchedButton.transform.Find("Icon").GetComponent<Image>().sprite = itemsData.icon;
            searchedButton.GetComponent<Button>().onClick.AddListener(delegate
            {
                selectedItemId = item.Key;
                ShowItemPreview(itemsData);
                ShowPreviewPanel();
            });
        }
    }
    public void ShowItemPreview(ItemData itemData)
    {
        iconPreviewImage.sprite = itemData.icon;
        descriptionTextPreview.text = itemData.description;
        nameTextPreview.text = itemData.itemName;
    }

    public void ShowPreviewPanel()
    {
        previewPanel.alpha = 1f;
        previewPanel.interactable = true;
        previewPanel.blocksRaycasts = true;
    }

    public void DeleteItem()
    {
        inventory.RemoveItem(selectedItemId);
    }
    public void UseSelectedItem()
    {
        if (selectedItemId != 0)
        {
            inventory.UseItem(selectedItemId);
        }
    }
}
