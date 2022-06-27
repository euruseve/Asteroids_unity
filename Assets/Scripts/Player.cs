using UnityEngine;

public class Player : MonoBehaviour
{
    public Bullet bulletPrefab;
    [SerializeField] private float thrustSpeed = 1f;
    [SerializeField] private float turnSpeed = 1f;

    private Rigidbody2D _rb;
    private bool _thrusting;
    private float _turnDirection;

    private void Start() 
    {
        _rb = GetComponent<Rigidbody2D>();    
    }

    private void Update() 
    {
        _thrusting = Input.GetKey(KeyCode.W);

        if(Input.GetKey(KeyCode.A))
        {
            _turnDirection = 1f;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            _turnDirection = -1f;
        }
        else
        {
            _turnDirection = 0f;
        }


        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }


    }

    private void FixedUpdate() 
    {
        if(_thrusting)
        {
            _rb.AddForce(this.transform.up * thrustSpeed);
        }

        if(_turnDirection != 0f)
        {
            _rb.AddTorque(_turnDirection * turnSpeed);
        }
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Asteroid")
        {
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = 0f;

            this.gameObject.SetActive(false);

            FindObjectOfType<GameManager>().PlayerDied();
        }    
    }
}
