using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public RealTimeCharacter owner;
    public int weaponDamage = 10;

    [Header("Hitbox Settings")]
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers; 

    // 这个函数由 Animation Event 调用
    public void ExecuteAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemyCollider in hitEnemies)
        {
            RealTimeCharacter enemy = enemyCollider.GetComponent<RealTimeCharacter>();

            if (enemy != null && enemy != owner)
            {
                CombatManagerRealTime.Instance.ProcessAttack(owner, enemy, weaponDamage);
            }
        }
    }

    // 在编辑器中绘制攻击范围
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
