using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
public class EnemyFightView : MonoBehaviour {

    //场上征战的敌方卡牌  //int 为pos
    private static Dictionary<int, Card> viewCardDic = new Dictionary<int, Card>();

    //5个卡位坐标
    private static Dictionary<int, Vector2> viewPosList = new Dictionary<int, Vector2>();
 
    //卡牌的父节点
    private static GameObject parentGame = null;

    private static float doSize = 1.0f;

    private static float enemyCardScale = 0.5f;
    //单列模式
    public static EnemyFightView instance = null;
    public static EnemyFightView getInstance()
    {
        if (instance == null)
        {
            instance = new EnemyFightView();
        }
        return instance;
    }

	// Use this for initialization
	void Start () 
    {
        EnemyFightController.getInstance().initEnemyFightData();

         parentGame = this.gameObject;
         //初始化摆位

         //卡牌父节点尺寸
         Vector2 vec2 = this.gameObject.transform.GetComponent<RectTransform>().sizeDelta;

         //卡牌节点尺寸
         float itemWidth = 0.0f;

         float itemHeight = 0.0f;

         //卡牌间的距离间距
         float space = 20.0f;

         //卡牌加间隙占的总宽度
         float allCardWidth = 0.0f;

         //第一张卡牌的坐标位置
         float fristX = 0.0f;
        
         doSize = 1.0f;
         
         float middPanelHeight = FightUIData.getInstance().getMiddHeight();
       
         //初始化首发卡牌对象
         Dictionary<int, CardData>  cardDataDic = EnemyFightController.getInstance().getFristCards();
         for (int i = 1; i <= cardDataDic.Count; i++)
         {
               CardEnemy card = CardEnemy.create(cardDataDic[i]);
                card.setParent( this.gameObject );
              
                if (1 == i)
                {
                    itemWidth = card.getSize().x;
                    itemHeight = card.getSize().y; 
                    if (AdaptationResolutionMG.getInstance().getDeviceSizeType() == DEVICE_SIZE_TYPE.Leng)
                    {
                        doSize = AdaptationResolutionMG.getInstance().getRawWidth() / (float)Screen.width;
                    }
                    else if (AdaptationResolutionMG.getInstance().getDeviceSizeType() == DEVICE_SIZE_TYPE.Compare)
                    {
                        doSize = AdaptationResolutionMG.getInstance().getDoSizeWidth();
                    }
                    else
                    {
                        doSize = Screen.width / (float)(itemWidth * Const.FRIST_CARD_NUM + space * (Const.FRIST_CARD_NUM - 1));    
                    }

                    allCardWidth = (itemWidth * Const.FRIST_CARD_NUM) + (Const.FRIST_CARD_NUM - 1) * space;
                    allCardWidth = allCardWidth * doSize;
                    //表示已经超出屏幕 在现有的基础上再一次缩放
                    if (Screen.width < allCardWidth)
                    {
                        doSize = doSize * Screen.width / allCardWidth;
                    }
                    itemWidth = itemWidth * doSize;
                    itemHeight = itemHeight * doSize;
                    space = space * doSize;
                     
                    allCardWidth = (itemWidth * Const.FRIST_CARD_NUM) + (Const.FRIST_CARD_NUM - 1) * space;
                     
                    fristX = ( Screen.width - allCardWidth ) * 0.5f - Screen.width * 0.5f + itemWidth * 0.5f;
                }

                card.setScale(doSize * enemyCardScale);

                int key = cardDataDic[i].Pos;
                
                viewPosList.Add(key,new Vector2(fristX + (i-1) * (itemWidth + space), -middPanelHeight * 0.5f + itemHeight * 1.3f));

                viewCardDic.Add(key, card);

                card.setPosition(viewPosList[i]);  
         } 
	}

    public void Update()
    {
        if (FightController.getInstance().RightToPlay == FightController.RIGHTTOPLAY.ENEMY)
        {
            FightController.getInstance().RightToPlay = FightController.RIGHTTOPLAY.MINE;
            Debug.Log("敌人交出出牌权");
        }
    }

    public void DestroyCardsObject(CardEnemy card)
    {
        int pos = card.cardData.Pos;
        int id = viewCardDic[pos].getCardData().getId();
        //清理数据
        EnemyFightController.getInstance().destroy(id);
  
        card.DestoryCard(); 

        //清理对象
        viewCardDic.Remove(pos);

        createCardObject(pos);
    }

    
    public  void createCardObject( int pos )
    {
        CardData cardData =  EnemyFightController.getInstance().substitute(pos);
        CardEnemy card = CardEnemy.create(cardData);
        card.setParent(parentGame);
        card.setScale(doSize * enemyCardScale);
        card.moveTo(viewPosList[pos]);   
        viewCardDic.Add(pos, card);
    }  
}
