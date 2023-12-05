using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    [SerializeField]
    private bool _canRestart;
    [SerializeField]
    public GameObject pauseMenu;
    [SerializeField]
    public GameObject _mainCanvas;
    // Start is called before the first frame update
    private void Awake()
    {
        _mainCanvas = GameObject.Find("Canvas").gameObject;
        _mainCanvas.SetActive(true);
    }
    void Start()
    {
        _canRestart = false;
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        RestartGame();
        PauseGame();
    }
    private void RestartGame()
    {
        if(_canRestart && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Game");
        }
    }

    public void Restart()
    {
        _canRestart = true;
    }
    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeSelf == false)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }

    }
}
