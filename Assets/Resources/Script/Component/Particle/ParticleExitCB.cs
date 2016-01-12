using UnityEngine;
using System.Collections;

public class ParticleExitCB : MonoBehaviour {

    IEnumerator destroyParticle()
    {
        yield return new WaitForSeconds(0.5f);
        DestroyObject(this.gameObject);
    }
	// Use this for initialization
	void Start () {
        StartCoroutine(destroyParticle()); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
