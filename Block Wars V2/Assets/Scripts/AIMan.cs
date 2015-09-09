using UnityEngine;
using System.Collections;

public class AIMan : BlockManControl {

	private BlockManControl target;

	public int moveLevel = 0;
	public int attackLevel = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		TargetSpotting ();
		AttackGuidance ();
	}

	protected override void HandleKeyInput (){

	}

	private void TargetSpotting(){
		float distanceToClosestTarget = Mathf.Infinity;
		if(target != null) distanceToClosestTarget = (transform.position - target.transform.position).magnitude;
		foreach (BlockManControl entity in activeBlockMen) {
			if(entity != this){
				float distance = (entity.transform.position - this.transform.position).magnitude;
				if(distance < distanceToClosestTarget){
					target = entity;
					distanceToClosestTarget = distance;
				}
			}
		}
	}

	private void AttackGuidance(){
		Vector3 targetTrace = target.transform.position - transform.position;
		Vector3 targetDirection = targetTrace / targetTrace.magnitude;
		switch (moveLevel) {
		case 0:
			rBody.AddForce (targetDirection * power);
			break;
		case 1:
			Vector3 targetVelocityCorrection = targetDirection + (target.velocity / target.velocity.magnitude);
			targetVelocityCorrection = targetVelocityCorrection / targetVelocityCorrection.magnitude;
			rBody.AddForce(targetVelocityCorrection * power);
			break;
		case 2:
			targetVelocityCorrection = targetTrace + target.velocity;
			targetVelocityCorrection = targetVelocityCorrection / targetVelocityCorrection.magnitude;
			rBody.AddForce(targetVelocityCorrection * power);
			break;
		}

		Vector3 currentFacingDirection = transform.rotation * Vector3.left;
		Vector3 twistDirection = targetDirection - currentFacingDirection;
		Vector3 torqueRotationVector = Vector3.Cross (twistDirection, currentFacingDirection);
		torqueRotationVector = torqueRotationVector / torqueRotationVector.magnitude;
		//float velocityCorrectionAngle = Mathf.Asin ((Vector3.Dot(targetDirection, currentFacingDirection))/(targetDirection.magnitude * currentFacingDirection.magnitude));
		switch (attackLevel) {
		case 0:
			rBody.AddTorque(torqueRotationVector);
			break;
		/*case 1:
			Vector3 currentTorque = rBody.angularVelocity / rBody.mass;
			float breakTime = currentTorque.magnitude / turnPower;
			Vector3 turnAngle = currentTorque / currentTorque.magnitude;
			Vector3 desiredTurnAngle = torqueRotationVector;
			Vector3 correctionAngle = turnAngle - desiredTurnAngle;
			if(correctionAngle.magnitude = 0){

			}
			else correctionAngle /= correctionAngle.magnitude;
		*/
		default:
			break;
		}
	}
}
