using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Canvas")]
    public GameObject inventoryCanvas;

    [Header("Sistemas")]
    public Inventory inventory;

    private bool isInventoryOpen = false;

    private void Awake()
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

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isInventoryOpen) CloseInventory();
            else OpenInventory();
        }
    }

    public void OpenInventory()
    {
        inventoryCanvas?.SetActive(true);
        isInventoryOpen = true;
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseInventory()
    {
        inventoryCanvas?.SetActive(false);
        isInventoryOpen = false;
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}
