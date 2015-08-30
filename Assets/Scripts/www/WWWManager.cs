using UnityEngine;
using System.Collections;
using WWWKit;
using System.Collections.Generic;
using UnityEngine.Events;

public class WWWManager : SingletonMonoBehaviour<WWWManager>{
	//private
	float timeout = 3.0f;
	WWWClient mClient;
	GameSetting gs;
	string RequestUrl;

	public enum POST{
		LOGIN,
		UPDATE,
		LOGOUT,
	}
	public enum GET{
		GAME,
		USER,
		PLAY,
		WINNER,
		PIECES,
	}
	Dictionary<POST,string> postUrl;
	Dictionary<GET,string> getUrl;

	override protected void Awake(){
		if(this != Instance)
		{
			Destroy(this);
			return;
		}
		DontDestroyOnLoad(this);
		gs = Resources.Load<GameSetting>("Setting/GameSetting");
		RequestUrl = gs.serverUrl + gs.serverPort;

		postUrl = new Dictionary<POST,string>(){
			{POST.LOGIN,RequestUrl+gs.loginUrl},
			{POST.LOGOUT,RequestUrl+gs.logoutUrl},
			{POST.UPDATE,RequestUrl+gs.updatePieceUrl},
		};
		getUrl = new Dictionary<GET,string>(){
			{GET.GAME,RequestUrl+gs.gameStateUrl},
			{GET.USER,RequestUrl+gs.userStateUrl},
			{GET.PLAY,RequestUrl+gs.playStateUrl},
			{GET.WINNER,RequestUrl+gs.winnerUrl},
			{GET.PIECES,RequestUrl+gs.pieceStateUrl},
		};
	}

	//-------------------------------------------------------------
	// POSTリクエスト
	// @param
	// @リクエストURL
	// @callback
	//-------------------------------------------------------------
	public void Post( POST url,Dictionary<string,string> post,UnityAction<Dictionary<string,object>> callback){
		mClient = new WWWClient (this);
		mClient.URL = postUrl[url];
		foreach(KeyValuePair<string,string> post_arg in post)
		{
			mClient.AddData(post_arg.Key, post_arg.Value);
		}
		mClient.Timeout = timeout;
		mClient.OnDone = (WWW www) => { callback( JsonParser.Instance.Parse(www)); };
		mClient.Request();
	}

	//-------------------------------------------------------------
	// GETリクエスト
	// @param
	// @リクエストURL
	// @callback
	// @brif POSTの時に使用したデータを消してやらないとPOSTだと判断されてしまう
	//-------------------------------------------------------------
	public void Get( GET url,UnityAction<Dictionary<string,object>> callback){
		mClient = new WWWClient (this);
		mClient.URL = string.Format (getUrl [url], GlobalDataPool.Instance.playId);
		mClient.Timeout = timeout;
		mClient.OnDone = (WWW www) => { callback( JsonParser.Instance.Parse(www)); };
		mClient.Request();
	}
}
