using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class Block 
{
	private GameObject mesh;
	private Mesh geo;
	private int seed;
	
	private float[,] heights;
	private int samples;
	private float width;
	private float cellSize;
	
	private Vector3[] verts;
	private int[] faces;
	
	public Transform transform {
		
		get {
			return this.mesh.transform;
		}
	}
	
	public Block (int _seed)
	{
		this.seed = _seed;
		this.samples = 11;
		this.width = 100.0f;
		
		
		this.heights = new float[this.samples,this.samples];
		this.mesh = new GameObject();
		this.mesh.AddComponent(typeof(MeshFilter));
		this.mesh.AddComponent(typeof(MeshRenderer));

		this.geo = new Mesh();
		
		// Mesh m = this.mesh.gameObject.GetComponent<MeshFilter>().mesh;
		
		this.verts = new Vector3[this.samples*this.samples + 1 + 4];
		this.geo.vertices = this.verts;
		
		this.faces = new int[(this.samples-1)*(this.samples-1)*2*3 + (4*this.samples*3)];
		this.geo.triangles = this.faces;
		
		MeshFilter mf = (MeshFilter)this.mesh.gameObject.GetComponent(typeof(MeshFilter));
		MeshRenderer mr = (MeshRenderer)this.mesh.gameObject.GetComponent(typeof(MeshRenderer));
		mf.mesh = this.geo;
		mr.renderer.material.color = Color.green;
	
		generateMesh();	
		makeTrees();
	}

	public float getHeightAt(float x, float z)
	{
		int i = (int)Math.Floor((x + this.width/2)/ this.cellSize);
		int j = (int)Math.Floor((z + this.width/2)/ this.cellSize);
		
		float u = ((x + this.width/2) - i*this.cellSize) / this.cellSize;
		float v = ((z + this.width/2) - j*this.cellSize) / this.cellSize;
		
		float yPos = 0;
		
		yPos += (1-u) * (1-v) * this.heights[i,j];
		yPos += u * (1-v) * this.heights[i+1,j];
		yPos += (1-u) * v * this.heights[i,j+1];
		yPos += u * v * this.heights[i+1,j+1];
		
		return yPos;
	}
	
	private void generateMesh()
	{		
		this.cellSize = this.width/(this.samples-1);
		
		Mesh m = this.mesh.gameObject.GetComponent<MeshFilter>().mesh;
		m.Clear();
		
		Debug.Log ("verts/faces:");
		Debug.Log (this.verts.Length);
		Debug.Log (this.faces.Length);
		
		int face = 0;
		Debug.Log (cellSize);
		for (int i=0; i<this.samples; i++)
		{
			for (int j=0; j<this.samples; j++)
			{
				this.heights[i,j] = UnityEngine.Random.Range(0.0f,5.0f);
				this.verts[j*this.samples+i] = new Vector3(i*this.cellSize - (this.width/2), this.heights[i,j], j*this.cellSize - (this.width/2));				
			}
		}
		
		// add four bottom verts
		int start = this.samples * this.samples;
		this.verts[start] = new Vector3(-this.width/2, -10, -this.width/2);
		this.verts[start+1] = new Vector3(this.width/2, -10, -this.width/2);
		this.verts[start+2] = new Vector3(this.width/2, -10, this.width/2);
		this.verts[start+3] = new Vector3(-this.width/2, -10, this.width/2);
		
		for (int i=0; i<this.samples-1; i++)
		{
			for (int j=0; j<this.samples-1; j++)
			{				
				face = j*(this.samples-1) + i;
				
				// add triangle 1
				this.faces[face*2*3] = j*this.samples+i;
				this.faces[face*2*3+2] = j*this.samples+i+1;
				this.faces[face*2*3+1] = (j+1)*this.samples+i;

				// add triangle 2
				this.faces[face*2*3+3] = j*this.samples+i+1;

				this.faces[face*2*3+5] = ((j+1)*this.samples)+i+1;
				this.faces[face*2*3+4] = ((j+1)*this.samples)+i;				
				
			}
		}	

		face = (this.samples-1) * (this.samples-1) * 6;
		
		int A = start;
		int B = start+1;
		int C = start+2;
		int D = start+3;
		
		bool firstHalf;
		
		for (int p=0; p < this.samples-1; p++)
		{
			if (p < this.samples/2) { firstHalf = true; } else { firstHalf = false; }
			
			this.faces[face++] = p;
			this.faces[face++] = p+1;
						
			if (firstHalf)
			{
				this.faces[face++] = A;
			}
			else
			{
				this.faces[face++] = B;
			}
			
			this.faces[face++] = p*this.samples + (this.samples-1);
			this.faces[face++] = (p+1)*this.samples + (this.samples-1);
			if (firstHalf)
			{
				this.faces[face++] = B;
			}
			else
			{
				this.faces[face++] = C;
			}
			
			this.faces[face++] = this.samples * (this.samples - 1) + (this.samples-(p+1));
			this.faces[face++] = this.samples * (this.samples - 1) + (this.samples-(p+2));
			if (firstHalf)
			{
				this.faces[face++] = C;
			}
			else
			{
				this.faces[face++] = D;
			}
			
			this.faces[face++] = (this.samples-(p+1))*this.samples;
			this.faces[face++] = (this.samples-(p+2))*this.samples;
			if (firstHalf)
			{
				this.faces[face++] = D;
			}
			else
			{
				this.faces[face++] = A;
			}
			
		}
		
		
		this.faces[face++] = A;
		this.faces[face++] = getVertIndex((int)this.samples/2, 0);
		this.faces[face++] = B;

		this.faces[face++] = C;
		this.faces[face++] = getVertIndex((int)this.samples/2, this.samples-1);
		this.faces[face++] = D;
		
		this.faces[face++] = B;
		this.faces[face++] = getVertIndex(this.samples-1, (int)this.samples/2);
		this.faces[face++] = C;
		
		this.faces[face++] = D;
		this.faces[face++] = getVertIndex(0, (int)this.samples/2);
		this.faces[face++] = A;
		
		/*
		face = (this.samples-1) * (this.samples-1) * 6;
		this.faces[face] = start;
		this.faces[face+2] = start+1;
		this.faces[face+1] = start+2;
		*/
		
		m.vertices = this.verts;
		m.triangles = this.faces;
		
		Debug.Log (face);
		Debug.Log (this.geo.triangles);
		
		this.geo.RecalculateBounds();		
		// this.geo.RecalculateNormals();
		this.geo.Optimize();
		
	}
	
	private void makeTrees()
	{
		
	}
	
	private int getVertIndex(int i, int j)
	{
		return (j * this.samples) + i;
	}
}

