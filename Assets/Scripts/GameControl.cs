using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    [SerializeField]
    private bool _canRestart;
    // Start is called before the first frame update
    void Start()
    {
        _canRestart = false;
    }

    // Update is called once per frame
    void Update()
    {
        RestartGame();
        QuitGame();
    }
    private void RestartGame()
    {
        if(_canRestart && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        }
    }

    public void Restart()
    {
        _canRestart = true;
    }

    public void QuitGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

}
