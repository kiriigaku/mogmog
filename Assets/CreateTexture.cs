using UnityEngine;
using System.Collections;

public class CreateTexture : MonoBehaviour
{

		private Texture2D tex;
		private Color col;

		// Use this for initialization
		void Start ()
		{
				tex = new Texture2D (512, 424, TextureFormat.RGB24, false);
//				col = new Color.red;

				for (int i=0; i<512; i++) {
						for (int k=0; k<424; k++) {

//							tex.SetPixel(i, k, Color.red);
								int val = Random.Range (0, 4);
//								if (val == 0) {
//										tex.SetPixel (i, k, Color.red);
//								}
//								else if (val == 1) {
//										tex.SetPixel (i, k, Color.blue);
//								}
//								else if (val == 2) {
//										tex.SetPixel (i, k, Color.red);
//								} else {
//										tex.SetPixel (i, k, Color.black);
//								}
				if (k < 424/2) {
					tex.SetPixel (i, k, Color.red);
				}else{
					tex.SetPixel (i, k, Color.blue);
				}
			}
		}

				tex.Apply ();

				this.renderer.material.SetTextureScale ("_MainTex", new Vector2 (-1, 1));
		}
	
		// Update is called once per frame
		void Update ()
		{

				this.renderer.material.mainTexture = tex;
		
		}
}
