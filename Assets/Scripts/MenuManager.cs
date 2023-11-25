using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    [SerializeField]
    protected GameControl gameControl;
    void Start()
    {
        if (gameControl == null) Debug.Log("Not found gameControl!");
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void SettingMenu()
    {
        SceneManager.LoadScene("SettingMenu");
    }

    public void ResumeGame()
    {
        gameControl.pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
		    Application.Quit();
        #endif
    }

}
