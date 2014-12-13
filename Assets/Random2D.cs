using UnityEngine;
using System.Collections;

public class Random2D : MonoBehaviour
{

		public int RandomPointCnt = 1000;
		public GameObject sphere;
		private Vector2[] RandomPointPos;
		private int MaxWidth = 512;
		private int MaxHeight = 424;

		// Use this for initialization
		void Start ()
		{

				RandomPointPos = new Vector2[RandomPointCnt];
				for (int i=0; i<RandomPointCnt; i++) {
						RandomPointPos [i] = new Vector2 (Random.Range (0, MaxWidth), Random.Range (0, MaxHeight));
//						Instantiate (sphere, new Vector3 (RandomPointPos [i].x, RandomPointPos [i].y, 0), transform.rotation);
				}
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

	   public Vector2[] getRandomPointPos(){
		return RandomPointPos;
	   }
}
