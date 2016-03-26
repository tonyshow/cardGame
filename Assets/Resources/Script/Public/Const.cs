using UnityEngine;
using System.Collections;

public class Const
{ 
    //卡牌花色位
    public static int CARD_TYPE_BIT = 10000;
     
    //开战首发数
    public static int FRIST_CARD_NUM = 5;

    public enum FIGHT_BTN_TYPE
    {
        NO_OUT,
        TIP,
        ATK
    }

    public enum CardState
    {
        none = 0,
        waitFight = 1,
    }
}