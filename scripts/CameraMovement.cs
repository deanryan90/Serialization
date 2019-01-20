using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System;
using System.Collections.Generic;

public class CameraMovement : MonoBehaviour {

	public Transform target;
	Vector3 reletivePosition;
	static public bool cameraSwitch = false;
	static public float speed = 10.0f;
	static public int zoomSpeed = 20;
	static public int smooth = 5;
	
	void Start () 
	{
		reletivePosition = target.transform.position - transform.position;
	}
	void Update () 
	{
		transform.position = target.transform.position - reletivePosition;
	}
	public float Speed
	{  
		get
		{
			return speed;
		}
		set 
		{
			speed = value;
		}
	}

	public int ZoomSpeed
	{
		get
		{
			return zoomSpeed;
		}
		set 
		{
			zoomSpeed = value;
		}
	}
	public int Smooth
	{
		get
		{
			return smooth;
		}
		set 
		{
			smooth = value;
		}
	}

	
	
}


