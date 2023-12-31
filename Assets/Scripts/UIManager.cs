using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    private Player _player;
    [SerializeField]
    private Sprite[] _livesSprite;
    [SerializeField]
    private Image _currentLiveSprite;
    [SerializeField]
    private TextMeshProUGUI _scroceText, _bestScroseText;
    [SerializeField]
    private TextMeshProUGUI _gameOverText;
    [SerializeField]
    private TextMeshProUGUI _restartGameText;
    [SerializeField]
    private GameControl _gameControl;
    // Start is called before the first frame update

    private void Awake()
    {
        this.gameObject.SetActive(true);
    }


    void Start()
    {
        _player = Player.Instance;
        _gameControl = GameObject.Find("GameControl").GetComponent<GameControl>();
        _gameOverText = transform.Find("GameOverText").GetComponent<TextMeshProUGUI>();
        _restartGameText = transform.Find("RestartText").GetComponent <TextMeshProUGUI>();
        _scroceText = transform.Find("ScorePoint").GetComponent<TextMeshProUGUI>();
        _bestScroseText = transform.Find("BestScorePoint").GetComponent<TextMeshProUGUI>();


        if (_bestScroseText == null) Debug.LogError("Cant find _bestScroseText");
        if (_scroceText == null) Debug.LogError("Cant find _scroceText");
        if (_gameOverText == null) Debug.LogError("Cant find _gameOverText");
        if (_restartGameText == null) Debug.LogError("Cant find _restartGameText");
        if (_player == null) Debug.LogError("Cant find player, UIManager!");
        if(_player != null)
        {
            UpdateLiveSprite(_player.GetLives());
            UpdateScoreText(_player.GetScorePoint());
            UpdateBestScore(PlayerPrefs.GetInt("BestScore", 0));
        }
        _gameOverText.enabled = false;
        _restartGameText.enabled = false;
    }

    public void UpdateLiveSprite(int index)
    {
        _currentLiveSprite.sprite = _livesSprite[index];
    }
    public void DisplayGameOver()
    {
        _gameOverText.enabled = true;
        _restartGameText.enabled = true;
        StartCoroutine(loopGameOverText());
        _gameControl.Restart();
    }

    public void UpdateScoreText(int index)
    {
        _scroceText.text = "Score: " + index;
    }

    public void UpdateBestScore(int index)
    {
        _bestScroseText.text = "Best: " + index;
    }

    IEnumerator loopGameOverText()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "GAME OVER";
        }
    }

}
