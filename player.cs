using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {
	public GameObject flashlight;

	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("e")) {
			RaycastHit rh;
			if (Physics.Raycast(transform.position,transform.forward,out rh,2)) {
				rh.collider.SendMessage("Activate",SendMessageOptions.DontRequireReceiver);
			}
		}
		if (Input.GetKeyDown("f")) {
			if (flashlight.activeSelf) flashlight.SetActive(false);
			else flashlight.SetActive(true);
		}
	}
}
