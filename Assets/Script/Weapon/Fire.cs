using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Fire : MonoBehaviour
{
    [SerializeField] private float LifeTime = 5.0f;
    [SerializeField] private float Range = 3.0f;
    [SerializeField] private float Damage = 1.0f;

    public void setRange(float range) { Range = range; }
    public void setDamage(float damage) { Damage = damage; }
    public void setLifeTime(float lifeTime) { LifeTime = lifeTime; }
    public float getRange() { return Range; }
    public float getDamage() { return Damage; }
    public float getLifeTime() { return LifeTime; }
    // ¶}©l¿U¿N
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monsters"))
        {
            Monsters Monster = collision.GetComponent<Monsters>();
            Monster.FireDamage = Damage;
            Monster.setFire(true);
        }
    }
    // µ²§ô¿U¿N
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Monsters"))
        {
            Monsters Monster = collision.GetComponent<Monsters>();
            Monster.setFire(false);
        }
    }
    private void Update()
    {
        LifeTime -= Time.deltaTime;
        if(LifeTime <= 0)
        {
            
            Destroy(this.gameObject);
        }
    }


}
