using System;
using UnityEngine;

public class Tree
{
	private GameObject _obj;
	private float _height;
	
	
	public Tree (float height)
	{
		this._obj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		this._obj.transform.localScale = new Vector3(0.5f, height, 0.5f);
		
	}
	
	public Transform transform {
		get {
			return this._obj.transform;
		}
	}
	
	public float chop()
	{
		
		this._obj.transform.localScale = new Vector3(0.5f, 1f, 0.5f);
		return this._height;
	}
}

