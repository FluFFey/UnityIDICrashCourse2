using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour {

    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float lifetime;
    private float timeAlive;
    // Use this for initialization
    void Start ()
    {
        timeAlive = 0;
        GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity * speed;
    }
	
	// Update is called once per frame
	void Update ()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive >= lifetime)
        {
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyBehavior>().dealDamage(damage, transform.position);
        }
        if (collision.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
