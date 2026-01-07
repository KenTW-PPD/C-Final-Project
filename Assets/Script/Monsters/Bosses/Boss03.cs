using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Boss03 : Monsters
{
    public GameOverUI ui;
    public TextMeshProUGUI title;
    private float AttackRange = 50.0f;
    public MonstersBullet bullet;
    [SerializeField] private float distance = 100;
    WaitForSeconds AttackFrequency = new WaitForSeconds(1.0f);



    private void Start()
    {
        setHealth(200.0f);
        setAttackPower(10.0f);

        setMoveSpeed(3.5f);
        target = GameObject.FindWithTag("Player");
        scene = GameObject.FindWithTag("Scenes");
        setAttackMode("Ranged");
        StartCoroutine(Fire());
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
    public void GameClear()
    {
        ui.GameOver();
        title.text = "Game Clear!";
    }
    public override void DropItem()
    {
        if (getHealth() <= 0)
        {
            GameClear();
            Destroy(this.gameObject);
        }
    }
}
