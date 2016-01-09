using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ChoiceHeroSceneMG : MonoBehaviour {

    public  Image imgBg;
    public  GameObject btnObj;
    public  GameObject slider;


    string[] iconList = new string[6] { "card_Femaledemons", "card_general", "card_queen", "card_wizard", "card_woman", "1" };
    private int[] unlock = new int[] { 1, 5, 9, 12, 15, 16, 18 };
	// Use this for initialization
    void Eve( GameObject obj  )
    {
        Application.LoadLevel("EnterGame");
        //返回到上界面
      //  SMGameEnvironment.Instance.SceneManager.LoadScreen("EnterGame"); 
    }

    void EveSure(GameObject obj)
    {
        string touchTag = obj.transform.parent.gameObject.name;
        Debug.Log(touchTag);
        int tag = int.Parse(touchTag);
        if (tag == 1)
        {
            Application.LoadLevel("ChoiceEnemy");
            //SMGameEnvironment.Instance.SceneManager.LoadScreen("ChoiceEnemy"); 
            //MsgLoadingg.create("Fight");
        }
        else
        {  
            MsgPrompts.create(string.Format("角色Lv{0:0}级开放", unlock[tag]), Color.red);
        }
    }

    void open( GameObject item )
    {
        RawImage icon = item.transform.Find("icon").gameObject.GetComponent<RawImage>();
        icon.material = null;

        GameObject objLock = item.transform.Find("lock").gameObject;
        objLock.active = false;
    }
    void Lock( GameObject item )
    {
        RawImage icon = item.transform.Find("icon").gameObject.GetComponent<RawImage>();
       // Material materialGray = Resources.Load("Material/gray") as Material;
      //  icon.material = materialGray;
        icon.color = new Color32(255, 255, 255, 255);
        GameObject objBtn = item.transform.Find("Button").gameObject;
       
        Button button = objBtn.gameObject.GetComponent<Button>();
        Image img = objBtn.gameObject.GetComponent<Image>(); 
       // img.material = materialGray;

        Image btnImg = objBtn.transform.Find("Image").gameObject.GetComponent<Image>();
       // btnImg.material = materialGray;
         
        Image infoImg = item.transform.Find("Image/Image").gameObject.GetComponent<Image>();
      //  infoImg.material = materialGray;

        Image infoImgbg = item.transform.Find("Image").gameObject.GetComponent<Image>();
        //infoImgbg.material = materialGray;
         
        Image cardBgImg = item.transform.Find("cardBg").gameObject.GetComponent<Image>();
        //cardBgImg.material = materialGray; 
    }

	void Start () {
        float scale = Screen.height / 640.0f;            //实际适配缩放比例  
        Vector2 imgBgSize = imgBg.transform.GetComponent<RectTransform>().sizeDelta;
        float x = (imgBgSize.x * scale - Screen.width) * 0.5f;
        imgBg.transform.localPosition = new Vector3(x, 0, 0);

        Button btn = btnObj.transform.GetComponent<Button>();
        EventListener.Get(btn.gameObject).onClick = Eve; 
        for( int i = 1 ; i <= 4 ; ++i )
        {
            string itemName = "item" + i.ToString();
            GameObject item = slider.transform.Find( itemName ).gameObject;
            item.name = i.ToString();
            GameObject objBtn = item.transform.Find("Button").gameObject; 
            EventListener.Get(objBtn).onClick = EveSure; 
            if( i > 1 )
            {
                Lock(item);
            }
            else
            {
                open(item);
            }
        } 
	} 

}
