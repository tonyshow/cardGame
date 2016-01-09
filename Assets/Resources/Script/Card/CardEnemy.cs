using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class CardEnemy : Card
{
    //创建一张卡片
    public static CardEnemy create()
    {
        CardEnemy card = new CardEnemy();
        card.init();
        return card;
    }
    public CardEnemy()
    { 
    
    }
    public static CardEnemy create(CardData cards)
    {
        CardEnemy card = new CardEnemy();
        card.init(cards);
        return card;
    }

    private void init()
    {
        GameObject resObj = Resources.Load("Prefab/Enemy") as GameObject;
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
     
    public void setObjName()
    {
        this.cardObj.transform.name = ("enemy" + this.cardData.Pos);
    }
    

    private void addScript()
    {
        EnemyCollider cardCollision = this.cardObj.gameObject.AddComponent<EnemyCollider>();
        CardEnemy card = this.cardObj.gameObject.AddComponent<CardEnemy>();

        card.cardData = this.cardData;
        card.cardObj = this.cardObj;
        card.cardState = this.cardState;
    }
}
