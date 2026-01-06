using System.Collections;
using UnityEngine;

public class Boss02 : Monsters
{
    private float AttackRange = 50.0f;
    public MonstersBullet bullet;
    [SerializeField] private float distance = 100;
    WaitForSeconds AttackFrequency = new WaitForSeconds(1.0f);
    private void Start()
    {
        setHealth(200.0f);
        setAttackPower(8.0f);

        setMoveSpeed(2.0f);
        target = GameObject.FindWithTag("Player");
        scene = GameObject.FindWithTag("Scenes");
        setAttackMode("Ranged");
        StartCoroutine(Fire());
    }
    public override void DropItem()
    {
        if (getHealth() <= 0)
        {
            scene.GetComponent<Scenes>().addScore(1);
            for (int i = 0; i < 50; i++)
                Instantiate(Exp, this.transform.position, Quaternion.identity);
            for (int i = 0; i < 8; i++)
            {
                int x = Random.Range(1, 11);
                int y = Random.Range(1, 11);
                Vector2 spawunPosition = new Vector2(this.transform.position.x + x, this.transform.position.y + y);
                Instantiate(chest, spawunPosition, Quaternion.identity);
            }
            Destroy(this.gameObject);
        }
    }
    IEnumerator Fire()
    {
        while (true)
        {
            if (distance <= AttackRange)
            {
                MonstersBullet newbullet = Instantiate(bullet, this.transform.position, Quaternion.identity);
                newbullet.target = target.transform.position;
                newbullet.setBullet(getAttackPower(), 7f);
            }


            yield return AttackFrequency;
        }

    }
    private void FixedUpdate()
    {
        distance = Vector2.Distance(this.transform.position, target.transform.position);
    }
}
