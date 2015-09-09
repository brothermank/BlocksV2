using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager activeManager;
	public GameObject scoreboard;
	public GameObject scoreboardEntryPrefab;
	public GameObject canvas;

	void Awake(){
		activeManager = this;
	}

	public void StartGame(){
		SetUpUI();
		foreach (SpawnPoint spawn in SpawnPoint.activeSpawnPoints) {
			spawn.Spawn();
		}
	}

	void SetUpUI(){
		GameObject instance = Instantiate (scoreboard) as GameObject;
		instance.transform.SetParent (canvas.transform);
		RectTransform rt = instance.GetComponent<RectTransform> ();
		rt.offsetMin = new Vector2 (900, 0);
		rt.offsetMax = new Vector2 (0, 0);
		foreach (SpawnPoint spawn in SpawnPoint.activeSpawnPoints) {
			GameObject scoreboardInstance = Instantiate(scoreboardEntryPrefab) as GameObject;
			scoreboardInstance.transform.SetParent(instance.transform);
			spawn.scoreboardEntry = scoreboardInstance.transform.FindChild("Text 1").GetComponent<Text>();
			scoreboardInstance.transform.FindChild("Text").GetComponent<Text>().text = spawn.entityName;
		}
	}

	public void OnKill(BlockManControl killer, BlockManControl victim){
		foreach (SpawnPoint spawn in SpawnPoint.activeSpawnPoints) {
			if(spawn.entityName == killer.name){
				spawn.IncreaseScore(1);
				break;
			}
		}
		if (BlockManControl.activeBlockMen.Count <= 2)
			ResetLevel ();
	}

	private void ResetLevel(){
		for (int i = BlockManControl.activeBlockMen.Count - 1; i >= 0; i--) {
			BlockManControl.activeBlockMen[i].Kill();
		}
		foreach (SpawnPoint spawn in SpawnPoint.activeSpawnPoints) {
			spawn.Spawn();
		}
	}
}
