using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinMovement : MonoBehaviour
{
    public static List<GameObject> pumpkins = new List<GameObject>();
    string position = "";
    public bool isHit = false;
    bool isStandby = false;
    float rotationSpeed = 110.0f;

    // Start is called before the first frame update
    void Start()
    {
        pumpkins.Add(gameObject);
        spawnPumpkin(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHit && !isStandby)
        {
            if (string.Compare(position, "left") == 0)
            {
                if (gameObject.transform.position.x < 9.5f)
                {
                    gameObject.transform.position = new Vector2(gameObject.transform.position.x + GameController.speed * Time.deltaTime, gameObject.transform.position.y);
                    transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
                } else
                {
                    isStandby = true;
                    isHit = true;
                    spawnPumpkin(gameObject);
                    if (!GameController.specialActivated)
                    {
                        ProgressPumpkinController.updateProgress(false);
                        GameController.missedPumpkins++;
                    }
                }
            }
            else if (string.Compare(position, "top") == 0)
            {
                if (gameObject.transform.position.y > -5.5f)
                {
                    gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - GameController.speed * Time.deltaTime);
                    transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
                }
                else
                {
                    isStandby = true;
                    isHit = true;
                    spawnPumpkin(gameObject);
                    if (!GameController.specialActivated)
                    {
                        ProgressPumpkinController.updateProgress(false);
                        GameController.missedPumpkins++;
                    }
                }
            }
            else if (string.Compare(position, "right") == 0)
            {
                if (gameObject.transform.position.x > -9.5f)
                {
                    gameObject.transform.position = new Vector2(gameObject.transform.position.x - GameController.speed * Time.deltaTime, gameObject.transform.position.y);
                    transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
                }
                else
                {
                    isStandby = true;
                    isHit = true;
                    spawnPumpkin(gameObject);
                    if (!GameController.specialActivated)
                    {
                        ProgressPumpkinController.updateProgress(false);
                        GameController.missedPumpkins++;
                    }
                }
            }
            else
            {
                if (gameObject.transform.position.y < 5.5f)
                {
                    gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + GameController.speed * Time.deltaTime);
                    transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
                }
                else
                {
                    isStandby = true;
                    isHit = true;
                    spawnPumpkin(gameObject);
                    if (!GameController.specialActivated)
                    {
                        ProgressPumpkinController.updateProgress(false);
                        GameController.missedPumpkins++;
                    }
                }
            }
        }
        else if(isHit && !isStandby)
        {
            spawnPumpkin(gameObject);
            isStandby = true;
            if (!GameController.specialActivated)
            {
                ProgressPumpkinController.updateProgress(true);
            }
        }
    }

    // Randomize pumpkin spawn position
    void spawnPumpkin(GameObject pumpkin)
    {
        float randomNum = Random.Range(0.0f, 100.0f);

        if(randomNum < 25.0f)
        {
            // left
            pumpkin.transform.position = new Vector2(-9.5f, Random.Range(-4.0f, 4.0f));
            position = "left";
        } else if(randomNum < 50.0f)
        {
            // top
            pumpkin.transform.position = new Vector2(Random.Range(-8.1f, 8.1f), 5.6f);
            position = "top";
        } else if(randomNum < 75.0f)
        {
            // right
            pumpkin.transform.position = new Vector2(9.5f, Random.Range(-4.0f, 4.0f));
            position = "right";
        } else
        {
            // bottom
            pumpkin.transform.position = new Vector2(Random.Range(-8.1f, 8.1f), -5.6f);
            position = "bottom";
        }
    }

    public static void resetHit()
    {
        for (int i = 0; i < PumpkinMovement.pumpkins.Count; i++)
        {
            PumpkinMovement.pumpkins[i].GetComponent<PumpkinMovement>().isHit = false;
            PumpkinMovement.pumpkins[i].GetComponent<PumpkinMovement>().isStandby = false;
        }
    }

    public static void hitAll()
    {
        for (int i = 0; i < pumpkins.Count; i++)
        {
            if (!pumpkins[i].GetComponent<PumpkinMovement>().isHit)
            {
                pumpkins[i].GetComponent<PumpkinMovement>().isHit = true;
                ProgressPumpkinController.updateProgress(true);
            }
        }
    }
}
