using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SpawnPoint : MonoBehaviour {

	public static List<SpawnPoint> activeSpawnPoints = new List<SpawnPoint>();

	public enum SpawnType{ControlledEntity};

	public Weapon.WeaponType weaponEquipped;
	public ControlProfile.DefaultProfiles cProfileSpawned;
	public string entityName = "Unnamed";
	public SpawnType spawnType;
	private int unitScore = 0;
	public bool isNPC = false;
	public Obstacle.Type obstacleType; 
	public Color color;
	
	public Text scoreboardEntry;

	private GameObject controlledEntityPrefab;
	private GameObject NPCEntityPrefab;

	void Awake(){
		controlledEntityPrefab = Resources.Load ("Entities/BlockMan1") as GameObject;
		NPCEntityPrefab = Resources.Load ("Entities/BlockMan1NPC") as GameObject;

		activeSpawnPoints.Add (this);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void IncreaseScore(int amount){
		unitScore += amount;
		scoreboardEntry.text = "" + unitScore;
	}

	public void Spawn(){
		switch (spawnType) {
		case SpawnType.ControlledEntity:
			GameObject instance = null;
			if(!isNPC) instance = Instantiate(controlledEntityPrefab) as GameObject;
			else instance = Instantiate(NPCEntityPrefab) as GameObject;
			instance.transform.position = transform.position;
			instance.transform.rotation = transform.rotation;
			BlockManControl bmc = instance.GetComponent<BlockManControl>();
			bmc.SetControlProfile(cProfileSpawned);
			bmc.EquipWeapon(weaponEquipped);
			bmc.name = entityName;
			bmc.SetColor(color);
			break;
		default:
			Debug.Log("Non-implemented Spawn Type '" + spawnType.ToString() + "'... see file SpawnPoint.cs, function Spawn");
			break;
		}
	}

	public void SetColor(int i){
		switch (i) {
		case 0:
			color = Color.green;
			break;
		case 1:
			color = Color.red;
			break;
		case 2:
			color = Color.blue;
			break;
		case 3:
			color = Color.yellow;
			break;
		case 4:
			color = Color.white;
			break;
		case 5:
			color = Color.black;
			break;
		case 6:
			color = Color.grey;
			break;
		default:
			break;
		}
	}


}

/*[CustomEditor(typeof(SpawnPoint)), CanEditMultipleObjects]
public class PropertyHolderEditor : Editor {
	
	public SerializedProperty 
		spawnType_Prop,
		weapon_Prop,
		cProfile_Prop,
		entityName_Prop,
		valForAB_Prop,
		valForA_Prop,
		valForC_Prop,
		controllable_Prop;
	
	void OnEnable () {
		// Setup the SerializedProperties
		spawnType_Prop = serializedObject.FindProperty ("spawnType");
		valForAB_Prop = serializedObject.FindProperty("valForAB");
		valForA_Prop = serializedObject.FindProperty ("valForA");
		valForC_Prop = serializedObject.FindProperty ("valForC");
		controllable_Prop = serializedObject.FindProperty ("controllable");        
	}
	
	public override void OnInspectorGUI() {
		serializedObject.Update ();
		
		EditorGUILayout.PropertyField( state_Prop );
		
		PropertyHolder.Status st = (PropertyHolder.Status)state_Prop.enumValueIndex;
		
		switch( st ) {
		case PropertyHolder.Status.A:            
			EditorGUILayout.PropertyField( controllable_Prop, new GUIContent("controllable") );            
			EditorGUILayout.IntSlider ( valForA_Prop, 0, 10, new GUIContent("valForA") );
			EditorGUILayout.IntSlider ( valForAB_Prop, 0, 100, new GUIContent("valForAB") );
			break;
			
		case PropertyHolder.Status.B:            
			EditorGUILayout.PropertyField( controllable_Prop, new GUIContent("controllable") );    
			EditorGUILayout.IntSlider ( valForAB_Prop, 0, 100, new GUIContent("valForAB") );
			break;
			
		case PropertyHolder.Status.C:            
			EditorGUILayout.PropertyField( controllable_Prop, new GUIContent("controllable") );    
			EditorGUILayout.IntSlider ( valForC_Prop, 0, 100, new GUIContent("valForC") );
			break;
			
		}
		
		
		serializedObject.ApplyModifiedProperties ();
	}
}*/		