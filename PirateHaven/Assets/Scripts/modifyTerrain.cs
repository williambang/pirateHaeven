

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class modifyTerrain : MonoBehaviour
{
/*
	 float[,,] element;
     int mapX, mapY;
     TerrainData terrainData;
     Vector3 terrainPosition;
     public Terrain myTerrain;
     float[,,] map;
     private Vector3 lastPos;

	 int numberOfTextures;

 void Awake()
     {
		 
		 myTerrain = GameObject.FindGameObjectWithTag("terrain").GetComponent<Terrain>();

		 numberOfTextures = myTerrain.terrainData.alphamapLayers;

         map = new float[myTerrain.terrainData.alphamapWidth, myTerrain.terrainData.alphamapHeight, numberOfTextures];
 
         element = new float[1, 1, numberOfTextures];
         terrainData = myTerrain.terrainData;
         terrainPosition = myTerrain.transform.position;
 
         lastPos = transform.position;

		 
     }

	void Start() {
		StartCoroutine(textureLoop());
	}

	IEnumerator textureLoop () { 
	yield return new WaitForSeconds(60);
	UpdateMapOnTheTarget();
	StartCoroutine(textureLoop());
	}


	void UpdateMapOnTheTarget()
	{

			mapX = (int)(((transform.position.x - terrainPosition.x) / terrainData.size.x) * terrainData.alphamapWidth);
			mapY = (int)(((transform.position.z - terrainPosition.z) / terrainData.size.z) * terrainData.alphamapHeight);

			map[mapY, mapX, 0] = element[0, 0, 0] = 0;
			map[mapY, mapX, 1] = element[0, 0, 1] = 1;

			myTerrain.terrainData.SetAlphamaps(mapX, mapY, element);

			myTerrain.Flush();
		}

	 */
}

