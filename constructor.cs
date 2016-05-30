using UnityEngine;
using System.Collections;

public class constructor : MonoBehaviour {

	public byte xsize=0;
	public byte ysize=0;
	public bool fwd_open=false;
	public bool back_open=false;
	public bool left_open=false;
	public bool right_open=false;
	public float enter_width=1.5f;

	public Material roof_material;
	public Material floor_material;
	public Material wall_material;
	GameObject wall_window_2m;
	GameObject wall_window_3m;
	GameObject wall_window_4m;
	GameObject wall_05m;
	GameObject wall_1m;
	GameObject wall_15m;
	GameObject wall_2m;
	GameObject wall_door_1m;
	GameObject wall_door_15m;
	GameObject wall_door_2m;
	GameObject floor_tile;
	GameObject door_frame;
	GameObject window_frame;

	void Awake () {
		wall_05m=Resources.Load<GameObject>("structures/wall_0.5m");
		wall_1m=Resources.Load<GameObject>("structures/wall_1m");
		wall_15m=Resources.Load<GameObject>("structures/wall_1.5m");
		wall_2m=Resources.Load<GameObject>("structures/wall_2m");
		floor_tile=Resources.Load<GameObject>("structures/floor_plate");
		wall_window_2m=Resources.Load<GameObject>("structures/wall_window_2m");
		wall_window_3m=Resources.Load<GameObject>("structures/wall_window_3m");
		wall_window_4m=Resources.Load<GameObject>("structures/wall_window_4m");
		wall_door_1m=Resources.Load<GameObject>("structures/wall_door_1m");
		wall_door_15m=Resources.Load<GameObject>("structures/wall_door_1.5m");
		wall_door_2m=Resources.Load<GameObject>("structures/wall_door_2m");
		door_frame=Resources.Load<GameObject>("structures/door_frame");
		window_frame=Resources.Load<GameObject>("structures/window_frame");
	}


	void Start () {
			if (xsize<=4) xsize=4;
			if (ysize<=4) ysize=4;
		int kitchen_x=2;
		int central_x=2;
		int rooms_x=0;
		if (xsize<10) {
			if (xsize%2==0) {kitchen_x=xsize/2-1;central_x=xsize/2+1;} else {kitchen_x=(xsize-1)/2;central_x=xsize-kitchen_x;}
		}
		else {
				if (xsize%2==0) {kitchen_x=xsize/4;}
			else {kitchen_x=(xsize-1)/4;}
				central_x=(xsize-kitchen_x)/2-1;rooms_x=(xsize-kitchen_x)/2+1;
		}

		int  kitchen_y=2;
		int central_y=ysize;
		int rooms_y=4;
		int add_rooms_y=0;
		int  san_y=1;
		int passage_y=1;
		int rooms_n=0;
		kitchen_y=ysize/3; if (kitchen_y<2) kitchen_y=2;
		san_y=(ysize-kitchen_y)/2; if (san_y>4) san_y=4; 
		passage_y=ysize-kitchen_y-san_y; if (passage_y>5) passage_y=5;
		kitchen_y=ysize-san_y-passage_y;
		if (ysize>10) {
			if (ysize>15) {
				if (back_open) {
					if (ysize%4==0&&ysize/4/rooms_x<=3) {rooms_y=ysize/4;add_rooms_y=ysize/4;rooms_n=4;} else {
						if (ysize%3==0&&ysize/3/rooms_x<=3) {rooms_y=ysize/3;add_rooms_y=ysize/3;rooms_n=3;}
						else {rooms_n=2;add_rooms_y=ysize/2;rooms_y=ysize-add_rooms_y;}
					}
					}
			}
		}
		//details
		byte i=0; 
		string fwd=""; string back="";string right="";string left="";
		string center_right=""; string center_left="";
		  //kitchen
		if (fwd_open) fwd=WindowsDivide(kitchen_x);
		if (left_open) left=WindowsDivide(kitchen_y);
		if (kitchen_y>3) {
		i=0;
		if ((kitchen_y-3)%2==0) {while (i<kitchen_y-3) {right+="wt";i+=2;} right+="wh";right+="dh";}
		else {while (i<kitchen_y-2) {right+="wt";i+=2;} right+="ws";right+="dh";}
		} else {
			switch (kitchen_y) {
			case 1:right="dm";break;
			case 2: right="wsdh";break;
			case 3:right="whdh";break;
			}
		}
		ConstructRoom(kitchen_x,kitchen_y,0,ysize-kitchen_y,"kitchen",fwd,back,right,left);
		center_left=right; 
		fwd="";back="";right="";

		 //toilet&bath
		if (left_open) left=WindowsDivide(san_y); else left="";
		if (kitchen_x/2>1.5f) {
			fwd=WallsDivide(kitchen_x/2-1.5f); //в текущем контексте как back, а затем в passage как fwd
			ConstructRoom(kitchen_x/2,san_y,0,passage_y,"toilet","",fwd+"dh","",left);
			ConstructRoom(kitchen_x/2,san_y,kitchen_x/2,passage_y,"bath","","dh"+fwd,"","");
		}
		else {
			ConstructRoom(kitchen_x/2,san_y,0,passage_y,"toilet","","dm","",left);
			ConstructRoom(kitchen_x/2,san_y,kitchen_x/2,passage_y,"bath","","dm","","");
			fwd="dm";
		} left="";
		//passage
		if (Random.value>0.7f) right="e1";
		else {
			if (passage_y>3&&passage_y%1==0) {
				back=WallsDivide((passage_y-2)/2.0f);
				right=back+"ds"+back;
			}
			else {
				if (passage_y==1) right="dm";
				else {
					if (passage_y>=1.5f) right="dh"+WallsDivide(passage_y-1.5f);
				}
			}
		}
		back=WallsDivide((passage_y-enter_width));
		i=(byte)(enter_width*10);
		switch (i) {
		case 10: left="dm";break;
		case 15: left="dh";break;
		case 20: left="ds";break;
		}
		i=0;
		left+=back;
		if (back_open) back=WindowsDivide(kitchen_x); else back="";
		if (fwd!="dm") fwd=fwd+"dhdh"+fwd; else fwd="dmdm";
		ConstructRoom(kitchen_x,passage_y,0,0,"passage",fwd,back,right,left);
		center_left+=WallsDivide(ysize-kitchen_y-passage_y)+right;
		fwd="";right="";left="";back="";

		//основные комнаты
		float l=0;
		if (rooms_n>0)  {
			left=WallsDivide(rooms_y-1.5f); if (rooms_n==4) left+="dh"; else left="dh"+left;
			if (right_open) right=WindowsDivide(rooms_y); else right="";
			if (fwd_open) fwd=WindowsDivide(rooms_x); else fwd="";
			ConstructRoom(rooms_x,rooms_y,xsize-rooms_x,ysize-rooms_y,"room1",fwd,back,right,left);
			center_right=left;
			l+=rooms_y;
			if (rooms_n==2) {
				left="dh"+WallsDivide(add_rooms_y-1.5f);
				if (right_open) right=WindowsDivide(add_rooms_y); else right="";
				if (back_open) back=WindowsDivide(rooms_x); else back="";
				ConstructRoom(rooms_x,add_rooms_y,xsize-rooms_x,ysize-rooms_y-add_rooms_y,"room2","",back,right,left);
				center_right=WallsDivide(ysize-rooms_y-add_rooms_y)+left;
				right="";back="";left="";
				l+=add_rooms_y;
			}
			else {
				left=WallsDivide(rooms_y-1.5f);
				if (right_open) right=WindowsDivide(rooms_y);
				back="";
				ConstructRoom(rooms_x,rooms_y,xsize-rooms_x,ysize-2*rooms_y,"room2","",back,right,left+"dh");
				if (rooms_n==3&&back_open) {back=WindowsDivide(rooms_y);}
				ConstructRoom(rooms_x,rooms_y,xsize-rooms_x,ysize-3*rooms_y,"room3","",back,right,"dh"+left);
				center_right+=left+"dhdh"+left;
				l+=2*rooms_y;
				if (rooms_n==4) {
					if (back_open) back=WindowsDivide(rooms_y); else back="";
					ConstructRoom(rooms_x,rooms_y,xsize-rooms_x,0,"room4","",back,right,"dh"+left);
					center_right+="dh"+left;
					l+=rooms_y;
				}
			}}

		if (fwd_open) fwd=WindowsDivide(central_x); else fwd="";
		if (back_open) back=fwd; else back="";
		if (right_open) center_right+=WindowsDivide(ysize-l); else center_right+=WallsDivide(ysize-l);
		ConstructRoom(central_x,central_y,kitchen_x,0,"central",fwd,back,center_right,center_left);

	

	}

	void ConstructRoom(int size_x,int size_y,float xpos,float ypos,string nm,string fwd,string back,string right,string left) {
		GameObject room=new GameObject(nm);
		room.transform.parent=transform;
		room.transform.localPosition=new Vector3(xpos,0,ypos);
		room.transform.localRotation=Quaternion.Euler(0,0,0);
		bool havehalf_x=false;bool havehalf_y=false;
		if (size_x%2!=0) havehalf_x=true;
		if (size_y%2!=0) havehalf_y=true;

		//floor&&roof
		GameObject y=null;
		GameObject x=Instantiate(floor_tile,transform.position,transform.rotation) as GameObject;
		x.transform.parent=room.transform;
		x.transform.localPosition=new Vector3(size_x/2.0f,0,size_y/2.0f);
		x.transform.localScale=new Vector3(size_x,1,size_y);
		x.transform.localRotation=Quaternion.Euler(0,0,0);
		x.GetComponent<Renderer>().material=floor_material;

		x=Instantiate(floor_tile,transform.position,transform.rotation) as GameObject;
		x.transform.parent=room.transform;
		x.transform.localPosition=new Vector3(size_x/2.0f,3,size_y/2.0f);
		x.transform.localScale=new Vector3(size_x,1,size_y);
		x.transform.localRotation=Quaternion.Euler(180,0,0);

		x.GetComponent<Renderer>().material=roof_material;

		float i=0; float l=0;
		//fwd_walls
		if (fwd.Length==0) {
		if (size_x>=3)
			while (i+2<=size_x) {
				x=Instantiate(wall_2m,transform.position,transform.rotation) as GameObject;
				x.transform.parent=room.transform;
				x.transform.localPosition=new Vector3(i+1,1.5f,size_y);
				x.GetComponent<Renderer>().material=wall_material;
				i+=2;
			}
		if (havehalf_x) {	
			x=Instantiate(wall_1m,transform.position,transform.rotation) as GameObject;
			x.transform.parent=room.transform;
			x.transform.localPosition=new Vector3(size_x-0.5f,1.5f,size_y);
			x.transform.localRotation=Quaternion.Euler(0,180,0);
			x.GetComponent<Renderer>().material=wall_material;
			}}
		else {
			byte j=0;l=0;
				while (j<fwd.Length) {
				x=null;y=null;
					bool need_rotation=false;
					i=0;
					switch (fwd.Substring(j,2)) {
					case "ws":x=Instantiate(wall_05m,transform.position,transform.rotation) as GameObject;i=0.5f;need_rotation=true;break;
				case "wm":x=Instantiate(wall_1m,transform.position,transform.rotation) as GameObject;i=1;need_rotation=true;break;
					case "wh":x=Instantiate(wall_15m,transform.position,transform.rotation) as GameObject;i=1.5f;need_rotation=true;break;
					case "wt":x=Instantiate(wall_2m,transform.position,transform.rotation) as GameObject;i=2;break;
					case "o2":
					   x=Instantiate(wall_window_2m) as GameObject;
					   i=2;
					   y=Instantiate(window_frame) as GameObject;
					   y.transform.parent=room.transform;
					   y.transform.localPosition=new Vector3(l+i/2,1.725f,size_y);
					break;	
				    case "o3":
					   x=Instantiate(wall_window_3m,transform.position,transform.rotation) as GameObject;
					   i=3;
					   need_rotation=true;
					   y=Instantiate(window_frame) as GameObject;
					   y.transform.parent=room.transform;
					   y.transform.localPosition=new Vector3(l+i/2,1.725f,size_y);
					   y.transform.localScale=new Vector3(1.6f,1,1);
					break;
					case "o4":
					   x=Instantiate(wall_window_4m,transform.position,transform.rotation) as GameObject;
					   i=4;
					   need_rotation=true;
					   y=Instantiate(window_frame) as GameObject;
					y.transform.parent=room.transform;
					y.transform.localPosition=new Vector3(l+i/2,1.725f,size_y);
					y.transform.localScale=new Vector3(2,1,1);
					break;
					case "dm":
					       x=Instantiate(wall_door_1m,transform.position,transform.rotation) as GameObject;
					       i=1;
					       y=Instantiate(door_frame) as GameObject;
					y.transform.parent=room.transform;
					y.transform.localPosition=new Vector3(l+i/2,1.25f,size_y);
					y.transform.localScale=new Vector3(0.8f,1,1);
					break;
					case "dh":
					x=Instantiate(wall_door_15m,transform.position,transform.rotation) as GameObject;
					i=1.5f;
					need_rotation=true;
					y=Instantiate(door_frame) as GameObject;
					y.transform.parent=room.transform;
					y.transform.localPosition=new Vector3(l+i/2,1.25f,size_y);
					y.transform.localScale=new Vector3(1.2f,1,1);
					break;
					case "ds":
					x=Instantiate(wall_door_2m,transform.position,transform.rotation) as GameObject;
					i=2;
					y=Instantiate(door_frame) as GameObject;
					y.transform.parent=room.transform;
					y.transform.localPosition=new Vector3(l+i/2,1.25f,size_y);
					y.transform.localScale=new Vector3(1.2f,1,1);
					break;
					case "e1":i=1;x=null;break;
					case "eh": i=1.5f;x=null;break;
					case "e2": i=2;x=null;break;
					}
					if (x!=null) {
					x.transform.parent=room.transform;
					x.transform.localPosition=new Vector3(l+i/2,1.5f,size_y);
					if (need_rotation) x.transform.localRotation=Quaternion.Euler(0,180,0);
						x.GetComponent<Renderer>().material=wall_material;
					if (y!=null) {						
						y.transform.localRotation=Quaternion.Euler(0,180,0);
						y=null;
					}
					l+=i;
				}
					j+=2;
				}
		}
		//left walls
		i=0;x=null;y=null;
		if (left=="") {
		while (i+2<=size_y) {
			x=Instantiate(wall_2m,transform.position,transform.rotation) as GameObject;
			x.transform.parent=room.transform;
			x.transform.localPosition=new Vector3(0,1.5f,i+1);
			x.GetComponent<Renderer>().material=wall_material;
			x.transform.localRotation=Quaternion.Euler(0,-90,0);
			i+=2;
		}
		if (havehalf_y) {	
			x=Instantiate(wall_1m,transform.position,transform.rotation) as GameObject;
			x.transform.parent=room.transform;
			x.transform.localPosition=new Vector3(0,1.5f,size_y-0.5f);
			x.transform.localRotation=Quaternion.Euler(0,90,0);
			x.GetComponent<Renderer>().material=wall_material;
		}}
		else {
			byte j=0;l=0;
				while (j<left.Length) {
				    x=null;y=null;
					bool need_rotation=false;
					i=0;
					switch (left.Substring(j,2)) {
					case "ws":x=Instantiate(wall_05m,transform.position,transform.rotation) as GameObject;i=0.5f;need_rotation=true;break;
				case "wm":x=Instantiate(wall_1m,transform.position,transform.rotation) as GameObject;i=1;need_rotation=true;break;
					case "wh":x=Instantiate(wall_15m,transform.position,transform.rotation) as GameObject;i=1.5f;need_rotation=true;break;
					case "wt":x=Instantiate(wall_2m,transform.position,transform.rotation) as GameObject;i=2;break;
					case "o2":
					x=Instantiate(wall_window_2m,transform.position,transform.rotation) as GameObject;
					i=2;
					y=Instantiate(window_frame) as GameObject;
					y.transform.parent=room.transform;
					y.transform.localPosition=new Vector3(0,1.5f,size_y-l-i/2);
					break;	
				case "o3":
					x=Instantiate(wall_window_3m,transform.position,transform.rotation) as GameObject;
					i=3;
					need_rotation=true;
					y=Instantiate(window_frame) as GameObject;
					y.transform.parent=room.transform;
					y.transform.localPosition=new Vector3(0,1.725f,size_y-l-i/2);
					y.transform.localScale=new Vector3(1.6f,1,1);
					break;
					case "o4":
					x=Instantiate(wall_window_4m,transform.position,transform.rotation) as GameObject;
					i=4;
					need_rotation=true;
					y=Instantiate(window_frame) as GameObject;
					y.transform.parent=room.transform;
					y.transform.localPosition=new Vector3(0,1.725f,size_y-l-i/2);
					y.transform.localScale=new Vector3(2f,1,1);
					break;
				case "dm":
					x=Instantiate(wall_door_1m,transform.position,transform.rotation) as GameObject;
					i=1;
					y=Instantiate(door_frame) as GameObject;
					y.transform.parent=room.transform;
					y.transform.localPosition=new Vector3(0,1.25f,size_y-l-i/2);
					y.transform.localScale=new Vector3(0.8f,1,1);
					break;
				case "dh":
					x=Instantiate(wall_door_15m,transform.position,transform.rotation) as GameObject;
					i=1.5f;
					need_rotation=true;
					y=Instantiate(door_frame) as GameObject;
					y.transform.parent=room.transform;
					y.transform.localPosition=new Vector3(0,1.25f,size_y-l-i/2);
					y.transform.localScale=new Vector3(1.2f,1,1);
					break;
				case "ds":
					x=Instantiate(wall_door_2m,transform.position,transform.rotation) as GameObject;
					i=2;
					need_rotation=true; 
					y=Instantiate(door_frame) as GameObject;
					y.transform.parent=room.transform;
					y.transform.localPosition=new Vector3(0,1.25f,size_y-l-i/2);
					y.transform.localScale=new Vector3(1.2f,1,1);
					break;
					case "e1":i=1;break;
					case "eh": i=1.5f;break;
					case "e2": i=2;break;
					}
					if (x!=null) {
					x.transform.parent=room.transform;
					x.transform.localPosition=new Vector3(0,1.5f,size_y-l-i/2);
					if (need_rotation) x.transform.localRotation=Quaternion.Euler(0,90,0); else x.transform.localRotation=Quaternion.Euler(0,-90,0);
						x.GetComponent<Renderer>().material=wall_material;}
				if (y!=null) {
					y.transform.localRotation=Quaternion.Euler(0,90,0);
					y=null;
				}
				    l+=i;
					j+=2;
				}
		}
		x=null;y=null;i=0;
		//right_walls
		if (right=="") {
		while (i+2<=size_y) {
			x=Instantiate(wall_2m,transform.position,transform.rotation) as GameObject;
			x.transform.parent=room.transform;
			x.transform.localPosition=new Vector3(size_x,1.5f,i+1);
			x.GetComponent<Renderer>().material=wall_material;
			x.transform.localRotation=Quaternion.Euler(0,90,0);
			i+=2;
		}
		if (havehalf_y) {	
			x=Instantiate(wall_1m,transform.position,transform.rotation) as GameObject;
			x.transform.parent=room.transform;
			x.transform.localPosition=new Vector3(size_x,1.5f,size_y-0.5f);
			x.transform.localRotation=Quaternion.Euler(0,-90,0);
			x.GetComponent<Renderer>().material=wall_material;
			}}
		else {
			byte j=0;
				i=size_y;
				while (j<right.Length) {
				l=0;x=null;y=null;
					bool need_rotation=false;
					switch (right.Substring(j,2)) {
					case "ws":x=Instantiate(wall_05m,transform.position,transform.rotation) as GameObject;l=0.5f;need_rotation=true;break;
					case "wm":x=Instantiate(wall_1m,transform.position,transform.rotation) as GameObject;l=1;break;
					case "wh":x=Instantiate(wall_15m,transform.position,transform.rotation) as GameObject;l=1.5f;need_rotation=true;break;
					case "wt":x=Instantiate(wall_2m,transform.position,transform.rotation) as GameObject;l=2;break;
				case "o2":
					x=Instantiate(wall_window_2m,transform.position,transform.rotation) as GameObject;
					l=2;
					need_rotation=true;
					y=Instantiate(window_frame) as GameObject;
					y.transform.parent=room.transform;
					y.transform.localPosition=new Vector3(size_x,1.725f,i-l/2);
					break;	
				case "o3":
					x=Instantiate(wall_window_3m,transform.position,transform.rotation) as GameObject;
					l=3;
					y=Instantiate(window_frame) as GameObject;
					y.transform.parent=room.transform;
					y.transform.localPosition=new Vector3(size_x,1.725f,i-l/2);
					y.transform.localScale=new Vector3(1.6f,1,1);
					break;
				case "o4":
					x=Instantiate(wall_window_4m,transform.position,transform.rotation) as GameObject;
					l=4;
					need_rotation=true;
					y=Instantiate(window_frame) as GameObject;
					y.transform.parent=room.transform;
					y.transform.localPosition=new Vector3(size_x,1.725f,i-l/2);
					y.transform.localScale=new Vector3(2,1,1);
					break;
					case "dm":
					x=Instantiate(wall_door_1m,transform.position,transform.rotation) as GameObject;
					l=1;
					y=Instantiate(door_frame) as GameObject;
					y.transform.parent=room.transform;
					y.transform.localPosition=new Vector3(size_x,1.725f,i-l/2);
					y.transform.localScale=new Vector3(0.8f,1,1);
					break;
					case "dh":
					x=Instantiate(wall_door_15m,transform.position,transform.rotation) as GameObject;
					l=1.5f;
					need_rotation=true;
					y=Instantiate(door_frame) as GameObject;
					y.transform.parent=room.transform;
					y.transform.localPosition=new Vector3(size_x,1.25f,i-l/2);
					y.transform.localScale=new Vector3(1.2f,1,1);
					break;
					case "ds":
					x=Instantiate(wall_door_2m,transform.position,transform.rotation) as GameObject;
					l=2;
					need_rotation=true;
					y=Instantiate(door_frame) as GameObject;
					y.transform.parent=room.transform;
					y.transform.localPosition=new Vector3(size_x,1.25f,i-l/2);
					y.transform.localScale=new Vector3(1.2f,1,1);
					break;
					case "e1":l=1;x=null;break;
					case "eh": l=1.5f;x=null;break;
					case "e2": l=2;x=null;break;
					} 
					if (x!=null) {
					x.transform.parent=room.transform;
					x.transform.localPosition=new Vector3(size_x,1.5f,i-l/2);
					if (y!=null) {
						y.transform.localRotation=Quaternion.Euler(0,-90,0);
						y=null;
					}
					i-=l;
					if (need_rotation) x.transform.localRotation=Quaternion.Euler(0,-90,0); else x.transform.localRotation=Quaternion.Euler(0,90,0);
					x.GetComponent<Renderer>().material=wall_material;
					}
					j+=2;
				}
		} 
		//backwalls
		x=null;y=null;i=0;
		if (back=="") {
		while (i+2<=size_x) {
			x=Instantiate(wall_2m,transform.position,transform.rotation) as GameObject;
			x.transform.parent=room.transform;
			x.transform.localPosition=new Vector3(i+1,1.5f,0);
			x.GetComponent<Renderer>().material=wall_material;
			x.transform.localRotation=Quaternion.Euler(0,180,0);
			i+=2;
		}
		if (havehalf_x) {	
			x=Instantiate(wall_1m,transform.position,transform.rotation) as GameObject;
			x.transform.parent=room.transform;
			x.transform.localPosition=new Vector3(size_x-0.5f,1.5f,0);
			x.GetComponent<Renderer>().material=wall_material;
		}}
		else {
			byte j=0;l=0;
			while (j<back.Length) {
				x=null;y=null;
				bool need_rotation=false;
				i=0;
				switch (back.Substring(j,2)) {
				case "ws":x=Instantiate(wall_05m,transform.position,transform.rotation) as GameObject;i=0.5f;need_rotation=true;break;
				case "wm":x=Instantiate(wall_1m,transform.position,transform.rotation) as GameObject;i=1;need_rotation=true;break;
				case "wh":x=Instantiate(wall_15m,transform.position,transform.rotation) as GameObject;i=1.5f;need_rotation=true;break;
				case "wt":x=Instantiate(wall_2m,transform.position,transform.rotation) as GameObject;i=2;break;
				case "o2":
					x=Instantiate(wall_window_2m,transform.position,transform.rotation) as GameObject;
					i=2;
					y=Instantiate(window_frame) as GameObject;
					y.transform.parent=room.transform;
					y.transform.localPosition=new Vector3(l+i/2,1.725f,0);
					y.transform.localScale=new Vector3(1,1,1);
					break;	
				case "o3":
					x=Instantiate(wall_window_3m,transform.position,transform.rotation) as GameObject;
					i=3;
					need_rotation=true;
					y=Instantiate(window_frame) as GameObject;
					y.transform.parent=room.transform;
					y.transform.localPosition=new Vector3(l+i/2,1.725f,0);
					y.transform.localScale=new Vector3(1.6f,1,1);
					break;
				case "o4":
					x=Instantiate(wall_window_4m,transform.position,transform.rotation) as GameObject;
					i=4;
					need_rotation=true;
					y=Instantiate(window_frame) as GameObject;
					y.transform.parent=room.transform;
					y.transform.localPosition=new Vector3(l+i/2,1.725f,0);
					y.transform.localScale=new Vector3(2,1,1);
					break;
				case "dm":
					x=Instantiate(wall_door_1m,transform.position,transform.rotation) as GameObject;
					i=1;
					y=Instantiate(door_frame) as GameObject;
					y.transform.parent=room.transform;
					y.transform.localPosition=new Vector3(l+i/2,1.25f,0);
					y.transform.localScale=new Vector3(0.8f,1,1);
					break;
				case "dh":
					x=Instantiate(wall_door_15m,transform.position,transform.rotation) as GameObject;
					i=1.5f;
					need_rotation=true;
					y=Instantiate(door_frame) as GameObject;
					y.transform.parent=room.transform;
					y.transform.localPosition=new Vector3(l+i/2,1.25f,0);
					y.transform.localScale=new Vector3(1.2f,1,1);
					break;
				case "ds":
					x=Instantiate(wall_door_2m,transform.position,transform.rotation) as GameObject;
					i=2;
					y=Instantiate(door_frame) as GameObject;
					y.transform.parent=room.transform;
					y.transform.localPosition=new Vector3(l+i/2,1.25f,0);
					y.transform.localScale=new Vector3(1.2f,1,1);
					break;
				case "e1":i=1;x=null;break;
				case "eh": i=1.5f;x=null;break;
				case "e2": i=2;x=null;break;
				}
				if (x!=null) {
					x.transform.parent=room.transform;
					x.transform.localPosition=new Vector3(l+i/2,1.5f,0);
					if (y!=null) {
						y.transform.localRotation=Quaternion.Euler(0,0,0);
						y=null;}
					l+=i;
					if (!need_rotation) x.transform.localRotation=Quaternion.Euler(0,180,0);
					x.GetComponent<Renderer>().material=wall_material;}
				j+=2;
			}
		}
	}

	string WallsDivide (float l) { //поблочное разделение стенами и возврат шифрованной строкой
		if (l==0) return("");
		byte x=(byte) l;
		bool have_half=false;
		string s="";
		byte i=0;
		if (l-x==0.5f) {have_half=true;}
		if (x%2==0) {while (i<x) {s+="wt";i+=2;} if (have_half) {if (Random.value>=0.5f) s+="ws"; else s="ws"+s;}}
		else {if (have_half) {s+="wh";} i=1; while (i<x) {s+="wt";i+=2;}}
		return (s);
	}

	string WindowsDivide (float l) { //заполнение окнами по мере возможности
		if (l==0) return ("");
		byte x=(byte)l;
		bool have_half=false; if (l-x==0.5f) have_half=true;
		string s="";
		byte i=0;
		if (x%4==0) {
			while(i<x) {s+="o4";i+=4;} if (have_half) {if (Random.value>=0.5f) s+="ws"; else s="ws"+s;}
		}
		else {
			if (x%3==0) {while(i<x) {s+="o3";i+=3;} if (have_half) {if (Random.value>=0.5f) s+="ws"; else s="ws"+s;}}
			else {
				if (x==2) s="o2"; 
				else {
					if (x%2==1) {s="wm";}
					i=1;
					if ((x-1)%3==0) {while (i<x) {s+="o3";i+=3;}}
					else {while (i<x-1) {s+="o3";i+=3;} s+="wm";}
				}
			}
		}  
		return (s);
	}
}
