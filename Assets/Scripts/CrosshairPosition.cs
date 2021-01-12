using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairPosition : MonoBehaviour
{
    GameObject crosshair;
    private AudioSource gunshot;
    private float fireRate = 4;
    private float lastFired = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        gunshot = GetComponent<AudioSource>();
        Cursor.visible = false;
        crosshair = GameObject.Find("Crosshair");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float mousePosX = mousePos.x;
        float mousePosY = mousePos.y;

        // update crosshair position
        crosshair.transform.position = new Vector2(mousePosX, mousePosY);

        // left click event
        if (!GameController.specialActivated)
        {
            if (Input.GetMouseButtonDown(0))
            {
                gunshot.Play();

                bool pumpkinHit = false;
                bool witchHit = false;
                int pumpkinCount = 0;

                // pumpkin collision
                for (int i = 0; i < PumpkinMovement.pumpkins.Count; i++)
                {
                    if (crosshair.GetComponent<BoxCollider2D>().IsTouching(PumpkinMovement.pumpkins[i].GetComponent<BoxCollider2D>()))
                    {
                        pumpkinHit = true;
                        pumpkinCount++;
                        PumpkinMovement.pumpkins[i].GetComponent<PumpkinMovement>().isHit = true;
                    }
                }

                if (pumpkinHit)
                {
                    if (pumpkinCount >= 2)
                    {
                        GameController.updateScore(8);
                    }
                    GameController.updateScore(3);
                }

                // witch collision
                if (crosshair.GetComponent<BoxCollider2D>().IsTouching(WitchMovement.witch.GetComponent<CircleCollider2D>()))
                {
                    WitchMovement.hitWitch();
                    witchHit = true;
                }

                if (!pumpkinHit && !witchHit)
                {
                    GameController.updateScore(-1);
                    DogMovement.showDog();
                }
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                if (Input.GetButton("Fire1"))
                {
                    if (Time.time - lastFired > 1 / fireRate)
                    {
                        lastFired = Time.time;
                        gunshot.Play();

                        bool pumpkinHit = false;
                        bool witchHit = false;

                        // pumpkin collision
                        for (int i = 0; i < PumpkinMovement.pumpkins.Count; i++)
                        {
                            if (crosshair.GetComponent<BoxCollider2D>().IsTouching(PumpkinMovement.pumpkins[i].GetComponent<BoxCollider2D>()))
                            {
                                pumpkinHit = true;
                                PumpkinMovement.pumpkins[i].GetComponent<PumpkinMovement>().isHit = true;
                            }
                        }

                        if (pumpkinHit)
                        {
                            GameController.updateScore(1);
                        }

                        // witch collision
                        if (crosshair.GetComponent<BoxCollider2D>().IsTouching(WitchMovement.witch.GetComponent<CircleCollider2D>()))
                        {
                            WitchMovement.hitWitch();
                            witchHit = true;
                        }

                        if (!pumpkinHit && !witchHit)
                        {
                            GameController.updateScore(-2);
                        }
                    }
                }
            }
        }
    }
}
