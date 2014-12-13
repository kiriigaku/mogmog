using UnityEngine;
using System.Collections;

//BaseObjにmeshfilter,meshrendereを追加した空のGameObjectをアタッチする

public class CreateBaraBaraMesh : MonoBehaviour {

	public GameObject BaseObj;
	private GameObject BaseObj2;
	private Mesh _mesh;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){

		Event e = Event.current;
		if(e.button == 0 && e.isMouse){
//			Debug.Log ("ok");
			_mesh = GetComponent<MeshFilter>().mesh;
//			Debug.Log( _mesh.GetIndices (0)[0]);

//			for(int i=0; i<_mesh.GetIndices(0).Length; i++){
//				Debug.Log( _mesh.GetIndices (0)[i]);
//			}

			int[] _indices =  _mesh.GetIndices (0);

			for(int i=0; i < _mesh.triangles.Length; i+=3){

				Mesh _hogeMesh = new Mesh();
				Vector3[] vertices = new Vector3[3];
				vertices[0] = _mesh.vertices[_indices[i]];
				vertices[1] = _mesh.vertices[_indices[i+1]];
				vertices[2] = _mesh.vertices[_indices[i+2]];
				
				int[] triangles = new int[3];
				triangles[0] = 0;
				triangles[1] = 1;
				triangles[2] = 2;
				
				//			Vector2[] uvs = new Vector2[2];
				_hogeMesh.vertices = vertices;
				_hogeMesh.triangles = triangles;
				//			_hogeMesh.uv = uvs;
				_hogeMesh.RecalculateNormals();
				_hogeMesh.RecalculateBounds();
				
				GameObject _hoge;
				_hoge = (GameObject)Instantiate(BaseObj, transform.position, transform.rotation);
				_hoge.transform.parent = this.transform;
				_hoge.GetComponent<MeshFilter>().mesh = _hogeMesh;

			}
			this.renderer.enabled = false;
//			Debug.Log(_mesh.triangles.Length);


		}
	}
}
