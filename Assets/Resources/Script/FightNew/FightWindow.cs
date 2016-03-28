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
        MsgPrompts.create("蓄力"); 
        this.fightState.Waiver(); 
    } 

    /// <summary>
    /// 刷新界面数据
    /// </summary>
    public void RefreshData()
    {
        this.TextRemainingCardNum.text = string.Format("剩余卡牌:{0}", fightData.CardNumber());
    }
}
