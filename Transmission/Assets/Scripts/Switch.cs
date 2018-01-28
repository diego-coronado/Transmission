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

	public List<MovementControl> _playersInside;
	private SpriteRenderer _spriteRenderer;
	private bool _isPlayerInside = false;

	// Use this for initialization
	void Awake () {
		_playersInside = new List<MovementControl> ();
		_spriteRenderer = GetComponent<SpriteRenderer> ();
		if (_energyLink == null) {
			_energyLink = FindObjectOfType<EnergyLink> ();
		}
	}


	void Update(){
		if (_isPlayerInside) {
			if (_spriteRenderer.color != Color.green) {
				CheckStepOnSwitch ();

			} else {
				if (_requireLink && !_energyLink.LineRenderer.enabled) {
					_isOn = false;
					if (_switchColor == MovementControl.PlayerType.BluePlayer) {
						_spriteRenderer.color = new Color(0,0.5f,1);	
					} else if(_switchColor == MovementControl.PlayerType.RedPlayer) {
						_spriteRenderer.color = Color.red;	
					} else if(_switchColor == MovementControl.PlayerType.AnyPlayer) {
						_spriteRenderer.color = Color.yellow;	
					}else{
						_spriteRenderer.color = new Color(0.5f,0,1);
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
				} else if(_switchColor == MovementControl.PlayerType.AnyPlayer){
					_isPlayerInside = true;
					CheckStepOnSwitch ();
				}else {
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
			} else if(_switchColor == MovementControl.PlayerType.AnyPlayer) {
				_isPlayerInside = false;
			} else {
				if (_switchColor == playerScript._playerType) {
					_isPlayerInside = false;
				}
			}

			if (!_isPlayerInside) {
				if (_switchColor == MovementControl.PlayerType.BluePlayer) {
					_spriteRenderer.color = new Color(0,0.5f,1);	
				} else if(_switchColor == MovementControl.PlayerType.RedPlayer) {
					_spriteRenderer.color = Color.red;	
				} else if(_switchColor == MovementControl.PlayerType.AnyPlayer) {
					_spriteRenderer.color = Color.yellow;	
				}else{
					_spriteRenderer.color = new Color(0.5f,0,1);
				}
				_isOn = false;
			}
		}
	}

	void OnDisable(){
		_spriteRenderer.color = Color.white;
		_isPlayerInside = false;
	}

	void OnEnable(){
		if (_switchColor == MovementControl.PlayerType.BluePlayer) {
			_spriteRenderer.color = new Color(0,0.5f,1);	
		} else if(_switchColor == MovementControl.PlayerType.RedPlayer) {
			_spriteRenderer.color = Color.red;	
		}else if(_switchColor == MovementControl.PlayerType.AnyPlayer){
			_spriteRenderer.color = Color.yellow;
		}else{
			_spriteRenderer.color = new Color(0.5f,0,1);
		}
	}
		

	void CheckStepOnSwitch(){
		bool activate = true;
		//linkActivated me dice si el jugador presiono el boton de accion
		if (_switchColor == MovementControl.PlayerType.BluePlayer &&
			!_energyLink.BluePlayer.linkActivated) {
			activate = false;
		}
		if (_switchColor == MovementControl.PlayerType.RedPlayer &&
			!_energyLink.RedPlayer.linkActivated) {
			activate = false;
		}

		if (_switchColor == MovementControl.PlayerType.AnyPlayer) {
			if (!_playersInside[0].linkActivated ) {
				activate = false;	
			}
		}

		//chequeamos si el link esta activo entre los jugadores
		if (_requireLink && !_energyLink.LineRenderer.enabled) {
			activate = false;
		}
		if (activate) {
			_spriteRenderer.color = Color.green;
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
