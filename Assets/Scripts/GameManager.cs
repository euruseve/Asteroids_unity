using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public ParticleSystem explosion;
    public float respawnTime = 3f;
    public int lives = 3;
    public int score = 0;


    public void AsteroidDestoyed(Asteroid asteroid)
    {
        explosion.transform.position = asteroid.transform.position;
        explosion.Play();

        if(asteroid.size < 0.75f)
        {
            score += 100;
        }
        else if(asteroid.size < 1.2f)
        {
            score += 50;
        }
        else
        {
            score += 25;
        }
    }

    public void PlayerDied()
    {
        lives--;
        
        explosion.transform.position = player.transform.position;
        explosion.Play();

        if(lives <= 0)
        {
            GameOver();
        }
        else
        {
            Invoke(nameof(Respawn), respawnTime);
        }

    }

    private void Respawn()
    {
        player.transform.position = Vector3.zero;
        player.gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions");
        player.gameObject.SetActive(true);
        Invoke(nameof(TurnOnCollisions), 3f);
    }

    private void TurnOnCollisions()
    {
        player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    
   


    private void GameOver()
    {
        lives = 3;
        score = 0;

        Invoke(nameof(Respawn), respawnTime);
    }
}
