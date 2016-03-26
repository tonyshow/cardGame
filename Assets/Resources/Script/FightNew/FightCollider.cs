using UnityEngine;
using System.Collections;

public class FightCollider : MonoBehaviour
{
    void OnTriggerEnter(Collider coll)
    {  
        if( this.gameObject.name == "EnemyPos")
        {
            StartCoroutine(DestroyCard(coll.gameObject));
        }
        
    }

    IEnumerator DestroyCard(GameObject vObj)
    {
        yield return new WaitForSeconds(0.1f);
        GameObject.DestroyObject(vObj);
    }
}
