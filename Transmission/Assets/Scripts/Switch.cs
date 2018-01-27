using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {
	private bool _isOn;
	public bool IsOn {
		get { return _isOn;}
		set { _isOn = value;}
	}
	public bool _mustHoldDown;
	public string OnActivateMessage = "";
	private Renderer _renderer;
	// Use this for initialization
	void Awake () {
		_renderer = GetComponent<Renderer>();
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (!this.enabled) {
			return;
		}
		if(other.tag =="Player"){
			if(_renderer.material.color != Color.green){
				_renderer.material.color = Color.green;
				if (OnActivateMessage != "") {
					SendMessage (OnActivateMessage);
				}
			}
			_isOn = true;
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		if (!this.enabled) {
			return;
		}
		if(other.tag =="Player"){
			if(_renderer.material.color != Color.green){
				_renderer.material.color = Color.green;
			}
			_isOn = true;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (!this.enabled || !_mustHoldDown) {
			return;
		}
		if(other.tag =="Player"){
			_renderer.material.color = Color.yellow;
			_isOn = false;
		}
	}

	void OnDisable(){
		_renderer.material.color = Color.white;
	}

	void OnEnable(){
		_renderer.material.color = Color.yellow;
	}
		
}
