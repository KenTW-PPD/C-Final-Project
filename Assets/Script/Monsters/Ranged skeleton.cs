using System.Collections;
using UnityEngine;

public class Rangedskeleton : Monsters
{
    private float AttackRange = 30.0f;
    public MonstersBullet bullet;
    [SerializeField] private float AttackFrequency = 2.0f;
    [SerializeField] private float distance = 100;
    void Start()
    {
        setHealth(10f + level * 10);
        setAttackPower(3 + level * 2);

        setMoveSpeed(1.5f);
        
        setAttackMode("Ranged");
        target = GameObject.FindWithTag("Player");
        scene = GameObject.FindWithTag("Scenes");
        StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        while (true)
        {
            if(distance <= AttackRange)
            {
                MonstersBullet newbullet = Instantiate(bullet, this.transform.position, Quaternion.identity);
                newbullet.target = target.transform.position;
                newbullet.setBullet(getAttackPower());
            }
           

            yield return new WaitForSeconds(AttackFrequency);
        }

    }
    private void FixedUpdate()
    {
        distance = Vector2.Distance(this.transform.position, target.transform.position);
    }

}
