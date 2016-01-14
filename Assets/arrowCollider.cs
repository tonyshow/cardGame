using UnityEngine;
using System.Collections;

public class arrowCollider : MonoBehaviour {

	// Use this for initialization
    void OnCollisionEnter(Collision coll)
    {
        Debug.Log("箭头遇到碰撞");
	}  
}
