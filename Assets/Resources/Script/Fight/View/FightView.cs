/*--战斗界面主要是以高做适配
 * 
 * 
 */
using UnityEngine;
using System.Collections;
public delegate void FightTouchEve( Const.FIGHT_BTN_TYPE state);
public class FightView : MonoBehaviour {
     
    public GameObject middPanelShow;
    public GameObject middPanel;
    public GameObject mainPanel;
    public GameObject bg_top;
    public GameObject bg_bottom;
    FightTouchEve touchEve = new FightTouchEve(MineFightView.getInstance().FightViewTouchFn);
 
    void doSizeUI( GameObject obj , float scale )
    {
        obj.transform.GetComponent<RectTransform>().anchoredPosition3D = obj.transform.GetComponent<RectTransform>().anchoredPosition3D * scale;
        obj.transform.localScale = obj.transform.localScale * scale; 
    }
	void Start () {
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
	}
    void BtnCallBack(GameObject obj)
    {
        string objName = obj.name;
        if ("ButtonNoOut" == objName)
        {
            touchEve(Const.FIGHT_BTN_TYPE.NO_OUT); 
        }
        else if ("ButtonTip" == objName)
        {
            touchEve(Const.FIGHT_BTN_TYPE.TIP);
        }
        else if ("ButtonAtk" == objName)
        {
            touchEve(Const.FIGHT_BTN_TYPE.ATK);
        }
    }
     
}
