using UnityEngine;
using System.Collections;

public class texture_replacer : MonoBehaviour {
	public Texture new_tx;

	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().material.SetTexture("_MainTex",new_tx);
	}

}
