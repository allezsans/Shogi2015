using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameSetting : ScriptableObject {

	[Header("タイトル")]
	public string titleText;

	[Header("通信全般")]
	public string serverUrl;
	public string serverPort;
	public float timeOut;

	[Header("POST")]
	public string loginUrl;
	public string updatePieceUrl;
	public string logoutUrl;

	[Header("GET")]
	public string gameStateUrl;
	public string userStateUrl;
	public string playStateUrl;
	public string winnerUrl;
	public string pieceStateUrl;
}
