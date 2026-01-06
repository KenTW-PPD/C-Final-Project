using UnityEngine;

public class Camera_move : MonoBehaviour
{
    // map size是指整個地圖的一半大小 ex.地圖寬1000 高800 則map size為(500,400)
    public Scenes scenes;
    public GameObject player;
    public Vector2 camPosition;
    public Camera camera;
    public void camMove()
    {
        float camHight = camera.orthographicSize;
        float camWeidth = camHight * camera.aspect;
        camPosition = player.transform.position;

        if (camPosition.y + camHight >= scenes.Size.y)
        {
            camPosition.y = scenes.Size.y - camHight;
        }
        else if(camPosition.y - camHight <= -(scenes.Size.y))
        {
            camPosition.y = -(scenes.Size.y) + camHight;
        }
        if (camPosition.x + camWeidth >= scenes.Size.x)
        {
            camPosition.x = scenes.Size.x - camWeidth;
        }
        else if(camPosition.x - camWeidth <= -(scenes.Size.x))
        {
            camPosition.x = -(scenes.Size.x) + camWeidth;
        }
        camera.transform.position = new Vector3(camPosition.x,camPosition.y,-10);
    }
    
    void Start()
    {
        camera = this.gameObject.GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        camMove();
    }
}
