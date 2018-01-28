using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePath : MonoBehaviour {
	public float _radius = 5;
	public int _laps = 5;
	public bool _isGravityOn;
	public string _onFinishLaps = "ActivateGateKey";
	private Switch _switchScript;
	private EnergyLink _energyLink;
	// Use this for initialization
	void Start () {
		_switchScript = GetComponent<Switch> ();
		_energyLink = FindObjectOfType<EnergyLink> ();
		_playerStartAngles = new float[2];
	}
	private float[] _playerStartAngles;
	// Update is called once per frame
	void Update () {

		if (_switchScript._switchColor == MovementControl.PlayerType.PurplePlayer) {
			int playersWithEnoughLaps = 0;
			for (int i = 0; i < _switchScript._playersInside.Count; i++) {
				MovementControl player = _switchScript._playersInside [i];
				if (!_energyLink.LineRenderer.enabled) {
					_playerStartAngles[i] = player.CirclingAngle;
				}
				Debug.Log ("circling angle:"+player.CirclingAngle);
				if (Mathf.Abs(player.CirclingAngle - _playerStartAngles[i]) > 360*_laps) {
					playersWithEnoughLaps++;

				}
			}
			if (playersWithEnoughLaps == 2) {
				SendMessage (_onFinishLaps, SendMessageOptions.DontRequireReceiver);
				DettachPlayersInside ();
			}

		} else {
			for (int i = 0; i < _switchScript._playersInside.Count; i++) {
				MovementControl player = _switchScript._playersInside [i];
				if (player._playerType != _switchScript._switchColor) {
					return;
				}

				if (Mathf.Abs(player.CirclingAngle - _playerStartAngles[i]) > 360*_laps) {
					SendMessage (_onFinishLaps, SendMessageOptions.DontRequireReceiver);
					DettachPlayersInside ();
				}
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
		Debug.Log ("attach players inside");
		_isGravityOn = true;
		for (int i = 0; i < _switchScript._playersInside.Count; i++) {
			
			MovementControl player = _switchScript._playersInside [i];
			if (_switchScript._switchColor != MovementControl.PlayerType.PurplePlayer) {
				if (_switchScript._switchColor != player._playerType) {
					return;
				}
			}
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
			_playerStartAngles [i] = angle;
		}
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere (transform.position, _radius);
	}
}
