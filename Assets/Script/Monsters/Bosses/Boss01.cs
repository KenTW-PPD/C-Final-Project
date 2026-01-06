using UnityEngine;

public class Boss01 : Monsters
{
    private void Start()
    {
        setHealth(120.0f);
        setAttackPower(5.0f);

        setMoveSpeed(3.0f);
        target = GameObject.FindWithTag("Player");
        scene = GameObject.FindWithTag("Scenes");
        setAttackMode("Melee");
    }


    public override void DropItem()
    {
        if (getHealth() <= 0)
        {
            scene.GetComponent<Scenes>().addScore(1);
            for (int i = 0; i < 30; i++)
                Instantiate(Exp, this.transform.position, Quaternion.identity);
            for (int i = 0; i < 4; i++)
            {
                int x = Random.Range(1, 11);
                int y = Random.Range(1, 11);
                Vector2 spawunPosition = new Vector2(this.transform.position.x + x, this.transform.position.y + y);
                Instantiate(chest, spawunPosition, Quaternion.identity);
            }
            Destroy(this.gameObject);
        }
    }
}
