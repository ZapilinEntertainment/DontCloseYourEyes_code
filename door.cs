using UnityEngine;
using System.Collections;

public class door : MonoBehaviour {
	public Vector3 pivot=new Vector3(-1f,1,0);
	public float max_angle=90;
	public float toughness=1;
	bool open=false;
	bool rotating=false;
	Quaternion rotateTo;
	Quaternion start_rot;
	public float start_angle=0;
	public bool fixd=false;
	public bool double_sided=true;

	void Start() {
		start_rot=transform.rotation;
		if (start_angle!=0) {
			if (start_angle>max_angle) start_angle=max_angle;
			if (start_angle<-max_angle)  start_angle=-max_angle;
			if (!double_sided&&start_angle<0) start_angle*=-1;
			transform.RotateAround(pivot,Vector3.up,start_angle); open=true;}
		else {open=false;}
	}

	public void Activate() {
		Vector3 ppos=transform.InverseTransformPoint(Global.player.transform.position);
		if (!open) {
			if (double_sided) {
				if (ppos.z<0) rotateTo=Quaternion.Euler(start_rot.eulerAngles+new Vector3(0,-max_angle,0));
				else rotateTo=Quaternion.Euler(start_rot.eulerAngles+new Vector3(0,max_angle,0));
			}
			else {rotateTo=Quaternion.Euler(start_rot.eulerAngles+new Vector3(0,-max_angle,0));}
			open=true;
		}
		else {
			rotateTo=start_rot;
			open=false;
		}
		rotating=true;
	}

	void Update () {
		if (rotating) {
			transform.rotation = Quaternion.RotateTowards(transform.rotation,rotateTo, 30*Time.deltaTime);
			if (transform.rotation==rotateTo) {
				rotating=false;
				if (rotateTo==start_rot) {
					open=false;
					}
			}
		}
		else {
			if (fixd&&open) {if (transform.rotation!=start_rot) rotateTo=start_rot; else {open=false;}}
		}
	}
}
