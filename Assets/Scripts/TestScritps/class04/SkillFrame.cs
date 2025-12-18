using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillFrame : MonoBehaviour
{
    public TMP_Text skillNameText;
    public TMP_Text damageText;

    private CombatSkill mySkill;

    public void Setup(CombatSkill skill)
    {
        mySkill = skill;
        skillNameText.text = skill.skillName;
        damageText.text = skill.attackPower.ToString();
    }
    
    public void OnClick()
    {
        CombatManagerTurnBased.Instance.ExecuteAttack(mySkill, AttackDirection.PlayerToEnemy);
    }
}
