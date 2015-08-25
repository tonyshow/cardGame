using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class MsgPrompts : Msg
{
 
    // Use this for initialization
    void Start()
    {
        Debug.Log("start");
    }
    
    void callBack()
    {
        Debug.Log("callBack");
        Destroy(this.gameObject);  
    }
    public  void close()
    {
        Destroy(this.gameObject);
    }
    public bool init()
    {
        return true;
    }
    public static MsgPrompts create()//Component t
    {
        //Destroy(t);
        GameObject Perfab = Resources.Load("Prefab/MsgGoneTextPrompts") as GameObject;
        GameObject gameObject = Instantiate(Perfab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 1)) as GameObject;
        MsgPrompts msgObj = gameObject.gameObject.AddComponent<MsgPrompts>(); 
        return msgObj;
    } 
}
