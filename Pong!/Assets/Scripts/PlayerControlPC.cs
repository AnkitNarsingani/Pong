using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlPC : MonoBehaviour {

	void Start () {
		
	}
	
	void Update ()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space))
            GetComponentInParent<PlayerColor>().OnTouchDown(Vector3.one);
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > -1.68f)
            transform.position += Vector3.left * 10 * Time.deltaTime;
        else if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < 1.95f)
            transform.position -= Vector3.left * 10 * Time.deltaTime;
    }
}
