using UnityEngine;
using System.Collections;

public class CharacterCtrl : MonoBehaviour {
	public float _speed;
	private Vector3 move;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void MoveDirection(Vector2 dirc)
	{
		move = new Vector3(dirc.x, dirc.y, 0);
		this.transform.position += move * _speed;
		
		print(dirc+"asdfasdf");
	}
}
