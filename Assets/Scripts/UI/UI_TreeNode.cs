using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_TreeNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private Image skillIcon;
    [SerializeField] private string lockedColorHex = "#878787";
    private Color lastColor;
    public bool isUnlocked;
    public bool isLocked;

    private void Awake()
    {
        UpdateIconColor(GetColorByHex(lockedColorHex));
    }

    private void Unlock()
    {
        isUnlocked = true;
        UpdateIconColor(Color.white);
    }
    public bool CanBeUnlocked()
    {
        if (isLocked || isUnlocked)
            return false;

        return true;
    }


    private void UpdateIconColor(Color color)
    {
        if (skillIcon == null)
            return;
        lastColor = skillIcon.color;
        skillIcon.color = color;

    }



    public void OnPointerDown(PointerEventData eventData)
    {
        if (CanBeUnlocked())
            Unlock();
        else
            Debug.Log("Cant be unlocked");

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isUnlocked == false)
            UpdateIconColor(Color.white * .9f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isUnlocked == false)
            UpdateIconColor(lastColor);
    }

    private Color GetColorByHex(string hexNumber)
    {
        ColorUtility.TryParseHtmlString(hexNumber, out Color color);
        return color;
    }
}
