using UnityEngine;
using System.Collections;

/** 战斗状态 
 * 作者：tony */
public class FightState : MonoBehaviour {

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

    /// <summary>
    /// 交出出牌权
    /// </summary>
    public void Waiver()
    {
        this._rightToPlay = RightToPlay.enemy;
    }
}
