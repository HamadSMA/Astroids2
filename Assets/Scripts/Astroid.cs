using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Astroid : MonoBehaviour
{
    [SerializeField] private ParticleSystem destroyedParticles;
    public int size = 3;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = 0.5f * size * Vector2.one;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 direction = new Vector2(Random.value, Random.value).normalized;
        float spawnSpeed = Random.Range(4f - size, 5f - size);
        rb.AddForce(direction * spawnSpeed, ForceMode2D.Impulse);

        gameManager.astroidCount++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            gameManager.astroidCount--;
            Destroy(collision.gameObject);

            if(size > 1)
            {
                for(int i = 0; i< 2; i++)
                {
                    Astroid newAstroid = Instantiate(this, transform.position, Quaternion.identity);
                    newAstroid.size = size - 1;
                    newAstroid.gameManager = gameManager;
                }
            }
            Instantiate(destroyedParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
