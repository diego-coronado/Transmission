using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTrigger : MonoBehaviour {
	public bool _activated;
	private bool _startChecking;
	private static CameraControl _cameraControl;
	public MovementControl[] _players;
	private Collider2D[] _colliders;
	// Use this for initialization
	void Awake () {
		_colliders = GetComponentsInChildren<Collider2D> ();
		if (_cameraControl == null) {
			_cameraControl = FindObjectOfType<CameraControl> ();
		}

		_players = FindObjectsOfType<MovementControl> ();

		foreach (var theCol in _colliders) {
			theCol.enabled = true;
			theCol.isTrigger = true;
		}
	}

	void Update(){
		if (_activated || !_startChecking) {
			return;
		}

        for (int i = 0; i < _players.Length; i++)
        {
            var thePlayer = _players[i];
            if (thePlayer.transform.position.y <= transform.position.y)
            {
                return;
            }
        }
        
		_activated = true;
		foreach (var theCol in _colliders) {
			theCol.enabled = true;
			theCol.isTrigger = false;
		}
		_cameraControl.MoveToNextLevel ();
	}
		
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag =="Player") {
			_startChecking = true;
		}
	}
}
