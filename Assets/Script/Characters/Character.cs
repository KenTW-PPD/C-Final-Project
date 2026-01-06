using JetBrains.Annotations;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
public class Character : MonoBehaviour , Prize
{
    // 其他
    public LayerMask enemyLayer;
    public GameOverUI gameover;
    
    // 所持有武器
    public List<Weapons> WeaponList;

    // 角色屬性相關
    [SerializeField] private float Health;
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float Defense;
    [SerializeField] private float AttackPower;
    [SerializeField] private float PickupRangeSize;

    // 經驗、等級相關
    public float ExperiencePoint;
    [SerializeField] private int Level;
    public UpgradeUI upgradeUI;
    public float getPickupRangeSize() { return PickupRangeSize; }
    public float getAttackPower() { return AttackPower; }
    public float getHealth() { return Health; }
    public float getMoveSpeed() { return MoveSpeed; }
    public float getDefense() { return Defense; }
    public int getLevel() { return Level; }

    public void setPickupRangeSize(float size) { PickupRangeSize = size; }
    public void setHealth(float health) { Health = health; }
    public void setMoveSpeed(float moveSpeed) { MoveSpeed = moveSpeed; }
    public void setDefense(float defense) { Defense = defense; }
    public void setAttackPower(float attackPower) { AttackPower = attackPower; }
    public void setLevel(int level) { Level = level; }

    public virtual UpgradeOption GenerateOption(int rarity)
    {
        return null;
    }
    public void AddWeapon(Weapons Weapon)
    {
        Weapons w = Instantiate(Weapon, Vector2.zero, Quaternion.identity);
        w.GetComponent<SpriteRenderer>().enabled = false;
        w.setEquiped(true);
        WeaponList.Add(w);
        StartCoroutine(AttackRoutine(w));
    }
    public void LevelUP()
    {
        // 設一公式決定下一等級升級所需經驗值
        float target = 300 * Level * 0.1f + ((Level - 1) * 1.1f);
        if (ExperiencePoint >= target)
        {
            ExperiencePoint -= target;
            Level += 1;
            // 升級介面
            Time.timeScale = 0f;
            upgradeUI.GetComponent<Canvas>().enabled = true;
            upgradeUI.BuildUpgradeUI();
        }

    }

    private void FindtheNearestMonster(Weapons Weapon)
    {
        // 抓取特定範圍內的Monster
        Collider2D[] monsters = Physics2D.OverlapCircleAll(this.transform.position,
            Weapon.getAttackRange(),enemyLayer);

        float minDistance = Mathf.Infinity;
        Monsters nearestMonster = null;
        foreach (Collider2D collider in monsters)
        {
            float distance = (collider.transform.position - this.transform.position).sqrMagnitude;
            if(distance < minDistance)
            {
                minDistance = distance;
                nearestMonster = collider.GetComponent<Monsters>();
            }
            
        }
        //Debug.Log("Nearest Monster: " + nearestMonster);
        // 攻擊
        float power = AttackPower + Weapon.getAttackPower();
        if (nearestMonster != null)
        {
            Weapon.Fire(this.transform.position, nearestMonster.transform.position, power);
        }
        //else { Debug.LogWarning("搜尋結束：範圍內沒有任何標籤為 'Monsters' 的物件！"); }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.CompareTag("XP"))
        {
            //Debug.Log("Get XP");
            Experience_Points EXP = collision.GetComponent<Experience_Points>();
            ExperiencePoint += EXP.getExperiencePoint();
            LevelUP();
        }
        if(collision.CompareTag("Monsters"))
        {
            Monsters monsters = collision.GetComponent<Monsters>();
            Health -= monsters.getAttackPower() / Defense;
        }
        if (collision.CompareTag("MonstersBullet"))
        {
            MonstersBullet bullet = collision.GetComponent<MonstersBullet>();
            Health -= bullet.getDamage() / Defense;

        }
    }

    public IEnumerator AttackRoutine(Weapons Weapon)
    {
        while (true)
        {
            FindtheNearestMonster(Weapon);
            //Debug.Log("Attack with " + Weapon.name);
            yield return new WaitForSeconds(Weapon.getAttackSpeed());
        }
    }



    void Update()
    {
        if(Health <= 0)
        {
            gameover.GameOver();
        }
        
    }

    // 確定攻擊範圍用的
    /*
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        // 這裡的20.0f跟 Weapon.getAttackRange() 一致
        Gizmos.DrawWireSphere(this.transform.position, 20.0f);
    }
    */
    private void Start()
    {
        upgradeUI = GameObject.FindWithTag("UpgradeUI").GetComponent<UpgradeUI>();
    }
}
