public enum AttackDirection
{
    PlayerToEnemy,
    EnemyToPlayer
}

// 攻击包：封装一次攻击的所有信息
public class AttackPackage
{
    public AttackDirection direction;
    public float totalDamage; // 最终计算出的伤害值
    public CombatSkill skillUsed; // 使用的技能
}
