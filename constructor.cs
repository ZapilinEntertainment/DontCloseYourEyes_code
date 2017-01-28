using UnityEngine;
using System.Collections;

public static class constructor{

	public static Material potolok_material;
	public static  Material floor_material;
	public static  Material wall_material;
	public static  Material outside_wall_material;
	public static Material padik_wall_material;
	public static Material padik_floor_material;

	public static GameObject wall_window_2m;
	public static GameObject wall_window_3m;
	public static GameObject wall_window_4m;
	public static GameObject wall_05m;
	public static GameObject wall_1m;
	public static GameObject wall_15m;
	public static GameObject wall_2m;
	public static GameObject wall_door_1m;
	public static GameObject wall_door_15m;
	public static GameObject wall_door_2m;
	public static GameObject floor_tile;
	public static GameObject door_frame;
	public static GameObject window_frame;
	public static GameObject ladder;
	public static GameObject platform;
	public static GameObject floor;
	public static GameObject padik_door; //pddr

	public static GameObject CreateCourtyard_0 (string name,Vector3 pos,int xsize, int ysize, byte frs,byte open,int density) {
		//fr=first row
		int kf=1;
		bool fwd_fr=(frs&8)==0,back_fr=(frs&4)==0,right_fr=(frs&2)==0,left_fr=(frs&1)==0;
		GameObject cy=new GameObject(name);
		if (fwd_fr||(!fwd_fr&&!right_fr&&!left_fr)) {
			if (Random.value>0.5f) kf=-1; else kf=1;
			CreateHouse_ResHigh (cy,xsize,ysize/8,new Vector3(0,0,ysize/8*7),Quaternion.Euler(0,180,0),1,4,density+kf*density/3);
			if (Random.value>0.5f) kf=-1; else kf=1;
			CreateHouse_ResHigh (cy,xsize/8,ysize/2,new Vector3(0,0,ysize/4),Quaternion.Euler(0,90,0),2,2,density+kf*density/3);
			if (Random.value>0.5f) kf=-1; else kf=1;
			CreateHouse_ResHigh (cy,xsize/8,ysize/2,new Vector3(xsize*7/8,0,ysize/4),Quaternion.Euler(0,-90,0),3,2,density+kf*density/3);
			if (back_fr) {
				if (Random.value>0.5f) kf=-1; else kf=1;
				CreateHouse_ResHigh (cy,xsize,ysize/8,Vector3.zero,Quaternion.Euler(0,0,0),4,4,density+kf*density/3);
			}
			else {
				if (Random.value>0.5f) kf=-1; else kf=1;
				CreateHouse_ResHigh (cy,3*xsize/8,ysize/8,new Vector3(0,0,0),Quaternion.Euler(0,0,0),4,2,density+kf*density/3);
			}
		}
		else {
	
	}
		return (cy);
	}

	public static GameObject CreateHouse_ResHigh (GameObject courtyard,int xsize,int ysize,Vector3 pos, Quaternion rot, int number,byte padiks,int floors) {
		bool left=true;
		bool right =false;
		GameObject h=new GameObject("house");
		h.name="h"+number.ToString(); if (h.name.Length==2) h.name="h0"+number.ToString();
		if (courtyard!=null) {h.transform.parent=courtyard.transform;h.transform.localPosition=pos;h.transform.localRotation=rot;}
		int q_x=xsize/2;
		int e_x=q_x/6; if (e_x<4) e_x=4;
		q_x-=e_x/2;
		int q_y=ysize; 
		int e_y=(int)(Random.value*q_y/2+q_y/2);
		if (e_y<4) e_y=4;
		for (byte i=1;i<=padiks;i++) {
			if (i==padiks) right=true;
			constructor.CreatePadik(h,new Vector3((q_x*2+e_x)*(i-1),0,0),q_x,q_y,e_x,e_y,right,left,i,floors);
			left=false;
		}
		house hsc=h.AddComponent<house>();
		hsc.rsize_x=(q_x*2+e_x)*padiks;
		return (h);
	}

	//--------------------CREATE SMALL HOUSE
	public static GameObject CreateSmallHouse () 
	{
		GameObject myHouse=new GameObject("small house");
		return (myHouse);
	}

	//--------------CREATE PADIK
	public static GameObject CreatePadik (GameObject house,Vector3 pos, int q_x,int q_y,float e_x, float e_y,bool right_windows, bool left_windows,int number,int floors) 
	{
		if (e_y>q_y) e_y=q_y;
		if (e_y<6) e_y=6;
		if (e_x<4) e_x=4;
		int j=0;
		GameObject pad=new GameObject("padik"+number.ToString());
		pad.transform.parent=house.transform;
		pad.transform.localPosition=pos;
		pad.transform.localRotation=Quaternion.Euler(0,0,0);
		CreateWall (pad,q_x,new Vector3(0,-1.5f,q_y),'f',false,true,1); //left forward wall
		CreateWall (pad,q_x,new Vector3(q_x+e_x,-1.5f,q_y),'f',false,true,1); //right forward wall
		CreateWall (pad,2*q_x+e_x,new Vector3(0,-1.5f,0),'b',false,true,1); //backwall

		if (right_windows) {
			CreateWall (pad,q_y,new Vector3(e_x+2*q_x,-1.5f,q_y),'r',false,true,1); //right outside wall
		}
		CreateWall (pad,q_y-e_y,new Vector3(q_x,-1.5f,q_y),'r',false,true,1); //left inpassage wall
		if (left_windows) {
			CreateWall (pad,q_y,new Vector3(0,-1.5f,q_y),'l',false,true,1); //left outside wall
		}
		CreateWall (pad,q_y-e_y,new Vector3(q_x+e_x,-1.5f,q_y),'l',false,true,1); //right inpassage wall

		//entrance
		GameObject p=new GameObject("p00");
		p.transform.parent=pad.transform;
		p.transform.localRotation=Quaternion.Euler(0,0,0);
		float ply=(e_y-2)/2.0f;
		p.transform.localPosition=new Vector3(q_x,0,ply+2);
		GameObject q=GameObject.Instantiate(ladder) as GameObject;
		q.transform.parent=p.transform;
		q.transform.localPosition=new Vector3(1,0.75f,-1);
		q.transform.localRotation=Quaternion.Euler(0,0,0);
		CreateWall (p,ply,new Vector3(0,0,ply),'l',false,false,2);
		CreateWall (p,ply,new Vector3(e_x,0,ply),'r',false,false,2);

		q=GameObject.Instantiate(padik_door) as GameObject;
		q.name="fpddr000o";
		q.transform.parent=p.transform;
		q.transform.localPosition=new Vector3(e_x/2,1.5f,ply);
		q.transform.localRotation=Quaternion.Euler(0,0,0);
		if (e_x>3) {
			CreateWall(p,e_x/2-1.5f,new Vector3(0,0,ply),'f',true,false,2);
			CreateWall(p,e_x/2-1.5f,new Vector3(e_x/2+1.5f,0,ply),'f',true,false,2);
		}

		q=GameObject.Instantiate(Global.str_plate53degree) as GameObject;
		q.transform.parent=p.transform;
		q.transform.localPosition=new Vector3(e_x/2+1,0.75f,-1);
		q.transform.localRotation=Quaternion.Euler(53,180,0);
		q.transform.localScale=new Vector3(e_x-2,2.5f,1);
		q=GameObject.Instantiate(Global.str_triangle2m) as GameObject;
		q.transform.parent=p.transform;
		q.transform.localPosition=new Vector3(e_x,1.5f,-1);
		q.transform.localRotation=Quaternion.Euler(0,-90,0);
		q.GetComponent<Renderer>().material=padik_wall_material;
		q=GameObject.Instantiate(floor_tile) as GameObject;
		q.transform.parent=p.transform;
		q.transform.localPosition=new Vector3(e_x/2,0,ply/2);
		q.transform.localRotation=Quaternion.Euler(0,0,0);
		q.transform.localScale=new Vector3(e_x,1,ply);
		q.GetComponent<Renderer>().material=padik_floor_material;
		p.transform.localRotation=Quaternion.Euler(0,0,0);

		string s;
		int floor_number=1;
		while (floor_number<floors) {
			s=floor_number.ToString();
			if (s.Length==1) s="p0"+s; else s='p'+s;		
			CreatePlatform(floor_number,pad,new Vector3(q_x,floor_number*3,ply+2),e_x,ply);
			CreateFloor(pad,floor_number,e_x,ply,q_x,q_y,right_windows,left_windows);
			floor_number++;
			// да сколько можно виснуть на вайлах!!!!
		}
		CreateFloor(pad,floor_number,e_x,ply,q_x,q_y,right_windows,left_windows);
		padik pdo=pad.AddComponent<padik>();
		pdo.q_x=q_x; pdo.q_y=q_y;pdo.e_x=e_x;
		pdo.mySubsector=number;
		if (house.GetComponent<house>()!=null) {
			house h=house.GetComponent<house>();
		pad.BroadcastMessage("FloorOptimization",(int)(h.ppos.y/h.floor_height+0.5f),SendMessageOptions.DontRequireReceiver);
		}
			return (pad);
	}

	//----------------------------CREATE FLOOR----------------------
	static GameObject CreateFloor (GameObject padik,int floor_number, float hall_x,float hall_y, int q_x,int q_y, bool right_windows,bool left_windows) {
		//first floor
		GameObject f=new GameObject();
		f.name=floor_number.ToString();
		if (f.name.Length==1) f.name="f0"+f.name; else f.name="f"+f.name;
		f.transform.parent=padik.transform;
		f.transform.localPosition=new Vector3(0,floor_number*3-1.5f,0);
		f.transform.localRotation=Quaternion.Euler(0,0,0);
		CreateDoor(f,new Vector3(q_x,1.2f,0.25f),new Vector3(0,-90,0),1.5f,false);
		CreateDoor(f,new Vector3(q_x+hall_x,1.2f,0.25f),new Vector3(0,-90,0),1.5f,false);
		Quaternion rotation=Quaternion.identity;
		GameObject q=CreateQuarteer_0(f,q_x,q_y,new Vector3(q_x+hall_x,0,0),rotation,hall_y*2+2,true,true,right_windows,false,true,false,false); 
		q.name="qr";
		q=CreateQuarteer_0(f,q_x,q_y,new Vector3(0,0,0),rotation,hall_y*2+2,true,true,left_windows,false,true,true,false);
		q.transform.parent=f.transform;
		q.name="ql";

		GameObject p=new GameObject("hall");
		p.transform.parent=f.transform;
		p.transform.localPosition=new Vector3(q_x,0,0);
		p.transform.localRotation=Quaternion.Euler(0,0,0);
		q=GameObject.Instantiate(floor_tile) as GameObject ;
		q.transform.parent=p.transform;
		q.transform.localPosition=new Vector3(hall_x/2,0,hall_y/2);
		q.transform.localRotation=Quaternion.Euler(0,0,0);
		q.transform.localScale=new Vector3(hall_x,1,hall_y);
		q.GetComponent<Renderer>().material=padik_floor_material;
		q=GameObject.Instantiate(q,q.transform.position,q.transform.rotation) as GameObject ;
		q.transform.parent=p.transform;
		q.transform.Translate(0,-0.1f,0);
		q.transform.Rotate(180,0,0);
		q.GetComponent<Renderer>().material=padik_floor_material;
		q=GameObject.Instantiate(wall_2m) as GameObject;
		q.transform.parent=p.transform;
		q.transform.localPosition=new Vector3(hall_x/2,-0.05f,hall_y);
		q.transform.localRotation=Quaternion.Euler(0,180,0);
		q.transform.localScale=new Vector3(hall_x,0.1f,1);
		q.GetComponent<Renderer>().material=padik_floor_material;
		CreateWall(p,hall_y-1.5f,new Vector3(0,0,hall_y),'l',false,false,2);
		CreateBlock(p,new Vector3(0,0,1.5f),"lrh000",2);
		CreateWall(p,hall_y-1.5f,new Vector3(hall_x,0,hall_y),'r',false,false,2);
		CreateBlock(p,new Vector3(hall_x,0,1.5f),"rrh000",2);
		CreateWindows(p,hall_x,Vector3.zero,'b',2);
		return(f);
	}

	//---------------------CREATE PLATFORM-----------------------
	static GameObject CreatePlatform(int floor,GameObject padik, Vector3 pos,float width,float length) {
		GameObject p=new GameObject('p'+floor.ToString());
		if (p.name.Length==2) p.name="p0"+floor.ToString();
		p.transform.parent=padik.transform;
		p.transform.localPosition=pos;
		p.transform.localRotation=Quaternion.Euler(0,0,0);
		GameObject q=GameObject.Instantiate(ladder) as GameObject;
		q.transform.parent=p.transform;
		q.transform.localPosition=new Vector3(width-1,-0.75f,-1);
		q.transform.localRotation=Quaternion.Euler(0,180,0);
		q=GameObject.Instantiate(floor_tile) as GameObject;
		q.transform.parent=p.transform;
		q.transform.localPosition=new Vector3(width/2,0,length/2);
		q.transform.localRotation=Quaternion.Euler(0,0,0);
		q.transform.localScale=new Vector3(width,1,length);
		q.GetComponent<Renderer>().material=padik_floor_material;
		q=GameObject.Instantiate(q,q.transform.position,q.transform.rotation) as GameObject;
		q.transform.parent=p.transform;
		q.transform.Translate(0,-0.1f,0);
		q.transform.Rotate(180,0,0);
		q.GetComponent<Renderer>().material=padik_floor_material;
		q=GameObject.Instantiate(ladder) as GameObject;
		q.transform.parent=p.transform;
		q.transform.localPosition=new Vector3(1,0.75f,-1);
		q.transform.localRotation=Quaternion.Euler(0,0,0);
		CreateWall(p,length,new Vector3(0,0,length),'l',false,false,2);
		CreateWall(p,length,new Vector3(width,0,length),'r',false,false,2);
		CreateWindows(p,width,new Vector3(0,0,length),'f',2);
		q=GameObject.Instantiate(wall_2m) as GameObject;
		q.transform.parent=p.transform;
		q.transform.localPosition=new Vector3(width/2,-0.05f,0);
		q.transform.localRotation=Quaternion.Euler(0,0,0);
		q.transform.localScale=new Vector3(width,0.1f,1);
		q.GetComponent<Renderer>().material=padik_floor_material;
		return(p);
	}

	//если сделать несколько разных алгоритмов сборки, то стоит воспользоваться делегатами
	//и выделить процесс расчета комнат в отдельные функции
	static GameObject CreateQuarteer_0(GameObject floor,int xsize, int ysize,Vector3 pos,Quaternion rot, float e_y, bool fwd_open,bool back_open,bool right_open,bool left_open,bool left_exit,bool mirror_x,bool mirror_y) {
		GameObject quarteer=new GameObject();
		quarteer.transform.parent=floor.transform;
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


		//сборка комнат
		byte i=0; 
		float z=0;
		float dvz=ysize-e_y;		if (dvz<0) dvz=0;	//ширина внутреннего дворика
		string fwd=""; string back="";string right="";string left="";
		string center_right=""; string center_left="";

		//kitchen
		if (fwd_open) fwd=WindowsDivide(kitchen_x); 
						else fwd=WallsDivide(kitchen_x,false);
		if (dvz>=kitchen_y&&dvz>=2) 
		{
			left=WindowsDivide(kitchen_y);
			dvz-=kitchen_y;
		}
		else 
		{
			left=WindowsDivide(dvz)+WallsDivide(kitchen_y-dvz,false);
			dvz=0;
		}
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
			 ConstructRoom(quarteer,kitchen_x,kitchen_y,0,ysize-kitchen_y,"kitc",fwd,back,left,right);
				CreateDoor(quarteer,new Vector3(kitchen_x,1.2f,ysize-kitchen_y+z/2.0f-0.5f),new Vector3(0,-90,0),z,true);
				center_left=right; 
		}
		fwd="";back="";right="";

		//toilet&bath
		if (dvz>=san_y&&dvz>=2) {
			left=WindowsDivide(san_y);dvz-=san_y;
		}
		else {
			left=WindowsDivide(dvz)+WallsDivide(san_y-dvz,false);
			dvz=0;
		}
			back=""; //инверсия back и fwd для более удобного построения passage
			if (kitchen_x>=3) {z=1.5f;back="rh";}	else {z=1;back="rm";}
			fwd=WallsDivide(kitchen_x/2.0f-z,false);
		if (mirror_x) {
			ConstructRoom(quarteer,kitchen_x/2.0f,san_y,xsize-kitchen_x/2.0f,passage_y,"bath",WallsDivide(kitchen_x/2.0f,false),back+fwd,left,WallsDivide(san_y,false));
			ConstructRoom(quarteer,kitchen_x/2.0f,san_y,xsize-kitchen_x,passage_y,"toil",WallsDivide(kitchen_x/2.0f,false),fwd+back,WallsDivide(san_y,false),WallsDivide(san_y,false));
			CreateDoor(quarteer,new Vector3(xsize-kitchen_x/2.0f-z/2.0f-0.5f*(z/1.5f),1.2f,passage_y),new Vector3(0,0,0),z,false);
			CreateDoor(quarteer,new Vector3(xsize-kitchen_x/2.0f+z/2.0f-0.5f*(z/1.5f),1.2f,passage_y),new Vector3(0,0,0),z,false);
		}
		else {
			ConstructRoom(quarteer,kitchen_x/2.0f,san_y,0,passage_y,"bath",WallsDivide(kitchen_x/2.0f,false),fwd+back,WallsDivide(san_y,left_open),left);
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
			if (dvz>=passage_y-1.5f&&dvz>=2) {
				back=WallsDivide(passage_y-1.5f,true);
			}	
			else {
				back=WallsDivide(dvz,true)+WallsDivide(passage_y-1.5f-dvz,false);
			}
			dvz=0;
			//временный расчет для левой стены
		i=0;
		left=back+"rh";
			if (back_open) back=WindowsDivide(kitchen_x); else back=WallsDivide(kitchen_x,false);
		}
		else {
			//выход снизу
			left=WallsDivide(passage_y,false);
			back="rh"; i=0;
			back+=WallsDivide(kitchen_x-1.5f,false);
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

	static float CreateBlock(GameObject room,Vector3 start_pos,string code,int matcode) {
		bool need_rotation=false;
		bool other_side=false;
		GameObject x=null;
		GameObject y=null; //дополнительные детали
		float y_height=1.5f;
		string obj_id=""; //кодовый идентификатор типа блока
		float i=0; //длина блока 
		float z=0; //длина подвижной части
		switch (code.Substring(1,2)) {
		case "ws":x=GameObject.Instantiate(wall_05m) as GameObject;i=0.5f;need_rotation=true;other_side=true;obj_id="wl05";break;
		case "vs":x=GameObject.Instantiate(wall_05m) as GameObject;i=0.5f;need_rotation=true;obj_id="wl05";break;
		case "wm":x=GameObject.Instantiate(wall_1m) as GameObject;i=1;need_rotation=true;other_side=true;obj_id="wl1m";break;
		case "vm":x=GameObject.Instantiate(wall_1m) as GameObject;i=1;need_rotation=true;obj_id="wl1m";break;
		case "wh":x=GameObject.Instantiate(wall_15m) as GameObject;i=1.5f;need_rotation=true;other_side=true;obj_id="wl15";break;
		case "vh":x=GameObject.Instantiate(wall_15m) as GameObject;i=1.5f;need_rotation=true;obj_id="wl15";break;
		case "wt":x=GameObject.Instantiate(wall_2m) as GameObject;i=2;other_side=true;obj_id="wl2m";break;
		case "vt":x=GameObject.Instantiate(wall_2m) as GameObject;i=2;obj_id="wl2m";break;
		case "o2":
			x=GameObject.Instantiate(wall_window_2m) as GameObject;
			i=2;
			y=GameObject.Instantiate(window_frame) as GameObject;
			y_height=1.725f;
			other_side=true;
			obj_id="ww2m";
			need_rotation=true;
			break;	
		case "o3":
			x=GameObject.Instantiate(wall_window_3m) as GameObject;
			i=3;
			need_rotation=true;
			y=GameObject.Instantiate(window_frame) as GameObject;
			y_height=1.725f;
			y.transform.localScale=new Vector3(1.6f,1,1);
			other_side=true;
			obj_id="ww3m";
			break;
		case "o4":
			x=GameObject.Instantiate(wall_window_4m) as GameObject;
			i=4;
			need_rotation=true;
			y=GameObject.Instantiate(window_frame) as GameObject;
			y_height=1.725f;
			y.transform.localScale=new Vector3(2,1,1);
			other_side=true;
			obj_id="ww4m";
			break;
		case "dm":
			x=GameObject.Instantiate(wall_door_1m) as GameObject;
			i=1;
			y=GameObject.Instantiate(door_frame) as GameObject;
			y_height=1.25f;
			y.transform.localScale=new Vector3(0.8f,1,1);
			obj_id="wd1m";
			other_side=true;
			break;
		case "dh":
			x=GameObject.Instantiate(wall_door_15m) as GameObject;
			i=1.5f;
			need_rotation=true;
			y=GameObject.Instantiate(door_frame) as GameObject;
			y_height=1.25f;
			y.transform.localScale=new Vector3(1.2f,1,1);
			obj_id="wd15";
			other_side=true;
			break;
		case "ds":
			x=GameObject.Instantiate(wall_door_2m) as GameObject;
			i=2;
			y=GameObject.Instantiate(door_frame) as GameObject;
			y_height=1.25f;
			y.transform.localScale=new Vector3(1.2f,1,1);
			obj_id="wd2m";
			other_side=true;
			need_rotation=true;
			break;
		case "rm":
			x=GameObject.Instantiate(wall_door_1m) as GameObject;
			i=1;
			y=GameObject.Instantiate(door_frame) as GameObject;
			y_height=1.25f;
			y.transform.localScale=new Vector3(0.8f,1,1);
			obj_id="wd1m";
			need_rotation=true;
			break;
		case "rh":
			x=GameObject.Instantiate(wall_door_15m) as GameObject;
			i=1.5f;
			need_rotation=true;
			y=GameObject.Instantiate(door_frame) as GameObject;
			y_height=1.25f;
			y.transform.localScale=new Vector3(1.2f,1,1);
			obj_id="wd15";
			break;
		case "rs":
			x=GameObject.Instantiate(wall_door_2m) as GameObject;
			i=2;
			y=GameObject.Instantiate(door_frame) as GameObject;
			y_height=1.25f;
			y.transform.localScale=new Vector3(1.2f,1,1);
			obj_id="wd2m";
			need_rotation=true;
			break;
		case "ts":
			x=GameObject.Instantiate(Global.str_triangle2m) as GameObject;
			i=2;
			obj_id="tr2m";
			need_rotation=true;
			break;
		case "to":
			x=GameObject.Instantiate(Global.str_triangle2m) as GameObject;
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
					x=GameObject.Instantiate(x,x.transform.position,x.transform.rotation) as GameObject;
					x.transform.Rotate(0,180,0);
					x.transform.parent=room.transform;
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
					x=GameObject.Instantiate(x,x.transform.position,x.transform.rotation) as GameObject;
					x.transform.Rotate(0,180,0);
					x.transform.parent=room.transform;
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
					x=GameObject.Instantiate(x,x.transform.position,x.transform.rotation) as GameObject;
					x.transform.Rotate(0,180,0);
					x.transform.parent=room.transform;
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
					x=GameObject.Instantiate(x,x.transform.position,x.transform.rotation) as GameObject;
					x.transform.Rotate(0,180,0);
					x.transform.parent=room.transform;
					x.GetComponent<Renderer>().material=outside_wall_material;
					x.name=obj_id+code.Substring(3,3)+'o';
					x.GetComponent<Renderer>().material=outside_wall_material;break;
				}
				break;
			}
		}
		if (code.Length==7) x.name=x.name.Substring(0,8)+code[6];
		return(i);
	}

	//---------------------------------------------------CONSTRUCT ROOM

	static GameObject ConstructRoom(GameObject quarteer,float size_x,float size_y,float xpos,float ypos,string nm,string fwd,string back,string right,string left) {
		GameObject room=new GameObject(nm);
		AddObject(quarteer,room,new Vector3(xpos,0,ypos));
		bool havehalf_x=false;bool havehalf_y=false;
		if ((int)(size_x)%2!=0) havehalf_x=true;
		if ((int)(size_y)%2!=0) havehalf_y=true;
		//floor&&roof
		GameObject x=GameObject.Instantiate(floor_tile) as GameObject;
		x.transform.parent=room.transform;
		x.transform.localPosition=new Vector3(size_x/2.0f,0,size_y/2.0f);
		x.transform.localScale=new Vector3(size_x,1,size_y);
		x.transform.localRotation=Quaternion.Euler(0,0,0);
		x.GetComponent<Renderer>().material=floor_material;
		x.name="dflpl001i";

		x=GameObject.Instantiate(floor_tile) as GameObject;
		x.transform.parent=room.transform;
		x.transform.localPosition=new Vector3(size_x/2.0f,2.9f,size_y/2.0f);
		x.transform.localScale=new Vector3(size_x,1,size_y);
		x.transform.localRotation=Quaternion.Euler(180,0,0);
		x.GetComponent<Renderer>().material=potolok_material;
		x.name="uflpl001i";

		//автозаполнение
		if (fwd=="sfwl") fwd=WallsDivide(size_x,true);
		if (fwd=="sfws") fwd=WindowsDivide(size_x);
		if (back=="sfwl") back=WallsDivide(size_x,true);
		if (back=="sfws") back=WindowsDivide(size_x);
		if (right=="sfwl") right=WallsDivide(size_y,true);
		if (right=="sfws") right=WindowsDivide(size_y);
		if (left=="sfwl") left=WallsDivide(size_y,true);
		if (left=="sfws") left=WindowsDivide(size_y);

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

	static string WallsDivide (float l, bool doubleside) { //поблочное разделение стенами и возврат шифрованной строкой
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

	//--------------------------------------WINDOWS DIVIDE

	static string WindowsDivide (float l) { //заполнение окнами по мере возможности
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

	static void CreateWall (GameObject parent, float l,Vector3 startpos,char side,bool doubleside,bool outside,int matcode) {
		//outside - внешняя односторонняя стена
		string s=WallsDivide(l,doubleside);
		if (s.Length<=0) return;
		int j=0; float i=0;
		Vector3 mv_vector=Vector3.zero;
		switch (side) {
		case 'f': mv_vector=Vector3.right;if (outside) side='b';break;
		case 'b': mv_vector=Vector3.right;if (outside) side='f';break;
		case 'r': mv_vector=Vector3.back;if (outside) side='l';break;
		case 'l':mv_vector=Vector3.back;if (outside) side='r';break;
		}
		while (j<s.Length) {
			if (outside) i+=CreateBlock(parent,startpos+mv_vector*i,side+s.Substring(j,2)+"000o",matcode);
			else i+=CreateBlock(parent,startpos+mv_vector*i,side+s.Substring(j,2)+"000",matcode);
			j+=2;
		}
	}

	static void CreateWindows (GameObject parent, float l,Vector3 startpos,char side,int matcode) {
		//outside - внешняя односторонняя стена
		string s=WindowsDivide(l);
		if (s.Length<=0) return;
		int j=0; float i=0;
		Vector3 mv_vector=Vector3.zero;
		switch (side) {
		case 'f': mv_vector=Vector3.right;break;
		case 'b': mv_vector=Vector3.right;break;
		case 'r': mv_vector=Vector3.back;break;
		case 'l':mv_vector=Vector3.back;break;
		}
		while (j<s.Length) {
			i+=CreateBlock(parent,startpos+mv_vector*i,side+s.Substring(j,2)+"000",matcode);
			j+=2;
		}
	}

	static GameObject CreateDoor(GameObject quarteer,Vector3 pos, Vector3 rot,float width,bool doublesided) {
		GameObject x=GameObject.Instantiate(Global.str_door) as GameObject;
		x.transform.parent=quarteer.transform;
		x.transform.localRotation=Quaternion.Euler(rot);
		x.GetComponent<door>().double_sided=doublesided;
		x.transform.localScale=new Vector3(width/1.5f,1,1);
		x.transform.localPosition=pos;
		return (x);
	}

	static void AddObject (GameObject parent,GameObject child,Vector3 relative_pos,Vector3 relative_rotation) 
	{
		child.transform.parent=parent.transform;
		child.transform.localPosition=relative_pos;
		child.transform.localRotation=Quaternion.Euler(relative_rotation);
	}
	static void AddObject (GameObject parent, GameObject child,Vector3 relative_pos) 
	{
		child.transform.parent=parent.transform;
		child.transform.localPosition=relative_pos;
		child.transform.localRotation=Quaternion.Euler(Vector3.zero);
	}
	static void AddObject (GameObject parent, GameObject child) 
	{
		child.transform.parent=parent.transform;
		child.transform.localPosition=Vector3.zero;
		child.transform.localRotation=Quaternion.Euler(Vector3.zero);
	}



		
}
