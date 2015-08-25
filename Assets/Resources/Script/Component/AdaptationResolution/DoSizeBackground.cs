/*---功能 对960 X 640 的背景做适配
 *---作者：tony 
 *---时间：2015-08-14 
 */
using UnityEngine;
using System.Collections;
public class DoSizeBackground : MonoBehaviour
{
    void Start()
    {
        float width = Screen.width;             //设备的屏幕宽度
        float height = Screen.height;           //设备的屏幕高度
        float scale = 1.0f;                     //实际适配缩放比例

        //比较宽的的分配率
        if( width / height > 1.5f )
        {
            scale = width / 960;
        }
        //比较高的分配率
        else
        {
            scale = height / 640;
        }

        this.gameObject.transform.localScale = new Vector3(scale, scale, scale);
        
    }
}
