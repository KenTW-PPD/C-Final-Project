using System.Collections;
using UnityEngine;
using System.Collections.Generic;
public class Monsters : MonoBehaviour
{
    public GameObject scene;
    // 燒傷相關
    public float FireDamage;
    [SerializeField] private bool isFire = false;
    WaitForSeconds FireTickWait = new WaitForSeconds(1.0f);
    public Coroutine FireCoroutine;


    public GameObject target = null;
    // 相關數據
    public int level = 0;
    [SerializeField] private float Health;
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float AttackPower;
    [SerializeField] private string AttackMode; // ranged 遠程 or melee 近戰
    // 掉落物
    public GameObject Exp;
    public Chest chest;



    // 設定 / 取得 變數相關
    public void setHealth(float health) { Health = health; }
    public void setMoveSpeed(float moveSpeed) { MoveSpeed = moveSpeed; }
    public void setAttackPower(float attackPower) { AttackPower = attackPower; }
    public void setAttackMode(string mode) { AttackMode = mode; }
    public void setFire(bool f) { isFire = f; }

    public bool isOnFire() { return isFire; }

    public float getHealth() { return Health; }
    public float getMoveSpeed() { return MoveSpeed; }
    public float getAttackPower() { return AttackPower; }
    public string getAttackMode() { return AttackMode; }

    public void Move()
    {
        if(target != null)
        {
            Vector2 targetPosition = new Vector2(target.transform.position.x, target.transform.position.y);
            Vector2 mosterPosition = new Vector2(this.transform.position.x, this.transform.position.y);
            this.transform.position = Vector2.MoveTowards(mosterPosition, targetPosition, MoveSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Health -= collision.GetComponent<Bullet>().getDamage();
        }
    }
    
    private void DropItem()
    {
        if(Health <= 0)
        {
            // 順便計算分數(殺敵數)
            scene.GetComponent<Scenes>().addScore(1);

            Instantiate(Exp, this.transform.position, Quaternion.identity);
            int r = Random.Range(0, 100);
            if(r >= 90)
            {
                // 10% 機率掉落物品
                Instantiate(chest,this.transform.position,Quaternion.identity);
            }
            Destroy(this.gameObject);
        }
    }

    public void SetOnFire()
    {
        if (isFire)
        {
            // 確保不會重複啟動多個協程
            if (FireCoroutine == null)
                FireCoroutine = StartCoroutine(OnFire());
        }
        else
        {
            if (FireCoroutine != null)
            {
                StopCoroutine(FireCoroutine);
                FireCoroutine = null;
            }
        }
    }

    IEnumerator OnFire()
    {
        while (true)
        {
            if (isFire)
            {
                Health -= FireDamage;
            }
            yield return FireTickWait;
        }
    }
    // 變更至子項實現
    /*
    public void Upgrade()
    {
        int random = Random.Range(1, 101);
        // 怪獸機率性升級成精英怪 機率取5%
        if(random > 95)
        {
            setHealth(Health * 1.25f);
            setAttackPower(AttackPower * 1.25f);
            this.gameObject.transform.localScale *= 1.5f;
        }
    }*/


    void Update()
    {
        SetOnFire();
        Move();
        DropItem();
    }
}