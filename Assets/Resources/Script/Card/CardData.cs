/*卡牌数据
 * 
 */
using UnityEngine;
using System.Collections;  
//卡牌的扮演类型
public enum CardCosplay
{
    None,       //无
    Enemy,      //敌人
    Mine        //我军
}

//卡牌的花色
public enum CardType
{
    None = 0,       //无
    hei =  1,        //黑
    hong = 2,       //红
    mei = 3,        //梅花
    fang = 4,       //方块
    xiao = 5,       //小王
    da = 6          //大王
}
 

//public static int typeBit = 10000;
public class CardData
{
    private CardCosplay coslay;  //卡牌扮演类型 
    private CardType type;    //卡牌花色
    private int id;               //卡牌的唯一标识
    private  int number;           //卡牌数字
    private int pos;               //站位
    public  CardData()
    {
        this.coslay = CardCosplay.None;
        this.type = CardType.None;
        this.number = 0;
        this.id = 0;
        this.pos = 0;
    }

    public CardData(CardCosplay coslay, CardType type, int number )
    {
        this.coslay = coslay;
        this.type = type;
        this.number = number;
        this.id = (int)(this.type) * Const.CARD_TYPE_BIT + number;
        this.pos = 0;
    }

    public int getId()
    {
        return this.id;
    }

    public int getNumber()
    {
        return this.number;
    }

    public int  Pos
    {
        get { return pos; }
        set { pos = value; } 
    }

    public CardCosplay getCosplay()
    {
        return this.coslay;
    }

    public CardType getType()
    {
        return this.type;
    }
}
