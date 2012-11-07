using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	
	private Block block;
	private GameObject pc;
	private GameObject root;
	
	private float CamRot = 45.0f;
	private float CamRotTarget = 45.0f;
	private bool CamTurning = false;
	private float CamRotSpeed = 50.0f;
	
	private float charSpeed = 5.0f;
	
	// Use this for initialization
	void Start () 
	{		
	
		pc = GameObject.Find ("Character");
		root = GameObject.Find ("Root");
		root.transform.rotation = Quaternion.Euler(0,CamRot,0);
		
		if (root == null || pc == null)
		{
			Debug.Log ("Missing asset");
		}
		
		block = new Block(23);
		
		// Tetra t = new Tetra();
	}
	
	// Update is called once per frame
	void Update () 
	{
		handleInput();
		
		if (CamTurning)
		{
			if (CamRot < CamRotTarget)
			{
				CamRot += CamRotSpeed * Time.deltaTime;
			}
			else
			{
				CamRot -= CamRotSpeed * Time.deltaTime;
			}
				
			if (Mathf.Abs (CamRot - CamRotTarget) < 1)
			{
				CamRot = CamRotTarget;
				CamTurning = false;
			}
			
			root.transform.rotation = Quaternion.Euler(0, CamRot, 0);
			
		}
	}
	
	private void handleInput()
	{
		Vector3 pos = pc.transform.position;
		// pos.x += 0.1f;
		
		if (CamTurning)
		{
			return;
		}
		
		if (Input.GetKey (KeyCode.Q))
		{
			CamRotTarget -= 90.0f;
			CamTurning = true;
		}
		else if (Input.GetKey(KeyCode.E))
		{
			CamRotTarget += 90.0f;
			CamTurning = true;
		}			
		
		if (Input.GetKey("up"))
		{
			pos.z += charSpeed * Time.deltaTime;
		}
		else if (Input.GetKey ("down"))
		{
			pos.z -= charSpeed * Time.deltaTime;
		}
		
		if (Input.GetKey("left"))
		{
			pos.x -= charSpeed * Time.deltaTime;
		}
		else if (Input.GetKey("right"))
		{
			pos.x += charSpeed * Time.deltaTime;
		}
		
		pos.y = block.getHeightAt(pos.x, pos.z) + 0.55f;
		
		pc.transform.position = pos;
		
	}
}
