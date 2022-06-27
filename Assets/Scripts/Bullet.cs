using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 500f;
    [SerializeField] private float maxLifetime = 10f;

    private Rigidbody2D _rb;

    private void Awake() 
    {
        _rb = GetComponent<Rigidbody2D>();    
    }

    public void Project(Vector2 direction)
    {
        _rb.AddForce(direction * speed);

        Destroy(this.gameObject, maxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        Destroy(gameObject);
    }
}
