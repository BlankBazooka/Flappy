using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    [SerializeField]
    GameObject panel;
    public void Pause()
    {
         Time.timeScale = 0.0f;
         panel.SetActive(true);
    }
    public void UnPause()
    {
        Time.timeScale = 1.0f;
        panel.SetActive(false);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.instance.ResetScore();
        UnPause();
    }
    public void Quit()
    {
        Application.Quit();
    }
}
