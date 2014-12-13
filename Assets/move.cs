using UnityEngine;
using System.Collections;

public class move : MonoBehaviour {
	private float valuex;
	private float valuey;
	private float valuez;
	private int cnt=0; 
	private Vector3 startPos;

	// Use this for initialization
	void Start () {
				valuex = Random.Range (-10.0f, 10.0f);
				valuey = Random.Range (-10.0f, 10.0f);
				valuez = Random.Range (-10.0f, 10.0f);

		startPos = transform.position;
		}
	
	// Update is called once per frame
	void Update () {
		cnt += 1;
//		transform.position += new Vector3 (valuex * 0.001f, valuey * 0.001f, valuez * 0.001f);

		if (cnt > 300) {
			transform.position -= new Vector3 (valuex * 0.001f, valuey * 0.001f, valuez * 0.001f);
		} else {
			transform.position += new Vector3 (valuex * 0.001f, valuey * 0.001f, valuez * 0.001f);
		}

		if (transform.position == startPos) {
			cnt = 0;
				}
	}
}
