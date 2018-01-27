using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTrigger : MonoBehaviour {
	public NextLevelTrigger _other;
	public bool _activated;
	private static CameraControl _cameraControl;
	// Use this for initialization
	void Start () {
		if (_cameraControl == null) {
			_cameraControl = FindObjectOfType<CameraControl> ();
		}
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			_activated = true;
			if (_other._activated) {
				_cameraControl.MoveToNextLevel ();
				_other.gameObject.SetActive (false);
				gameObject.SetActive (false);
			}
		}
	}
}
