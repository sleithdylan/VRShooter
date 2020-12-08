using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Player health
    public int health = 100;
    // Bullet
    public GameObject bulletPrefab;
    // Player shooting cooldown
    public float shootingCooldown = 0.1f;
    // Player melee cooldown
    public float meleeCooldown = 0.5f;
    // Shooting timer
    private float shootingTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shootingTimer -= Time.deltaTime;
        // If action button is pressed
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
        {
            if (shootingTimer <= 0f)
            {
                // Cooldown
                shootingTimer = shootingCooldown;

                // Melee
                Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);
                Enemy meleeEnemy = null;
                bool meleeAttack = false;

                // If enemy is within radius of player, perform melee
                foreach(Collider collider in colliders) {
                    if (collider.GetComponent<Enemy>() != null)
                    {
                        meleeAttack = true;
                        meleeEnemy = collider.GetComponent<Enemy>();
                        break;
                    }
                }

                if (meleeAttack == false) 
                {
                    // Spawn bullet
                    GameObject bulletObject = Instantiate(bulletPrefab);

                    // Set bullet position to players position
                    bulletObject.transform.position = this.transform.position;

                    // Gets bullet component
                    Bullet bullet = bulletObject.GetComponent<Bullet>();

                    // Moves bullet forward
                    bullet.direction = transform.forward;

                } else {
                    // Cooldown
                    shootingTimer = meleeCooldown;

                    // Destroy Enemy which is close
                    Destroy(meleeEnemy.gameObject);
                }
            }
        }
    }
}
