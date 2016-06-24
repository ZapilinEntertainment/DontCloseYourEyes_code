using UnityEngine;
using System.Collections;

public class padik : MonoBehaviour {
	public int mySubsector=0;
	public int q_x;
	public int q_y;
	public float e_x;
	float rsize_x;
	public bool dynamic_optimization=false;
	public byte prev_insubsector=0;
	byte drawstate=0;
	bool closed=false;
	byte last_psector=0;

	void Start () {rsize_x=q_x*2+e_x;}

	public void PadikOptimize (byte player_sector,byte player_subsector,bool inside) {
		byte nsector=0;
		if (player_subsector==mySubsector) {
			dynamic_optimization=true;
			BroadcastMessage("WindowState",(byte)(1),SendMessageOptions.DontRequireReceiver);
			BroadcastMessage("Optimize",player_sector,SendMessageOptions.DontRequireReceiver);
		}
			else {
			dynamic_optimization=false;
			if (inside) {gameObject.SetActive(false);return;}
			else {
				if (player_subsector>mySubsector) {if (player_sector<4) nsector=3; else nsector=9;}
				else {
					if (player_sector<4) nsector=1; else nsector=7;
				}
				BroadcastMessage("ClosedOptimize",nsector,SendMessageOptions.DontRequireReceiver);
				BroadcastMessage("WindowState",(byte)(5),SendMessageOptions.DontRequireReceiver);
			}
		}

	}

	void Update () {
		if (!dynamic_optimization)  return;
			byte insubsector=0;
			Vector3 ppos=transform.InverseTransformPoint(Global.player.transform.position);
		Vector3 count_point=Vector3.zero;
		byte psector=0;
		if (ppos.x<0) {
			if (ppos.z>q_y) {count_point=new Vector3(0,0,q_y);psector=1;}
			else {
				if (ppos.z>0) {count_point=new Vector3(0,0,ppos.z);psector=4;}
				else psector=7;
			}
		}
		else {
			if (ppos.x>rsize_x) {
				if (ppos.z>q_y) {count_point=new Vector3(rsize_x,0,q_y);psector=2;}
				else {
					if (ppos.z<0) {count_point=new Vector3(rsize_x,0,0);psector=8;}
					else {count_point=new Vector3(rsize_x,0,ppos.z);psector=5;}
				}
			}
			else {
				if (ppos.z>q_y) {count_point=new Vector3(ppos.x,0,q_y);psector=3;}
				else {
					if (ppos.z<0) {count_point=new Vector3(ppos.x,0,0);psector=9;}
					else {count_point=new Vector3(rsize_x/2,0,q_y/2);psector=6;}
				}
			}
		}
		float d=Vector3.Distance(new Vector3(Global.ppos.x,0,Global.ppos.z),count_point);
	
		last_psector=psector;
		if (ppos.x<q_x) 	insubsector=1;
		else { if (ppos.x>q_x+e_x) insubsector=3; else insubsector=2;}
		if (insubsector!=prev_insubsector) {
			prev_insubsector=insubsector;
			BroadcastMessage("SelfOptimize",true,SendMessageOptions.DontRequireReceiver);
		}
	}


}
