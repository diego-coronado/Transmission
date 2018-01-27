using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyLink : MonoBehaviour {
	
    public Transform first;
    public Transform second;

	public LayerMask mask;

    public float minDistance = 5;
    public float minTimeBetweenLinks = 0.5f;

    private LineRenderer _lineRenderer;
	public LineRenderer LineRenderer {
		get{ return _lineRenderer; }
		set{ _lineRenderer = value; }
	}
    private MovementControl _player1;
    private MovementControl _player2;
    private float _distance;
    private bool _canConnect;
    

    // Use this for initialization
    void Start() {
        _lineRenderer = GetComponent<LineRenderer>();
        _player1 = first.GetComponent<MovementControl>();
        _player2 = second.GetComponent<MovementControl>();
	}

    // Update is called once per frame
    void Update() {

		if (Physics2D.Linecast(first.position,second.position,mask)) {
			_player1.GetComponent<Light>().enabled = false;
			_player2.GetComponent<Light>().enabled = false;
			_player1.isEnabled = false;
			_player2.isEnabled = false;
			_canConnect = false;
			_lineRenderer.enabled = false;
			return;
		}

        _distance = Vector3.Distance(first.position, second.position);
        if (_distance <= minDistance && _player1.isEnabled)
        {
            _canConnect = true;
        }
        else
        {
            _canConnect = false;
        }

        if (_canConnect)
        {
            _player1.GetComponent<Light>().enabled = true;
            _player2.GetComponent<Light>().enabled = true;

            float timeBetweenLinks = Mathf.Abs(_player1.timeSinceLinkActivated - _player2.timeSinceLinkActivated);
            Debug.Log(timeBetweenLinks);
            if (_player1.linkActivated && _player2.linkActivated && timeBetweenLinks <= minTimeBetweenLinks)
            {
                _lineRenderer.enabled = true;
                _lineRenderer.SetPosition(0, first.position);
                _lineRenderer.SetPosition(1, second.position);
            }
            else
            {
                _lineRenderer.enabled = false;
            }
        }
        else
        {
            _player1.GetComponent<Light>().enabled = false;
            _player2.GetComponent<Light>().enabled = false;

            _lineRenderer.enabled = false;
            if (_player1.linkActivated || _player2.linkActivated)
            {
                _player1.isEnabled = false;
                _player2.isEnabled = false;
            }
            else
            {
                _player1.isEnabled = true;
                _player2.isEnabled = true;
            }
        }
    }
}
