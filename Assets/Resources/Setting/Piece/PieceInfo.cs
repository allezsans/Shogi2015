using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PieceInfo : ScriptableObject{

	[Header("成る前")]
	public List<Vector2> movableVectorBeforePromote;
	public Sprite spriteBeforePromote;

	[Header("成った後")]
	public List<Vector2> movableVectorAfterPromote;
	public Sprite spriteAfterPromote;
}
