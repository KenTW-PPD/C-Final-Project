using Unity.VisualScripting;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public ChestUI UI;
    void Awake()
    {
        UI = GameObject.FindWithTag("ChestUI").GetComponent<ChestUI>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UI.GetComponent<Canvas>().enabled = true;
            UI.BuildChestUI();
            Time.timeScale = 0f;
            Destroy(this.gameObject);
        }
    }

}
