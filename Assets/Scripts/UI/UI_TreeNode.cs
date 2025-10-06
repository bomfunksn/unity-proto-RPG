using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_TreeNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{

    private UI ui;
    private RectTransform rect;
    [SerializeField] private Skill_DatatSO skillData;
    [SerializeField] private string skillName;
    [SerializeField] private Image skillIcon;
    [SerializeField] private string lockedColorHex = "#878787";
    private Color lastColor;
    public bool isUnlocked;
    public bool isLocked;



    private void Awake()
    {

        ui = GetComponentInParent<UI>();
        rect = GetComponent<RectTransform>();
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
        ui.skillTooltip.ShowToolTip(true, rect, skillData);

        if (isUnlocked == false)
            UpdateIconColor(Color.white * .9f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.skillTooltip.ShowToolTip(false, rect);

        if (isUnlocked == false)
            UpdateIconColor(lastColor);
    }

    private Color GetColorByHex(string hexNumber)
    {
        ColorUtility.TryParseHtmlString(hexNumber, out Color color);
        return color;
    }

        private void OnValidate()
    {
        if (skillData == null)
            return;

        skillName = skillData.displayName;
        skillIcon.sprite = skillData.icon;
        gameObject.name = "UI_TreeNode - " + skillData.displayName;
    }
}
