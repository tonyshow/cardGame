using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class FightWindow : MonoBehaviour
{
  
    //战斗出牌权状态
    [SerializeField]
    FightState fightState;

    //我方牌管理
    [SerializeField]
    FightMineCard fightMineCard;

    //敌方牌管理
    [SerializeField]
    FightEnemyCard fightEnemyCard;

    //战斗数据管理
    [SerializeField]
    FightData fightData;

    //剩余卡牌数
    [SerializeField]
    Text TextRemainingCardNum;
      
    [SerializeField]
    LastFightData lastFightData;

    [SerializeField]
    GameObject LastCardPanel;

    [SerializeField]
    GameObject CardPrefabs;

    void Awake()
    { 
        this.fightState.rightToPlay = FightState.RightToPlay.mine;
    }
    
    void Start()
    {
        this.fightData.CreateAPair();
        //初始化我方首发卡牌
        this.fightMineCard.CreateFristCard();
        this.fightEnemyCard.CreateFristCard();
        this.RefreshData();
    }
    
    /// <summary>
    /// 攻击按钮事件回调
    /// </summary>
    public void EveAtk()
    {
        if (FightState.RightToPlay.mine == this.fightState.rightToPlay)
        {
             StartCoroutine(this.fightMineCard.DoAtk());
        }
    }

    /// <summary>
    /// 蓄力按钮事件回调
    /// </summary>
    public void EveStorage()
    {
        MsgPrompts.create("我方蓄力");
        this.lastFightData.ClearUp();
        this.fightState.Waiver(); 
    } 

    /// <summary>
    /// 刷新界面数据
    /// </summary>
    public void RefreshData()
    {
        this.TextRemainingCardNum.text = string.Format("剩余卡牌:{0}", fightData.CardNumber());
    }

    public void RefreshLastObject(List<FightCard> vAllNoChoiceCard)
    {
        for( int i = 0; i < LastCardPanel.transform.childCount; ++i)
        {
            DestroyObject(LastCardPanel.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < vAllNoChoiceCard.Count ; ++i)
        {
            //卡牌对象
            GameObject CardObj = GameObject.Instantiate(CardPrefabs) as GameObject;
            CardObj.transform.SetParent(LastCardPanel.transform);
            CardObj.transform.localPosition = Vector3.zero;
            CardObj.transform.localScale = Vector3.one;
            //设置卡牌UI数据
            CardPrefabsCtr cardPrefabsCtr = CardObj.GetComponent<CardPrefabsCtr>();
            cardPrefabsCtr.SetCard(vAllNoChoiceCard[i].GetCardDetail());
        }
    }
}
