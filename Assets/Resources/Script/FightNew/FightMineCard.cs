using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/** 我方卡牌数据
 * 作者：tony */
public class FightMineCard : MonoBehaviour {

    //卡牌预设
    [SerializeField]
    GameObject CardPrefabs;

    [SerializeField]
    FightState fightState;
  
    //卡牌坐标管理
    [SerializeField]
    FightCardTransfrom fightCardTransfrom;

    //战斗卡牌数据管理
    [SerializeField]
    FightData fightData; 

    //卡牌对象集合int为卡牌id，GameObject为卡牌对象
    Dictionary<int, GameObject> dicCardObj = new Dictionary<int, GameObject>();

    //当前战场空位    key为战场位置标号 
    Dictionary<int, int> listSpcaePos = new Dictionary<int, int>();

    //目标位置
    [SerializeField]
    Transform targetTransform;

    /**卡牌攻击间隔*/
    [SerializeField]
    float atkSpaceTime = 0.2f;

    /**卡牌发牌间隔*/
    [SerializeField]
    float playingTime = 0.2f;

    /// <summary>
    /// 创建战斗卡牌
    /// </summary>
    /// <param name="vCardId"></param>
    public void CreateCard( int vPos , float vPlayingTime )
    {
        if ( this.listSpcaePos.ContainsKey(vPos) )
        {
            //从牌库中获取卡牌id
            int cardId = this.fightData.RandCard();

            //卡牌对象
            GameObject CardObj = GameObject.Instantiate(CardPrefabs) as GameObject;

            //设置卡牌对象上的数据
            FightCard fightCard = CardObj.GetComponent<FightCard>();
            fightCard.pos = vPos;
            CardDetail cardDetail = ConfigDatas.Inst.card.GetCardDetail(cardId);
            fightCard.SetCardDetail(cardDetail);

            //设置卡牌UI数据
            CardPrefabsCtr cardPrefabsCtr = CardObj.GetComponent<CardPrefabsCtr>();
            cardPrefabsCtr.SetCard(cardDetail);

            //设置卡牌位置
            fightCardTransfrom.SetTransform(CardObj.transform, vPos);

            //设置攻击位置
            FightAtkAnim atkAnim = CardObj.GetComponent<FightAtkAnim>();
            atkAnim.SetTragetPos(targetTransform.position);

            //卡牌动画
            FightCardAnim cardAnim = CardObj.GetComponent<FightCardAnim>();
            StartCoroutine(cardAnim.Licensing(vPlayingTime));

            Button btn = CardObj.transform.GetComponent<Button>(); 
            btn.onClick.AddListener(delegate ()
            {
                EveCard(CardObj);
            }); 
            dicCardObj.Add(cardId, CardObj);
        }
    }

    /// <summary>
    /// 创建首发卡牌
    /// </summary>
    public void CreateFristCard()
    {
        for (int i = 0; i < 5; ++i)
        {
            this.listSpcaePos.Add(i, i);
            this.CreateCard(i,i* playingTime);
            this.listSpcaePos.Remove(i);
        }
    }

    /// <summary>
    /// 卡牌点击事件响应
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="vFightCardState"></param>
    void EveCard(GameObject vObj )
    {
        FightCard vFightCard = vObj.GetComponent<FightCard>();
        FightCardAnim vAnim = vObj.GetComponent<FightCardAnim>();
        if (vFightCard.postionState == FightCard.PostionState.Unchecked )
        { 
            vAnim.PlayToWait();
            vFightCard.postionState = FightCard.PostionState.choice;
            return;
        }
        else
        {
            vAnim.PlayToInitPos();
            vFightCard.postionState = FightCard.PostionState.Unchecked;
            return;
        }
    }

    /// <summary>
    /// 执行攻击
    /// </summary>
    /// <returns></returns>
    public IEnumerator DoAtk()
    { 
        List<int> canAtkCardList = new List<int>();
        foreach( KeyValuePair<int , GameObject> item in dicCardObj)
        {
            FightCard vFightCard = item.Value.GetComponent<FightCard>();
            if(vFightCard.postionState == FightCard.PostionState.choice )
            { 
                FightAtkAnim atkAnim = item.Value.GetComponent<FightAtkAnim>();
                atkAnim.PlayAtk(canAtkCardList.Count * atkSpaceTime);
                canAtkCardList.Add(vFightCard.GetCardDetail().id);
                if (!listSpcaePos.ContainsKey(vFightCard.pos))
                {
                    listSpcaePos.Add(vFightCard.pos, vFightCard.pos);
                }
            }
           
        }
        if(canAtkCardList.Count == 0 )
        {
            MsgPrompts.create("未选择一张卡片");
        }
        else
        {
            for(int i = 0; i<canAtkCardList.Count; ++i)
            {
                this.dicCardObj.Remove(canAtkCardList[i]);
            }
            yield return new WaitForSeconds(canAtkCardList.Count * atkSpaceTime + 1); 
            this.MineLicensing();

            //this.fightState.Waiver();
        } 
    }

    /// <summary>
    /// 给我方补发牌
    /// </summary>
    void MineLicensing()
    {
        if (this.fightData.IsHaveCard())
        {
            int number = 0;
            foreach (KeyValuePair<int,int> item in this.listSpcaePos )
            { 
                this.CreateCard(item.Key, number* playingTime);
                ++number;
            } 
            this.listSpcaePos.Clear();
        }
    }
}
