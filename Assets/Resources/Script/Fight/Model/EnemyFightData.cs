using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EnemyFightData
{
    //卡牌仓库
    Dictionary<int, CardData> cardsDic = new Dictionary<int, CardData>();

    //卡牌出货记录
    Dictionary<int, CardData> mineTakeCardsDic = new Dictionary<int, CardData>();

    List<int> indexList = new List<int>();
    //单列模式
    public static EnemyFightData instance = null;
    public static EnemyFightData getInstance()
    {
        if (instance == null)
        {
            instance = new EnemyFightData();
        }
        return instance; 
    }
     

    //生产一副牌 数量52张
    public void createCards()
    {
        //花色
        for (int type = 1; type <= 4; ++type)
        {
            for (int number = 1; number <= 13; ++number)
            {
                CardData cards = new CardData( CardCosplay.Enemy , ( CardType ) type , number );
                int id = cards.getId();
                indexList.Add(id);
                cardsDic.Add( id , cards);
            }   
        }
    }

    //总仓库剩余卡牌数
    public int cardsNumber()
    {
        return cardsDic.Count;
    }

    //从总仓库拿取一张卡牌
    public CardData takeCard(int pos)
    {
        int randCardIndex = Random.Range(0, this.cardsNumber());
        CardData cards = cardsDic[indexList[randCardIndex]];
        cards.Pos = pos;
        int key = cards.getId();
        cardsDic.Remove(key);
        indexList.Remove(key);
        return cards;
    }

    //清理场上已经死亡的卡牌
    //参数为卡牌唯一id
    public void destroyCard(int id)
    {
        cardsDic.Remove(id);
    }
}


