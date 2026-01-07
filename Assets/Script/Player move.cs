using UnityEngine;
using UnityEngine.InputSystem;

public class Player_move : MonoBehaviour
{
    public Scenes MainScene;
    [SerializeField] private Vector2 mapSize;
    public Character player;
    public Vector2 moveDirection;


    public void PlayerMove()
    {
        Vector2 PlayerPosition = player.GetComponent<Transform>().position;
        moveDirection = Vector2.zero;
        if (Keyboard.current.wKey.isPressed)
        {
            if(PlayerPosition.y < mapSize.y)
                moveDirection.y = 1;
            else moveDirection.y = 0;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            if(PlayerPosition.y > -mapSize.y)
                moveDirection.y = -1;
            else moveDirection.y = 0;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            if(PlayerPosition.x < mapSize.x)
                moveDirection.x = 1;
            else moveDirection.x = 0;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            if (PlayerPosition.x > -mapSize.x)
                moveDirection.x = -1;
            else moveDirection.x = 0;
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
        mapSize = MainScene.Size;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }
}
