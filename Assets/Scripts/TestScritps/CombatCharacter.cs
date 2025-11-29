using UnityEngine;

public class CombatCharacter : MonoBehaviour
{
    public int characterID; // 用于在数据库中查找对应的Info
    public CombatCharacterInfo info; // 运行时存储的角色数据
}
