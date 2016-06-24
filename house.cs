using UnityEngine;
using System.Collections;

public class house : MonoBehaviour {
	bool inside=false;
	public int q_x=15;
	public int q_y=15;
	public int e_x=6;
	public int e_y=6;
	public int floors =5;
	public float floor_height=3;
	public int padiks=1;
	public byte player_sector=0;
	byte player_subsector=0;
	byte prev_sector=0;
	byte prev_subsector=0;
	public int rsize_x=0;
	public Vector3 ppos;
	public int player_floor=0;

	void Start () {
		bool left=true;
		bool right =false;
		for (byte i=1;i<=padiks;i++) {
			if (i==padiks) right=true;
			constructor.CreatePadik(gameObject,new Vector3((q_x*2+e_x)*(i-1),0,0),q_x,q_y,e_x,e_y,right,left,i,floors);
			left=false;
		}
		rsize_x=(q_x*2+e_x)*padiks;
	}

	void Update () {
		ppos=transform.InverseTransformPoint(Global.player.transform.position);
		int nf=(int)(ppos.y/floor_height+0.5f);
		if (nf!=player_floor) {player_floor=nf;}
		if (ppos.x<0) {
			if (ppos.z>q_y) {player_sector=1;}
			else {
				if (ppos.z<0) player_sector=7;
				else {player_sector=4;}
			}
		}
		else {
			if (ppos.x>rsize_x) {
				if (ppos.z>q_y) {player_sector=3;}
				else {
					if (ppos.z<0) {player_sector=9;}
					else {player_sector=6;}
				}
			}
			else {
				if (ppos.z>q_y) {player_sector=2;}
				else {
					if (ppos.z<0) {player_sector=8;}
					else {player_sector=5;}
				}
			}
		}
		bool send=false;
		if (padiks>1&&(player_sector==2||player_sector==5||player_sector==8)) {
			player_subsector=(byte)(Mathf.Floor(ppos.x/(q_x*2+e_x))+1);
		}
		if (player_sector!=prev_sector) {
			prev_sector=player_sector;
			send=true;
		}
		if (prev_subsector!=player_subsector) {prev_subsector=player_subsector;send=true;}
		if (send) {
			for (int i=0;i<transform.childCount;i++) {
				GameObject g=transform.GetChild(i).gameObject;
				if (!g.activeSelf) g.SetActive(true);
				if (g.GetComponent<padik>()!=null) {
					if (player_sector!=5) g.GetComponent<padik>().PadikOptimize(player_sector,player_subsector,false);
					else g.GetComponent<padik>().PadikOptimize(player_sector,player_subsector,true);
				}
		}
	}
	}
}
