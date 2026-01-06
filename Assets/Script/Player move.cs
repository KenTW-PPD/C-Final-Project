using UnityEngine;
using UnityEngine.InputSystem;

public class Player_move : MonoBehaviour
{
    public Character player;
    public Vector2 moveDirection;


    public void PlayerMove()
    {
        moveDirection = Vector2.zero;
        if (Keyboard.current.wKey.isPressed)
        {
            moveDirection.y = 1;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            moveDirection.y = -1;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            moveDirection.x = 1;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            moveDirection.x = -1;
        }

    }
    private void FixedUpdate()
    {
        this.transform.Translate(moveDirection  * player.getMoveSpeed() * Time.fixedDeltaTime);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }
}
