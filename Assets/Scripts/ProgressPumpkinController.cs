using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressPumpkinController : MonoBehaviour
{
    public static List<GameObject> progressPumpkins = new List<GameObject>();
    public static Sprite emptyPumpkin;
    public static Sprite fullPumpkin;
    public static int progressIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        progressIndex = 0;
        progressPumpkins.Add(gameObject);
        emptyPumpkin = Resources.Load<Sprite>("emptyPumpkin");
        fullPumpkin = Resources.Load<Sprite>("fullPumpkin");
    }

    public static void updateProgress(bool isHit)
    {
        if (isHit)
        {
            GameObject.Find("ProgressPumpkin" + progressIndex).GetComponent<SpriteRenderer>().sprite = fullPumpkin;
        }
        progressIndex++;
    }

    public static void resetProgressPumpkins()
    {
        for (int i = 0; i < progressPumpkins.Count; i++)
        {
            progressPumpkins[i].GetComponent<SpriteRenderer>().sprite = emptyPumpkin;
        }

        progressIndex = 0;
    }
}
