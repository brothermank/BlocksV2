using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameSetupHandler : MonoBehaviour {

	public GameObject playerFormPrefab;
	public GameObject addPlayerButtonPanel;

	void Start(){
		if (Application.loadedLevel == 1)
			ActivateSpawnpoints ();
	}

	public void AddPlayerForm(){
		GameObject instance = Instantiate (playerFormPrefab) as GameObject;
		instance.transform.SetParent (transform, false);
		addPlayerButtonPanel.GetComponent<RectTransform> ().SetAsLastSibling ();
	}

	public static void CheckForGameStart(){
		if (PlayerCreationForm.registeredPlayers.Count == PlayerCreationForm.activePlayerforms) {
			StartGame();
		}
	}

	private static void StartGame(){
		Application.LoadLevel (1);
	}

	private void ActivateSpawnpoints(){
		List<PlayerCreationForm.PlayerCreationRecipe> requestedSpawns = PlayerCreationForm.registeredPlayers;
		int registeredPlayers = requestedSpawns.Count;
		SpawnPoint[] spawnPoints = (SpawnPoint[])FindObjectsOfType<SpawnPoint> ();
		int count = 0;
		foreach (SpawnPoint spawn in spawnPoints) {
			if(count < registeredPlayers){
				spawn.SetColor(requestedSpawns[count].colorToggle);
				spawn.weaponEquipped = (Weapon.WeaponType)requestedSpawns[count].weapon;
				spawn.entityName = requestedSpawns[count].name;
				spawn.isNPC = requestedSpawns[count].isNPC;
				spawn.cProfileSpawned = (ControlProfile.DefaultProfiles)requestedSpawns[count].controlProfile; 
			}
			else break;
			count++;
		}
		for (int i = count; i < spawnPoints.Length; i++) {
			SpawnPoint.activeSpawnPoints.Remove(spawnPoints[i]);
			Destroy( spawnPoints[i].gameObject);
		}
		GameManager.activeManager.StartGame ();
	}
	
}
