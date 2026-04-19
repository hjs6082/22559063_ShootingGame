using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUI_Lobby : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;

    void Start()
    {
        startButton.onClick.AddListener(OnClickStartButton);
        quitButton.onClick.AddListener(OnClickQuitButton);
    }

    public void OnClickStartButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
    }
}
