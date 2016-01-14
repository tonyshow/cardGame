using UnityEngine;
using System.Collections;

public class FightController
{
    public enum RIGHTTOPLAY
    {
        NONE,
        ENEMY,  //敌方出牌权
        ENEMYING,  //敌方出牌权
        MINEING,    //我方出牌权
        MINE    //我方出牌权
    }
    //单列模式
    public static FightController instance = null;
    public static FightController getInstance()
    {
        if (instance == null)
        {
            instance = new FightController();
        }
        return instance;
    }
    private RIGHTTOPLAY rightToPlay = RIGHTTOPLAY.NONE;
    public RIGHTTOPLAY RightToPlay
    {
        set { rightToPlay = value; }
        get { return rightToPlay; }
    }
      
}
