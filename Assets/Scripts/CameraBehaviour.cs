using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    public Transform parent;
	
	void Update () {
        transform.position = new Vector3(parent.position.x, parent.position.y, transform.position.z);
	}
}
