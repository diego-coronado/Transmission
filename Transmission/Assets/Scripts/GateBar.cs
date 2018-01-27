using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateBar : MonoBehaviour {

	public ChargeSwitch gateSwitch;

    private void Update()
    {
        transform.localScale = new Vector3(Mathf.Clamp(gateSwitch.GetChargePercentage(), 0, 1), 1, 0);
    }
}
