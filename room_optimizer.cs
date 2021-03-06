using UnityEngine;
using System.Collections;

public class room_optimizer : MonoBehaviour {
	public float xsize=2;
	public float ysize=2;
	public float angle_vis_limit=50;
	public bool dynamic_optimization=false;
	public Vector3 inpos=Vector3.zero;
	public bool use_start_inpos=false;
	public house h;
	public float height=3;
	public byte drawstate=0;
	byte prev_drawstate=0;
	public byte lastq=0;
	public int floor=0;
	public int lastfloor=0;
	byte window_state=0;

	void Start () {
		h=transform.root.GetComponent<house>();
		if (!use_start_inpos) inpos=h.transform.InverseTransformPoint(transform.position);
		floor=int.Parse(transform.parent.parent.name.Substring(1,2));
	}

	void Update() {
		if (!dynamic_optimization) return;
		byte nq=0;
		float x=h.ppos.x-inpos.x; float y=h.ppos.z-inpos.z;
		if (x<0) {
			if (y>ysize) nq=1;
			else {
				if (y<0) nq=7;
				else nq=4;
			}
		}
		else {
			if (x>xsize) {
				if (y>ysize) nq=3;
				else {
					if (y<0) nq=9;
					else nq=6;
				}
			}
			else {
				if (y>ysize) nq=2;
				else {
					if (y<0) nq=8;
					else nq=5;
				}
			}
		}
		bool extent_culling=false;
		if (nq!=lastq||h.player_floor!=lastfloor) {
			Optimize(nq);
			}
	}

	public void Optimize (byte nq) {
		if (drawstate==1) {
			if (prev_drawstate!=drawstate)	{
				prev_drawstate=drawstate;
				for (int i=0;i<transform.childCount;i++) {
					GameObject v=transform.GetChild(i).gameObject;
					if (v.name.Length<9||v.name[8]!='o')	v.SetActive(false);
				}
			}
				return;
		}
		else {
			if (prev_drawstate!=drawstate) {
				prev_drawstate=drawstate;
			}
		}
		bool oup=true; bool up=true; bool down=true;bool odown=true;
		bool off_all=false;
		if (floor>h.player_floor) {oup=false;down=false;}
		else {
			if (floor<h.player_floor) {up=false;odown=false;}
			else {oup=false;odown=false;}
		} lastfloor=h.player_floor;
			lastq=nq;
		bool fwd=true; bool back=true;bool right=true;bool left=true;
		bool ofwd=true; bool oback=true;bool oright=true;bool oleft=true;

		switch (nq) {
		case 1:fwd=false;left=false;oright=false;oback=false;break;
		case 2: fwd=false;oright=false;oleft=false;oback=false;break;
		case 3: fwd=false;right=false;oleft=false;oback=false;break;
		case 4: left=false;ofwd=false;oright=false;oback=false;break;
		case 5: if (floor!=h.player_floor) off_all=true; else { ofwd=false;oright=false;oleft=false;oback=false;}break;
		case 6: right=false;oleft=false;ofwd=false;oback=false;break;
		case 7: left=false;back=false;ofwd=false;oright=false;break;
		case 8: back=false;ofwd=false;oright=false;oleft=false;break;
		case 9: right=false;back=false;ofwd=false;oleft=false;break;
		}
		for (int i=0;i<transform.childCount;i++) {
			GameObject x=transform.GetChild(i).gameObject;
			string s=x.name;
			if (s.Length!=9) continue;
			if (off_all) {x.SetActive(false);continue;}
			if (drawstate==0)
			{switch (s[0]) {
			case 'f':
				if (s[8]=='o') {if (ofwd) x.SetActive(true); else x.SetActive(false);}
				else {if (fwd) x.SetActive(true); else x.SetActive(false);}
				break;
			case 'b':
				if (s[8]=='o') {if (oback) x.SetActive(true); else x.SetActive(false);}
				else {if (back) x.SetActive(true); else x.SetActive(false);}
				break;
			case 'r':
				if (s[8]=='o') {if (oright) x.SetActive(true); else x.SetActive(false);}
				else {if (right) x.SetActive(true); else x.SetActive(false);}
				break;
			case 'l':
				if (s[8]=='o') {if (oleft) x.SetActive(true); else x.SetActive(false);}
				else {if (left) x.SetActive(true); else x.SetActive(false);}
				break;
		case 'd':
				if (s[8]=='o') {if (odown) x.SetActive(true); else x.SetActive(false);}
				else {if (down) x.SetActive(true); else x.SetActive(false);}
				break;
		case 'u':
				if (s[8]=='o') {if (oup) x.SetActive(true); else x.SetActive(false);}
				else {if (up) x.SetActive(true); else x.SetActive(false);}
				break;
				}}
		}
	}

	public void ClosedOptimize (byte nq) {
		dynamic_optimization=false;
		bool ofwd=true; bool oback=true;bool oright=true;bool oleft=true;

		switch (nq) {
		case 1:oright=false;oback=false;break;
		case 2: oright=false;oleft=false;oback=false;break;
		case 3: oleft=false;oback=false;break;
		case 4: ofwd=false;oright=false;oback=false;break;
		case 5: ofwd=false;oright=false;oleft=false;oback=false;break;
		case 6: oleft=false;ofwd=false;oback=false;break;
		case 7: ofwd=false;oright=false;break;
		case 8: ofwd=false;oright=false;oleft=false;break;
		case 9: ofwd=false;oleft=false;break;
		}
		for (int i=0;i<transform.childCount;i++) {
			GameObject x=transform.GetChild(i).gameObject;
			if (x.name.Length!=9||x.name[8]!='o') x.SetActive(false);
			else {
            switch (x.name[0]) {
				case 'f':
					if (ofwd) x.SetActive(true); else x.SetActive(false);
					break;
				case 'b':
					if (oback) x.SetActive(true); else x.SetActive(false);
					break;
				case 'r':
					if (oright) x.SetActive(true); else x.SetActive(false);
					break;
				case 'l':
					if (oleft) x.SetActive(true); else x.SetActive(false);
					break;
				}}
		}
	}

	public void SelfOptimize(bool x) {
		dynamic_optimization=x;
	}

	public void DrawState(byte x) {
		if (x!=prev_drawstate) {
			drawstate=x;
		Optimize(lastq);
		}
	}

	public void FloorOptimization(int nf) {
		if (dynamic_optimization) return;
		if (nf!=floor) {
			int i=0; GameObject v=null;
				for (i=0;i<transform.childCount;i++) {
					v=transform.GetChild(i).gameObject;
				if (v.name.Length==9&&v.name.Substring(1,4)=="flpl") {
					if (v.name[0]=='d'&&(nf<floor&&v.name[8]=='i'||nf>floor&&v.name[8]=='o')||v.name[0]=='u'&&(nf<floor&&v.name[8]=='o'||nf>floor&&v.name[8]=='i')) {
						v.SetActive(false);
					}
					else v.SetActive(true);
					}
			}
		}}

}
