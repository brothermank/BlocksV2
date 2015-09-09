using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerCreationForm : MonoBehaviour {

	public static List<PlayerCreationRecipe> registeredPlayers = new List<PlayerCreationRecipe>();
	public static int activePlayerforms = 0;
	PlayerCreationRecipe currentActiveRecipe;
	public GameObject thisPanel;


	public Text name;
	public Toggle isNPC;
	public Slider colorToggle;
	public Slider weapon;
	public Slider controlProfile;

	public struct PlayerCreationRecipe{

		public string name;
		public bool isNPC;
		public int colorToggle;
		public int weapon;
		public int controlProfile;

		public PlayerCreationRecipe(string name, bool isNPC, float colorToggle, float weapon, float controlProfile){
			this.name = name;
			this.isNPC = isNPC;
			this.colorToggle = (int)colorToggle;
			this.weapon = (int)weapon;
			this.controlProfile = (int)controlProfile;
		}

	}

	// Use this for initialization
	void Start () {
		currentActiveRecipe = new PlayerCreationRecipe ("NPC", true, Random.Range (0, 4), Random.Range(0,1), 0);
		//registeredPlayers.Add (currentActiveRecipe);
		activePlayerforms++;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RemoveThisForm(){
		activePlayerforms--;
		registeredPlayers.Remove (currentActiveRecipe);
		GameSetupHandler.CheckForGameStart ();
		Destroy (gameObject);
	}

	public void RegisterForGame(){
		registeredPlayers.Remove(currentActiveRecipe);
		currentActiveRecipe = new PlayerCreationRecipe (name.text, isNPC.isOn, colorToggle.value, weapon.value, controlProfile.value);
		Debug.Log ("weapon value is " + weapon.value);
		registeredPlayers.Add (currentActiveRecipe);
		thisPanel.GetComponent<Image> ().color = Color.green;
		GameSetupHandler.CheckForGameStart ();
	}


}
