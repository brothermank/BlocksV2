using UnityEngine;
using System;
using System.Collections;

public class Weapon : MonoBehaviour {

	public enum WeaponType{Sword1, Spear1};
	public BlockManControl wielder;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static Weapon GetWeaponPrefab(WeaponType type){
		/*switch (type) {
		case WeaponType.Spear1:
			return Resources.Load("Weapons/Spear1");
			break;
		case WeaponType.Sword1:	
			break;
		default:
			Debug.Log("Weapon type '" + type + "' is not implemented");
			break;
		}*/
		GameObject prefab = Resources.Load ("Weapons/" + type.ToString ()) as GameObject;
		GameObject instance = Instantiate (prefab) as GameObject;
		return instance.GetComponent<Weapon> ();
	}


	void OnTriggerEnter(Collider c){
		if (c.tag == "Player" && c.transform != transform.parent.transform.parent.transform) {
			BlockManControl target = c.GetComponent<BlockManControl>();
			GameManager.activeManager.OnKill(wielder, target);
			target.Kill();
		}
	}
}
