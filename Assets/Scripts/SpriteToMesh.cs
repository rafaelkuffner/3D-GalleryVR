using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class SpriteToMesh : MonoBehaviour {

    public Sprite mySpr;
    public bool generate = false;
	// Use this for initialization
    void Start()
    {
    }
	
	// Update is called once per frame
	void Update () {
        if (generate)
        {
            Mesh m = create(mySpr);
        MeshFilter f= this.gameObject.GetComponent<MeshFilter>();
        f.mesh = m;
        }
	}

    private Mesh create(Sprite sprite)
    {
        Mesh mesh = new Mesh();
        mesh.SetVertices(Array.ConvertAll(sprite.vertices, i => (Vector3)i).ToList());
        mesh.SetUVs(0, sprite.uv.ToList());
        mesh.SetTriangles(Array.ConvertAll(sprite.triangles, i => (int)i), 0);

        return mesh;
    }
}
