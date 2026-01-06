using UnityEngine;

public class zombie : Monsters
{
    void Start()
    {
        setHealth(15f + level * 10);
        setAttackPower(2f + level * 2);

        setMoveSpeed(2.0f);
        
        setAttackMode("Melee");
        target = GameObject.FindWithTag("Player");
        scene = GameObject.FindWithTag("Scenes");
    }



}
