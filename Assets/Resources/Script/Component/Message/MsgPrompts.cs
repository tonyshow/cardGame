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
    }
    
    void callBack()
    {
        Tweener twenner = this.transform.DOScale(2.5f, 1.0f).SetEase(Ease.OutBack);
        twenner.OnComplete(delegate()
        {
            this.close();
        });
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
        Tweener twenner = obj.transform.DOScale(1.5f , 1.0f).SetEase(Ease.OutBack); 
        twenner.OnComplete(callBack);
    }
    public static MsgPrompts create()//Component t
    {
        GameObject Perfab = Resources.Load("Prefab/MsgPrompts") as GameObject;
        GameObject gameObject = Instantiate(Perfab, new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0), new Quaternion(0, 0, 0, 1)) as GameObject;
        MsgPrompts msgObj = gameObject.gameObject.AddComponent<MsgPrompts>();
        Tweener tw =  Perfab.transform.DOLocalMoveX(0.1f,1.0f);
        tw.OnComplete(delegate()
        {

        });
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
