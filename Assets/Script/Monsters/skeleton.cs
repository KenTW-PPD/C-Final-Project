using UnityEngine;

public class skeleton : Monsters
{
    
    void Start()
    {
        setHealth(10 + level * 10);
        setAttackPower(1 + level * 2);

        setMoveSpeed(3.5f);

        setAttackMode("Melee");
        target = GameObject.FindWithTag("Player");
        scene = GameObject.FindWithTag("Scenes");
    }
}
