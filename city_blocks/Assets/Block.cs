using UnityEngine;
using System;

public class Block 
{
	private GameObject mesh;
	private int seed;
	
	public Transform transform {
		
		get {
			return this.mesh.transform;
		}
	}
	
	public Block (int _seed)
	{
		this.seed = _seed;
		
		generateMesh();
		
		Debug.Log("Block created");
	}
	
	private void generateMesh()
	{
		this.mesh = GameObject.CreatePrimitive(PrimitiveType.Cube);
		this.mesh.transform.localScale = new Vector3(80, 10, 80);
		this.mesh.transform.Translate(new Vector3(0, -5, 0));
	}
}

