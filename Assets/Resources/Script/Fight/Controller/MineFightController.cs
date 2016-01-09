using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MineFightController 
{ 
    //单列模式
    public static MineFightController instance = null;
    public static MineFightController getInstance()
    {
        if (instance == null)
        {
            instance = new MineFightController();
        }
        return instance;
    }

    //初始化我的仓库
    public void initMineFightData()
    {
        MineFightData.getInstance().createCards();
    }

    //拿取首发卡牌
    public Dictionary<int, CardData> getFristCards()
    {
        Dictionary<int, CardData> dic = new Dictionary<int, CardData>();
        for (int num = 1; num <= Const.FRIST_CARD_NUM; ++num)
        {
            int pos = num;
            CardData cards = MineFightData.getInstance().takeCard(pos);
            dic.Add(pos, cards);
        }
        return dic;
    }

    //从仓库随机一个替补数据
    public CardData substitute(int pos)
    {
        //断言
        //Debug.Assert( 0 == MineFightData.getInstance().cardsNumber() );
        return MineFightData.getInstance().takeCard(pos);
    }

    //view报告销毁数据
    public void destroy( int id )
    {
        MineFightData.getInstance().destroyCard(id);
    }

}
