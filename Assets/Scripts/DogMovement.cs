using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMovement : MonoBehaviour
{
    static bool startAnimation = false;

    private static AudioSource laugh;
    private float speed = 1.4f;
    private bool movingUp = true;

    void Start()
    {
        laugh = GetComponent<AudioSource>();    
    }

    // Update is called once per frame
    void Update()
    {
        if(startAnimation)
        {
            StartCoroutine(moveDog());
        }
    }

    public static void showDog()
    {
        startAnimation = true;
        laugh.Play();
    }

    IEnumerator moveDog()
    {
        if (gameObject.transform.position.y <= -4.32 && movingUp)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + speed * Time.deltaTime);
        }
        else if (gameObject.transform.position.y >= -6.34)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - speed * Time.deltaTime);
        }

        if (gameObject.transform.position.y >= -4.32)
        {
            yield return new WaitForSeconds(1);
            movingUp = false;
        }

        if (gameObject.transform.position.y <= -6.34)
        {
            startAnimation = false;
            movingUp = true;
        }
    }
}
