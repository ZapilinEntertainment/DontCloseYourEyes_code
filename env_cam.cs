using UnityEngine;
using System.Collections;

public class env_cam : MonoBehaviour {

	void Update () {
		if (Global.cam) {
			transform.forward=Global.cam.transform.forward;
		}
	}
}
