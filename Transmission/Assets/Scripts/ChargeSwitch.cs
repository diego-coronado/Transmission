using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class ChargeSwitch : MonoBehaviour
{
    public float maxTimeDifference = 1f;
    public float chargeIncreaseSpeed = 2.5f;
    public float chargeReductionSpeed = 0.5f;
    public float maxCharge = 100;

    private float _lastLinkActivatedTime;
    private MovementControl _player = null;
    private float _energyCharged;
    private float timeDif;
    private bool _isLinkPressed = false;
    private Player _input;

    private void Start()
    {
        _input = ReInput.players.GetPlayer(0);
    }

    // Update is called once per frame
    void Update()
    {

        if (_player)
        {
            if (_player._playerId == 0)
            {
                if (_input.GetButtonDown("ActivateLeftLink"))
                {
                    ChargeEnergy();
                }
            }
            else if (_player._playerId == 1)
            {
                if (_input.GetButtonDown("ActivateRightLink"))
                {
                    ChargeEnergy();
                }
            }

            if (_isLinkPressed)
            {
                timeDif = Mathf.Abs(Time.timeSinceLevelLoad - _player.timeSinceLinkActivated);
                if (timeDif >= maxTimeDifference)
                {
                    Debug.Log("disminucion de carga");
                    _energyCharged -= Time.deltaTime * chargeReductionSpeed;
                    Debug.Log("Energy charged: " + _energyCharged);
                }
            }
        }
    }

    public void ChargeEnergy()
    {
        if (_player)
        {
            Debug.Log("time diff: " + timeDif);
            Debug.Log("aumento de carga");
            _energyCharged += chargeIncreaseSpeed;
            _isLinkPressed = true;
            Debug.Log("Energy charged: " + _energyCharged);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _player = other.GetComponent<MovementControl>();
            _energyCharged = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _player = null;
            _energyCharged = 0;
            _isLinkPressed = false;
        }
    }
}
