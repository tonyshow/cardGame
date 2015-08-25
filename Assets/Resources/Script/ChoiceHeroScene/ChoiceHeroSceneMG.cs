using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ChoiceHeroSceneMG : MonoBehaviour {

   public  Image imgBg;
	// Use this for initialization
	void Start () {

        float scale = Screen.height / 640.0f;            //实际适配缩放比例  
        Vector2 imgBgSize = imgBg.transform.GetComponent<RectTransform>().sizeDelta;
        float x = (imgBgSize.x * scale - Screen.width) * 0.5f;
        imgBg.transform.localPosition = new Vector3(x, 0, 0);
	} 

}
