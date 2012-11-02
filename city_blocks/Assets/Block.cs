using UnityEngine;
using System;

public class Block 
{
	private GameObject mesh;
	private int seed;
	
	public Block (int _seed)
	{
		this.seed = _seed;
		
		generateMesh();
		
		Debug.Log("Block created");
	}
	
	private void generateMesh()
	{
		this.mesh = GameObject.CreatePrimitive(PrimitiveType.Cube);
	}
}

