using UnityEngine;
using UnityEngine.EventSystems;

public class MonstersBullet : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float Damage;
    [SerializeField] private float LiftTime = 15.0f;
    public Vector2 target, moveDirection;
    public void setDamage(float damage) { Damage = damage; }

    public float getDamage() { return Damage; }
    public float getMoveSpeed() { return MoveSpeed; }


    public void setBullet(float damage, float speed = 5.0f)
    {
        Damage = damage;
        MoveSpeed = speed;
    }
    public void Move()
    {
        transform.Translate(moveDirection * MoveSpeed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        Move();
        LiftTime -= Time.deltaTime;
        if (LiftTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        moveDirection = (target - (Vector2)transform.position).normalized;
    }
}
