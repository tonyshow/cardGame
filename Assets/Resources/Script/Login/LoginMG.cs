using UnityEngine;
using System.Collections;
using UnityEngine.UI; 
using DG.Tweening;
using UnityEngine.SceneManagement;
public class LoginMG : MonoBehaviour {


    public float time = 2.0f;
    void eveTouchBg(GameObject obj)
    {
        
        SceneManager.LoadSceneAsync("EnterGame"); 
    }
    void callBack()
    {
        Button btn = this.gameObject.GetComponent<Button>();
        if (null == btn)
        {
            btn = this.gameObject.AddComponent<Button>();
            btn.transition = Selectable.Transition.None;
        } 
        EventListener.Get(this.gameObject).onClick = eveTouchBg;
        btn.interactable = true; 
    }

	void Start () { 
        int   randNum = Random.Range(1, 3);
        float doSize = Screen.width / 960.0f;
        float bgHeight = this.transform.GetComponent<RectTransform>().sizeDelta.y * doSize;

        float moveY = 0;
       
        //从下往上移动
        if (1 == randNum)
        {
            moveY = bgHeight * 0.5f - Screen.height * 0.5f; 
            this.gameObject.transform.localPosition = new Vector3(this.gameObject.transform.localPosition.x, moveY, this.gameObject.transform.localPosition.z); 
        }
        //从上往下掉
        else
        {
            moveY = -(bgHeight * 0.5f  - Screen.height * 0.5f) ; 
            this.gameObject.transform.localPosition = new Vector3(this.gameObject.transform.localPosition.x, moveY, this.gameObject.transform.localPosition.z); 
        }


        Tweener twenner = this.transform.GetComponent<RectTransform>().DOLocalMoveY(-(moveY - 500 * doSize), time);
        twenner.OnComplete(callBack);


        //Hashtable hasg = new Hashtable();
        //hasg.Add("y", -(moveY - 500 * doSize));
        //hasg.Add("easetype", iTween.EaseType.easeOutQuart);
        //hasg.Add("time", time);
        //hasg.Add("oncomplete", "callBack"); 
        //iTween.MoveBy(this.transform.gameObject, hasg );
	}
	
}
