using UnityEngine;
using TMPro;

public class TimeDisplay : MonoBehaviour
{
    private TextMeshProUGUI time;
    public Scenes scenes;

    private void Start()
    {
        time = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        time.text = "Time: " + (int)scenes.getTime() + "s";
    }

}
