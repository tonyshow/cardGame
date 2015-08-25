using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class LoginMG : MonoBehaviour {


    public float time = 5.5f;
    void eveTouchBg(GameObject obj)
    { 
        SMGameEnvironment.Instance.SceneManager.LoadScreen("EnterGame");
    }
    void callBack()
    {

        Button btn = this.gameObject.GetComponent<Button>();
        EventListener.Get(btn.gameObject).onClick = eveTouchBg;
        btn.interactable = true; 
    }

	void Start () {
        float bgHeight = 2555.0f;
        int randNum = Random.Range(1, 3);

        Debug.Log("randNum = " + randNum); 
        //从下往上移动
        if (1 == randNum)
        {
            this.gameObject.transform.localPosition = new Vector3(0, bgHeight, 0);
        }
        //从上往下掉
        else
        {
            this.gameObject.transform.localPosition = new Vector3(0, -bgHeight + Screen.height, 0);

        }
        iTween.MoveTo(this.gameObject, iTween.Hash("y", 800, "time", time, "oncomplete", "callBack"));
	}
	
}
