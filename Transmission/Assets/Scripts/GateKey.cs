using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateKey : MonoBehaviour {
	public bool _isActivated = false;
	public bool IsActivated {
		get{return _isActivated; }
		set{_isActivated = value;}
	}


	// Use this for initialization
	void Start () {
		
	}
	
	void ActivateGateKey () {
		_isActivated = true;
	}
}
