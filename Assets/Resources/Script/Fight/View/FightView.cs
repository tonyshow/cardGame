/*--战斗界面主要是以高做适配
 * 
 * 
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
public delegate IEnumerator FightTouchEve( Const.FIGHT_BTN_TYPE state);
public delegate void ShakeMainPanel();
public class FightView : MonoBehaviour {
     
    public GameObject middPanelShow;
    public GameObject middPanel;
    public GameObject mainPanel;
    public GameObject bg_top;
    public GameObject bg_bottom;
    public GameObject enemyPanel;
    public GameObject minePanel;

    private GameObject mouseObj = null;
    private Vector3 downMousePos;
    static private GameObject _mainPanel;
    FightTouchEve touchEve = new FightTouchEve(MineFightView.getInstance().FightViewTouchFn); 
    void doSizeUI( GameObject obj , float scale )
    {
        obj.transform.GetComponent<RectTransform>().anchoredPosition3D = obj.transform.GetComponent<RectTransform>().anchoredPosition3D * scale;
        obj.transform.localScale = obj.transform.localScale * scale; 
    }
	void Start () {

        //初始化出牌权
        FightController.getInstance().RightToPlay = FightController.RIGHTTOPLAY.MINE;
        
        float doSizeH = AdaptationResolutionMG.getInstance().getDoSizeHeight();
       
        float bg_topHeight = bg_top.transform.GetComponent<RectTransform>().sizeDelta.y *doSizeH;
        float bg_bottomHeight = bg_bottom.transform.GetComponent<RectTransform>().sizeDelta.y * doSizeH;
        Vector2 oldMiddPanelShowSize = middPanelShow.transform.GetComponent<RectTransform>().sizeDelta * doSizeH;

        FightUIData.getInstance().setFightUIHeight(bg_topHeight, bg_bottomHeight);
         
        middPanelShow.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, oldMiddPanelShowSize.y );

        middPanel.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, oldMiddPanelShowSize.y);
        mainPanel.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        bg_top.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width,  bg_topHeight );

        GameObject bg_name = bg_top.transform.FindChild("bg_name").gameObject;
        doSizeUI(bg_name, doSizeH);

        
        GameObject icon_hp = bg_top.transform.FindChild("icon_hp").gameObject;
        doSizeUI(icon_hp, doSizeH);
   
        bg_bottom.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width,  bg_bottomHeight );
        icon_hp = bg_bottom.transform.FindChild("icon_hp").gameObject;
        doSizeUI(icon_hp, doSizeH);

        //初始化战斗按钮相关
        GameObject Buttons = bg_bottom.transform.FindChild("Buttons").gameObject;
        doSizeUI(Buttons, doSizeH);
        GameObject ButtonNoOut = Buttons.transform.FindChild("ButtonNoOut").gameObject;
        GameObject ButtonTip = Buttons.transform.FindChild("ButtonTip").gameObject;
        GameObject ButtonAtk = Buttons.transform.FindChild("ButtonAtk").gameObject;
         
        EventListener.Get(ButtonNoOut).onClick = BtnCallBack;
        EventListener.Get(ButtonTip).onClick = BtnCallBack;
        EventListener.Get(ButtonAtk).onClick = BtnCallBack;

       
        FightUIData.getInstance().EnemyVec3 = enemyPanel.transform.GetComponent<RectTransform>().position;
        FightUIData.getInstance().MineVec3 = minePanel.transform.GetComponent<RectTransform>().position;

        _mainPanel = mainPanel;
	}

    //敌方受到攻击
    static public void shake()
    {
        Debug.Log("抖动"); 
        _mainPanel.transform.DOShakePosition(0.2f,30);
    }
    void BtnCallBack(GameObject obj)
    {
        string objName = obj.name;
        if ("ButtonNoOut" == objName)
        {
            StartCoroutine(touchEve(Const.FIGHT_BTN_TYPE.NO_OUT)); 
        }
        else if ("ButtonTip" == objName)
        {
            StartCoroutine(touchEve(Const.FIGHT_BTN_TYPE.TIP)); 
        }
        else if ("ButtonAtk" == objName)
        {
            StartCoroutine(touchEve(Const.FIGHT_BTN_TYPE.ATK)); 
        }
    }
    double Angle(Vector3 o, Vector3 s, Vector3 e)
    {
        double cosfi = 0, fi = 0, norm = 0;
        double dsx = s.x - o.x;
        double dsy = s.y - o.y;
        double dex = e.x - o.x;
        double dey = e.y - o.y;

        cosfi = dsx * dex + dsy * dey;
        norm = (dsx * dsx + dsy * dsy) * (dex * dex + dey * dey);
        cosfi /= System.Math.Sqrt(norm);

        if (cosfi >= 1.0) return 0;
        if (cosfi <= -1.0) return System.Math.PI;
        fi = System.Math.Acos(cosfi);

        if (180 * fi / System.Math.PI < 180)
        {
            return 180 * fi / System.Math.PI;
        }
        else
        {
            return 360 - 180 * fi / System.Math.PI;
        }
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!mouseObj)
            {
                mouseObj = Object.Instantiate(Resources.Load("Prefab/mouseLine") as GameObject);
                mouseObj.transform.parent = middPanel.transform;
                mouseObj.transform.localScale = new Vector3(1,1,1);
                mouseObj.GetComponent<RectTransform>().localPosition = Input.mousePosition - new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);
                downMousePos = Input.mousePosition;
            } 
            
        }
        if (Input.GetButton("Fire1"))
        {
            if (mouseObj)
            { 
                float space = Vector3.Distance(downMousePos, Input.mousePosition); 
                Vector2 size = new Vector2(mouseObj.GetComponent<RectTransform>().sizeDelta.x, (float)space); 
                mouseObj.GetComponent<RectTransform>().sizeDelta = size;
                double angleOfLine =  Mathf.Atan2((downMousePos.y - Input.mousePosition.y), (downMousePos.x - Input.mousePosition.x)) * 180 / Mathf.PI +90 ; 
                Vector3 nn = new Vector3(0, 0, (float)angleOfLine);
                Quaternion rotation = Quaternion.Euler(nn);
                mouseObj.GetComponent<RectTransform>().rotation = rotation;  
            } 
        } 
        if (Input.GetButtonUp("Fire1"))
        {
            if (mouseObj)
            {
               DestroyObject(mouseObj);
            } 
        }
        
    }
}
