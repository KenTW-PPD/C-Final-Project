using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Normoral_people : Character
{
    public Sprite icon;
    private List<Weapons> ActiveWeapons = new List<Weapons>();
    private void Awake()
    {
        setHealth(50.0f);
        setMoveSpeed(10.0f);
        setDefense(1.0f);
        setAttackPower(10.0f);
        setPickupRangeSize(50.0f);
        ExperiencePoint = 0;

        foreach (Weapons Weapon in WeaponList)
        {
            Weapons w = Instantiate(Weapon,Vector2.zero,Quaternion.identity);
            w.GetComponent<SpriteRenderer>().enabled = false;
            w.setEquiped(true);
            //Debug.Log(w);
            ActiveWeapons.Add(w);
            StartCoroutine(AttackRoutine(w));
        }
        // �N�����w���W�S�w����List�л\������List
        WeaponList = ActiveWeapons;
    }

    public override UpgradeOption GenerateOption(int rarity)
    {
        int r = Random.Range(0,5);
        float n;
        UpgradeOption option = new UpgradeOption();
        option.icon = icon;
        switch (r)
        {
            case 0:
                n = Random.Range(0.1f * rarity * rarity, 0.5f * rarity * rarity);
                option.Name = "Attack Power Up";
                option.Description = $"Increase Attack Power by {n:F1}";
                // lambda ��F�������ܶq n ����
                option.OnSelect = () => { this.setAttackPower(getAttackPower() + n); };
                break;
            case 1:
                n = Random.Range(0.01f * rarity * rarity, 0.05f * rarity * rarity);
                option.Name = "Defense Up";
                option.Description = $"Increase Defense by {n:F1}";
                option.OnSelect = () => { this.setDefense(getDefense() + n); };
                break;
            case 2:
                n = Random.Range(0.01f * rarity * rarity, 0.1f * rarity * rarity);
                option.Name = "Move Speed Up";
                option.Description = $"Increase Move Speed by {n:F1}";
                option.OnSelect = () => { setMoveSpeed(getMoveSpeed() + n); }; 
                break;
            case 3:
                n = Random.Range(0.5f * rarity * rarity, 2f * rarity * rarity);
                option.Name = "Health Up";
                option.Description = $"Increase Health by {n:F1}";
                option.OnSelect = () => { setHealth(getHealth() + n); };
                break;
            default:
                n = Random.Range(0.3f * rarity * rarity, 1.0f * rarity * rarity);
                option.Name = "Pickup Range Size Up";
                option.Description = $"Increase Pickup Range Size by {n:F1}";
                option.OnSelect = () => { setPickupRangeSize(getPickupRangeSize() + n); };
                
                break;
        }
        return option;
    }

}
