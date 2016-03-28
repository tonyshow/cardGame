using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
/**战斗的卡牌位置和缩放计算
作者:tony
*/
public class FightCardTransfrom : MonoBehaviour {

    //战斗区域对象
    [SerializeField]
    GameObject FightPanel;

    //我方卡牌的缩放比例
    float mineCardScale = -1.0f;

    //敌方卡牌的缩放比例
    float enemyCardScale = -1.0f;

    //卡牌的尺寸
    Vector2 cardSize = new Vector2();

    //作战总区域的尺寸
    Vector2 fightSize = new Vector2();

    //我方作战区域
    Vector2 mineFightSize = new Vector2();

    //敌方作战区域
    Vector2 enemyFightSize = new Vector2();

    //我方卡牌预设
    [SerializeField]
    GameObject mineCardPrefabs;

    //敌方卡牌预设
    [SerializeField]
    GameObject enemyCardPrefabs;

    //单方持有卡牌上限
    [SerializeField]
    int cardMaxNum = 5;

    /// 敌人卡牌占中间高度的百分比
    [SerializeField]
    float per = 0.5f;

    //我方纵轴坐标
    float mineY;

    //敌方纵轴坐标
    float enemyY;

    //我方卡牌间隙
    float mineSpace;

    //敌方卡牌间隙
    float enemySpace;

    //我方卡牌位置
    List<Vector2> mindPosList = new List<Vector2>();

    //敌方卡牌位置
    List<Vector2> enemyPosList = new List<Vector2>();

   
    void Start()
    {
        fightSize = new Vector2(FightPanel.transform.GetComponent<RectTransform>().rect.width, FightPanel.transform.GetComponent<RectTransform>().rect.height);
        cardSize = mineCardPrefabs.transform.GetComponent<RectTransform>().sizeDelta;

        mineFightSize = new Vector2(fightSize.x, fightSize.y * (1 - per));
        enemyFightSize = new Vector2(fightSize.x, fightSize.y * per);

        this.getMineCardScale();
        this.getEnemyCardScale();
        this.doSize();

        mindPosList.Clear();
        enemyPosList.Clear();
        for (int i = 0; i < cardMaxNum; ++i)
        {
            Vector2 vMine = new Vector2(-fightSize.x * 0.5f + (mineCardScale * cardSize.x) * (i + 0.5f) + mineSpace * (i + 1), mineY);
            mindPosList.Add(vMine);

            Vector2 vEnemy = new Vector2(-fightSize.x * 0.5f + (enemyCardScale * cardSize.x) * (i + 0.5f) + enemySpace * (i + 1), enemyY);
            enemyPosList.Add(vEnemy);
        } 
        FightWindow fightWindow = this.GetComponent<FightWindow>(); 
    }

    /// <summary>
    /// 设置卡牌大小位置
    /// </summary>
    /// <param name="vTransform"></param>
    public void SetTransform( Transform vTransform , int vPos )
    {
        vTransform.gameObject.SetActive(false);
        vTransform.SetParent(this.FightPanel.transform);
        vTransform.GetComponent<RectTransform>().anchoredPosition = mindPosList[vPos];
        vTransform.GetComponent<RectTransform>().sizeDelta = vTransform.GetComponent<RectTransform>().sizeDelta * mineCardScale;
        vTransform.localScale = Vector3.one;
        vTransform.SetAsLastSibling();
    }

    /// <summary>
    /// 计算y坐标
    /// </summary>
    void doSize()
    {
        this.enemyY = fightSize.y * (0.5f - per) + fightSize.y * per * 0.5f;
        this.mineY = fightSize.y * (0.5f - per) - fightSize.y * (1 - per) * 0.5f; 
        mineSpace = (fightSize.x - cardMaxNum * mineCardScale * cardSize.x) / (cardMaxNum + 1.0f);
        enemySpace = (fightSize.x - cardMaxNum * enemyCardScale * cardSize.x) / (cardMaxNum + 1.0f);
    }

    void Update()
    {
        #if (UNITY_EDITOR)
            //this.doSize();
        #endif
    }

    /// <summary>
    /// 计算我方卡牌缩放比例
    /// </summary>
    void getMineCardScale()
    {
        if (mineFightSize.x / mineFightSize.y > cardSize.x * cardMaxNum / cardSize.y)
        {
            mineCardScale = mineFightSize.y / cardSize.y;
        }
        else
        {
            mineCardScale = mineFightSize.x / (cardSize.x * cardMaxNum);
        }
    }

    /// <summary>
    /// 计算我方卡牌缩放比例
    /// </summary>
    void getEnemyCardScale()
    {
        if (enemyFightSize.x / enemyFightSize.y > cardSize.x * cardMaxNum / cardSize.y)
        {
            enemyCardScale = enemyFightSize.y / cardSize.y;
        }
        else
        {
            enemyCardScale = enemyFightSize.x / (cardSize.x * cardMaxNum);
        }
    }

    /// <summary>
    /// 获得战斗单方卡牌上限
    /// </summary>
    /// <returns></returns>
    public int GetCardMaxNum()
    {
        return this.cardMaxNum;
    }

    /// <summary>
    /// 我方卡牌位置集合
    /// </summary>
    /// <returns></returns>
    public float GetPos(int vPos)
    {
        return mindPosList[vPos].x;
    }

}
