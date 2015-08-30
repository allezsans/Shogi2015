using UnityEngine;
using System.Collections;

public sealed class GlobalDataPool : SimpleSingleton<GlobalDataPool>{
	public string ipAddr{ get; set; }
	public long userId{ get; set; }
	public long playId{ get; set; }
	public string gameState{ get; set; }
	public string role{ get; set; }
	public long firstPlayerId{ get; set; }
	public string firstPlayerName{ get; set; }
	public long lastPlayerId{ get; set; }
	public string lastPlayerName{ get; set; }
	public bool isFirst{ get; set; }
	public long turnCount{ get; set; }
	public long watchCount{ get; set; }
	public long turnPlayer{ get; set; }
}
