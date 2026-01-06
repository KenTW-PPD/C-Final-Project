using UnityEngine;

public class Gun : Weapons
{
    public Bullet gun_bullet;
    void Awake()
    {
        setPower(5.0f);
        setRange(20.0f);
        setSpeed(0.5f);
        setOneFireBulletCount(1);
        setName("Gun");
        setBullet(gun_bullet);
        setEquiped(false);
    }
    public override UpgradeOption GenerateOption(int rarity)
    {
        UpgradeOption option = new UpgradeOption();
        option.icon = GetComponent<SpriteRenderer>().sprite;

        if(isEquiped())
        {
            float n;
            int r = Random.Range(0, 4);
            switch (r)
            {
                case 0:
                    n = Random.Range(0.1f * rarity * rarity, 0.5f * rarity * rarity);
                    option.Name = "Power Up";
                    option.Description = $"Increase Power by {n:F1}";
                    option.OnSelect = () => { this.setPower(getAttackPower() + n); };
                    break;
                case 1:
                    n = Random.Range(0.01f * rarity * rarity, 0.1f * rarity * rarity);
                    option.Name = "Attack Speed Up";
                    option.Description = $"Increase Attack Speed by {n:F1}";
                    option.OnSelect = () => { 
                        float s = Mathf.Max(0.01f, getAttackSpeed() - n);
                        this.setSpeed(s); };
                    break;
                case 2:
                    n = Random.Range(0.15f * rarity * rarity, 0.45f * rarity * rarity);
                    option.Name = "Attack Range Up";
                    option.Description = $"Increase Attack Range by {n:F1}";
                    option.OnSelect = () => { this.setRange(getAttackRange() + n); };
                    break;
                default:
                    option.Name = "One Fire Bullet Count Up";
                    option.Description = $"Increase One Fire Bullet Count by 1";
                    option.OnSelect = () => { this.setOneFireBulletCount(getOneFireBulletCount() + 1); };
                    break;

            }
        }
        else
        {
            option.Name = "Get Gun";
            option.Description = "Equip the Gun weapon";
            option.OnSelect = () => {
                Character character = GameObject.FindWithTag("Player").GetComponent<Character>();
                character.AddWeapon(this);
            };
        }
            return option;
    }
}
