using UnityEngine;
using TMPro;
public class HealthDisplay : MonoBehaviour
{
    public Character character;
    private TextMeshProUGUI Health;

    private void Start()
    {
        character = GameObject.FindWithTag("Player").GetComponent<Character>();
        Health = GetComponent<TextMeshProUGUI>();
    }
    
    void Update()
    {
        Health.text = "Health: " + character.getHealth().ToString();
    }
}
