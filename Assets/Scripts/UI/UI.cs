using UnityEngine;

public class UI : MonoBehaviour
{
    public UI_SkillToolTip skillTooltip;
    public UI_SkillTree skillTree;
    public bool skillTreeEnabled;

    private void Awake()
    {
        skillTooltip = GetComponentInChildren<UI_SkillToolTip>();
        skillTree = GetComponentInChildren<UI_SkillTree>(true);
    }
    public void ToggleSkillTreeUI()
    {
        skillTreeEnabled = !skillTreeEnabled;
        skillTree.gameObject.SetActive(skillTreeEnabled);
        skillTooltip.ShowToolTip(false, null);
    }
}
