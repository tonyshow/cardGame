using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Msg : MonoBehaviour
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
    
    virtual public bool init()
    {
        return true;
    }
    public static Msg create()//Component t
    {
        //Destroy(t);
        GameObject Perfab = Resources.Load("Prefab/MsgGoneTextPrompts") as GameObject;
        GameObject gameObject = Instantiate(Perfab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 1)) as GameObject;
        Msg msgObj = gameObject.gameObject.AddComponent<Msg>();
        return msgObj;
    } 
    virtual public void close()
    {
        Destroy(this.gameObject);
    }
}
