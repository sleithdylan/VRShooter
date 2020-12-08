using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Enemy damage
    public int damage = 3;
    // Enemy Speed
    public float speed = 3.5f;
    // Enemy stop distance
    public float distanceToStop = 1f;
    // Enemy Direction
    public Vector3 direction;
    // Player
    public Player player;
    // isRushingPlayer
    public bool isRushingPlayer = true;
    // Attacking speed
    public float attackingInterval = 0.5f;
    // Attacking timer
    private float attackingTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Checks the distance between the enemy and player
        if (Vector3.Distance(transform.position, player.transform.position) < distanceToStop) 
        {
            isRushingPlayer = false;
        }
        // Checks if enemy is rushing player
        if (isRushingPlayer)
        {
            // Enemy position
            transform.position += direction * speed * Time.deltaTime;
        } else {
            attackingTimer -= Time.deltaTime;

            if (attackingTimer <= 0f) 
            {
                // Reset enemy attacking timer
                attackingTimer = attackingInterval;
                // Decrease players health
                player.health -= damage;
            }
        }
    }
}
