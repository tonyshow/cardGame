using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class FightWindow : MonoBehaviour
{
 
    //我方卡牌预设
    [SerializeField]
    GameObject mineCardPrefabs;

    //敌方卡牌预设
    [SerializeField]
    GameObject enemyCardPrefabs;

    //敌方目标位置
    [SerializeField]
    Transform enemyTransform;

    //战斗卡牌的属性
    FightCardTransfrom fightCardTransfrom;

    //战斗卡牌的攻击动作
    FightCardAnim fightCardAnim;

    //战斗数据
    FightData fightData;

    //战斗中我方卡牌库 key 为位置
    Dictionary<int, GameObject> dicMineCard = new Dictionary<int, GameObject>();

    //key 为位置待出战的卡牌位置
    List<int> listChoiceCard = new List<int>();
    List<int> listSpcaePos = new List<int>();

    /**卡牌攻击间隔*/
    [SerializeField]
    float atkSpaceTime = 0.2f;

    void Awake()
    {
        this.fightCardTransfrom = this.GetComponent<FightCardTransfrom>();
        this.fightData = this.GetComponent<FightData>();
    }
    
    /// <summary>
    /// 创建我方卡牌
    /// </summary>
    void CreateMineCard( int vPos , int vCardId , float vDelayTime )
    { 
        GameObject obj = GameObject.Instantiate(mineCardPrefabs) as GameObject;
        CardPrefabsCtr cardPrefabsCtr = obj.GetComponent<CardPrefabsCtr>(); 
        cardPrefabsCtr.SetCard(ConfigDatas.Inst.card.GetCardDetail(vCardId));
        if (dicMineCard.ContainsKey(vPos))
        {
            dicMineCard[vPos] = obj;
        }  
        else
        {
            dicMineCard.Add(vPos, obj);
        }      
        this.fightCardTransfrom.SetTransform(obj.transform, vPos);
        obj.transform.SetAsLastSibling();
        FightAtkAnim atkAnim = obj.GetComponent<FightAtkAnim>();
        atkAnim.SetTragetPos(enemyTransform.position);
        
        Button btn = obj.transform.GetComponent<Button>();
        int pos = vPos;
        btn.onClick.AddListener(delegate ()
        {
            EveCard(obj, pos);
        }); 

        FightCardAnim cardAnim = obj.GetComponent<FightCardAnim>();
        StartCoroutine( cardAnim.Licensing(vDelayTime) );
    }

    /// <summary>
    /// 创建敌方卡牌
    /// </summary>
    void createEnemyCard(int pos)
    {
        GameObject.Instantiate(mineCardPrefabs);
    }

    public void InitCard()
    {
        for( int i = 0; i < fightCardTransfrom.GetCardMaxNum(); ++i )
        { 
            if(dicMineCard.ContainsKey(i))
            {
                GameObject.DestroyObject(dicMineCard[i]);
                dicMineCard.Remove(i);
            }
            int cardId = fightData.MineRandCard();
            CreateMineCard(i, cardId , i * 0.1f );
        }
    }

    /// <summary>
    /// 卡牌点击事件响应
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="vFightCardState"></param>
    void EveCard(GameObject obj , int vPos )
    {
        FightCardState FightCardState = obj.transform.GetComponent<FightCardState>();
        if(FightCardState.state == Const.CardState.none)
        {
            FightCardState.state = Const.CardState.waitFight;
            FightCardAnim anim = obj.transform.GetComponent<FightCardAnim>();
            anim.PlayToWait();
            listChoiceCard.Add(vPos);
            return;
        }
        else
        {
            FightCardState.state = Const.CardState.none;
            FightCardAnim anim = obj.transform.GetComponent<FightCardAnim>();
            anim.PlayToInitPos();
            listChoiceCard.Remove(vPos);
            return;
        }
    }

    /// <summary>
    /// 执行攻击
    /// </summary>
    /// <returns></returns>
    IEnumerator DoAtk()
    {
        int cardNumber = listChoiceCard.Count;
        if (cardNumber > 0)
        {  
            for (int i = 0; i < cardNumber; ++i)
            {
                int id = listChoiceCard[i];
                FightAtkAnim atkAnim = dicMineCard[id].GetComponent<FightAtkAnim>();
                atkAnim.PlayAtk(i * atkSpaceTime);
                dicMineCard.Remove(id);
                listSpcaePos.Add(id);
            }  
            listChoiceCard.Clear();
        }
        else
        {
            MsgPrompts.create("未选择一张卡片");
        }
        yield return new WaitForSeconds(cardNumber* atkSpaceTime + 1); 
        this.MineLicensing();
    }

    /// <summary>
    /// 攻击按钮事件回调
    /// </summary>
    public void EveAtk()
    {
        StartCoroutine(this.DoAtk());
    }

    /// <summary>
    /// 蓄力按钮事件回调
    /// </summary>
    public void EveStorage()
    {
        MsgPrompts.create("蓄力");
        for (int i = 0; i < listChoiceCard.Count; ++i)
        {
            FightCardAnim anim = dicMineCard[listChoiceCard[i]].GetComponent<FightCardAnim>();
            anim.PlayToInitPos();
        }
        listChoiceCard.Clear(); 
    }

    /// <summary>
    /// 给我方发牌
    /// </summary>
    void MineLicensing()
    {
        if (fightData.IsHaveCard())
        {
            for (int i = 0; i < listSpcaePos.Count; ++i)
            {
                int pos = listSpcaePos[i];
                int cardId = fightData.MineRandCard();
                if(cardId> 0 )
                {
                    CreateMineCard(pos, cardId, i * 0.1f);
                }                
            }
            listSpcaePos.Clear();
        }
    }
}
