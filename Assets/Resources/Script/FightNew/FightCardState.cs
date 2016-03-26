using UnityEngine;
using System.Collections;

public class FightCardState : MonoBehaviour {
     
    //状态
    public Const.CardState state = Const.CardState.none;

    /// <summary>
    /// 修改状态
    /// </summary>
    /// <param name="vState"></param>
    public void Set(Const.CardState vState )
    {
        this.state = vState;
    }
}
