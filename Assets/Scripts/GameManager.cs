using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Astroid astroidPrefab;
    public int astroidCount = 0;
    private int level = 0;

    // Update is called once per frame
   private void Update()
    {
        if(astroidCount == 0)
        {
            level++;
            int numberOfAstroids = 2 + (2 * level);
            for(int i = 0; i < numberOfAstroids; i++)
            {
                SpawnAstroid();
            }
        }
    }

    private void SpawnAstroid()
    {
        float offset = Random.Range(0, 1f);
        Vector2 viewportSpawnPosition = Vector2.zero;

        int edge = Random.Range(0, 4);
        if(edge == 0)
        {
            viewportSpawnPosition = new Vector2(offset, 0);
        } 
        else if(edge == 1)
        {
            viewportSpawnPosition = new Vector2(offset, 1);
        } 
        else if(edge == 2) 
        {
            viewportSpawnPosition = new Vector2(0, offset);
        } 
        else if (edge == 3)
        {
            viewportSpawnPosition = new Vector2(1, offset);
        }

        Vector2 worldSpawnPosition = Camera.main.ViewportToWorldPoint(viewportSpawnPosition);
        Astroid astroid = Instantiate(astroidPrefab, worldSpawnPosition, Quaternion.identity);
        astroid.gameManager = this;
    }

    public void GameOver()
    {
        StartCoroutine(Restart());
    }
    private IEnumerator Restart()
    {
        Debug.Log("Game Over");
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        yield return null;
    }
}
