using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIHandler : MonoBehaviour
{
    [Header("Script References")]
    [Space(8)]
    [SerializeField] Inventory inventory;
    [SerializeField] ItemsDataBaseSO dataBase;

    [Header("Items")]
    [Space(8)]
    [SerializeField] GameObject itemButton;
    [SerializeField] List<GameObject> instantiatedButtons = new();
    private int selectedItemId;

    [Header("Canvas")]
    [Space(8)]
    [SerializeField] ScrollRect itemsScroll;
    [SerializeField] CanvasGroup previewPanel;
    [SerializeField] Image iconPreviewImage;
    [SerializeField] TextMeshProUGUI amountTextPreview;
    [SerializeField] TextMeshProUGUI nameTextPreview;
    


    private void Start()
    {
        InstantiateButtons();
        ShowItems();
    }

    public void InstantiateButtons()
    {
        for (int i = 0; i < dataBase.Items.Count; i++)
        {
            GameObject theButtons = Instantiate(itemButton, itemsScroll.content);

            theButtons.SetActive(false);

            instantiatedButtons.Add(theButtons);
        }
    }

    public void ShowItems()
    {
        foreach (var item in inventory.Items)
        {
            AtributtesControllerSO itemsData = dataBase.SearchItemByID(item.Key);

            GameObject searchedButton = instantiatedButtons.Find(x => x.activeSelf == false);
            searchedButton.SetActive(true);
            searchedButton.transform.Find("Icon").GetComponent<Image>().sprite = itemsData.Icon;
            searchedButton.transform.Find("Quantity").GetComponent<TextMeshProUGUI>().text = item.Value.ToString();
            searchedButton.GetComponent<Button>().onClick.AddListener(delegate
            {
                selectedItemId = item.Key;
                ShowItemPreview(itemsData, item.Value);
                ShowPreviewPanel();
            });
        }
    }

    public void ShowItemPreview (AtributtesControllerSO itemData, int amount) 
    {
        iconPreviewImage.sprite = itemData.Icon;
        amountTextPreview.text = itemData.Name;
        nameTextPreview.text = amount.ToString();
    }

    public void ShowPreviewPanel () 
    {
        previewPanel.alpha = 1f;
        previewPanel.interactable = true;
        previewPanel.blocksRaycasts = true;
    }

    public void DeleteItem ()
    {
        inventory.RemoveItem(selectedItemId, 1);
    }
}


