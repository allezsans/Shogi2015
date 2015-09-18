using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
	Waiting waiting;

	public float RequestInterval = 1.0f;
	// Use this for initialization
	void Start () {
		keyName = Resources.Load<JsonKeyName> ("Setting/JsonKeyName");
		state = State.unknown;
	}
	
	// Update is called once per frame
	void Update () {
		CheckState ();
	}

	void InitState(State s)
	{
		state = s;
		// stateが変わった時の初期化処理
		switch (s)
		{
			case State.waiting:
				waiting = new Waiting();
				waiting.Init();
				break;
			case State.playing:
				if( waiting != null ) waiting.Dispose();
				WWWManager.Instance.Get(WWWManager.GET.PIECES, data =>
				{
					// TODO:全体の駒を扱うクラスを作って処理を渡す
					foreach (var piece in data)
					{
						InitPieces(piece.Value);
					}
				});
				break;
		}
	}

	void InitPieces(object obj)
	{
		var pi = (Dictionary<string, object>)obj;

		var p = Resources.Load("Prefab/piece");
		var piece = GameObject.Instantiate(p) as GameObject;
		piece.GetComponent<Piece>().SetPieceInfo(pi[keyName.pieceName].ToString(), (long)pi[keyName.posX], (long)pi[keyName.posY]);
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
			default:
				state = State.unknown;
				break;
		}
		if (state == State.unknown) {
			Debug.LogError("state is unknown...");
		}
	}

	void CheckChangeState(State s){
		if (state != s)
		{
			InitState(s);
		}
	}
}
