using UnityEngine;
using System.Collections;

public class PlayerHead : MonoBehaviour {

	private const float SENSITIVITY = 2f;
	//angles set from Unity
	public float maxLookUpAngle;
	public float maxLookDownAngle;
	private float yAxisMovement = 0.0f;
	
	void Start() { }
	
	void Update() {
		PollForAxisAdjustments();
	}
	
	private void PollForAxisAdjustments() {
		yAxisMovement -= Input.GetAxis("Mouse Y") * SENSITIVITY;
		yAxisMovement = Mathf.Clamp(yAxisMovement, maxLookDownAngle,
										maxLookUpAngle);
		makeYAxisAdjustments(yAxisMovement);
	}
	
	private void makeYAxisAdjustments(float adjustment) {
		if (adjustment == 0) return;
		
		transform.localEulerAngles = new Vector3(adjustment, 
			transform.localEulerAngles.y, transform.localEulerAngles.z);
	}
}
