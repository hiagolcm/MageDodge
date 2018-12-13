using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustCloudController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 0.6f);
	}
}
