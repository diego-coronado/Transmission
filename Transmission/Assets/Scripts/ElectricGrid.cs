using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GateKey))]
public class ElectricGrid : MonoBehaviour {

	private Switch[] _panels;
	private Switch[] _chosenPanels;
	//cuantas veces debes activar el grid para que termine
	public int _requiredClicks = 5;
	private int _clickCounter;
	public string OnActivateMessage ="ActivateGateKey";
	// Use this for initialization
	void Start () {
		_panels = GetComponentsInChildren<Switch>();
		_chosenPanels = new Switch[2];
		ChooseNewPanels();
	}
	
	// Update is called once per frame
	void Update () {
		int counter = 0;
		foreach(var theSwitch in _panels){
			if(theSwitch.IsOn && theSwitch.enabled){
				counter++;
			}
		}
		if(counter >= 2){
			_clickCounter++;
			if (_clickCounter >= _requiredClicks) {
				SendMessage (OnActivateMessage);
				Finish ();
			} else {
				ChooseNewPanels();
			}
		}
	}

	private int _previousFirstIndex;
	private int _previousSecondIndex;

	void ChooseNewPanels(){
		foreach(var theSwitch in _panels){
			theSwitch.IsOn = false;
			theSwitch.enabled = false;
		}

		int firstIndex = Random.Range(0,_panels.Length);
		while(firstIndex == _previousFirstIndex || firstIndex == _previousSecondIndex){
			firstIndex = Random.Range(0,_panels.Length);
		}
		int secondIndex = Random.Range(0,_panels.Length);
		while(secondIndex == firstIndex || secondIndex == _previousSecondIndex || secondIndex == _previousFirstIndex){
			secondIndex = Random.Range(0,_panels.Length);
		}

		_previousFirstIndex = firstIndex;
		_previousSecondIndex = secondIndex;

		_chosenPanels[0] = _panels[firstIndex];
		_chosenPanels[0].enabled = true;
		_chosenPanels[1] = _panels[secondIndex];
		_chosenPanels[1].enabled = true;

	}

	void Finish(){
		foreach(var theSwitch in _panels){
			theSwitch.IsOn = false;
			theSwitch.enabled = false;
			theSwitch.GetComponent<Renderer> ().material.color = Color.green;
		}

	}
}
