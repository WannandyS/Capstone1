using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ExitButton()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
