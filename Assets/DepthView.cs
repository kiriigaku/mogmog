using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class DepthView : MonoBehaviour
{

		MeshFilter meshFilter;
		Triangulator tr;
		public GameObject DM;
		public GameObject sphere;
		public GameObject Random2D;
		public int depthReng = 1000;
		public GameObject depthview;
		private DepthSourceManager _DM;
		private KinectSensor _Sensor;
		private CoordinateMapper _Mapper;
		private Vector3[] SensorPosition;
		private int y = 0;
		private int x = 0;
		private ushort max = 0;
		private ushort min = 0;
		private Vector2[] pos;
		private int machPointCnt = 0;
		private Texture2D tex;
		private Color[] col;
		private bool create = false;
		FrameDescription depthFrameDesc;
		byte[] depthBitmapBuffer;
		// Use this for initialization
		void Start ()
		{
				depthFrameDesc = KinectSensor.GetDefault ().DepthFrameSource.FrameDescription;
				depthBitmapBuffer = new byte[depthFrameDesc.LengthInPixels * 3];

				Debug.Log (depthFrameDesc.Width);
				Debug.Log (depthFrameDesc.Height);


//				tex = new Texture2D (512, 424, TextureFormat.RGB24, false);
				tex = new Texture2D (depthFrameDesc.Width, depthFrameDesc.Height, TextureFormat.RGB24, false);
				depthview.renderer.material.SetTextureScale ("_MainTex", new Vector2 (1, 1));


				pos = new Vector2[200];
				SensorPosition = new Vector3[depthFrameDesc.Width * depthFrameDesc.Height];
				_Sensor = KinectSensor.GetDefault ();
				if (_Sensor != null) {
						_Mapper = _Sensor.CoordinateMapper;
						var frameDesc = _Sensor.DepthFrameSource.FrameDescription;
			
						// Downsample to lower resolution
						//			CreateMesh(frameDesc.Width / _DownsampleSize, frameDesc.Height / _DownsampleSize);
			
						if (!_Sensor.IsOpen) {
								_Sensor.Open ();
						}
				}

				for (int i=0; i<depthFrameDesc.Width*depthFrameDesc.Height; i++) {

						if (x > depthFrameDesc.Width-1) {
								x = 0;
								y += 1;
						}
						SensorPosition [i] = new Vector3 (x, y, 0);
						x += 1;
				}

				meshFilter = GetComponent<MeshFilter> () as MeshFilter;
		}
	
		// Update is called once per frame
		void Update ()
		{
				_DM = DM.GetComponent<DepthSourceManager> ();

				Vector2[] _RandomPointPos = Random2D.GetComponent<Random2D> ().getRandomPointPos ();
				ushort[] _depthData = _DM.GetData ();
				int _x = 0;
				int _y = 0;

				for (int i=0; i<_depthData.Length; i+=1) { //i++

						int colorindex = i * 3;
						depthBitmapBuffer [colorindex + 0] = (byte)255;
						depthBitmapBuffer [colorindex + 1] = (byte)255;
						depthBitmapBuffer [colorindex + 2] = (byte)255;
						SensorPosition [i].z = _depthData [i];
//						tex.SetPixel (_x, _y, Color.white);

			
			
						if (_depthData [i] > depthReng && _depthData [i] < depthReng + 200) {

				
				float val = map (_depthData [i], depthReng, depthReng + 300, 0.0f, 255.0f);
//								depthBitmapBuffer [colorindex + 0] = (byte)val;
//								depthBitmapBuffer [colorindex + 1] = (byte)val;
//								depthBitmapBuffer [colorindex + 2] = (byte)val;
								depthBitmapBuffer [colorindex + 0] = (byte)0;
								depthBitmapBuffer [colorindex + 1] = (byte)0;
								depthBitmapBuffer [colorindex + 2] = (byte)0;
								//								Color _col = new Color (val, val, val, 1);
//								tex.SetPixel (_x, _y, _col);

								for (int k=0; k<_RandomPointPos.Length; k++) {
										if (_RandomPointPos [k].x == SensorPosition [i].x && _RandomPointPos [k].y == SensorPosition [i].y) {
												machPointCnt += 1;
												Vector3 _pos = new Vector3 (_RandomPointPos [k].x, _RandomPointPos [k].y, 0.0f);
												if (create == true) {
														Instantiate (sphere, _pos, transform.rotation);
												}
												pos [machPointCnt] = _RandomPointPos [k];

										}
								}
						}
				}
//				Debug.Log ("machPoint = " + machPointCnt);

				if (machPointCnt > 3) {
						tr = new Triangulator ();
						meshFilter.mesh = tr.CreateInfluencePolygon (pos);
				}
				machPointCnt = 0;

				tex.LoadRawTextureData (depthBitmapBuffer);
				tex.Apply ();
				depthview.renderer.material.mainTexture = tex;

		create = false;
		
		}
	
		private float map (float value, float low1, float hight1, float low2, float hight2)
		{
				return Mathf.Lerp (low2, hight2, Normalize (value, low1, hight1));
		}
	
		private float Normalize (float value, float low, float hight)
		{
				return (value - low) / (hight - low);
		}

		void OnGUI ()
		{
		
				Event e = Event.current;
				if (e.button == 0 && e.isMouse) {
						create = true;
				}
		}

}
