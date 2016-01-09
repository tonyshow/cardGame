using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class MsgPrompts : Msg
{
 
    // Use this for initialization
    void Start()
    {
        runAction(this.gameObject);
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

    void runAction( GameObject obj )
    {
//         Hashtable args = new Hashtable();
//         args.Add("time", 1.0f);     
//         args.Add("y", Screen.height*0.75f);
//         args.Add("easetype","easeOutSine");
//         args.Add("oncomplete", "callBack");
//         iTween.MoveTo(obj, args ); 

        Tweener twenner = obj.transform.DOLocalMoveY(Screen.height * 0.75f, 1.0f);
        twenner.OnComplete(callBack);
    }
    public static MsgPrompts create()//Component t
    {
        GameObject Perfab = Resources.Load("Prefab/MsgPrompts") as GameObject;
        GameObject gameObject = Instantiate(Perfab, new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0), new Quaternion(0, 0, 0, 1)) as GameObject;
        MsgPrompts msgObj = gameObject.gameObject.AddComponent<MsgPrompts>();      
        return msgObj;
    }

    public static MsgPrompts create( string text )//Component t
    {
        GameObject Perfab = Resources.Load("Prefab/MsgPrompts") as GameObject;
        GameObject gameObject = Instantiate(Perfab, new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0), new Quaternion(0, 0, 0, 1)) as GameObject;
        Text textObj = gameObject.transform.Find("Image/Text").gameObject.GetComponent<Text>();
        textObj.text = text;
        MsgPrompts msgObj = gameObject.gameObject.AddComponent<MsgPrompts>();
        return msgObj;
    }

    public static MsgPrompts create(string text ,Color color )//Component t
    {
        GameObject Perfab = Resources.Load("Prefab/MsgPrompts") as GameObject;
        GameObject gameObject = Instantiate(Perfab, new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0), new Quaternion(0, 0, 0, 1)) as GameObject;
        Text textObj = gameObject.transform.Find("Image/Text").gameObject.GetComponent<Text>();
        textObj.text = text;
        textObj.color = color;
        MsgPrompts msgObj = gameObject.gameObject.AddComponent<MsgPrompts>();
        return msgObj;
    }

}
