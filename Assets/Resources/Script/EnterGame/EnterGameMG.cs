using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class EnterGameMG : MonoBehaviour {


    void BtnCallBack( GameObject obj )
    {
        string objName = obj.name;
        if( "Button" == objName )
        { 
            SceneManager.LoadSceneAsync("ChoiceHeroScene"); 
        }
        else if ( "Button2" == objName )
        {
            SceneManager.LoadSceneAsync("ChoiceHeroScene"); 
        }
    }
	// Use this for initialization
	void Start () {

        Button btnObj = this.gameObject.transform.Find("btnBg/Button").GetComponent<Button>();
        EventListener.Get(btnObj.gameObject).onClick = BtnCallBack;


        Button btnObj2 = this.gameObject.transform.Find("btnBg/Button2").GetComponent<Button>();
        EventListener.Get(btnObj2.gameObject).onClick = BtnCallBack;
	}
	
}
