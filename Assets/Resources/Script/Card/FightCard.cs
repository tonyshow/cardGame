using UnityEngine;
using System.Collections;
/** 战斗卡牌类
 * 作者：tony */
public class FightCard : MonoBehaviour {

    //位置状态
    public enum PostionState
    {
        //未选中
        Unchecked = 0,
        //选中
        choice = 1,
    }

    //是否可选择出战
    public bool _isCanChoice = true;

    //位置状态
    PostionState _postionState = PostionState.Unchecked;

    //卡牌数据
    CardDetail cardDetail;
     
    int _pos;
     
    int _hurtMultiple = 1;

    /// <summary>
    /// 设置卡牌数据
    /// </summary>
    /// <param name="vCardDetail"></param>
    public void SetCardDetail(CardDetail vCardDetail)
    {
        this.cardDetail = vCardDetail;
    }

    /// <summary>
    /// 获得卡牌数据
    /// </summary>
    /// <returns></returns>
    public CardDetail GetCardDetail()
    {
        return this.cardDetail;
    }

    /// <summary>
    /// 位置状态
    /// </summary>
    public PostionState postionState { set { this._postionState = value; } get { return this._postionState; } }

    /// <summary>
    /// 位置
    /// </summary>
    public int pos { set { this._pos = value; } get { return this._pos; } }

    /// <summary>
    /// 伤害倍数
    /// </summary>
    public int hurtMultiple { set { this._hurtMultiple = value; } get { return this._hurtMultiple; } }

    public int GetHurt()
    {
        return this.cardDetail.number * this._hurtMultiple;
    }
}
