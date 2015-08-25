using UnityEngine;
using System.Collections;
public class AdaptationResolutionHeight : MonoBehaviour
{
    public GameObject obj;
    void Start()
    {
        float scale = Screen.height / 640.0f;            //实际适配缩放比例  
        if( null == obj )
        { 
            this.gameObject.transform.localScale = new Vector3(scale, scale, scale);
        }
        else
        {
            obj.transform.localScale = new Vector3(scale, scale, scale);
        }
        
    }
}
