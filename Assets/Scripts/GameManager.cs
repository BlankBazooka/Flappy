using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Variables de juego
    public int obstaculos = 0;
    public float timer = 0f;
    public int vidas;
    public int score = 0;

    public static GameManager instance;
    void Start()
    {
        obstaculos = 0;
        timer = 0f;
        vidas = 3;
        score = 0;
    }
    void Update()
    {
        if (vidas <= 0)
        {
            SceneManager.LoadScene("Scene1");
            vidas = 3;
            timer = 0f;
            obstaculos = 0;
            score = 0;
        }
    }
    public void SumarObstaculos()
    {
        obstaculos++;
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
