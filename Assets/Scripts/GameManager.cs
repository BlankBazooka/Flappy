using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Variables de juego
    public int obstaculos = 0;
    public float timer = 0f;
    public int vidas;
    public int score = 0;

    public GameObject spawner;
    public GameObject obstaclePrefab;
    public GameObject enemyPrefab;
    public GameObject coinPrefab;

    public float spawnRate = 2f;
    public float spawnRange = 2.5f;
    private float timerSpawn = 0f;
    public static GameManager instance;
    private bool SpawnArriba;
    public bool canSpawn = false;

    void Start()
    {
        obstaculos = 0;
        timer = 0f;
        vidas = 3;
        score = 0;
    }
    void Update()
    {
        if (spawner == null)
        {
            spawner = GameObject.FindGameObjectWithTag("spawn");
        }
        if (vidas <= 0)
        {
            SceneManager.LoadScene("Scene1");
            vidas = 3;
            timer = 0f;
            obstaculos = 0;
            score = 0;
        }
        if (obstaculos >= 8)
        {
            timerSpawn += Time.deltaTime;
        }

        if (timerSpawn >= spawnRate && SceneManager.GetActiveScene().name == "Scene2" && canSpawn)
        {
            SpawnObjects();
            timerSpawn = 0f;
        }
    }
    public void SumarObstaculos()
    {
        obstaculos++;
    }
    void SpawnObjects()
    {
        float yPos = Random.Range(-spawnRange, spawnRange);
        if (SpawnArriba)
        {
            Instantiate(obstaclePrefab, new Vector3(spawner.transform.position.x + 15f, 5.60f, 0), Quaternion.identity);
            SpawnArriba = false;
        }
        else
        {
            Instantiate(obstaclePrefab, new Vector3(spawner.transform.position.x + 15f, -4.80f, 0), Quaternion.identity);
            SpawnArriba = true;
        }

        if (Random.value < 0.5f)
        {
            Instantiate(coinPrefab, new Vector3(spawner.transform.position.x + 16f, yPos + 1f, 0), Quaternion.identity);
        }

        if (Random.value < 0.2f)
        {
            Instantiate(enemyPrefab, new Vector3(spawner.transform.position.x + 17f, yPos - 1f, 0), Quaternion.identity);
        }
    }
    public void ResetScore()
    {
        obstaculos = 0;
        score = 0;
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
