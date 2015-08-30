using UnityEngine;
using System.Collections;
using MyPos;

public class TranslatePosition : SingletonMonoBehaviour<TranslatePosition>{
	Vector2 grid;

	// 将棋板の画像は10マス分の幅で構成されている
	const float gridNum = 10.0f;
	const string board = "board";
	override protected void Awake(){
		base.Awake ();
		var rect = GameObject.Find (board).GetComponent<RectTransform> ();
		if (rect == null) {
			Debug.LogError( "Can't find gameObject that name is" + board);
		}
		grid = new Vector2 (rect.sizeDelta.x / gridNum , rect.sizeDelta.y / gridNum);
	}

	public Vector2 BoradToScreen(Pos boardPos){
	 	return new Vector2 ( (boardPos.x-1) * -grid.x , (boardPos.y-1) * -grid.y);
	}

	public Pos ScreenToBorad(Vector2 screenPos){
		return new Pos ((int)((screenPos.x+1) / -grid.x), (int)((screenPos.y+1) / -grid.y));
	}
}
