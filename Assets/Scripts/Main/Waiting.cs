using UnityEngine;
using System.Collections;

public class Waiting{

	WaitingEffect WaitingPlate;

	public void Init(){
		WaitingPlate = Resources.Load<WaitingEffect> ("Prefab/Waiting");
		WaitingPlate = GameObject.Instantiate<WaitingEffect>(WaitingPlate);
		WaitingPlate.transform.parent = GameObject.Find("Canvas").transform;
		WaitingPlate.transform.localScale = Vector3.one;
		WaitingPlate.transform.localPosition = Vector3.zero;
	}

	public void Dispose(){
		iTween.Stop ();
		GameObject.Destroy (WaitingPlate);
	}
}
