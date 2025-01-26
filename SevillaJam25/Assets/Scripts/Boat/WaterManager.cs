using UnityEngine;
[RequireComponent (typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class WaterManager : MonoBehaviour
{
   private MeshFilter meshFilter;
    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter> ();
    }

    private void Update()
    {
        //Grab the mesh's vertices
        Vector3[] verts = meshFilter.mesh.vertices;
        for(int i = 0; i < verts.Length; i++) //Iterate
        {
            //Modify y components based on the global x components
            verts[i].y = WaveManager.Instance.GetWaveHeight(transform.position.x + verts[i].x);
        }
        meshFilter.mesh.vertices = verts; //Relocate verts
        meshFilter.mesh.RecalculateNormals(); //Fix normals
    }
}
