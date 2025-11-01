using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ItemSlot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Inventory_Item itemInSlot { get; private set; }
    protected Inventory_Player inventory;
    protected UI ui;
    protected RectTransform rect;

    [Header("UI Slot Setup")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemStackSize;
    [SerializeField] private Sprite defaultIcon;

    protected void Awake()
    {
        ui = GetComponentInParent<UI>();
        rect = GetComponent<RectTransform>();
        inventory = FindFirstObjectByType<Inventory_Player>();
    }


    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (itemInSlot == null || itemInSlot.itemData.itemType == ItemType.Material)
            return;

        inventory.TryEquipItem(itemInSlot);

        if (itemInSlot == null)
            ui.itemTooltip.ShowToolTip(false, null);
        else
            ui.itemTooltip.ShowToolTip(true, rect, itemInSlot);
    }

    public void UpdateSlot(Inventory_Item item)
    {
        itemInSlot = item;

        if (item == null)
        {
            itemStackSize.text = "";
            itemIcon.color = new Color(1, 1, 1, 0.15f);
            itemIcon.sprite = defaultIcon;
            return;
        }

        Color color = Color.white; color.a = .9f;
        itemIcon.color = color;
        itemIcon.sprite = itemInSlot.itemData.itemIcon;
        itemStackSize.text = item.stackSize > 1 ? item.stackSize.ToString() : "";
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemInSlot == null)
            return;

        ui.itemTooltip.ShowToolTip(true, rect, itemInSlot);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.itemTooltip.ShowToolTip(false, null);
    }


}
