using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

	public static bool a;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public static void GoToScene(int scene){
		Application.LoadLevel (scene);
	}
}
