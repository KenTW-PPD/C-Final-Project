using UnityEngine;

public class Pickup_range : MonoBehaviour
{
    public Character player;
    private float originalSize;
    public void ChangeSize()
    {
        if (player.getPickupRangeSize() != originalSize)
        {
            originalSize = player.getPickupRangeSize();
            this.transform.localScale = new Vector3(originalSize, originalSize, 1);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        originalSize = player.getPickupRangeSize();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeSize();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("XP"))
        {
            Vector2 xpPosition = new Vector2(collision.transform.position.x, collision.transform.position.y);
            Vector2 playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);
            collision.transform.position = Vector2.MoveTowards(xpPosition, playerPosition, 5.0f * Time.deltaTime);
        }
    }
}
