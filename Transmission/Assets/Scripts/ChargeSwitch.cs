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
    public string OnChargedMessage = "";

    private float _lastLinkActivatedTime;
    private MovementControl _player = null;
    private float _energyCharged = 0;
    private float timeDif;
    private bool _isLinkPressed = false;
    private Player _input;
    private bool _hasPlayerLeft = false;
    private bool _finishedCharging = false;

    private void Start()
    {
        _input = ReInput.players.GetPlayer(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (_hasPlayerLeft && _energyCharged>0 && !_finishedCharging)
        {
            _energyCharged -= Time.deltaTime * chargeReductionSpeed;
        }

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
                if (timeDif >= maxTimeDifference && !_finishedCharging)
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

            if (_energyCharged > maxCharge)
            {
                if (OnChargedMessage != "")
                {
                    _finishedCharging = true;
                    SendMessage(OnChargedMessage);
                }
            }

            _isLinkPressed = true;
            Debug.Log("Energy charged: " + _energyCharged);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _player = other.GetComponent<MovementControl>();
            _hasPlayerLeft = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _hasPlayerLeft = true;
            _isLinkPressed = false;
        }
    }

    public float GetChargePercentage()
    {
        return _energyCharged / maxCharge;
    }
}
