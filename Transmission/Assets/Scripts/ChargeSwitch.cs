using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeSwitch : MonoBehaviour
{
    public float maxTimeDifference = 1f;
    public float chargeIncreaseSpeed = 2.5f;
    public float chargeReductionSpeed = 0.5f;

    private float _lastLinkActivatedTime;
    private MovementControl _player = null;
    private float _energyCharged;
    private float timeDif;
    private bool _isLinkPressed = false;

    // Update is called once per frame
    void Update()
    {
        if (_player)
        {
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
