using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateObserver : MonoBehaviour {

	enum State{
		waiting,
		playing,
		exit,
		finish,
		unknown,
	}

	State state;
	float pastTime;
	JsonKeyName keyName;
	bool isChangeState = false;	// ステートが変わったフレームの時true

	public float RequestInterval = 1.0f;
	// Use this for initialization
	void Start () {
		keyName = Resources.Load<JsonKeyName> ("Setting/JsonKeyName");
		state = State.unknown;
	}
	
	// Update is called once per frame
	void Update () {
		CheckState ();
		// stateが変わった時の初期化処理
		switch(state){
			case State.waiting:
				break;
			case State.playing:
				if(isChangeState){
					WWWManager.Instance.Get(WWWManager.GET.PIECES, data => {
						var p = Resources.Load("Prefab/piece");
						var piece = GameObject.Instantiate(p) as GameObject;
						piece.GetComponent<Piece>().SetPieceInfo("fu",5,4);
					});
				}
				break;
		}
		isChangeState = false;
	}
	
	void CheckState(){
		pastTime += Time.deltaTime;
		if (pastTime >= RequestInterval) {
			WWWManager.Instance.Get(WWWManager.GET.GAME, data => {
				SetStateFromString( data[keyName.state].ToString() );
			});
			pastTime = 0.0f;
		}
	}

	void SetStateFromString(string stateName){
		switch(stateName){
			case "waiting": CheckChangeState(State.waiting); break;
			case "playing": CheckChangeState(State.playing); break;
			case "finish": CheckChangeState(State.finish); break;
			case "exit": CheckChangeState(State.exit); break;
			default: state = State.unknown; break;
		}
		if (state == State.unknown) {
			Debug.LogError("state is unknown...");
		}
	}

	void CheckChangeState(State s){
		if(state != s) isChangeState = true; 
		state = s;
	}
}
