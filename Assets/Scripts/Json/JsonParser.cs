using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using MiniJSON;

public sealed class JsonParser : SimpleSingleton<JsonParser>{
	public Dictionary<string,object> Parse(WWW www){
		return Json.Deserialize (www.text) as Dictionary<string,object>;
	}
}
