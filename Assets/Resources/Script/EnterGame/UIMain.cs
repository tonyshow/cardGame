using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIMain : MonoBehaviour
{

    AssetBundle assetbundle = null;
    void Start()
    {
        CreatImage(loadSprite("icon_button")); 
    }

    private void CreatImage(GameObject go)
    { 
        //go.layer = LayerMask.NameToLayer("UI");
        go.transform.parent = this.gameObject.transform;
        //go.transform.localScale = Vector3.one;  
    }

    private GameObject loadSprite(string spriteName)
    {
#if USE_ASSETBUNDLE
		if(assetbundle == null)
			assetbundle = AssetBundle.CreateFromFile(Application.streamingAssetsPath +"/Main.assetbundle");
				return assetbundle.Load(spriteName) as Sprite;
#else
        return Instantiate(Resources.Load<GameObject>("Prefab/Atlas/Public/" + spriteName)) as GameObject;
#endif
    }

}

