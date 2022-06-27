using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject pauseCanvas;
    public GameObject gameOverCanvas;
    public GameObject scoreCanvas;

    public Player player;
    public ParticleSystem explosion;

    public Text scoreText;

    public float respawnTime = 3f;
    public int lives = 3;
    public int score = 0;


    private void Start() 
    {
        pauseCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
    }


    private void Update() 
    {
        scoreText.text = $"Score: {score}";    

        if(Input.GetKey(KeyCode.Escape))
        {
            MenuPause();
        }
    }


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

        if(score >= 1000)
        {
            score -= 800;
        }
        else
        {
            score -= 200;
        }
        
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
        Time.timeScale = 0;

        scoreCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
    }


    private void MenuPause()
    {
        Time.timeScale = 0;
        scoreCanvas.SetActive(false);
        pauseCanvas.SetActive(true);
    }

    public void Resume()
    {
        scoreCanvas.SetActive(true);
        pauseCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

}
