using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float Damage;
    [SerializeField] private Weapons Weapon; // 什麼武器發射的
    [SerializeField] private float LiftTime = 15.0f;
    public Vector2 target,moveDirection;
    public void setDamage(float damage) { Damage = damage; }

    public float getDamage() { return Damage; }
    public float getMoveSpeed() { return MoveSpeed; }

    
    public void setBullet(float damage, Weapons weapon, float speed = 5.0f)
    {
        Damage = damage;
        Weapon = weapon;
        MoveSpeed = speed;
    }
    public void Move()
    {
        transform.Translate(moveDirection * MoveSpeed * Time.deltaTime);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monsters"))
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        Move();
        LiftTime -= Time.deltaTime;
        if(LiftTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        moveDirection = (target - (Vector2)transform.position).normalized;
    }
}
