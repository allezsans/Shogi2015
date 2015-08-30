using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JsonKeyName : ScriptableObject {
	
	[Header("ゲーム情報")]
	public string userId;
	public string playId;
	public string userName;
	public string roomNo;

	[Header("対戦情報")]
	public string state;
	public string role;
	public string firstPlayer;
	public string lastPlayer;
	public string turnCount;
	public string watcherCount;
	public string turnPlyaer;
	public string winner;

	[Header("駒情報")]
	public string pieceName;
	public string owner;
	public string posX;
	public string posY;
	public string promote;
	public string moveId;
	public string getId;
}
