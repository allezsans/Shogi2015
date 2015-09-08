using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class WaitingEffect : MonoBehaviour {
	Text[] texts;
	public float animHeight = 30;
	public float animTime = 1;
	public float delayRate = 1;
	// Use this for initialization
	void Start () {
		texts = GetComponentsInChildren<Text> ();
		Anim ();
	}
	
	// Update is called once per frame
	float nowTime = 0.0f;
	void Update () {
		nowTime += Time.deltaTime;
		if (nowTime >= (texts.Length * delayRate + animTime)) {
			Anim ();
			nowTime = 0.0f;
		}
	}

	void Anim(){
		foreach( var g in texts.Select( (value, index) => new { value, index } ) ){
			iTween.MoveBy( g.value.gameObject ,
			              iTween.Hash(	"y",animHeight,
			            "time",animTime,
			            "easetype",iTween.EaseType.easeInExpo,
			            "delay",g.index * delayRate,
			            "looptype",iTween.LoopType.none));
			iTween.MoveAdd( g.value.gameObject ,
			               iTween.Hash("y",-animHeight,
			            "time",animTime,
			            "easetype",iTween.EaseType.easeOutExpo,
			            "delay",g.index * delayRate + animTime,
			            "looptype",iTween.LoopType.none));
		}
	}

	public void Dispose(){
		iTween.Stop ();
		Destroy (this.gameObject);
	}
}
