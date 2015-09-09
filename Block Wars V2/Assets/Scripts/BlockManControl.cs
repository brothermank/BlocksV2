using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockManControl : MonoBehaviour {

	public static List<BlockManControl> activeBlockMen = new List<BlockManControl> ();

	Weapon currentWeapon;
	private Transform weaponAttachment;
	protected Rigidbody rBody;
	private BoxCollider visualBody;
	
	public Vector3 velocity{
		get{
			return rBody.velocity;
		}
	}

	public string name;
	public float power = 3;
	public float turnPower = 1;

	public ControlProfile.DefaultProfiles controlProfile;
	ControlProfile cProfile;
	

	void Awake(){
		cProfile = ControlProfile.GetDefaultProfile (controlProfile);
		weaponAttachment = transform.FindChild ("Weapon Attatchment");
		rBody = GetComponent<Rigidbody> ();
		activeBlockMen.Add (this);
	}

	void Start () {
		//EquipWeapon (Weapon.WeaponType.Spear1);
	}
	
	// Update is called once per frame
	void Update () {
		HandleKeyInput ();
	}

	public void SetControlProfile(ControlProfile.DefaultProfiles profile){
		controlProfile = profile;
		cProfile = ControlProfile.GetDefaultProfile (profile);
	}

	protected virtual void HandleKeyInput(){
		if (Input.GetKey (cProfile.up)) {
			rBody.AddForce(Vector3.up * power);
		}
		if (Input.GetKey (cProfile.down)) {
			rBody.AddForce(Vector3.down * power);
		}
		if (Input.GetKey (cProfile.left)) {
			rBody.AddForce(Vector3.left * power);
		}
		if (Input.GetKey (cProfile.right)) {
			rBody.AddForce(Vector3.right * power);
		}
		if (Input.GetKey (cProfile.rotateClockwise)) {
			rBody.AddTorque(new Vector3(0,0,-1));
		}
		if (Input.GetKey (cProfile.rotateCounterClockwise)) {
			rBody.AddTorque(new Vector3(0,0,1));
		}
	}

	public void EquipWeapon(Weapon.WeaponType type){
		Debug.Log ("Spawning " + type.ToString ());
		if (currentWeapon != null) {
			Destroy(currentWeapon.gameObject);
		}
		currentWeapon = Weapon.GetWeaponPrefab (type);
		currentWeapon.transform.SetParent (weaponAttachment);
		currentWeapon.transform.localPosition = new Vector3 (0, 0, 0);
		currentWeapon.transform.localRotation = new Quaternion (0, 0, 0, 0);
		currentWeapon.wielder = this;
	}

	public void Kill(){
		activeBlockMen.Remove (this);
		Debug.Log (activeBlockMen.Count);
		Destroy (this.gameObject);
	}

	public void SetColor(Color c){
		foreach(Renderer r in GetComponentsInChildren<Renderer>()){
			r.material.SetColor("_Color", c);
		}
	}
}
