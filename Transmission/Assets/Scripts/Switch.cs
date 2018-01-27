using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {
	
	public MovementControl.PlayerType _switchColor;
	public bool _requireLink;
	public bool _mustHoldDown;
	public string OnActivateMessage = "";

	private static EnergyLink _energyLink;
	private bool _isOn;
	public bool IsOn {
		get { return _isOn;}
		set { _isOn = value;}
	}

	private List<MovementControl> _playersInside;
	private Renderer _renderer;
	private bool _isPlayerInside = false;

	// Use this for initialization
	void Awake () {
		_playersInside = new List<MovementControl> ();
		_renderer = GetComponent<Renderer>();
		if (_energyLink == null) {
			_energyLink = FindObjectOfType<EnergyLink> ();
		}
	}


	void Update(){
		if (_isPlayerInside) {
			if (_renderer.material.color != Color.green) {
				CheckStepOnSwitch ();

			} else {
				if (_requireLink && !_energyLink.LineRenderer.enabled) {
					_isOn = false;
					if (_switchColor == MovementControl.PlayerType.BluePlayer) {
						_renderer.material.color = Color.blue;	
					} else if(_switchColor == MovementControl.PlayerType.RedPlayer) {
						_renderer.material.color = Color.red;	
					}else{
						_renderer.material.color = new Color(0.5f,0,1);
					}
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (!this.enabled) {
			return;
		}
		if(other.tag =="Player"){
			MovementControl playerScript = other.GetComponent<MovementControl> ();
			_playersInside.Add (playerScript);
			if (playerScript != null) {

				if (_switchColor == MovementControl.PlayerType.PurplePlayer) {
					if (_playersInside.Count == 2) {
						_isPlayerInside = true;
						CheckStepOnSwitch ();
					}
				} else {
					if (_switchColor == playerScript._playerType) {
						_isPlayerInside = true;
						CheckStepOnSwitch ();
					}
				}
			}

		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (!this.enabled) {
			return;
		}
		if(other.tag =="Player"){
			MovementControl playerScript = other.GetComponent<MovementControl> ();
			_playersInside.Remove (playerScript);

			if (playerScript._playerType != MovementControl.PlayerType.PurplePlayer) {
				if (!_mustHoldDown) {
					return;
				}
			}	

			if (_switchColor == MovementControl.PlayerType.PurplePlayer) {
				_isPlayerInside = false;
			} else {
				if (_switchColor == playerScript._playerType) {
					_isPlayerInside = false;
				}
			}

			if (_switchColor == MovementControl.PlayerType.BluePlayer) {
				_renderer.material.color = Color.blue;	
			} else if(_switchColor == MovementControl.PlayerType.RedPlayer) {
				_renderer.material.color = Color.red;	
			}else{
				_renderer.material.color = new Color(0.5f,0,1);
			}
			_isOn = false;


		}
	}

	void OnDisable(){
		_renderer.material.color = Color.white;
		_isPlayerInside = false;
	}

	void OnEnable(){
		if (_switchColor == MovementControl.PlayerType.BluePlayer) {
			_renderer.material.color = Color.blue;	
		} else if(_switchColor == MovementControl.PlayerType.RedPlayer) {
			_renderer.material.color = Color.red;	
		}else{
			_renderer.material.color = new Color(0.5f,0,1);
		}
	}
		

	void CheckStepOnSwitch(){
		bool activate = true;
		if (_requireLink && !_energyLink.LineRenderer.enabled) {
			activate = false;
		}
		if (activate) {
			_renderer.material.color = Color.green;
			if (OnActivateMessage != "") {
				SendMessage (OnActivateMessage,SendMessageOptions.DontRequireReceiver);
			}
			_isOn = true;
		}
	}

	void EnableSwitch(){
		this.enabled = true;
	}
}
