using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour {



	public float speed;
	private float x;
	public float PontoDeDestino;
	public float PontoOriginal;





	void Start () {

		PontoDeDestino *= transform.parent.parent.localScale.x;
		PontoOriginal *= transform.parent.parent.localScale.x;
	}
	
	void FixedUpdate () {

		float _moveDirection = Input.GetAxisRaw("Horizontal");
		x = transform.position.x;
		x += speed * Time.deltaTime * _moveDirection;
		transform.position = new Vector3 (x, transform.position.y, transform.position.z);


		if (x <= PontoDeDestino + Player.Instance.transform.position.x){
			x = PontoOriginal + Player.Instance.transform.position.x;
			transform.position = new Vector3 (x, transform.position.y, transform.position.z);
		}
		else if (x >= PontoOriginal + Player.Instance.transform.position.x)
        {
			x = PontoDeDestino + Player.Instance.transform.position.x;
			transform.position = new Vector3(x, transform.position.y, transform.position.z);
		}

	}
}
