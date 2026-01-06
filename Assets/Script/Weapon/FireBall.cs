using UnityEngine;

public class FireBall : Bullet
{
    public Fire fire;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monsters"))
        {
            Instantiate(fire, transform.position, Quaternion.Euler(0,0,90));
            Destroy(this.gameObject);
        }
    }
}
