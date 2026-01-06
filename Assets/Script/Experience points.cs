using UnityEngine;

public class Experience_Points: MonoBehaviour
{
    private float experience_point = 6.0f;
    public float getExperiencePoint() { return experience_point; }
    public void setExperiencePoint(float xp) { experience_point = xp; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Debug.Log("Get EXP");
            Destroy(this.gameObject);
        }
            
    }
}
