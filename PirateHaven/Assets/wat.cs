using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wat : MonoBehaviour {


    public GameObject prefab;

    public float point;
    private int space = 110;
    public int radius;

        // Use this for initialization
    void Start()
    {
        float ratio = space / radius;
        float objects = 360 / ratio;

        Vector3 center = transform.position;
        for (int i = 0; i < objects; i++)
        {
            Vector3 pos = RandomCircle(center, radius, ratio);
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
            var obj = Instantiate(prefab, pos, rot);
            obj.transform.parent = gameObject.transform;
            point = point + ratio;
        }
    }

    Vector3 RandomCircle(Vector3 center, float radius, float ratio)
    {
        
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(point * Mathf.Deg2Rad);
        pos.y = center.y;
        pos.z = center.z + radius * Mathf.Cos(point * Mathf.Deg2Rad);
        return pos;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
