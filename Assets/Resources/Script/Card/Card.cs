using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
using System.CodeDom;
public class Card   : MonoBehaviour 
{
    public enum CardState{
        none,
        waitFight,
        Fighting,
    }
    public GameObject cardObj;
    public CardData cardData;
    public CardState cardState;
    private  void init()
    {
        this.cardState = CardState.none;
        GameObject resObj = Resources.Load("Prefab/Card") as GameObject;
        GameObject cardObj = Object.Instantiate(resObj);
        this.cardObj = cardObj;
    }

    private void init(CardData cardData)
    {
        this.init();
        this.cardData = cardData;
        this.setCardImag();
        this.setCardTypeView();
        this.setCardNumberView();
        this.setObjName();
        this.addScript();
    }

    //创建一张卡片
    public static  Card create()
    { 
        Card card = new Card();
        card.init(); 
        return  card;
    }

    public Card()
    { 
    
    }
    public static Card create( CardData cardData )
    {
        Card card = new Card();
        card.init(cardData); 
        return card;
    }

    //设置卡片插画
    public void setCardImag()
    {
        int type =  (int)this.cardData.getType();
        int number = this.cardData.getNumber();
        string path = "Res/Card/IconCard/icon_card_" + type + "_" + number  ;

        Texture texture = Resources.Load(path) as Texture;
          
        RawImage img = cardObj.transform.FindChild("Pattern").GetComponent<RawImage>();
        img.texture = texture;   
    }

    public void addScript()
    {
       MineCollider cardCollision = this.cardObj.gameObject.AddComponent<MineCollider>();
       Card card = this.cardObj.gameObject.AddComponent<Card>();
       
       card.cardData = this.cardData;
       card.cardObj = this.cardObj;
       card.cardState = this.cardState;
    }
    public void setObjName()
    {
        this.cardObj.transform.name = ("mine" + this.cardData.Pos);
    }
    public void setCardTypeView()
    {
        int type = (int)this.cardData.getType();

        string path = "Res/Card/CardType/icon_cardType_" + type;

        Texture texture = Resources.Load(path) as Texture;

        RawImage img = cardObj.transform.FindChild("cardType").GetComponent<RawImage>();
        img.texture = texture;
    }
    public void setCardNumberView()
    { 
        int number = this.cardData.getNumber();
        if (number < 14)
        {
            string path = "Res/Card/CardNumber/icon_number_" + number;
            Texture texture = Resources.Load(path) as Texture;
            RawImage img = cardObj.transform.FindChild("cardType").FindChild("cardNum").GetComponent<RawImage>();
            img.texture = texture;
        }
        else
        {
            cardObj.transform.FindChild("cardType").FindChild("cardNum").transform.gameObject.SetActive(false);
        }
    }
    public void setCardState( CardState state )
    {
        this.cardState = state;
    }

    public CardState getCardState()
    {
        return this.cardState;
    }

    public GameObject getObj()
    {
        return this.cardObj;
    }

 
    public CardData getCardData()
    {
        return this.cardData;
    }

    //设置卡牌的父节点
    public void setParent( GameObject parentObj )
    {
        this.cardObj.transform.parent = parentObj.transform;
    }

    public void setPosition( Vector3 vet)
    {
        this.cardObj.transform.localPosition = vet;
    }

    public void setScale(float scale)
    {
        this.cardObj.transform.localScale = new Vector3( scale , scale ,scale );
    }


 
    public Vector2 getSize()
    {
        return this.cardObj.transform.GetComponent<RectTransform>().sizeDelta;
    }

    public void DestoryCard()
    {
        GameObject.DestroyObject(this.cardObj); 
    }

    //移动
    public void moveTo(Vector2 vec)
    { 
        Vector3 v3 = new Vector3(vec.x,vec.y,0.0f );
         this.cardObj.GetComponent<RectTransform>().DOLocalMove(v3, 0.5f);
    }
    public void moveTo(Vector2 vec, System.Action tt)
    {
        Vector3 v3 = new Vector3(vec.x, vec.y, 0.0f);
        Tweener tw =  this.cardObj.GetComponent<RectTransform>().DOLocalMove(v3, 0.5f);
        tw.OnComplete(delegate()
        {
            tt();
        });
    }
    public void correctRotate()
    {
        this.cardObj.GetComponent<RectTransform>().DORotate(Vector3.zero,0.05f);
    }
}
