using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private Button Quit;
    [SerializeField] private Button Restart; // Play Again
    [SerializeField] private Scenes scene;
    void Start()
    {
        Time.timeScale = 1f;
    }
    public void GameOver()
    {
        Time.timeScale = 0f;
        Scene currentScene = SceneManager.GetActiveScene();
        this.GetComponent<Canvas>().enabled = true;
        score.text = "Score : " + scene.getScore().ToString();


        Restart.onClick.RemoveAllListeners();
        Restart.onClick.AddListener(() => {
            SceneManager.LoadScene(currentScene.name);
        });
        Quit.onClick.RemoveAllListeners();
        Quit.onClick.AddListener(QuitGame);
    }
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
    


}
