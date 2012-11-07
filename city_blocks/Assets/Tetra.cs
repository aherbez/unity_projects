using UnityEngine;
using System;
using System.Collections;


[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Tetra
{
	private GameObject mesh;
	private Mesh geo;
	
	public Tetra ()
	{
		this.mesh = new GameObject();
		this.mesh.AddComponent(typeof(MeshFilter));
		this.mesh.AddComponent(typeof(MeshRenderer));

		Vector3 p0 = new Vector3(0,0,0);
		Vector3 p1 = new Vector3(10,0,0);
		Vector3 p2 = new Vector3(5f, 0, Mathf.Sqrt (7.5f));
		Vector3 p3 = new Vector3(5f, Mathf.Sqrt (7.5f), Mathf.Sqrt (0.75f)/3);
					
		this.geo = new Mesh();
		this.geo.Clear();
		this.geo.vertices = new Vector3[]{p0,p1,p2,p3};
		this.geo.triangles = new int[]{
			0,1,2,
			0,2,3,
			2,1,3,
			0,3,1
		};
		
		this.geo.RecalculateNormals();
		this.geo.RecalculateBounds();
		this.geo.Optimize();
		
		// MeshFilter mf = this.mesh.gameObject.GetComponent<MeshFilter>() as MeshFilter;
		MeshFilter mf = (MeshFilter)this.mesh.gameObject.GetComponent(typeof(MeshFilter));
		MeshRenderer mr = (MeshRenderer)this.mesh.gameObject.GetComponent(typeof(MeshRenderer));
		mf.mesh = this.geo;
		mr.renderer.material.color = Color.white;
	}
	
	public Transform transform 
	{
		get {
			return this.mesh.transform;
		}
	}
}


