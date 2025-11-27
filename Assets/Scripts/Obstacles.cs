using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField]
    float speed = 2.0f;
    [SerializeField]
    float timeBetweenReverse = 0f;
    int lastRand;

    public GameObject player;
    public GameObject obstacle;
    public GameObject enemy;
    public GameObject coin;

    Rigidbody2D rb;
    void Start()
    {
        lastRand = generateRand();
        rb = GetComponent<Rigidbody2D>();
        
        if (lastRand == 1)
        {
            rb.linearVelocity = Vector2.up * speed;
        }
        else if (lastRand == 2)
        {
            rb.linearVelocity = Vector2.down * speed;
        }
        InvokeRepeating("Reverse", 0, timeBetweenReverse);
    }

    void Update()
    {
    }
    void Reverse()
    {
        rb.linearVelocity *= -1;
    }
    public int generateRand()
    {
        return Random.Range(1, 3);
    }
}
