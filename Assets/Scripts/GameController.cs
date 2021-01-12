using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static Text score;
    public static Text levelText;
    public Text specialText;
    public static float speed = 3.5f;
    public GameObject pumpkin;
    public GameObject witch;
    public static int missedPumpkins = 0;
    public static bool isSpecial = false;
    public static bool specialActivated = false;
    public Animator levelAnimator;

    private static bool specialActivatedThisLevel = false;
    private static int points = 0;
    private int levelNum = 1;
    private int witchLevelCount = 0;
    private int roundCount = 1;
    private int specialPumpkinCount = 10;
    private AudioSource nextLevel;

    // Start is called before the first frame update
    void Start()
    {
        points = 0;
        speed = 3.5f;
        missedPumpkins = 0;

        score = GameObject.Find("Score").GetComponent<Text>();
        score.text = "Score: " + points;

        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        levelText.text = "Level " + levelNum;

        nextLevel = GetComponent<AudioSource>();

        if (!isSpecial)
        {
            specialText.text = "";
        }

        Instantiate(pumpkin, new Vector2(-12.5f, 1.0f), Quaternion.identity);
        Instantiate(pumpkin, new Vector2(-12.5f, 1.0f), Quaternion.identity);

        Instantiate(witch, new Vector2(-13.05f, 1.0f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        // if 3 pumpkins have been missed in a level, end the game
        if(missedPumpkins >= 3)
        {
            missedPumpkins = 0;
            PumpkinMovement.pumpkins.Clear();
            ProgressPumpkinController.progressPumpkins.Clear();
            SceneManager.LoadScene("GameOver");
        }

        // if 10 pumpkins have spawned, start the next level
        if (ProgressPumpkinController.progressIndex >= ProgressPumpkinController.progressPumpkins.Count && !isWitchOnScreen())
        {
            startNextLevel();
            ///levelAnimator.SetBool("levelStart", false);
        }

        // if all pumpkins have been hit this round and the witch is gone, spawn the next set of pumpkins
        if (roundPumpkinsHit() && !isWitchOnScreen())
        {
            clearSpecialPumpkins();
            PumpkinMovement.resetHit();
            roundCount++;
            spawnWitch();
            specialActivated = false;
        }

        // if special mode is available, activate it when spacebar is pressed
        if(isSpecial && !specialActivatedThisLevel && Input.GetKeyDown(KeyCode.Space))
        {
            PumpkinMovement.hitAll();

            specialActivated = true;
            specialActivatedThisLevel = true;

            for (int i = 0; i < specialPumpkinCount; i++)
            {
                Instantiate(pumpkin, new Vector2(-12.5f, 1.0f), Quaternion.identity);
            }

            specialText.text = "";
        }
    }

    public static void updateScore(int newPoints)
    {
        points += newPoints;
        score.text = "Score: " + points.ToString();
    }

    void startNextLevel()
    {
        levelNum++;
        levelText.text = "Level " + levelNum;
        levelAnimator.SetTrigger("levelStart");

        nextLevel.Play();

        speed += 0.5f;

        roundCount = 1;

        witchLevelCount = 0;

        missedPumpkins = 0;

        specialActivatedThisLevel = false;
        specialActivated = false;

        ProgressPumpkinController.resetProgressPumpkins();
        ProgressPumpkinController.progressIndex = 0;

        if (isSpecial)
        {
            clearSpecialPumpkins();

            specialText.text = "Special Mode Available!";
        }
    }

    bool roundPumpkinsHit()
    {
        int hitPumpkins = 0;
        for (int i = 0; i < PumpkinMovement.pumpkins.Count; i++)
        {
            if (PumpkinMovement.pumpkins[i].GetComponent<PumpkinMovement>().isHit)
            {
                hitPumpkins++;
            }
        }

        if(hitPumpkins == PumpkinMovement.pumpkins.Count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void spawnWitch()
    {
        float randomNum = Random.Range(0.0f, 100.0f);

        // if witch has not spawned twice by round 4, force her spawn
        if(roundCount >= 4 && witchLevelCount < 2)
        {
            WitchMovement.moveWitch();
            witchLevelCount++;
        }else if (randomNum < 33.0f)
        {
            WitchMovement.moveWitch();
            witchLevelCount++;
        }
    }

    bool isWitchOnScreen()
    {
        return WitchMovement.isMoving;
    }

    void clearSpecialPumpkins()
    {
        if (PumpkinMovement.pumpkins.Count > 2)
        {
            for (int i = 2; i < PumpkinMovement.pumpkins.Count; i++)
            {
                GameObject pumpkinToRemove = PumpkinMovement.pumpkins[i];
                Destroy(pumpkinToRemove);
            }
            PumpkinMovement.pumpkins.RemoveRange(2, 10);
        }
    }
}
