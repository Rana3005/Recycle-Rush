using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePush : MonoBehaviour
{
    public LayerMask pushLayers;
	public bool canPush;
	[Range(0.5f, 5f)] public float strength = 1.1f;

    private void OnControllerColliderHit(ControllerColliderHit hit){
        if(canPush) PushObstacle(hit);
    }

    private void PushObstacle(ControllerColliderHit hit){
        Rigidbody rigidbody = hit.collider.attachedRigidbody;
        if (rigidbody == null || rigidbody.isKinematic) return;

        // make sure we only push desired layer(s)
		var bodyLayerMask = 1 << rigidbody.gameObject.layer;
		if ((bodyLayerMask & pushLayers.value) == 0) return;

		// We dont want to push objects below us
		if (hit.moveDirection.y < -0.3f) return;

		// Calculate push direction from move direction, horizontal motion only
		Vector3 pushDir = new Vector3(hit.moveDirection.x, 0.0f, hit.moveDirection.z);

		// Apply the push and take strength into account
		rigidbody.AddForce(pushDir * strength, ForceMode.Impulse);
    }
}
