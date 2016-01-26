using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FightWin : Msg
{
    static private FightWin msgObj;
    void Start()
    {
        GameObject gb = this.transform.Find("mainPanel/bg/btns/Button").gameObject;
        EventListener.Get(gb).onClick = close;
    }  
    // Update is called once per frame
    void Update()
    {
  
    }
    public void close( GameObject gb )
    {

        Destroy(this.gameObject); 
        SceneManager.LoadSceneAsync("ChoiceHeroScene"); 
    }
    public bool init()
    {
        return true;
    }
    public static FightWin create()
    { 
        GameObject Perfab = Resources.Load("Prefab/Fight/fightResult") as GameObject;
        GameObject gameObject = Instantiate(Perfab, new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0), new Quaternion(0, 0, 0, 1)) as GameObject;
        FightWin msgObj = gameObject.gameObject.AddComponent<FightWin>();
        return msgObj; 
    }

     
}
