﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
public class MovementControl : MonoBehaviour {
    public int _playerId = 0;
    public float _speed = 5;
    public bool linkActivated = false;
    public bool isEnabled = false;
    public float timeSinceLinkActivated = 10000;

    private Player _input;
    private float _h;
    private float _v;

	// Use this for initialization
	void Start () {
        //Debug.Log(Application.targetFrameRate);
        _input = ReInput.players.GetPlayer(0);
        
	}

    // Update is called once per frame
    void Update() {
        if (_playerId == 0)
        {
            _h = _input.GetAxis("MoveLeftX");
            _v = _input.GetAxis("MoveLeftY");

            linkActivated = _input.GetButton("ActivateLeftLink");
            if (_input.GetButtonDown("ActivateLeftLink"))
            {
                timeSinceLinkActivated = Time.timeSinceLevelLoad;
                GameObject.FindObjectOfType<ChargeSwitch>().ChargeEnergy();
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
                GameObject.FindObjectOfType<ChargeSwitch>().ChargeEnergy();
            }
        }

        Vector2 movement = new Vector3(_h, _v, 0);
        movement *= _speed * Time.deltaTime;
        transform.Translate(movement);


    }
}
