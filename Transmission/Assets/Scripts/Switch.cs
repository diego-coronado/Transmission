using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {

	public bool _requireLink;
	public bool _mustHoldDown;
	public string OnActivateMessage = "";

	private static EnergyLink _energyLink;
	private bool _isOn;
	public bool IsOn {
		get { return _isOn;}
		set { _isOn = value;}
	}

	private Renderer _renderer;
	private bool _isPlayerInside = false;

	// Use this for initialization
	void Awake () {
		_renderer = GetComponent<Renderer>();
		if (_energyLink == null) {
			_energyLink = FindObjectOfType<EnergyLink> ();
		}
	}


	void Update(){
		if (_isPlayerInside) {
			if (_renderer.material.color != Color.green) {
				bool activate = true;
				if (_requireLink && !_energyLink.LineRenderer.enabled) {
					activate = false;
				}
				if (activate) {
					_renderer.material.color = Color.green;
					if (OnActivateMessage != "") {
						SendMessage (OnActivateMessage);
					}
					_isOn = true;

				}

			} else {
				if (_requireLink && !_energyLink.LineRenderer.enabled) {
					_isOn = false;
					_renderer.material.color = Color.yellow;
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (!this.enabled) {
			return;
		}
		if(other.tag =="Player"){
			_isPlayerInside = true;
			if(_renderer.material.color != Color.green){
				bool activate = true;
				if (_requireLink && !_energyLink.LineRenderer.enabled) {
					activate = false;
				}
				if (activate) {
					_renderer.material.color = Color.green;
					if (OnActivateMessage != "") {
						SendMessage (OnActivateMessage);
					}
					_isOn = true;

				}

			}
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (!this.enabled || !_mustHoldDown) {
			return;
		}
		if(other.tag =="Player"){
			_isPlayerInside = false;
			_renderer.material.color = Color.yellow;
			_isOn = false;
		}
	}

	void OnDisable(){
		_renderer.material.color = Color.white;
		_isPlayerInside = false;
	}

	void OnEnable(){
		_renderer.material.color = Color.yellow;
	}
		
}
