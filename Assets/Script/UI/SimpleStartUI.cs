using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SimpleStartUI : MonoBehaviour
{
    public Button StartButton;
    public Button QuitButton;
    private void Start()
    {
        StartButton.onClick.AddListener(StartGame);
        QuitButton.onClick.AddListener(QuitGame);
    }


    void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
