using UnityEngine;

public class Player_SkillManager : MonoBehaviour
{
    public Skill_Dash dash { get; private set; }
    private void Awake()
    {
        dash = GetComponentInChildren<Skill_Dash>();
    }

    public Skill_Base GetSkillByType(SkillType type)
    {
        switch (type)
        {
            case SkillType.Dash: return dash;
            default:
                Debug.Log($"Skill type {type} not implemented yet.");
                return null;
        }
        
    }

}
