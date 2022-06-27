using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Sprite[] sprites;

    public float speed = 10f;
    public float maxLifetime = 20f;

    [Header("Size values")]
    public float size = 1f;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;


    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb;

    private void Awake() 
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();    
    }

    private void Start() 
    {
        _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

        transform.eulerAngles = new Vector3(0f, 0f, Random.value * 360f);
        transform.localScale = Vector3.one * size;

        _rb.mass = size;

    }

    public void SetTrajectory(Vector2 direction)
    {
        _rb.AddForce(direction * speed);

        Destroy(this.gameObject, maxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Bullet")
        {
            if((size * 0.5f ) >= minSize)
            {
                CreateSplit();
                CreateSplit();
            }

            FindObjectOfType<GameManager>().AsteroidDestoyed(this);
            Destroy(gameObject);
        }
    }

    private void CreateSplit()
    {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;

        Asteroid half = Instantiate(this, position, transform.rotation);
        half.size = this.size * 0.5f;

        half.SetTrajectory(Random.insideUnitCircle.normalized * this.speed);
    }
}
