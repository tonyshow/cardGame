using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CardPrefabsCtr : MonoBehaviour {

    [SerializeField]
    Image icon;

    [SerializeField]
    Image type;

    [SerializeField]
    Image number;

    CardDetail card;

    // Use this for initialization
    void Start () {
        this.SetUI();
    }
	
    //设置UI数据
	public void SetUI()
    {
        if(null != this.card)
        {  
            this.icon.sprite = AppFileManager.LoadSprite( this.card.iconPath );
            this.type.sprite = AppFileManager.LoadSprite(this.card.typeSprite);
            this.type.SetNativeSize();
            if(this.card.type < 5 )
            {
                this.number.gameObject.SetActive(true);
                this.number.sprite = AppFileManager.LoadSprite(this.card.numberSprite);
            }
            else
            {
                this.number.gameObject.SetActive(false);
            }
            
        }
    }

    public void SetUI(CardDetail vCard)
    {
        this.card = vCard;
        this.SetUI();
    }

    public void SetCard(CardDetail vCard)
    {
        this.card = vCard; 
    }
}
