using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePath : MonoBehaviour {
	public float _radius = 5;
	public int _laps = 5;
	public bool _isGravityOn;

	private Switch _switchScript;
	// Use this for initialization
	void Start () {
		_switchScript = GetComponent<Switch> ();
	}

	private float _playerStartingAngle;
	// Update is called once per frame
	void Update () {
		foreach (var player in _switchScript._playersInside) {
			if (player.CirclingAngle - _playerStartingAngle > 360*5) {
				DettachPlayersInside ();
			}
		}

	}

	void DettachPlayersInside(){
		_isGravityOn = false;
		foreach (var player in _switchScript._playersInside) {
			player._movementMode = MovementControl.MovementMode.Normal;
		}
	}

	void AttachPlayersInside(){
		_isGravityOn = true;
		foreach (var player in _switchScript._playersInside) {
			player._movementMode = MovementControl.MovementMode.Circle;
			player.CirclingCenter = transform.position;
			player.CirclingRadius = _radius;

			Vector3 dirToPlayer = player.transform.position - transform.position;
			dirToPlayer.z = 0;
			float angle = Vector3.Angle (Vector3.up, dirToPlayer);
			if (dirToPlayer.x < 0) {
				angle = 360 - angle;
			}
			player.CirclingAngle = angle;
			_playerStartingAngle = angle;
		}
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere (transform.position, _radius);
	}
}
