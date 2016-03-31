using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
/** 战斗状态 
 * 作者：tony */
public class FightState : MonoBehaviour {

    [SerializeField]
    FightMineCard fightMindCard;

    [SerializeField]
    Text WaiverText;

    //出牌权
    public enum RightToPlay
    {
        //未确定出牌权
        none = 0,
        //敌方
        enemy = 1,
        //我方
        mine = 2,
    }

    private RightToPlay _rightToPlay = RightToPlay.none;

    //获得出牌权
    public RightToPlay rightToPlay { get { return this._rightToPlay; } set { this._rightToPlay = value; } }

    private Action enemyAction;

    /// <summary>
    /// 交出出牌权
    /// </summary>
    public void Waiver()
    {
        if(this._rightToPlay == RightToPlay.enemy)
        {
            this._rightToPlay = RightToPlay.mine;
            fightMindCard.MineGetWaiver();
            WaiverText.text = "我方出牌";
            return;
        }
        this._rightToPlay = RightToPlay.enemy;
        if(this.enemyAction!= null)
        {
            this.enemyAction();
            WaiverText.text = "敌方出牌";
        }
        return; 
    }

    
    public void AddEnemyWaiver( Action ac )
    {
        this.enemyAction = ac;
    }
}
