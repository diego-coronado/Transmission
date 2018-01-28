using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTrigger : MonoBehaviour {
	public bool _activated;
	private static CameraControl _cameraControl;
	public MovementControl[] _players;
	private Collider2D[] _colliders;
	// Use this for initialization
	void Awake () {
		_colliders = GetComponentsInChildren<Collider2D> ();
		if (_cameraControl == null) {
			_cameraControl = FindObjectOfType<CameraControl> ();
		}
		if (_players == null) {
			_players = FindObjectsOfType<MovementControl> ();
		}

		foreach (var theCol in _colliders) {
			theCol.enabled = false;
		}
	}

	void Update(){
		if (_activated) {
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
		}
		_cameraControl.MoveToNextLevel ();
	}
		
}
