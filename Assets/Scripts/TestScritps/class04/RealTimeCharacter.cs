using UnityEngine;

public class RealTimeCharacter : MonoBehaviour
{
    public CombatCharacterInfo info;

    // 当受到攻击时被调用
    public void OnTakeDamage(int damage)
    {
        info.currentHp -= damage;
        Debug.Log($"{name} took {damage} damage. HP: {info.currentHp}");

        if (info.currentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{name} died.");
        Destroy(gameObject);
    }
}
