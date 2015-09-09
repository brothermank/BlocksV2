using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

	Rigidbody rBod;
	float moveSpeed;
	public enum Type{Cylinder};

	// Use this for initialization
	void Start () {
		rBod = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (rBod.velocity.magnitude == 0) {
			rBod.velocity = new Vector3(Random.Range(-1f,1f), Random.Range(-1f,1f))  * moveSpeed;
		}
		rBod.velocity *= rBod.velocity.magnitude * moveSpeed;
	}


}
