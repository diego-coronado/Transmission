using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
public class MovementControl : MonoBehaviour {
	public enum PlayerType
	{
		BluePlayer,
		RedPlayer,
		PurplePlayer
	}
	public PlayerType _playerType;
    public float _speed = 5;
	public float _circleSpeed = 60;
	[System.NonSerialized]
    public bool linkActivated = false;
	[System.NonSerialized]
    public bool isEnabled = false;
	[System.NonSerialized]
    public float timeSinceLinkActivated = 10000;


	public enum MovementMode
	{
		Normal,
		Circle
	};
//	[System.NonSerialized]
	public MovementMode _movementMode;

	private Vector2 _circlingCenter;
	public Vector2 CirclingCenter {
		get{ return _circlingCenter; }
		set{ _circlingCenter = value; }
	}
	private float _circlingAngle;
	public float CirclingAngle {
		get{ return _circlingAngle; }
		set{ _circlingAngle = value; }
	}

	private float _circlingRadius;
	public float CirclingRadius {
		get{ return _circlingRadius; }
		set{ _circlingRadius = value; }
	}

    private Player _input;
    private float _h;
    private float _v;

	private Rigidbody2D _rigidbody;
	// Use this for initialization
	void Start () {
        _input = ReInput.players.GetPlayer(0);
		_rigidbody = GetComponent<Rigidbody2D> ();
	}

    // Update is called once per frame
    void Update() {
		ReceiveInputs ();
		Vector2 movement = Vector2.zero;
		if (_movementMode == MovementMode.Normal) {
			movement = new Vector3(_h, _v, 0);
			movement *= _speed * Time.deltaTime;	
			Vector2 pos = new Vector2(transform.position.x,transform.position.y);
			_rigidbody.MovePosition (pos + movement);
		} else {
			_circlingAngle += _h * _circleSpeed*Time.deltaTime;
			movement = _circlingCenter;
			Vector3 rotVector = Quaternion.Euler (0, 0, -_circlingAngle) * Vector3.up * _circlingRadius;
			movement += new Vector2 (rotVector.x, rotVector.y);


			_rigidbody.MovePosition (movement);
		}
      




    }

	void ReceiveInputs(){
		if (_playerType == PlayerType.BluePlayer)
		{
			_h = _input.GetAxis("MoveLeftX");
			_v = _input.GetAxis("MoveLeftY");

			linkActivated = _input.GetButton("ActivateLeftLink");
			if (_input.GetButtonDown("ActivateLeftLink"))
			{
				timeSinceLinkActivated = Time.timeSinceLevelLoad;
			}
		}
		else
		{   
			_h = _input.GetAxis("MoveRightX");
			_v = _input.GetAxis("MoveRightY");

			linkActivated = _input.GetButton("ActivateRightLink");
			if ( _input.GetButtonDown("ActivateRightLink"))
			{
				timeSinceLinkActivated = Time.timeSinceLevelLoad;
			}
		}
	}
}
