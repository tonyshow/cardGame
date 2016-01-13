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


    private Vector3 enemyVec3;
    private Vector3 mineVec3;

    public Vector3 EnemyVec3
    {
        set { enemyVec3 = value; }
        get { return enemyVec3; }
    }

    public Vector3 MineVec3
    {
        set { mineVec3 = value; }
        get { return mineVec3; }
    }

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
