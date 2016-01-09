using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class MsgLoading : Msg
{ 
    private Slider slider;
    private float per = 0.0f;
    private bool isGoScene = false;
    private float speed = 0.005f;
    private string sceneName = ""; 
    void Start()
    {
        slider = this.gameObject.transform.Find("Slider").GetComponent<Slider>();
        Debug.Log("start");
    }

    void callBack()
    {
        Debug.Log("callBack");
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        ++per;
        slider.value = per * speed;
        if (per * speed > 1 && isGoScene == false)
        {
            isGoScene = true;
            if (this.sceneName != null)
            {
                Application.LoadLevel(this.sceneName);
                //SMGameEnvironment.Instance.SceneManager.LoadScreen(this.sceneName);
            }
            else
            {
                Application.LoadLevel("MsgLoading");
               // SMGameEnvironment.Instance.SceneManager.LoadScreen("MsgLoading");
            } 
        }
    }
    public void close()
    {
        Destroy(this.gameObject);
    }
    public bool init()
    {
        return true;
    }
    public static MsgLoading create()
    { 
        GameObject Perfab = Resources.Load("Prefab/Loading") as GameObject;
        GameObject gameObject = Instantiate(Perfab) as GameObject;
        MsgLoading msgObj = gameObject.gameObject.AddComponent<MsgLoading>(); 
        return msgObj; 
    }

    public static MsgLoading create( string sceneName)
    { 
        GameObject Perfab = Resources.Load("Prefab/Loading") as GameObject;
        GameObject gameObject = Instantiate(Perfab) as GameObject;
        MsgLoading msgObj = gameObject.gameObject.AddComponent<MsgLoading>();
        msgObj.sceneName = sceneName;
        return msgObj;
    }
}
