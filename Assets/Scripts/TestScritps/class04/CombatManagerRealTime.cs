using UnityEngine;

public class CombatManagerRealTime : MonoBehaviour
{
    public static CombatManagerRealTime Instance;

    private void Awake()
    {
        Instance = this;
    }

    // 处理攻击请求
    public void ProcessAttack(RealTimeCharacter attacker, RealTimeCharacter victim, int skillPower)
    {
        int rawDamage = attacker.info.attackPower + skillPower;
        int finalDamage = Mathf.Max(0, rawDamage - victim.info.defensePower);
        
        victim.OnTakeDamage(finalDamage);
    }
}
