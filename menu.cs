using UnityEngine;
using System.Collections;

public class menu : MonoBehaviour {
	public GameObject player;
	public Light sun;
	public GameObject cam;
	public Material skbox;
	public float start_daytime=0;
	public float dayspeed=1;
	public bool day=true;
	public bool grow=true;
	float sun_radius=2;
	float draw_distance1=50;
	Color sunlight_clr;
	Color windows_clr;

	void Awake () {
		Global.str_door=Resources.Load<GameObject>("structures/door");
		constructor.wall_05m=Resources.Load<GameObject>("structures/wl05");
		constructor.wall_1m=Resources.Load<GameObject>("structures/wl1m");
		constructor.wall_15m=Resources.Load<GameObject>("structures/wl15");
		constructor.wall_2m=Resources.Load<GameObject>("structures/wl2m");
		constructor.floor_tile=Resources.Load<GameObject>("structures/flpl");
		constructor.wall_window_2m=Resources.Load<GameObject>("structures/ww2m");
		constructor.wall_window_3m=Resources.Load<GameObject>("structures/ww3m");
		constructor.wall_window_4m=Resources.Load<GameObject>("structures/ww4m");
		constructor.wall_door_1m=Resources.Load<GameObject>("structures/wd1m");
		constructor.wall_door_15m=Resources.Load<GameObject>("structures/wd15");
		constructor.wall_door_2m=Resources.Load<GameObject>("structures/wd2m");
		constructor.door_frame=Resources.Load<GameObject>("structures/drfr");
		constructor.window_frame=Resources.Load<GameObject>("structures/wwfr");
		constructor.ladder=Resources.Load<GameObject>("structures/stld");
		constructor.padik_door=Resources.Load<GameObject>("structures/pddr");

		constructor.potolok_material=Resources.Load<Material>("materials/room_roof_0");
		constructor.floor_material=Resources.Load<Material>("materials/floor_wood_0");
		constructor.wall_material=Resources.Load<Material>("materials/wpapers1");
		constructor.outside_wall_material=Resources.Load<Material>("materials/brick0");
		constructor.padik_wall_material=Resources.Load<Material>("materials/bg_wall_tx");
		constructor.padik_floor_material=Resources.Load<Material>("materials/concrete");
		Global.m_glass=Resources.Load<Material>("materials/glass");
		Global.m_glass_025=Resources.Load<Material>("materials/glass_0.25");
		Global.m_glass_05=Resources.Load<Material>("materials/glass_0.5");
		Global.m_glass_075=Resources.Load<Material>("materials/glass_0.75");
		Global.m_pseudoglass=Resources.Load<Material>("materials/pseudoglass");

		Global.daytime=start_daytime;
		Global.draw_distance1=draw_distance1;
	}

	void Start () {
		sunlight_clr=Color.white;
		Global.player=player;
		Global.str_triangle2m=Resources.Load<GameObject>("structures/tr2m");
		Global.str_plate53degree=Resources.Load<GameObject>("structures/pl53");
		Global.cam=cam;
	}

	void Update () {
		Global.ppos=Global.player.transform.position;
		if (!sun) return;
		if (grow) {
			Global.daytime+=Time.deltaTime*dayspeed/1440;
			if (day) {
				//skbox.SetFloat("_AtmosphereThickness",(1-daytime)*2+2);
				//skbox.SetFloat("_Exposure",(1-daytime)*1+1);
				skbox.SetFloat("_SunSize",Global.daytime*0.01f+0.04f);
			}
			}
		else {
			Global.daytime-=Time.deltaTime*dayspeed/1440;
			if (day) {
				//skbox.SetFloat("_AtmosphereThickness",daytime*1+1);
				//skbox.SetFloat("_Exposure",(1-daytime)*1+1);
				skbox.SetFloat("_SunSize",Global.daytime*0.01f+0.04f);
			}
			else {
				//float l=skbox.GetFloat("_AtmosphereThickness");
				//if (!grow) {if (l>0) {l-=dayspeed/40*Time.deltaTime;skbox.SetFloat("_AtmosphereThickness",l);}}
				//else {if (l<1) {l+=dayspeed*Time.deltaTime;skbox.SetFloat("_AtmosphereThickness",l);}}
				}
			}
		if (Global.daytime>=1) {
			if (grow) {grow=false;}
		}
		if (Global.daytime<=0) {
			if (!grow) {grow=true;if (day) {day=false;} 
			else {day=true;sun.gameObject.SetActive(true);}}
		}

		if (sun.gameObject.activeSelf) {
			if (grow) sun.transform.rotation=Quaternion.Euler(Global.daytime*90,0,0);
			else sun.transform.rotation=Quaternion.Euler((1-Global.daytime)*90+90,0,0);
			float intens=Global.daytime*2;
			sun.intensity=intens;
			if (intens<=0&&!day) {sun.gameObject.SetActive(false);}
			float x=sun_radius*Mathf.Cos(Global.daytime*90*Mathf.Deg2Rad);
			if (grow) x*=-1;
			float y=Mathf.Sqrt(sun_radius*sun_radius-x*x);
			sunlight_clr.b=0.9f+0.1f*(1-Global.daytime);
			sunlight_clr.r=0.9f+0.1f*Global.daytime;
			sun.color=sunlight_clr;
		}
		RenderSettings.ambientIntensity=Global.daytime;
		RenderSettings.ambientLight=sunlight_clr;
	}

}
