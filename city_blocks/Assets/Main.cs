using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	
	private Block block;
	private GameObject pc;
	private GameObject root;
	
	private float CamRot = 45.0f;
	private float CamRotTarget = 45.0f;
	private bool CamTurning = false;
	
	// Use this for initialization
	void Start () 
	{		
		block = new Block(23);
	
		pc = GameObject.Find ("Character");
		root = GameObject.Find ("Root");
		root.transform.rotation = Quaternion.Euler(0,CamRot,0);
		
		if (root == null || pc == null)
		{
			Debug.Log ("Missing asset");
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		handleInput();
	}
	
	private void handleInput()
	{
		Vector3 pos = pc.transform.position;
		// pos.x += 0.1f;
		if (Input.GetKey("up"))
		{
			pos.z += 0.1f;
		}
		else if (Input.GetKey ("down"))
		{
			pos.z -= 0.1f;
		}
		
		if (Input.GetKey("left"))
		{
			pos.x -= 0.1f;
		}
		else if (Input.GetKey("right"))
		{
			pos.x += 0.1f;
		}
		
		
		pc.transform.position = pos;
		
	}
}
