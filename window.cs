using UnityEngine;
using System.Collections;

public class window : MonoBehaviour {
	Renderer rr;
	byte opt_state=0;
	byte dyn_state=0;

	void Start () {
		rr=GetComponent<Renderer>();
	}

	void Update () {
		if (opt_state<5) {
			float d=Vector3.Distance(transform.position,Global.ppos);
			float d2=d/Global.draw_distance1;
			if (d2>=1) {if (dyn_state!=5) {dyn_state=5;rr.material=Global.m_pseudoglass;}}
			else {
				if (d2>=0.75f) {if (dyn_state!=4) {dyn_state=4;rr.material=Global.m_glass_075;}}
				else {
					if (d2>=0.5f) {if (dyn_state!=3) {dyn_state=3;rr.material=Global.m_glass_05;}}
					else {
						if (d2>=0.25f) {if (dyn_state!=2) {dyn_state=2;rr.material=Global.m_glass_025;}}
						else {if (dyn_state!=1) {dyn_state=1;rr.material=Global.m_glass;}}
					}
				}
			}
		}
	}

	public void WindowState (byte x) {
		opt_state=x;
		if (x==5) rr.material=Global.m_pseudoglass;
	}
}
