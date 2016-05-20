using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Movement : MonoBehaviour {

	public float playerMovSpeed;
	public GameObject head;
	private bool playerWantsToMove;
	private Vector3 playerMovDir;

	void Start () {
		playerWantsToMove = false;
	}

	void Update () {
		if (Cardboard.SDK.VRModeEnabled && Cardboard.SDK.Triggered) {
			playerWantsToMove = !playerWantsToMove;
		}
		if (playerWantsToMove == true) {
			movePlayer();
		}
	}

	public void movePlayer(){
		playerMovDir = new Vector3(-head.transform.forward.x,Mathf.Clamp(head.transform.forward.y,0,0),-head.transform.forward.z);
		transform.Translate(playerMovDir*playerMovSpeed*Time.deltaTime);
	}

}