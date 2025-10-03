using System.Collections.Generic;
using TMPro;
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
        InstanciateButtons();

        // Desuscribirse primero para evitar duplicación
        inventory.ItemAdded -= ShowItems;
        inventory.ItemRemoved -= ShowItems;

        // Suscribirse limpiamente
        inventory.ItemAdded += ShowItems;
        inventory.ItemRemoved += ShowItems;

        ShowItems();
    }

    private void OnDestroy()
    {
        // Limpiar suscripciones al destruir el objeto
        if (inventory != null)
        {
            inventory.ItemAdded -= ShowItems;
            inventory.ItemRemoved -= ShowItems;
        }
    }

    public void InstanciateButtons()
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
        // PASO 1: Desactivar todos los botones primero
        foreach (GameObject btn in list)
        {
            btn.SetActive(false);
            Button buttonComponent = btn.GetComponent<Button>();
            if (buttonComponent != null)
            {
                buttonComponent.onClick.RemoveAllListeners();
            }
        }

        // PASO 2: Activar solo los botones necesarios (uno por cada item único)
        int buttonIndex = 0;

        foreach (var item in inventory.Items)
        {
            if (buttonIndex >= list.Count) break;

            ItemData itemsData = dataBase.SearchItemByID(item.Key);
            if (itemsData == null)
            {
                Debug.LogWarning($"Item con ID {item.Key} no encontrado en la base de datos");
                continue;
            }

            // Activar y configurar el botón
            GameObject currentButton = list[buttonIndex];
            currentButton.SetActive(true);

            // Asignar el icono
            Image iconImage = currentButton.transform.Find("Icon").GetComponent<Image>();
            if (iconImage != null)
            {
                iconImage.sprite = itemsData.icon;
            }

            // Capturar variables para el closure
            int currentItemId = item.Key;
            ItemData currentItemData = itemsData;

            // Asignar listener limpio
            Button btn = currentButton.GetComponent<Button>();
            btn.onClick.AddListener(() =>
            {
                selectedItemId = currentItemId;
                ShowItemPreview(currentItemData);
                ShowPreviewPanel();
            });

            buttonIndex++;
        }

        Debug.Log($"Mostrando {buttonIndex} items únicos en el UI");
    }

    public void ShowItemPreview(ItemData itemData)
    {
        if (itemData == null) return;

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

    public void HidePreviewPanel()
    {
        previewPanel.alpha = 0f;
        previewPanel.interactable = false;
        previewPanel.blocksRaycasts = false;
    }

    public void DeleteItem()
    {
        if (selectedItemId != 0)
        {
            inventory.RemoveItem(selectedItemId);
            HidePreviewPanel();
            selectedItemId = 0; // Reset
        }
    }

    public void UseSelectedItem()
    {
        if (selectedItemId != 0)
        {
            inventory.UseItem(selectedItemId);
            HidePreviewPanel();
        }
    }
}