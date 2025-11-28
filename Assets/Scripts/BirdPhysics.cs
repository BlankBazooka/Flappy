using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdPhysics : MonoBehaviour
{
    // Referencias dentro del motor
    [SerializeField] TextMeshProUGUI ObstaculosText;
    [SerializeField] GameObject Obstaculos;
    [SerializeField] TextMeshProUGUI Timer;
    [SerializeField] TextMeshProUGUI Vidas;
    [SerializeField] TextMeshProUGUI Score;

    // Parametros de movimiento
    [SerializeField] float speed = 2.0f;
    [SerializeField] float force = 600.0f;

    // Sonidos
    [SerializeField] AudioClip coinSound;
    [SerializeField] float coinSoundVolume = 1f;

    // Objetos necesarios
    AudioSource CoinAudio;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CoinAudio = GetComponent<AudioSource>();
        MoveRight();
    }

    void Update()
    {
        // En mi caso el salto es con la barra espaciadora
        if (Input.GetMouseButtonDown(0))
        {
            rb.AddForce(Vector2.up * force);
        }
        // Si las vidas llegan a 0, reiniciamos la escena
        if (GameManager.instance.vidas == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        // Actualizamos el temporizador
        GameManager.instance.timer += Time.deltaTime;
        Timer.text = GameManager.instance.timer.ToString("F1");
        ActualizarVidas();
        ActualizarObs();
        ActualizarScore();

        if (GameManager.instance.obstaculos > 6 && SceneManager.GetActiveScene().name == "Scene1")
        {
            SceneManager.LoadScene("Scene2");
            GameManager.instance.ResetScore();
        }
    }

    public void MoveRight()
    {
        rb.linearVelocity = Vector2.right * speed;
    }

    public void PlaySound(AudioClip clip, float volume = 1f)
    {
        CoinAudio.PlayOneShot(clip, volume);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.instance.ResetScore();
    }

    public void ActualizarVidas()
    {
        Vidas.text = "Health: " + GameManager.instance.vidas.ToString();
    }    
    public void ActualizarObs()
    {
        ObstaculosText.text = "Walls: " + GameManager.instance.obstaculosReales.ToString();
    }
    public void ActualizarScore()
    {
        Score.text = "Score: " + GameManager.instance.score.ToString();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Detectar la distancia en X entre el objeto y el colisionador
        float distanceX = Mathf.Abs(collision.transform.position.x - transform.position.x);

        // Solo pillamos las monedas si estamos cerca de ellas, ya que collider de activacion es demasiado grande
        if (collision.CompareTag("coin") && distanceX < 2f)
        {
            PlaySound(coinSound, coinSoundVolume);
            collision.gameObject.SetActive(false);
            GameManager.instance.score++;
            Score.text = "Score: " + GameManager.instance.score.ToString();
        }

        // Si colisionamos con un enemigo, este se mueve hacia la izquierda, en este caso no controlamos la distancia del collider
        // porque el enemigo ya debe ser detectado por el primer collider (el mas grande)
        if (collision.CompareTag("enemy"))
        {
            collision.attachedRigidbody.linearVelocity = Vector2.left * speed;

            // Solo perdemos vida si estamos cerca del enemigo
            if (distanceX < 2f)
            {
                Destroy(collision.gameObject);
                GameManager.instance.vidas--;
            }
        }

        // Controlamos las colisiones son el collider mas grade de los muros
        if (collision.CompareTag("wall") && distanceX < 2f)
        {
            GameManager.instance.SumarObstaculos();
            GameManager.instance.AnctualizarObs();
            ObstaculosText.text = "Walls: " + GameManager.instance.obstaculosReales.ToString();
        }

        if (collision.CompareTag("Spawner"))
        {
            if (!GameManager.instance.canSpawn)
            {
                GameManager.instance.canSpawn = true;
            }
            if (SceneManager.GetActiveScene().name == "Scene2")
            GameManager.instance.GenerateGround();
        }
    }
}

