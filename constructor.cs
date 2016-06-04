using UnityEngine;
using System.Collections;

public class constructor : MonoBehaviour {


	public float enter_width=1.5f;
	public int q_x=15;
	public int q_y=15;
	public int entrance_y=11;
	int q_number=1;
	int floor_number=1;
	Vector3 exit_position;

	public Material potolok_material;
	public Material floor_material;
	public Material wall_material;
	public Material outside_wall_material;
	public Material padik_wall_material;
	public Material padik_floor_material;

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
	GameObject ladder;

	GameObject platform;
	GameObject floor;

	void Awake () {
		wall_05m=Resources.Load<GameObject>("structures/wl05");
		wall_1m=Resources.Load<GameObject>("structures/wl1m");
		wall_15m=Resources.Load<GameObject>("structures/wl15");
		wall_2m=Resources.Load<GameObject>("structures/wl2m");
		floor_tile=Resources.Load<GameObject>("structures/flpl");
		wall_window_2m=Resources.Load<GameObject>("structures/ww2m");
		wall_window_3m=Resources.Load<GameObject>("structures/ww3m");
		wall_window_4m=Resources.Load<GameObject>("structures/ww4m");
		wall_door_1m=Resources.Load<GameObject>("structures/wd1m");
		wall_door_15m=Resources.Load<GameObject>("structures/wd15");
		wall_door_2m=Resources.Load<GameObject>("structures/wd2m");
		door_frame=Resources.Load<GameObject>("structures/drfr");
		window_frame=Resources.Load<GameObject>("structures/wwfr");
		ladder=Resources.Load<GameObject>("structures/stld");
	}


	void Start () {
		entrance_y=q_y-entrance_y; if (entrance_y<0) entrance_y=0;
		//entrance
		GameObject p=new GameObject("p0");
		p.transform.parent=transform;
		p.transform.localRotation=Quaternion.Euler(0,0,0);
		p.transform.localPosition=new Vector3(0,0,-q_y+9);
		GameObject q=Instantiate(ladder) as GameObject;
		q.transform.parent=p.transform;
		q.transform.localPosition=new Vector3(-2,0.75f,-3);
		q.transform.localRotation=Quaternion.Euler(0,0,0);
		CreateBlock(p,new Vector3(-3,0,2),"lvt001",2);
		CreateBlock(p,new Vector3(-3,0,0),"lvt002",2);
		CreateBlock(p,new Vector3(3,0,2),"rvt001",2);
		CreateBlock(p,new Vector3(3,0,0),"rvt002",2);
		CreateBlock(p,new Vector3(3,0,-2),"rts000",2);
		CreateBlock(p,new Vector3(-3,0,2),"fwt001",2);
		CreateBlock(p,new Vector3(-1,0,2),"fds000",2);
		CreateBlock(p,new Vector3(1,0,2),"fwt002",2);
		q=Instantiate(Global.str_plate53degree) as GameObject;
		q.transform.parent=p.transform;
		q.transform.localPosition=new Vector3(1,0.75f,-3);
		q.transform.localRotation=Quaternion.Euler(53,180,0);
		q.transform.localScale=new Vector3(4,2.5f,1);
		q=Instantiate(floor_tile) as GameObject;
		q.transform.parent=p.transform;
		q.transform.localPosition=new Vector3(0,0,0);
		q.transform.localRotation=Quaternion.Euler(0,0,0);
		q.transform.localScale=new Vector3(6,1,4);
		q.GetComponent<Renderer>().material=padik_floor_material;

		//first floor
		GameObject f=new GameObject();
		f.name=floor_number.ToString();
		if (f.name.Length==1) f.name="f0"+f.name; else f.name="f"+f.name;
		f.transform.parent=transform;
		f.transform.localPosition=new Vector3(0,1.5f,-q_y+2.5f);
		f.transform.localRotation=Quaternion.Euler(0,0,0);
		CreateDoor(f,new Vector3(-3,1.2f,-q_y+0.75f),new Vector3(0,90,0),enter_width,false);

		Quaternion rotation=Quaternion.identity;
		q=CreateQuarteer_0(q_x,q_y,new Vector3(3,1.5f,-q_y),rotation,true,true,true,false,true,false,false); 
		q.transform.parent=f.transform;
		q.name='q'+f.name.Substring(1,2)+q_number.ToString();
		if (q.name.Length==4) {q.name=q.name.Substring(0,3)+'0'+q.name[3];}
		q_number++;
		q=CreateQuarteer_0(q_x,q_y,new Vector3(-q_x-3,1.5f,-q_y),rotation,true,true,true,false,true,true,false);
		q.transform.parent=f.transform;
		q.name='q'+f.name.Substring(1,2)+q_number.ToString();
		if (q.name.Length==4) {q.name=q.name.Substring(0,3)+'0'+q.name[3];}
		q_number++;
		p=new GameObject("hall");
		p.transform.parent=f.transform;
		p.transform.localPosition=Vector3.zero;
		p.transform.localRotation=Quaternion.identity;
		q=Instantiate(floor_tile) as GameObject ;
		q.transform.parent=p.transform;
	    q.transform.localPosition=new Vector3(0,1.5f,-q_y+2.5f);
		q.transform.localRotation=Quaternion.Euler(0,0,0);
		q.transform.localScale=new Vector3(6,1,5);
		q.GetComponent<Renderer>().material=padik_floor_material;
		float a=q_y/3; if (a<2) a=2; //kitchen_y
		float b=(q_y-a)/2; if (b>4) b=4; //san_y
		a=q_y-a-b;//passage_y 
		if (a>5) a=5;
		string s="";
		switch ((byte)(enter_width*10)) {
		case 10: s="rm"; break;
		case 15: s="rh";break;
		case 20: s="rs";break;
		}
		CreateBlock(p,new Vector3(-3,1.5f,-q_y+a),'l'+s+"000",2);
		CreateBlock(p,new Vector3(3,1.5f,-q_y+a),'r'+s+"000",2);
		s=WallsDivide(a-enter_width,false);
		byte j=0; float l=a-enter_width;
		while (j<s.Length) {
			CreateBlock(p,new Vector3(-3,1.5f,-q_y+l),'l'+s.Substring(j,2)+"000",2);
			l-=CreateBlock(p,new Vector3(3,1.5f,-q_y+l),'r'+s.Substring(j,2)+"000",2);
			j+=2;
		}
		//first platform	
		p=new GameObject("p1");
		p.transform.parent=transform;
		p.transform.localPosition=new Vector3(0,3,0);
		p.transform.localRotation=Quaternion.Euler(0,0,0);
		q=Instantiate(ladder) as GameObject;
		q.transform.parent=p.transform;
		q.transform.localPosition=new Vector3(2,-0.75f,-q_y+6);
		q.transform.localRotation=Quaternion.Euler(0,180,0);
		q=Instantiate(floor_tile) as GameObject;
		q.transform.parent=p.transform;
		q.transform.localPosition=new Vector3(0,0,-q_y+9);
		q.transform.localRotation=Quaternion.Euler(0,0,0);
		q.transform.localScale=new Vector3(6,1,4);
		q.GetComponent<Renderer>().material=padik_floor_material;
		q=Instantiate(q,q.transform.position,q.transform.rotation) as GameObject;
		q.transform.parent=p.transform;
		q.transform.Translate(0,-0.1f,0);
		q.transform.Rotate(180,0,0);
		q=Instantiate(ladder) as GameObject;
		q.transform.parent=p.transform;
		q.transform.localPosition=new Vector3(-2,0.75f,-q_y+6);
		q.transform.localRotation=Quaternion.Euler(0,0,0);
		q=Instantiate(wall_1m) as GameObject;
		q.transform.parent=p.transform;
		q.transform.localPosition=new Vector3(0,-0.05f,-q_y+7);
		q.transform.localRotation=Quaternion.Euler(0,180,0);
		q.transform.localScale=new Vector3(2,0.034f,1);
		q.GetComponent<Renderer>().material=padik_floor_material;
		CreateBlock(p,new Vector3(-3,0,-q_y+11),"lvt000",2);
		CreateBlock(p,new Vector3(-3,0,-q_y+9),"lvt001",2);
		CreateBlock(p,new Vector3(3,0,-q_y+11),"rvt000",2);
		CreateBlock(p,new Vector3(3,0,-q_y+9),"rvt001",2);

		CreateBlock(p,new Vector3(-3,0,-q_y+11),"fwm000",2);
		CreateBlock(p,new Vector3(-2,0,-q_y+11),"fo4000",2);
		CreateBlock(p,new Vector3(2,0,-q_y+11),"fwm001",2);
	}

	//если сделать несколько разных алгоритмов сборки, то стоит воспользоваться делегатами
	//и выделить процесс расчета комнат в отдельные функции
	GameObject CreateQuarteer_0(int xsize, int ysize,Vector3 pos,Quaternion rot, bool fwd_open,bool back_open,bool right_open,bool left_open,bool left_exit,bool mirror_x,bool mirror_y) {
		GameObject quarteer=new GameObject();
		quarteer.transform.parent=transform;
		quarteer.transform.localPosition=pos;
		quarteer.transform.localRotation=rot;
		int rooms_n=0;
		if (xsize<=4) {xsize=4;rooms_n=-1;}
		if (ysize<=4) ysize=4;
		int kitchen_x=2;
		int central_x=2;
		int rooms_x=0;
		if (xsize<10) {
			if (xsize%2==0) {kitchen_x=xsize/2-1;central_x=xsize/2+1;} else {kitchen_x=(xsize-1)/2;central_x=xsize-kitchen_x;}
			rooms_n=-1;
		}
		else {
			if (xsize%2==0) {kitchen_x=xsize/4;}
			else {kitchen_x=(xsize-1)/4;}
		}

		int  kitchen_y=2;
		int central_y=ysize;
		int rooms_y=4;
		int add_rooms_y=0;
		int  san_y=1;
		int passage_y=1;
		kitchen_y=ysize/3; if (kitchen_y<2) kitchen_y=2;
		san_y=(ysize-kitchen_y)/2; if (san_y>4) san_y=4; 
		passage_y=ysize-kitchen_y-san_y; if (passage_y>5) passage_y=5; 
		kitchen_y=ysize-san_y-passage_y;
		if (xsize>10&&rooms_n!=-1) {
			if (ysize>15) {
				if (ysize%4==0) {rooms_y=ysize/4;add_rooms_y=ysize/4;rooms_n=4;} else {
					if (ysize%3==0) {rooms_y=ysize/3;add_rooms_y=ysize/3;rooms_n=3;}
					else {rooms_n=2;add_rooms_y=ysize/2;rooms_y=ysize-add_rooms_y;}
				}
			}
			else {
				if (ysize%2==0) {if (ysize==12) {rooms_y=3;rooms_n=4;} else {rooms_y=ysize/2;add_rooms_y=rooms_y;rooms_n=2;}}
				else {
					if (ysize%3==0) {rooms_y=ysize/3;rooms_n=3;} else {rooms_y=(ysize-1)/2;add_rooms_y=rooms_y+1;rooms_n=2;}
				}
			}
		}
		if (rooms_n>0&&rooms_y>0) {
			rooms_x=xsize-kitchen_x; if (rooms_x%2==1) rooms_x=(rooms_x-1)/2+1; else rooms_x/=2;
			if (rooms_x/rooms_y>=4) rooms_x=rooms_y*4;
			central_x=xsize-kitchen_x-rooms_x;
		} else central_x=xsize-kitchen_x;
		//сборка
		byte i=0; float z=0; //ширина подвижной части
		string fwd=""; string back="";string right="";string left="";
		string center_right=""; string center_left="";
		//kitchen
		if (fwd_open) fwd=WindowsDivide(kitchen_x); else fwd=WallsDivide(kitchen_x,false);
		z=kitchen_y-entrance_y;
		if (z<=0) left=WindowsDivide(kitchen_y);
		else {
		if (left_open) left=WindowsDivide(entrance_y)+WallsDivide(kitchen_x-entrance_y,false); 
		else left=WallsDivide(entrance_y,true)+WallsDivide(kitchen_y-entrance_y,false);
	}
		z=0;
		back=WallsDivide(kitchen_x,false);
		if (kitchen_y>3) {
			right=WallsDivide(kitchen_y-1.5f,false)+"rh";
			z=1.5f;
		} else {
			switch (kitchen_y) {
			case 1:right="rm";z=1;break;
			case 2: right="vsrh";z=1.5f;break;
			case 3:right="vhrh";z=1.5f;break;
			}
		} 
		if (mirror_x) {
			ConstructRoom(quarteer,kitchen_x,kitchen_y,xsize-kitchen_x,ysize-kitchen_y,"kitc",fwd,back,left,right);
			CreateDoor(quarteer,new Vector3(xsize-kitchen_x,1.2f,ysize-kitchen_y+z/2.0f+0.5f),new Vector3(0,90,0),z,true);
			center_right=right; 
		}
			else {
			 ConstructRoom(quarteer,kitchen_x,kitchen_y,0,ysize-kitchen_y,"kitc",fwd,back,right,left);
			CreateDoor(quarteer,new Vector3(kitchen_x,1.2f,ysize-kitchen_y+z/2.0f-0.5f),new Vector3(0,-90,0),z,true);
			center_left=right; }
		fwd="";back="";right="";

		//toilet&bath
		if (left_open) left=WindowsDivide(san_y); else left=WallsDivide(san_y,false);
			back=""; //инверсия back и fwd для более удобного построения passage
			if (kitchen_x>=3) {z=1.5f;back="rh";}	else {z=1;back="rm";}
			fwd=WallsDivide(kitchen_x/2.0f-z,false);
		if (mirror_x) {
			ConstructRoom(quarteer,kitchen_x/2.0f,san_y,q_x-kitchen_x/2.0f,passage_y,"bath",WallsDivide(kitchen_x/2.0f,false),back+fwd,WallsDivide(san_y,left_open),WallsDivide(san_y,false));
			ConstructRoom(quarteer,kitchen_x/2.0f,san_y,q_x-kitchen_x,passage_y,"toil",WallsDivide(kitchen_x/2.0f,false),fwd+back,WallsDivide(san_y,false),WallsDivide(san_y,false));
			CreateDoor(quarteer,new Vector3(xsize-kitchen_x/2.0f-z/2.0f-0.5f*(z/1.5f),1.2f,passage_y),new Vector3(0,0,0),z,false);
			CreateDoor(quarteer,new Vector3(xsize-kitchen_x/2.0f+z/2.0f-0.5f*(z/1.5f),1.2f,passage_y),new Vector3(0,0,0),z,false);
		}
		else {
			ConstructRoom(quarteer,kitchen_x/2.0f,san_y,0,passage_y,"bath",WallsDivide(kitchen_x/2.0f,false),fwd+back,WallsDivide(san_y,left_open),WallsDivide(san_y,false));
			ConstructRoom(quarteer,kitchen_x/2.0f,san_y,kitchen_x/2.0f,passage_y,"toil",WallsDivide(kitchen_x/2.0f,false),back+fwd,WallsDivide(san_y,false),WallsDivide(san_y,false));
			CreateDoor(quarteer,new Vector3(kitchen_x/2.0f-z/2.0f-0.5f*(z/1.5f),1.2f,passage_y),new Vector3(0,0,0),z,false);
			CreateDoor(quarteer,new Vector3(kitchen_x/2.0f+z/2.0f-0.5f*(z/1.5f),1.2f,passage_y),new Vector3(0,0,0),z,false);
		}

		//passage
		if (Random.value>0.7f) right="e1";
		else {
			if (passage_y>3&&passage_y%1==0) {
				back=WallsDivide((passage_y-2)/2.0f,false); // как временная переменная
				right=back+"rs"+back;
			}
			else {
				if (passage_y==1) right="rm";
				else {
					if (passage_y>=1.5f) right="rh"+WallsDivide(passage_y-1.5f,false);
				}
			}
		}
		if (left_exit) {
		back=WallsDivide((passage_y-enter_width),false); //временный расчет для левой стены
		i=(byte)(enter_width*10);
		switch (i) {
			case 10: left="rm";break;
			case 15: left="rh";break;
			case 20: left="rs";break;
		}
		i=0;
		left+=back;
			if (back_open) back=WindowsDivide(kitchen_x); else back=WallsDivide(kitchen_x,false);
		}
		else {
			//выход снизу
			left=WallsDivide(passage_y,false);
			i=(byte)(enter_width*10);
			switch (i) {
			case 10: back="rm";break;
			case 15: back="rh";break;
			case 20: back="rs";break;
			} i=0;
			back+=WallsDivide(kitchen_x-enter_width,false);
		}
		if (z==1) fwd=fwd+"rmrm"+fwd;
		else fwd=fwd+"rhrh"+fwd;
		if (!mirror_x) {
			ConstructRoom(quarteer,kitchen_x,passage_y,0,0,"pass",fwd,back,right,left);
		center_left+=WallsDivide(ysize-kitchen_y-passage_y,false)+right;
		}
		else {
			ConstructRoom(quarteer,kitchen_x,passage_y,xsize-kitchen_x,0,"pass",fwd,back,left,right);
			center_right+=WallsDivide(ysize-kitchen_y-passage_y,false)+right;
		}
		fwd="";right="";left="";back="";

		//основные комнаты
		float l=0;
		if (rooms_n>0)  {
			left=WallsDivide(rooms_y-1.5f,false); 
			if (rooms_n%2==0) {left+="rh";l=1;} else {left="rh"+left;l=-1;}
			if (right_open) right=WindowsDivide(rooms_y); else right=WallsDivide(rooms_y,false);
			if (fwd_open) fwd=WindowsDivide(rooms_x); else fwd=WallsDivide(rooms_x,false);
			if (rooms_n==1) {if (back_open) back=WindowsDivide(rooms_x); else back=WallsDivide(rooms_x,false);}
			else back=WallsDivide(rooms_x,false);
			if (!mirror_x) {
				ConstructRoom(quarteer,rooms_x,rooms_y,xsize-rooms_x,ysize-rooms_y,"rm01",fwd,back,right,left);
				if (l==1) CreateDoor(quarteer,new Vector3(xsize-rooms_x,1.2f,ysize-rooms_y+1.25f),new Vector3(0,90,0),z,true);
				else CreateDoor(quarteer,new Vector3(xsize-rooms_x,1.2f,ysize-0.25f),new Vector3(0,90,0),z,true);
				center_right=left;}
			else {
				ConstructRoom(quarteer,rooms_x,rooms_y,0,ysize-rooms_y,"rm01",fwd,back,left,right);
				if (l==1) CreateDoor(quarteer,new Vector3(rooms_x,1.2f,ysize-rooms_y+0.25f),new Vector3(0,-90,0),z,true);
				else CreateDoor(quarteer,new Vector3(rooms_x,1.2f,ysize-0.25f),new Vector3(0,90,0),z,true);
				center_left=left;
			}
			l=rooms_y;
			if (rooms_n==2) {
				left="rh"+WallsDivide(add_rooms_y-1.5f,false);
				fwd=WallsDivide(rooms_x,false);
				if (right_open) right=WindowsDivide(add_rooms_y); else right=WallsDivide(add_rooms_y,false);
				if (back_open) back=WindowsDivide(rooms_x); else back=WallsDivide(rooms_x,false);
				if (!mirror_x) {
				ConstructRoom(quarteer,rooms_x,add_rooms_y,xsize-rooms_x,ysize-rooms_y-add_rooms_y,"rm02",fwd,back,right,left);
				CreateDoor(quarteer,new Vector3(xsize-rooms_x,1.2f,add_rooms_y-0.25f),new Vector3(0,90,0),z,true);
				center_right+=WallsDivide(ysize-rooms_y-add_rooms_y,false)+left;
				}
				else {
					ConstructRoom(quarteer,rooms_x,add_rooms_y,0,ysize-rooms_y-add_rooms_y,"rm02",fwd,back,left,right);
					CreateDoor(quarteer,new Vector3(rooms_x,1.2f,add_rooms_y-0.25f),new Vector3(0,90,0),z,true);
					center_left+=WallsDivide(ysize-rooms_y-add_rooms_y,false)+left;
				}
				right="";back="";left="";
				l+=add_rooms_y;
			}
			else {
				left=WallsDivide(rooms_y-1.5f,false);
				if (right_open) right=WindowsDivide(rooms_y);
				back=WallsDivide(rooms_x,false);
				if (!mirror_x) {
					ConstructRoom(quarteer,rooms_x,rooms_y,xsize-rooms_x,ysize-2*rooms_y,"rm02",WallsDivide(rooms_x,false),back,right,left+"rh");
					CreateDoor(quarteer,new Vector3(xsize-rooms_x,1.2f,ysize-2*rooms_y+1.25f),new Vector3(0,90,0),z,true);
				}
				else {
					ConstructRoom(quarteer,rooms_x,rooms_y,0,ysize-2*rooms_y,"rm02",WallsDivide(rooms_x,false),back,left+"rh",right);
					CreateDoor(quarteer,new Vector3(rooms_x,1.2f,ysize-2*rooms_y+1.25f),new Vector3(0,90,0),z,true);
				}
				bool a=false;
				if (rooms_n==3&&back_open) {back=WindowsDivide(rooms_x);a=true;}
				if (!mirror_x) {
					ConstructRoom(quarteer,rooms_x,rooms_y,xsize-rooms_x,ysize-3*rooms_y,"rm03",WallsDivide(rooms_x,false),back,right,"rh"+left);
					CreateDoor(quarteer,new Vector3(xsize-rooms_x,1.2f,ysize-2*rooms_y-0.25f),new Vector3(0,90,0),z,true);
				    center_right+=left+"rhrh"+left;
				}
				else {
					ConstructRoom(quarteer,rooms_x,rooms_y,0,ysize-3*rooms_y,"rm03",WallsDivide(rooms_x,false),back,"rh"+left,right);
					CreateDoor(quarteer,new Vector3(rooms_x,1.2f,ysize-2*rooms_y-0.25f),new Vector3(0,90,0),z,true);
					center_left+=left+"rhrh"+left;
				}
				l+=2*rooms_y;
				if (rooms_n==4) {
					if (back_open) back=WindowsDivide(rooms_x); else back=WallsDivide(rooms_x,false);
					if (!mirror_x) {
						ConstructRoom(quarteer,rooms_x,rooms_y,xsize-rooms_x,0,"rm04",WallsDivide(rooms_x,false),back,right,"rh"+left);
						CreateDoor(quarteer,new Vector3(xsize-rooms_x,1.2f,rooms_y-0.25f),new Vector3(0,90,0),z,true);
					    center_right+="rh"+left;
					}
					else {
						ConstructRoom(quarteer,rooms_x,rooms_y,0,0,"rm04",WallsDivide(rooms_x,false),back,"rh"+left,right);
						CreateDoor(quarteer,new Vector3(rooms_x,1.2f,rooms_y-0.25f),new Vector3(0,90,0),z,true);
						center_left+="rh"+left;
					}
					l+=rooms_y;
				}
			}}
		else {if (!mirror_x)center_right=WallsDivide(ysize,right_open); else center_left=WallsDivide(ysize,right_open);}

		if (fwd_open) fwd=WindowsDivide(central_x); else fwd=WallsDivide(central_x,false);
		if (back_open) back=fwd; else back=WallsDivide(central_x,false);
		if (!mirror_x)   {if (right_open) center_right+=WindowsDivide(ysize-l); else center_right+=WallsDivide(ysize-l,false);}
		else  {if (left_open) center_left+=WindowsDivide(ysize-l); else center_left+=WallsDivide(ysize-l,false);}
		bool b=false;
		if (rooms_n==0) b=true;
		if (!mirror_x) {
			ConstructRoom(quarteer,central_x,central_y,kitchen_x,0,"hall",fwd,back,center_right,center_left);
		}
		else {
			ConstructRoom(quarteer,central_x,central_y,rooms_x,0,"hall",fwd,back,center_right,center_left);
		}
			return (quarteer);
	}

	///// ----------------------------------------------CREATE BLOCK

	float CreateBlock(GameObject room,Vector3 start_pos,string code,int matcode) {
		bool need_rotation=false;
		bool other_side=false;
		GameObject x=null;
		GameObject y=null; //дополнительные детали
		float y_height=1.5f;
		string obj_id=""; //кодовый идентификатор типа блока
		float i=0; //длина блока 
		float z=0; //длина подвижной части
		switch (code.Substring(1,2)) {
		case "ws":x=Instantiate(wall_05m) as GameObject;i=0.5f;need_rotation=true;other_side=true;obj_id="wl05";break;
		case "vs":x=Instantiate(wall_05m) as GameObject;i=0.5f;need_rotation=true;obj_id="wl05";break;
		case "wm":x=Instantiate(wall_1m) as GameObject;i=1;need_rotation=true;other_side=true;obj_id="wl1m";break;
		case "vm":x=Instantiate(wall_1m) as GameObject;i=1;need_rotation=true;obj_id="wl1m";break;
		case "wh":x=Instantiate(wall_15m) as GameObject;i=1.5f;need_rotation=true;other_side=true;obj_id="wl15";break;
		case "vh":x=Instantiate(wall_15m) as GameObject;i=1.5f;need_rotation=true;obj_id="wl15";break;
		case "wt":x=Instantiate(wall_2m) as GameObject;i=2;other_side=true;obj_id="wl2m";break;
		case "vt":x=Instantiate(wall_2m) as GameObject;i=2;obj_id="wl2m";break;
		case "o2":
			x=Instantiate(wall_window_2m) as GameObject;
			i=2;
			y=Instantiate(window_frame) as GameObject;
			y_height=1.725f;
			other_side=true;
			obj_id="ww2m";
			need_rotation=true;
			break;	
		case "o3":
			x=Instantiate(wall_window_3m) as GameObject;
			i=3;
			need_rotation=true;
			y=Instantiate(window_frame) as GameObject;
			y_height=1.725f;
			y.transform.localScale=new Vector3(1.6f,1,1);
			other_side=true;
			obj_id="ww3m";
			break;
		case "o4":
			x=Instantiate(wall_window_4m) as GameObject;
			i=4;
			need_rotation=true;
			y=Instantiate(window_frame) as GameObject;
			y_height=1.725f;
			y.transform.localScale=new Vector3(2,1,1);
			other_side=true;
			obj_id="ww4m";
			break;
		case "dm":
			x=Instantiate(wall_door_1m) as GameObject;
			i=1;
			y=Instantiate(door_frame) as GameObject;
			y_height=1.25f;
			y.transform.localScale=new Vector3(0.8f,1,1);
			obj_id="wd1m";
			other_side=true;
			break;
		case "dh":
			x=Instantiate(wall_door_15m) as GameObject;
			i=1.5f;
			need_rotation=true;
			y=Instantiate(door_frame) as GameObject;
			y_height=1.25f;
			y.transform.localScale=new Vector3(1.2f,1,1);
			obj_id="wd15";
			other_side=true;
			break;
		case "ds":
			x=Instantiate(wall_door_2m) as GameObject;
			i=2;
			y=Instantiate(door_frame) as GameObject;
			y_height=1.25f;
			y.transform.localScale=new Vector3(1.2f,1,1);
			obj_id="wd2m";
			other_side=true;
			need_rotation=true;
			break;
		case "rm":
			x=Instantiate(wall_door_1m) as GameObject;
			i=1;
			y=Instantiate(door_frame) as GameObject;
			y_height=1.25f;
			y.transform.localScale=new Vector3(0.8f,1,1);
			obj_id="wd1m";
			need_rotation=true;
			break;
		case "rh":
			x=Instantiate(wall_door_15m) as GameObject;
			i=1.5f;
			need_rotation=true;
			y=Instantiate(door_frame) as GameObject;
			y_height=1.25f;
			y.transform.localScale=new Vector3(1.2f,1,1);
			obj_id="wd15";
			break;
		case "rs":
			x=Instantiate(wall_door_2m) as GameObject;
			i=2;
			y=Instantiate(door_frame) as GameObject;
			y_height=1.25f;
			y.transform.localScale=new Vector3(1.2f,1,1);
			obj_id="wd2m";
			break;
		case "ts":
			x=Instantiate(Global.str_triangle2m) as GameObject;
			i=2;
			obj_id="tr2m";
			need_rotation=true;
			break;
		case "to":
			x=Instantiate(Global.str_triangle2m) as GameObject;
			i=2;
			obj_id="tr2m";
			need_rotation=true;
			other_side=true;
			break;
		case "e1":return (1);break;
		case "eh":return(1.5f);break;
		case "e2": return(2);break;
		}
		obj_id=code[0]+obj_id;
		if (x!=null) {
			x.transform.parent=room.transform;
			switch (code[0]) {
			case 'f': 
				x.transform.localPosition=start_pos+new Vector3(i/2,1.5f,0);
				if (need_rotation) x.transform.localRotation=Quaternion.Euler(0,180,0); else x.transform.localRotation=Quaternion.Euler(0,0,0);
				x.GetComponent<Renderer>().material=wall_material;
				x.name=obj_id+code.Substring(3,3)+'i';
				if (x.GetComponent<Renderer>()) {
					switch (matcode) {
					case 1: x.GetComponent<Renderer>().material=outside_wall_material;break;
					case 2:  x.GetComponent<Renderer>().material=padik_wall_material;break;
					case 3: x.GetComponent<Renderer>().material=padik_floor_material;break;
					case 4:  x.GetComponent<Renderer>().material=wall_material;break;
					case 5:  x.GetComponent<Renderer>().material=floor_material;break;
					case 6:  x.GetComponent<Renderer>().material=potolok_material;break;
					}}
				if (y!=null) {
					y.transform.parent=x.transform;
					y.transform.localPosition=new Vector3(0,y_height-1.5f,0);
					y.transform.localRotation=Quaternion.Euler(0,0,0);
					y.name=y.name.Substring(0,4)+code.Substring(3,3)+'i';
				}
				if (other_side) {
					x=Instantiate(x,x.transform.position,x.transform.rotation) as GameObject;
					x.transform.Rotate(0,180,0);
					x.transform.parent=GetComponent<house>().out_fwd.transform;
					x.GetComponent<Renderer>().material=outside_wall_material;
					x.name=obj_id+code.Substring(3,3)+'o';
					x.GetComponent<Renderer>().material=outside_wall_material;break;
				}
				break;
			case 'b':
				x.transform.localPosition=start_pos+new Vector3(i/2,1.5f,0);
				if (need_rotation) x.transform.localRotation=Quaternion.Euler(0,0,0); else x.transform.localRotation=Quaternion.Euler(0,180,0);
				x.GetComponent<Renderer>().material=wall_material;
				x.name=obj_id+code.Substring(3,3)+'i';
				if (y!=null) {
					y.transform.parent=x.transform;
					y.transform.localPosition=new Vector3(0,y_height-1.5f,0);
					y.transform.localRotation=Quaternion.Euler(0,0,0);
					y.name=y.name.Substring(0,4)+code.Substring(3,3)+'i';
				}
				if (x.GetComponent<Renderer>()) {
					switch (matcode) {
					case 1: x.GetComponent<Renderer>().material=outside_wall_material;break;
					case 2:  x.GetComponent<Renderer>().material=padik_wall_material;break;
					case 3: x.GetComponent<Renderer>().material=padik_floor_material;break;
					case 4:  x.GetComponent<Renderer>().material=wall_material;break;
					case 5:  x.GetComponent<Renderer>().material=floor_material;break;
					case 6:  x.GetComponent<Renderer>().material=potolok_material;break;
					}}
				if (other_side) {
					x=Instantiate(x,x.transform.position,x.transform.rotation) as GameObject;
					x.transform.Rotate(0,180,0);
					x.transform.parent=GetComponent<house>().out_back.transform;
					x.GetComponent<Renderer>().material=outside_wall_material;
					x.name=obj_id+code.Substring(3,3)+'o';
					x.GetComponent<Renderer>().material=outside_wall_material;break;
				}
				break;
			case 'r':
				x.transform.localPosition=start_pos+new Vector3(0,1.5f,-i/2);
				if (!need_rotation) x.transform.localRotation=Quaternion.Euler(0,90,0); else x.transform.localRotation=Quaternion.Euler(0,-90,0);
				x.GetComponent<Renderer>().material=wall_material;
				x.name=obj_id+code.Substring(3,3)+'i';
				if (y!=null) {
					y.transform.parent=x.transform;
					y.transform.localPosition=new Vector3(0,y_height-1.5f,0);
					y.transform.localRotation=Quaternion.Euler(0,0,0);
					y.name=y.name.Substring(0,4)+code.Substring(3,3)+'i';
				}
				if (x.GetComponent<Renderer>()) {
					switch (matcode) {
					case 1: x.GetComponent<Renderer>().material=outside_wall_material;break;
					case 2:  x.GetComponent<Renderer>().material=padik_wall_material;break;
					case 3: x.GetComponent<Renderer>().material=padik_floor_material;break;
					case 4:  x.GetComponent<Renderer>().material=wall_material;break;
					case 5:  x.GetComponent<Renderer>().material=floor_material;break;
					case 6:  x.GetComponent<Renderer>().material=potolok_material;break;
					}}
				if (other_side) {
					x=Instantiate(x,x.transform.position,x.transform.rotation) as GameObject;
					x.transform.Rotate(0,180,0);
					x.transform.parent=GetComponent<house>().out_right.transform;
					x.GetComponent<Renderer>().material=outside_wall_material;
					x.name=obj_id+code.Substring(3,3)+'o';
					x.GetComponent<Renderer>().material=outside_wall_material;break;
				}
				break;
			case 'l':
				x.transform.localPosition=start_pos+new Vector3(0,1.5f,-i/2);
				if (!need_rotation) x.transform.localRotation=Quaternion.Euler(0,-90,0); else x.transform.localRotation=Quaternion.Euler(0,90,0);
				x.GetComponent<Renderer>().material=wall_material;
				x.name=obj_id+code.Substring(3,3)+'i';
				if (y!=null) {
					y.transform.parent=x.transform;
					y.transform.localPosition=new Vector3(0,y_height-1.5f,0);
					y.transform.localRotation=Quaternion.Euler(0,0,0);
					y.name=y.name.Substring(0,4)+code.Substring(3,3)+'i';
				}
				if (x.GetComponent<Renderer>()) {
					switch (matcode) {
					case 1: x.GetComponent<Renderer>().material=outside_wall_material;break;
					case 2:  x.GetComponent<Renderer>().material=padik_wall_material;break;
					case 3: x.GetComponent<Renderer>().material=padik_floor_material;break;
					case 4:  x.GetComponent<Renderer>().material=wall_material;break;
					case 5:  x.GetComponent<Renderer>().material=floor_material;break;
					case 6:  x.GetComponent<Renderer>().material=potolok_material;break;
					}}
				if (other_side) {
					x=Instantiate(x,x.transform.position,x.transform.rotation) as GameObject;
					x.transform.Rotate(0,180,0);
					x.transform.parent=GetComponent<house>().out_left.transform;
					x.GetComponent<Renderer>().material=outside_wall_material;
					x.name=obj_id+code.Substring(3,3)+'o';
					x.GetComponent<Renderer>().material=outside_wall_material;break;
				}
				break;
			}
		}
		return(i);
	}

	//---------------------------------------------------CONSTRUCT ROOM

	GameObject ConstructRoom(GameObject quarteer,float size_x,float size_y,float xpos,float ypos,string nm,string fwd,string back,string right,string left) {
		GameObject room=new GameObject(nm);
		room.transform.parent=quarteer.transform;
		room.transform.localPosition=new Vector3(xpos,0,ypos);
		room.transform.localRotation=Quaternion.Euler(0,0,0);
		bool havehalf_x=false;bool havehalf_y=false;
		if ((int)(size_x)%2!=0) havehalf_x=true;
		if ((int)(size_y)%2!=0) havehalf_y=true;
		//floor&&roof
		GameObject x=Instantiate(floor_tile,transform.position,transform.rotation) as GameObject;
		x.transform.parent=room.transform;
		x.transform.localPosition=new Vector3(size_x/2.0f,0,size_y/2.0f);
		x.transform.localScale=new Vector3(size_x,1,size_y);
		x.transform.localRotation=Quaternion.Euler(0,0,0);
		x.GetComponent<Renderer>().material=floor_material;
		x.name="dflpl001i";

		x=Instantiate(floor_tile,transform.position,transform.rotation) as GameObject;
		x.transform.parent=room.transform;
		x.transform.localPosition=new Vector3(size_x/2.0f,3,size_y/2.0f);
		x.transform.localScale=new Vector3(size_x,1,size_y);
		x.transform.localRotation=Quaternion.Euler(180,0,0);
		x.GetComponent<Renderer>().material=potolok_material;
		x.name="uflpl001i";

		float i=0; float l=0; 
		string h="";
		ushort c=0;
		//fwd_walls
			byte j=0;
		if (fwd.Length!=0) {
			while (j<fwd.Length) {
			h=c.ToString();
			if (h.Length!=3) {
				if (h.Length==1) h="00"+h;
				else h="0"+h;
			}
			l+=CreateBlock (room,new Vector3(l,0,size_y),'f'+fwd.Substring(j,2)+h,4);
					j+=2;
			c++;
			}}
		//left walls
		if (left.Length!=0) {
			l=0;
			j=0; c=0;
		while (j<left.Length) {
			h=c.ToString();
			if (h.Length!=3) {
				if (h.Length==1) h="00"+h;
				else h="0"+h;
			}
			l+=CreateBlock (room,new Vector3(0,0,size_y-l),'l'+left.Substring(j,2)+h,4);
			j+=2;
			c++;
			}}
		//right_walls
		if (right.Length!=0) {
			l=0;
			j=0;c=0;
		while (j<right.Length) {
			h=c.ToString();
			if (h.Length!=3) {
				if (h.Length==1) h="00"+h;
				else h="0"+h;
			}
			l+=CreateBlock (room,new Vector3(size_x,0,size_y-l),'r'+right.Substring(j,2)+h,4);
			j+=2;
			c++;
			}}
		//backwalls
		if (back.Length!=0) {
			l=0;
			j=0;c=0;
		while (j<back.Length) {
			h=c.ToString();
			if (h.Length!=3) {
				if (h.Length==1) h="00"+h;
				else h="0"+h;
			}
			l+=CreateBlock (room,new Vector3(l,0,0),'b'+back.Substring(j,2)+h,4);
			j+=2;
			c++;
			}}
		return(room);
	}
		
	//------------------------------------------WALLS DIVIDE

	string WallsDivide (float l, bool doubleside) { //поблочное разделение стенами и возврат шифрованной строкой
		if (l<=0) return("");
		byte x=(byte) l;
		bool have_half=false;
		string s="";
		byte i=0;
		if (l-x==0.5f) {have_half=true;}
		if (x%2==0) {
			while (i<x) {
				if (doubleside) s+="wt"; else s+="vt";i+=2;
			} 
			if (have_half) {
				if (Random.value>=0.5f) {if (doubleside) s+="ws"; else s+="vs";} else {if (doubleside) s="ws"+s; else s="vs"+s;}
			}
		}
		else {
			if (have_half) {if (doubleside) s+="wh"; else s+="vh"; i=1;} 
			while (i<x) {
				if (x-i>=2) {if (doubleside) s+="wt"; else s+="vt"; i+=2;} 
				else {if (doubleside) s+="wm"; else s+="vm";i+=1;}
			}
		}
		return (s);
	}

	string WindowsDivide (float l) { //заполнение окнами по мере возможности
		if (l<=0) return ("");
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
					s="wm";
					i=1;
					if ((x-1)%3==0) {while (i<x) {s+="o3";i+=3;}}
					else {while (i<x-1) {s+="o3";i+=3;} s+="wm";}
				}
			}
		}  
		return (s);
	}

	GameObject CreateDoor(GameObject quarteer,Vector3 pos, Vector3 rot,float width,bool doublesided) {
		GameObject x=Instantiate(Global.str_door) as GameObject;
		x.transform.parent=quarteer.transform;
		x.transform.localRotation=Quaternion.Euler(rot);
		x.GetComponent<door>().double_sided=doublesided;
		x.transform.localScale=new Vector3(width/1.5f,1,1);
		x.transform.localPosition=pos;
		return (x);
	}
		
}
