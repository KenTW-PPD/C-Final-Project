using UnityEngine;

public class MysticStaff_FireBall : Weapons
{
    public FireBall fireball_bullet;
    private Fire fire;
    public Sprite fire_icon;
    private void Awake()
    {
        setPower(8.0f);
        setRange(20.0f);
        setSpeed(1.0f);
        setOneFireBulletCount(1);
        setName("MysticStaff_FireBall");
        setBullet(fireball_bullet);
        setEquiped(false);
        fire = fireball_bullet.fire;
    }
    public override void Fire(Vector2 playerPosition, Vector2 targetPosition, float damage)
    {
        Bullet bullet_temp;
        for (int i = getOneFireBulletCount(); i > 0; i--)
        {
            bullet_temp = Instantiate(fireball_bullet, playerPosition, Quaternion.identity);
            bullet_temp.setBullet(damage, this, 10.0f);
            bullet_temp.target = targetPosition;
        }
    }


    public override UpgradeOption GenerateOption(int rarity)
    {
        UpgradeOption option = new UpgradeOption();
        option.icon = GetComponent<SpriteRenderer>().sprite;
        if (isEquiped())
        {
            float n;
            int r = Random.Range(0, 6);
            switch (r)
            {
                case 0:
                    n = Random.Range(0.2f * rarity * rarity, 0.6f * rarity * rarity);
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
                        this.setSpeed(s);
                    };
                    break;
                case 2:
                    n = Random.Range(0.2f * rarity * rarity, 0.5f * rarity * rarity);
                    option.Name = "Attack Range Up";
                    option.Description = $"Increase Attack Range by {n:F1}";
                    option.OnSelect = () => { this.setRange(getAttackRange() + n); };
                    break;
                case 3:
                    option.icon = fire_icon;
                    n = Random.Range(0.25f * rarity * rarity, 0.65f * rarity * rarity);
                    option.Name = "Range Up";
                    option.Description = $"Increase Fire Effect Range by {n:F1}";
                    option.OnSelect = () => { fire.setRange(fire.getRange() + n); };
                    break;
                case 4:
                    option.icon = fire_icon;
                    n = Random.Range(0.3f * rarity * rarity, 0.65f * rarity * rarity);
                    option.Name = "Fire Damage Up";
                    option.Description = $"Increase Fire Effect Damage by {n:F1}";
                    option.OnSelect = () => { fire.setDamage(fire.getDamage() + n); };
                    break;
                default:
                    option.icon = fire_icon;
                    option.Name = "Fire Duration UP";
                    option.Description = $"Increase Fire Effect Duration by 5 seconds";
                    option.OnSelect = () => { fire.setLifeTime(fire.getLifeTime() + 5.0f); };
                    break;
            }
        }
        else
        {
            option.Name = "Get Mystic Staff - Fire Ball";
            option.Description = "Equip the Mystic Staff - Fire Ball weapon";
            option.OnSelect = () => {
                Character character = GameObject.FindWithTag("Player").GetComponent<Character>();
                character.AddWeapon(this);
            };
        }
        return option;
    }
}

