using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;
public class PressStartToPlay : MonoBehaviour {
	public GameObject _creditsScreen;
	private Player _input;
	// Use this for initialization
	void Start () {
		_input = ReInput.players.GetPlayer (0);
		PlayerPrefs.SetInt ("Level", 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (_input.GetButtonDown("Pause")) {
			SceneManager.LoadScene ("gameplayHito");
		}

		if (_input.GetButtonDown("Select")) {
			_creditsScreen.SetActive (!_creditsScreen.active);
		}
	}
}
