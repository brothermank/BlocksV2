using UnityEngine;
using System.Collections;

public class ControlProfile {

	public enum DefaultProfiles{P1, P2};

	public KeyCode up;
	public KeyCode down;
	public KeyCode left;
	public KeyCode right;
	public KeyCode rotateClockwise;
	public KeyCode rotateCounterClockwise;
	
	public ControlProfile(KeyCode up, KeyCode down, KeyCode left, KeyCode right, KeyCode rotateClockwise, KeyCode rotateCounterClockwise){
		this.up = up;
		this.down = down;
		this.left = left;
		this.right = right;
		this.rotateClockwise = rotateClockwise;
		this.rotateCounterClockwise = rotateCounterClockwise;
	}

	public static ControlProfile GetDefaultProfile(DefaultProfiles profile){
		switch (profile) {
		case DefaultProfiles.P1:
			return new ControlProfile(KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.E, KeyCode.Q);
		case DefaultProfiles.P2:
			return new ControlProfile(KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.O, KeyCode.I);
		default:
			Debug.Log("Non-implemented Controlfile '" + profile.ToString() + "'... see file ControlProfile.cs, function GetDefaultProfile");
			break;
		}
		return null;
	}

	
}
