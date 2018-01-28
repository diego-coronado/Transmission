using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour {
    public Text timerText;

	private bool _moving = false;
	private Vector3 _targetPos;
    private float _timer;
    public float _maxTime = 30;
    private bool _runTimer;

	// Use this for initialization
	void Start () {
        _runTimer = true;
        _timer = _maxTime;
	}
	
	// Update is called once per frame
	void Update () {
        if (_runTimer)
        {
            _timer -= Time.deltaTime;
            Debug.Log(_timer);
            timerText.text = "Tiempo para terminar: "+_timer;
            if (_timer <= 0)
            {
                GameOver();
            }
        }

		if (_moving) {
			Vector3 newPos = Vector3.Lerp (transform.position, _targetPos, Time.deltaTime * 10);
			float distSqr = Vector3.SqrMagnitude (newPos - _targetPos);
			if (distSqr < 0.1f) {
				newPos = _targetPos;
				_moving = false;
			}
			transform.position = newPos;
		}
	}

	public void MoveToNextLevel(){
		_moving = true;
		_targetPos = transform.position + (Vector3.up * 10);
        int level = PlayerPrefs.GetInt("Level", 0);
        PlayerPrefs.SetInt("Level", level + 1);
        CancelInvoke("GameOver");
        Invoke("GameOver", _maxTime);
        _runTimer = true;
        _timer = _maxTime;

	}

    private void GameOver()
    {
        _runTimer = false;
        SceneManager.LoadScene(0);
    }
}
