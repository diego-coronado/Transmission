using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour {
	public Transform[] _gates;
	public GateKey[] _keys;
	private bool _shouldOpen;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (!_shouldOpen) {
			//todas las llaves deben esta activadas
			foreach (var theKey in _keys) {
				if (!theKey.IsActivated) {
					return;
				}
			}
			_shouldOpen = true;
		} else {
			foreach (var theGate in _gates) {
				Vector3 localScale = theGate.localScale;
				localScale.x -= Time.deltaTime*8;
				localScale.x = Mathf.Max (localScale.x, 0);
				theGate.localScale = localScale;	
			}

		}


	}
}
