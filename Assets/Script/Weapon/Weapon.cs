using UnityEngine;

public class Weapons : MonoBehaviour , Prize
{
    [SerializeField] private string WeaponName;
    [SerializeField] private float AttackPower;
    [SerializeField] private float AttackSpeed;
    [SerializeField] private float AttackRange;
    [SerializeField] private int OneFireBulletCount;
    [SerializeField] private bool Equiped = false;
    private Bullet bullet;

    public void setSpeed(float speed) { AttackSpeed = speed; }
    public void setPower(float power) { AttackPower = power; }
    public void setRange(float range) { AttackRange = range; }
    public void setName(string name) { WeaponName = name; }
    public void setOneFireBulletCount(int count) { OneFireBulletCount = count; }
    public void setBullet(Bullet bullet_prefab) { bullet = bullet_prefab; }
    public void setEquiped(bool b) { Equiped = b; }
    public bool isEquiped() { return Equiped; }
    public float getAttackSpeed() { return AttackPower; }
    public float getAttackRange() { return AttackRange; }
    public float getAttackPower() { return AttackPower; }
    public int getOneFireBulletCount() { return OneFireBulletCount; }
    public string getWeaponName() { return WeaponName; }

    public virtual void Fire(Vector2 playerPosition, Vector2 targetPosition, float damage)
    {
        Bullet bullet_temp;
        for(int i = getOneFireBulletCount(); i > 0; i--)
        {
            bullet_temp = Instantiate(bullet, playerPosition, Quaternion.identity);
            bullet_temp.setBullet(damage,this,15.0f);
            bullet_temp.target = targetPosition;
        }
        
    }

    public virtual UpgradeOption GenerateOption(int rarity)
    {
        return null;
    }

}
