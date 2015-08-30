using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using MyPos;

namespace MyPos{
	public struct Pos{
		public int x,y;
		public Pos(int _x,int _y){
			x = _x;
			y = _y;
		}
	}
}

public class Piece : MonoBehaviour {

	Pos pos = new Pos();
	List<Vector2> BehaviourBeforePromote;	// 成る前の動き
	List<Vector2> BehaviourAfterPromote;	// 成った後の動き
	List<Vector2> NowBehaviour;				// 現在の動き
	string pieceName;
	Sprite SpriteBeforePromote;
	Sprite SpriteAfterPromote;
	Image image;
	RectTransform rectTransform;

	void Awake(){
		image = GetComponent<Image> ();
		rectTransform = this.GetComponent<RectTransform> ();
	}

	public void SetPieceInfo(string _pieceName,int posX,int posY){
		pieceName = _pieceName;
		pos.x = posX;
		pos.y = posY;
		var type = Resources.Load<PieceInfo>("Setting/Piece/"+_pieceName);
		BehaviourBeforePromote = type.movableVectorBeforePromote;
		BehaviourAfterPromote = type.movableVectorAfterPromote;
		NowBehaviour = BehaviourBeforePromote;
		transform.SetParent (GameObject.Find("board").transform);
		rectTransform.anchoredPosition = TranslatePosition.Instance.BoradToScreen (pos);
		transform.localScale = Vector3.one;
		SpriteBeforePromote = type.spriteBeforePromote;
		SpriteAfterPromote = type.spriteAfterPromote;
		image.overrideSprite = SpriteBeforePromote;
	}
}
