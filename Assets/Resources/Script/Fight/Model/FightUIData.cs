using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class FightUIData
{
    //单列模式
    public static FightUIData instance = null;
    public static FightUIData getInstance()
    {
        if (instance == null)
        {
            instance = new FightUIData();
        }
        return instance;
    }
    private float topHeight = 1.0f;
    private float bottomHeight = 1.0f;
    private float middHeight = 1.0f;


    

    public void setFightUIHeight(float topHeight, float bottomHeight)
    {
        this.topHeight = topHeight;
        this.bottomHeight = bottomHeight;
        this.middHeight = Screen.height - this.topHeight - this.bottomHeight;
    }
    public float getMiddHeight()
    {
        return this.middHeight;
    }


}
