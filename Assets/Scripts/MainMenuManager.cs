using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public Button normalButton;
    public Button specialButton;
    public Button exitButton;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;

        exitButton.GetComponent<Button>();
        exitButton.onClick.AddListener(ExitGame);

        normalButton.GetComponent<Button>();
        normalButton.onClick.AddListener(StartNormal);

        specialButton.GetComponent<Button>();
        specialButton.onClick.AddListener(StartSpecial);
    }

    void StartNormal()
    {
        GameController.isSpecial = false;
        SceneManager.LoadScene("MainGame");
    }

    void StartSpecial()
    {
        GameController.isSpecial = true;
        SceneManager.LoadScene("MainGame");
    }

    void ExitGame()
    {
        Application.Quit();
    }
}
