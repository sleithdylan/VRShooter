using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    // Bullet Speed
    public float speed = 10f;
    // Bullet Direction
    public Vector3 direction;
    // How long bullet lasts
    public float lifetime = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Bullet position
        transform.position += direction * speed * Time.deltaTime;

        lifetime -= Time.deltaTime;
        // If bullets lasts for more than 2 seconds, destroy bullet
        if (lifetime <= 0f) 
        {
            // Destroy bullet
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            // Destroy enemy
            Destroy(other.gameObject);

            // Destroy bullet
            Destroy(this.gameObject);
        }
    }
}
