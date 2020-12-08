using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Player
    public Player player;
    // Enemy
    public GameObject enemyPrefab;
    // Enemy spawn distance
    public float enemySpawnDistance = 20f;
    // Spawn enemy every 2 seconds
    public float enemyInterval = 2.0f;
    // Minimum enemies
    public float minimumEnemyInterval = 0.5f;
    // After enemy spawns, another will spawn 100ms later
    public float enemyIntervalDecrement = 0.1f;
    // Enemy Timer
    private float enemyTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
