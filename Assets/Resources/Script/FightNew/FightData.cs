using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/**战斗中的战斗数据
* 作者:tony  */
public class FightData : MonoBehaviour {

    private int _cardMaxNum = 54;
    private int _mineMultiple = 0; 
    private int _enemyMultiple = 0;

    //伤害倍数上限
    [SerializeField]
    private int MultipleLimit = 3;

    //我方战力倍数
    public int mineMultiple { set { this._mineMultiple = value; }  get { return this._mineMultiple; } }
    //敌方战力倍数
    public int enemyMultiple { set { this._enemyMultiple = value; } get { return this._enemyMultiple; } }

    //一副牌的所有标识
    List<int> listCard = new List<int>();

    //我方手上的牌
    List<int> mineListCard = new List<int>();

    //敌方手上的牌
    List<int> enemyListCard = new List<int>();

    void Awake()
    {
        CreateAPair();
    }

    /// <summary>
    /// 创建一副牌
    /// </summary>
    public void CreateAPair()
    {
        listCard.Clear();
        foreach (KeyValuePair<int , CardDetail > item in ConfigDatas.Inst.card.GetAllCard())
        {
            listCard.Add(item.Key);
        } 
    }

    /// <summary>
    /// 随机出一张牌
    /// </summary>
    public int RandCard()
    {
        if(listCard.Count > 0 )
        {
            int randNumber = Random.Range(0, listCard.Count);
            int cardId = listCard[randNumber];
            listCard.Remove(cardId);
            return cardId;
        } 
        return -1;
    }

    /// <summary>
    /// 我方随机牌
    /// </summary>
    /// <returns></returns>
    public int MineRandCard()
    {
        int cardId = this.RandCard();
        mineListCard.Add(cardId);
        return cardId;
    }

    /// <summary>
    /// 我方随机牌
    /// </summary>
    /// <returns></returns>
    public int EnemyRandCard()
    {
        int cardId = this.RandCard();
        enemyListCard.Add(cardId);
        return cardId;
    }

    /// <summary>
    /// 是否还有牌可以拿
    /// </summary>
    /// <returns></returns>
    public bool IsHaveCard()
    {
        return this.listCard.Count > 0;
    }

    /// <summary>
    /// 牌库还剩多少卡牌
    /// </summary>
    /// <returns></returns>
    public int CardNumber()
    {
        return this.listCard.Count;
    }

    /// <summary>
    /// 换牌
    /// </summary>
    public List<int> Replacements(List<int> vList)
    {
        for (int i = 0; i < vList.Count; ++i)
        {
            int cardId = vList[i]; 
            this.listCard.Add(cardId);
        }

        List<int> temp = new List<int>();
        for (int i = 0; i < vList.Count; ++i)
        {
            temp.Add(this.RandCard());
        }
        return temp;
    }

    /// <summary>
    /// 我方换牌
    /// </summary>
    /// <returns></returns>
    public List<int> MineReplacements()
    {
        List<int> temp = this.Replacements(mineListCard);
        mineListCard.Clear();
        mineListCard = temp;
        if( this._mineMultiple < this.MultipleLimit )
        {
            this._mineMultiple++;
        }
        return mineListCard;
    }

    /// <summary>
    /// 敌方换牌
    /// </summary>
    /// <returns></returns>
    public List<int> EnemyReplacements()
    {
        List<int> temp = this.Replacements(enemyListCard);
        enemyListCard.Clear();
        enemyListCard = temp;
        if (this._enemyMultiple < this.MultipleLimit)
        {
            this._enemyMultiple++;
        }
        return mineListCard;
    }
}
