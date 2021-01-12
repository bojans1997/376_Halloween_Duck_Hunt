using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchMovement : MonoBehaviour
{
    static string position = "left";
    public static bool isMoving = false;
    public static GameObject witch;
    static int hitCount = 0;

    private float witchSpeed = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        witch = gameObject;
        isMoving = false;
        hitCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            if (string.Compare(position, "left") == 0)
            {
                if (gameObject.transform.position.x < 10.1f)
                {
                    gameObject.transform.position = new Vector2(gameObject.transform.position.x + (GameController.speed * Time.deltaTime * witchSpeed), gameObject.transform.position.y);
                }
                else
                {
                    isMoving = false;
                    spawnWitch();
                }
            }
            else
            {
                if (gameObject.transform.position.x > -10.1f)
                {
                    gameObject.transform.position = new Vector2(gameObject.transform.position.x - (GameController.speed * Time.deltaTime * witchSpeed), gameObject.transform.position.y);
                }
                else
                {
                    isMoving = false;
                    spawnWitch();
                }
            }
        }
    }

    public static void moveWitch()
    {
        isMoving = true;
    }

    public static void hitWitch()
    {
        if(hitCount < 2)
        {
            hitCount++;
        }
        else
        {
            GameController.updateScore(10);
            spawnWitch();
            hitCount = 0;
            isMoving = false;
        }
    }

    // Randomize witch spawn position
    static void spawnWitch()
    {
        hitCount = 0;
        float randomNum = Random.Range(0.0f, 100.0f);

        if (randomNum < 50.0f)
        {
            // left
            witch.transform.position = new Vector2(-10.1f, Random.Range(-3.3f, 3.3f));
            witch.GetComponent<SpriteRenderer>().flipX = false;
            position = "left";
        }
        else
        {
            // right
            witch.transform.position = new Vector2(10.1f, Random.Range(-3.3f, 3.3f));
            witch.GetComponent<SpriteRenderer>().flipX = true;
            position = "right";
        }
    }
}
