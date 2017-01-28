using UnityEngine;
using System.Collections;

public class house : MonoBehaviour {
	bool inside=false;
	public int q_x=15; //flat width
	public int q_y=15; //flat length
	public int e_x=6; //entrance width
	public int e_y=6; //entrance length
	public int floors =5;
	public float floor_height=3;
	public int padiks=1;
	public byte player_sector=0;
	public byte player_subsector=0;
	byte prev_sector=0;
	byte prev_subsector=0;
	public int rsize_x=0;
	public Vector3 ppos;
	public int player_floor=0;


	void Update () {
		ppos=transform.InverseTransformPoint(Global.ppos);
		int player_current_height=(int)(ppos.y/floor_height+0.5f);
		if (player_current_height!=player_floor) {player_floor=player_current_height;}

		//   1  2  3
		//   4  5  6
		//   7  8  9
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
			print(player_sector);
		}
		if (prev_subsector!=player_subsector) {prev_subsector=player_subsector;send=true;}
		if (send) {
			for (int i=0;i<transform.childCount;i++) {
				GameObject g=transform.GetChild(i).gameObject;
				if (!g.activeSelf) g.SetActive(true);
				if (g.GetComponent<padik>()!=null) {
					g.GetComponent<padik>().PadikOptimize(player_sector,player_subsector,player_sector==5);
				}
		}
	}
	}
}
