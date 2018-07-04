using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TerrainNav : MonoBehaviour 
{

    private NavMeshSurface surface;
	void Start () 
    {
        surface = GetComponent<NavMeshSurface>();
        
	}
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            surface.BuildNavMesh();
        }
    }

}
