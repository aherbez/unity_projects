using System;
using UnityEngine;

public class Tree
{
	private GameObject root;
	private GameObject _obj;
	private float _height;
	
	public Tree (float height)
	{
		this.root = new GameObject("Tree");
		this._obj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		this._obj.transform.localScale = new Vector3(1f, height, 1f);
		this._obj.transform.position = new Vector3(0f, height/2, 0f);
		
		this._obj.transform.parent = this.root.transform;
	}
	
	public Transform transform {
		get {
			return this.root.transform;
		}		
	}
	
	public float height {
		get {
			return this._height;
		}
		
		set {
			this._height = value;
		}
	}
	
	public float chop()
	{
		this._obj.transform.localScale = new Vector3(1f, 1f, 1f);
		this._obj.transform.position = new Vector3(0f, 0.5f, 0f);
		this._obj.transform.parent = this.root.transform;
		return this._height;
	}
}