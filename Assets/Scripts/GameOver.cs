using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private float waitTime = 1f;
    [SerializeField] private GameObject gameOverBG;
    [SerializeField] private GameObject victoryBG;

    public bool isPlayerWon;

    private void Start()
    {
        isPlayerWon = false;
        gameOverBG.transform.localPosition = new Vector3(0, -1500f, 0);
        victoryBG.transform.localPosition = new Vector3(0, -1500f, 0);
    }

    public void TriggerGameOverBG()
    {
        gameOverBG.transform.LeanMoveLocalY(0, 0.8f).setEaseOutExpo();
    }

    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu()
    {
        Debug.Log("Go to menu");
        SceneManager.LoadScene("Menu");
    }

    public void NextLevel()
    {
        Debug.Log("Go to next level");
    }

    public void TriggerVictoryBG()
    {
        Invoke("VictoryBGTriggerWait", waitTime);
    }

    void VictoryBGTriggerWait()
    {
        victoryBG.transform.LeanMoveLocalY(0, 0.8f).setEaseOutExpo();
    }
}
