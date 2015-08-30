using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TitleEffect : MonoBehaviour {

	public float effectSpeed = 0.5f;
	public float firstTextAlpha = 0.0f;
	public iTween.EaseType AlphaEaseType;

	GameSetting settingFile;
	JsonKeyName keyName;
	Text titleText;
	float pastTime;
	int textIndex;

	// Use this for initialization
	void Start () {
		settingFile = Resources.Load<GameSetting>("Setting/GameSetting");
		titleText = this.GetComponent<Text>();
		titleText.color = new Color(1,1,1,0);
		StartCoroutine (EffectText ());
	}

	// 文字送り＆アルファ値のエフェクト
	IEnumerator EffectText() {
		iTween.ValueTo(gameObject,iTween.Hash("from",firstTextAlpha,
		                                      "to",1.0f,
		                                      "time",effectSpeed*settingFile.titleText.Length,
		                                      "onupdate","UpdateTitleAlpha",
		                                      "easetype",iTween.EaseType.easeOutCubic));
		while (titleText.text.Length != settingFile.titleText.Length) {
			pastTime += Time.deltaTime;
			if(pastTime >= effectSpeed){
				titleText.text = settingFile.titleText.Substring (0,++textIndex);
				pastTime = 0f;
			}
			yield return null;
		}
	}

	void UpdateTitleAlpha(float alpha){
		titleText.color = new Color (1, 1, 1, alpha);
	}
}
