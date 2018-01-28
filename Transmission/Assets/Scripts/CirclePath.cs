using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePath : MonoBehaviour {
	public float _radius = 5;
	public bool _isGravityOn;
	private Switch _switchScript;
	// Use this for initialization
	void Start () {
		_switchScript = GetComponent<Switch> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void AttachPlayersInside(){
		_isGravityOn = true;
		foreach (var player in _switchScript._playersInside) {
			player._movementMode = MovementControl.MovementMode.Circle;
			player.CirclingCenter = transform.position;

		}
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere (transform.position, _radius);
	}
}
