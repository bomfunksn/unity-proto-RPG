using UnityEngine;

public class UI : MonoBehaviour
{
    public UI_SkillToolTip skillTooltip;
    private void Awake()
    {
        skillTooltip = GetComponentInChildren<UI_SkillToolTip>();
    }
}
