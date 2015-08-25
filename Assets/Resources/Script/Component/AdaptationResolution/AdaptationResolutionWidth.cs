using UnityEngine;
using System.Collections;
public class AdaptationResolutionWidth : MonoBehaviour
{
    void Start()
    {
        float width = Screen.width;             //设备的屏幕宽度
        float height = Screen.height;           //设备的屏幕高度
        float scale   = width / 960;            //实际适配缩放比例  
        this.gameObject.transform.localScale = new Vector3(scale, scale, scale);
    }
}
