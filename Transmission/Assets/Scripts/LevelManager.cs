using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    public Text timerText;

    public GameObject mainCamera;
    public Transform player1;
    public Transform player2;

    private float _timer;
    public float[] _maxTimePerLevel;
    private bool _runTimer;
    private int _currentLevel;

    private void Awake()
    {
        /*
		PlayerPrefs.SetInt("Level", 0);
        Application.Quit();
		*/
        _currentLevel = PlayerPrefs.GetInt("Level", 0);
        Debug.Log("lvl en awake "+_currentLevel);
        player1.position = new Vector3(4, 10 * _currentLevel - 4, 0);
        player2.position = new Vector3(-4, 10 * _currentLevel - 4, 0);
        mainCamera.transform.position = new Vector3(0, 10 * _currentLevel, -10);
        
    }

    private void Start()
    {
        _runTimer = true;
        _timer = _maxTimePerLevel[_currentLevel];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayerPrefs.SetInt("Level", 0);
        }

        if (_runTimer)
        {
            _timer -= Time.deltaTime;
            Debug.Log(_timer);
            timerText.text = "Tiempo para terminar: " + _timer;
            if (_timer <= 0)
            {
                GameOver();
            }
        }
    }

    public void MoveToNextLvl()
    {
        _currentLevel = PlayerPrefs.GetInt("Level", 0);
        PlayerPrefs.SetInt("Level", _currentLevel + 1);
        Debug.Log("nuevo lvl: "+(_currentLevel+1));
        CancelInvoke("GameOver");
        Invoke("GameOver", _maxTimePerLevel[_currentLevel + 1]);
        _timer = _maxTimePerLevel[_currentLevel + 1];
        _runTimer = true;
    }

    private void GameOver()
    {
        _runTimer = false;
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
