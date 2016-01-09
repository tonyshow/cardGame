using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class AddCardItem : MonoBehaviour {

    void btnEve( GameObject obj )
    {
        MsgLoading.create("Fight");
    }
	// Use this for initialization
	void Start () {
        int cardNums = 52;
        GameObject resItem = Resources.Load("Prefab/ItemInfo") as GameObject;

        float itemHeight = 252.0f; 
        for (int i = 1; i <= cardNums; ++i)
        {
            GameObject item = Instantiate( resItem );
            item.transform.parent = this.gameObject.transform; 
            item.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, itemHeight);
            item.transform.Find("itemBg").gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, itemHeight); 
            EventListener.Get(item.transform.Find("Button").gameObject).onClick = btnEve; 
        } 
        this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, itemHeight * cardNums - Screen.height); 
        this.gameObject.GetComponent<GridLayoutGroup>().cellSize = new Vector2(Screen.width, itemHeight); 
	} 
}
