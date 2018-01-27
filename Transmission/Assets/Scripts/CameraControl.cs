using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
	private bool _moving = false;
	private Vector3 _targetPos;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
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
	}
}
