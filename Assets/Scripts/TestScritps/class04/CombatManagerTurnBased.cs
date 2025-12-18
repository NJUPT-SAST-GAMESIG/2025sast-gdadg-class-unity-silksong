using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CombatManagerTurnBased : MonoBehaviour
{
    public static CombatManagerTurnBased Instance;

    [Header("UI References")]
    public Transform skillFrameContainer;
    public GameObject skillFramePrefab;
    public Slider playerHpBar;
    public Slider enemyHpBar;

    [Header("Battle Info")]
    public CombatCharacterInfo playerInfo;
    public CombatCharacterInfo enemyInfo;

    private AttackDirection currentTurn;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // 模拟进入战斗，ID 0 vs ID 1
        InitializeBattle(0, 1);
    }

    public void InitializeBattle(int playerId, int enemyId)
    {
        playerInfo = ItemInfoCombat.GetCharacterInfo(playerId);
        enemyInfo = ItemInfoCombat.GetCharacterInfo(enemyId);
        
        InitializeUI();

        currentTurn = AttackDirection.PlayerToEnemy;
    }

    private void InitializeUI()
    {
        // 初始化血条
        UpdateHpBars();
        
        foreach (var skill in playerInfo.skillList)
        {
            GameObject go = Instantiate(skillFramePrefab, skillFrameContainer);
            SkillFrame frame = go.GetComponent<SkillFrame>();
            frame.Setup(skill); 
        }
    }

    // 执行攻击的入口函数
    public void ExecuteAttack(CombatSkill skill, AttackDirection direction)
    {
        if (direction != currentTurn) return;

        StartCoroutine(ProcessTurn(skill, direction));
    }

    private IEnumerator ProcessTurn(CombatSkill skill, AttackDirection direction)
    {
        CombatCharacterInfo attacker = (direction == AttackDirection.PlayerToEnemy) ? playerInfo : enemyInfo;
        CombatCharacterInfo defender = (direction == AttackDirection.PlayerToEnemy) ? enemyInfo : playerInfo;

        int rawDamage = skill.attackPower + attacker.attackPower;
        int realDamage = Mathf.Max(0, rawDamage - defender.defensePower);

        defender.currentHp -= realDamage;
        if (defender.currentHp < 0) defender.currentHp = 0;

        Debug.Log($"{attacker.name} used {skill.skillName} dealing {realDamage} damage!");

        UpdateHpBars();

        if (defender.currentHp <= 0)
        {
            Debug.Log($"{defender.name} Defeated!");
            yield break;
        }

        SwitchTurn();
    }

    private void SwitchTurn()
    {
        if (currentTurn == AttackDirection.PlayerToEnemy)
        {
            currentTurn = AttackDirection.EnemyToPlayer;
            StartCoroutine(EnemyTurnRoutine());
        }
        else
        {
            currentTurn = AttackDirection.PlayerToEnemy;
        }
    }

    private IEnumerator EnemyTurnRoutine()
    {
        yield return new WaitForSeconds(1.5f); 
        
        int randomIndex = Random.Range(0, enemyInfo.skillList.Count);
        CombatSkill randomSkill = enemyInfo.skillList[randomIndex];

        ExecuteAttack(randomSkill, AttackDirection.EnemyToPlayer);
    }

    private void UpdateHpBars()
    {
        if (playerHpBar) playerHpBar.value = playerInfo.currentHp / playerInfo.maxHp;
        if (enemyHpBar) enemyHpBar.value = enemyInfo.currentHp / enemyInfo.maxHp;
    }
}
