using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    // Player
    public Player player;
    // Enemy
    public GameObject enemyPrefab;
    // Enemy spawn distance
    public float enemySpawnDistance = 100f;
    // Spawn enemy every 5 seconds
    public float enemyInterval = 5.0f;
    // Minimum enemies
    public float minimumEnemyInterval = 0.02f;
    // After enemy spawns, another will spawn 1 second later
    public float enemyIntervalDecrement = 1.0f;
    // Enemy Timer
    private float enemyTimer = 0f;
    // Health Text
    public TextMeshPro Health;
    // Health Text
    public TextMeshPro Gameover;
    // Enemy Timer
    private float gameTimer = 0f;
    // Restart Timer
    private float restartTimer = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void SetHealthText()
    {
        Health.text = " <sprite=0> " + player.health;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.health > 0)
        {
            gameTimer += Time.deltaTime;
            // Players Health
            SetHealthText();
        } else
        {
            // End screen
            Gameover.color = Color.red;
            Gameover.text = "Game Over!\n";
            Gameover.text += "\nYou surived for " + Mathf.Floor(gameTimer) + " seconds!";

            restartTimer -= Time.deltaTime;
            if (restartTimer <= 0f)
            {
                // Restart game
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
   
        enemyTimer -= Time.deltaTime;

        if (enemyTimer <= 0) 
        {
            // Spawn new enemy
            enemyTimer = enemyInterval;
            // Increase Difficulty Overtime
            enemyInterval -= enemyIntervalDecrement;
            // Difficulty limit
            if (enemyInterval < minimumEnemyInterval) 
            {
                enemyInterval = minimumEnemyInterval;
            }

            // Spawn enemy
            GameObject enemyObject = Instantiate(enemyPrefab);

            // Gets enemy component
            Enemy enemy = enemyObject.GetComponent<Enemy>();

            // Spawn enemy at random angle around player
            float randomAngle = Random.Range(0f, 2f * Mathf.PI);

            // Enemy random position
            enemy.transform.position = new Vector3(
                player.transform.position.x + Mathf.Cos(randomAngle) * enemySpawnDistance,
                enemy.transform.position.y,
                player.transform.position.z + Mathf.Sin(randomAngle) * enemySpawnDistance
            );

            // Allows the enemy to see player
            enemy.player = player;

            // Allows the enemy to walk towards player
            enemy.direction = (player.transform.position - enemy.transform.position).normalized;

            // Makes enemy to always face the player
            enemy.transform.LookAt(player.transform);
        }
    }
}
