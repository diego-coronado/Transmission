using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyLink : MonoBehaviour {
    public Transform first;
    public Transform second;
    private LineRenderer _lineRenderer;
    // Use this for initialization
    void Start() {
        _lineRenderer = GetComponent<LineRenderer>();
	}

    // Update is called once per frame
    void Update() {
        float distance = Vector3.Distance(first.position, second.position);
        Debug.Log(distance);
        if (distance < 5)
        {
            _lineRenderer.enabled = true;
            _lineRenderer.SetPosition(0, first.position);
            _lineRenderer.SetPosition(1, second.position);
            _lineRenderer.SetPosition(2, second.position);
        }else
        {
            _lineRenderer.enabled = false;
        }
       
	}
}
