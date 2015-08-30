using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Login : MonoBehaviour {
	[SerializeField] InputField userName;
	[SerializeField] InputField roomNo;

	string playerName = string.Empty;
	string playRoom = string.Empty;

	JsonKeyName keyName;

	void Start(){
		keyName = Resources.Load<JsonKeyName> ("Setting/JsonKeyName");
	}

	public void OnEndEditUserName(){
		playerName = userName.text;
	}

	public void OnEndEditRoomNo(){
		playRoom = roomNo.text;
	}

	public void OnClickEnterButton(){
		if (playerName == string.Empty || playRoom == string.Empty) return;

		Dictionary<string,string> keys = new Dictionary<string, string> (){
			{keyName.userName,playerName},
			{keyName.roomNo,playRoom}
		};
		WWWManager.Instance.Post(WWWManager.POST.LOGIN,keys, data => {
			GlobalDataPool.Instance.userId = (long)data[keyName.userId];
			GlobalDataPool.Instance.playId = (long)data[keyName.playId];
			GlobalDataPool.Instance.gameState = data[keyName.state].ToString();
			GlobalDataPool.Instance.role = data[keyName.role].ToString();
			Application.LoadLevel("Main");
		});
	}
}
