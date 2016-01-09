using UnityEngine;
using System.Collections;
using DG.Tweening;
public class ColliderExitCB : MonoBehaviour {
     
    void OnCollisionExit2D(Collision2D coll)
    {
        Debug.Log("OnCollisionEnter2D");
        this.gameObject.transform.DORotate(Vector3.zero, 0.1f);  
    }
}
