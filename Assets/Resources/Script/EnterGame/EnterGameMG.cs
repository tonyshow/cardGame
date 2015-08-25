using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class EnterGameMG : MonoBehaviour {


    void BtnCallBack( GameObject obj )
    {
        string objName = obj.name;
        if( "Button" == objName )
        {
            SMGameEnvironment.Instance.SceneManager.LoadScreen("ChoiceHeroScene");
        }
        else if ( "Button2" == objName )
        {
            SMGameEnvironment.Instance.SceneManager.LoadScreen("ChoiceHeroScene");
        }
    }
	// Use this for initialization
	void Start () {

        Button btnObj = this.gameObject.transform.Find("Button").GetComponent<Button>();
        EventListener.Get(btnObj.gameObject).onClick = BtnCallBack;


        Button btnObj2 = this.gameObject.transform.Find("Button2").GetComponent<Button>();
        EventListener.Get(btnObj2.gameObject).onClick = BtnCallBack;
	}
	
}
