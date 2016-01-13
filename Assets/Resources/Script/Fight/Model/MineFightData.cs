using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MineFightData
{
    //卡牌仓库
    Dictionary<int, CardData> CardsDic = new Dictionary<int, CardData>();

    List<int> indexList = new List<int>();

    //卡牌出货记录
    Dictionary<int, CardData> TakeCardsDic = new Dictionary<int, CardData>();

    //场上卡牌库
    Dictionary<int, CardData> FightingCardsDic = new Dictionary<int, CardData>();

    private int hp;
    //单列模式
    public static MineFightData instance = null;
    public static MineFightData getInstance()
    {
        if (instance == null)
        {
            instance = new MineFightData();
        }
        return instance; 
    }
     

    //生产一副牌 数量52张
    public void createCards()
    {
        CardsDic.Clear();
        TakeCardsDic.Clear();
        FightingCardsDic.Clear();
        //花色
        for (int type = 1; type <= 4; ++type)
        {
            for (int number = 1; number <= 13; ++number)
            {
                CardData cards = new CardData( CardCosplay.Mine , ( CardType ) type , number );
                int id = cards.getId();
                indexList.Add(id);
                CardsDic.Add( id , cards);

                hp = hp + number;
            }   
        }

        //小王大王数据
        for (int type = (int)CardType.xiao; type <= (int)CardType.da; ++type)
        {
            int number = type + 9;
            CardData cards = new CardData(CardCosplay.Mine, (CardType)type, number);
            int id = cards.getId();
            indexList.Add(id);
            CardsDic.Add(id, cards);
            hp = hp + number;
        } 
    }

    //总仓库剩余卡牌数
    public int cardsNumber()
    {
        return CardsDic.Count;
    }

    public int usingCardsNumber()
    {
        Debug.Log("场上剩余卡牌" + FightingCardsDic.Count);
        return FightingCardsDic.Count;
    }
    public int getHp()
    {
        return this.hp;
    }
    public void subHp(int _hp)
    {
        this.hp = this.hp - _hp;
    }
    //从总仓库拿取一张卡牌
    public CardData takeCard( int pos )
    {
        int randCardIndex = Random.Range(0, this.cardsNumber());
        CardData cards = CardsDic[indexList[randCardIndex]];
        cards.Pos = pos; 
        int key = cards.getId();
        CardsDic.Remove( key );
        indexList.Remove(key);
        TakeCardsDic.Add(key, cards);
        FightingCardsDic.Add(key, cards);
        return cards;
    }

    //清理场上已经死亡的卡牌
    //参数为卡牌唯一id
    public void destroyCard(int id)
    {
        int number = FightingCardsDic[id].getNumber();
        EnemyFightData.getInstance().subHp(number); 
        FightingCardsDic.Remove(id);
    }
}


