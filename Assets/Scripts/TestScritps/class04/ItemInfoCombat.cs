using System.Collections.Generic;

public class ItemInfoCombat
{
    // 模拟数据库：ID -> 角色信息
    private static Dictionary<int, CombatCharacterInfo> characterDatabase;

    static ItemInfoCombat()
    {
        characterDatabase = new Dictionary<int, CombatCharacterInfo>();

        // 初始化玩家数据 (ID: 0)
        CombatCharacterInfo player = new CombatCharacterInfo("Player", 200, 10, 5);
        player.skillList.Add(new CombatSkill("Attack One", 20));
        player.skillList.Add(new CombatSkill("Super Attack", 50));
        characterDatabase.Add(0, player);

        // 初始化敌人数据 (ID: 1)
        CombatCharacterInfo enemy = new CombatCharacterInfo("Enemy1", 100, 25, 0);
        enemy.skillList.Add(new CombatSkill("Peck", 30));
        enemy.skillList.Add(new CombatSkill("Dive", 60));
        characterDatabase.Add(1, enemy);
    }

    public static CombatCharacterInfo GetCharacterInfo(int id)
    {
        if (characterDatabase.ContainsKey(id))
        {
            // 注意：这里应该返回数据的深拷贝，防止直接修改数据库源数据
            // 为简化演示，此处直接返回引用（实际项目中请避免）
            return characterDatabase[id];
        }
        return null;
    }
}
