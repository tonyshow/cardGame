using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
public class MineFightView : MonoBehaviour {

    //场上征战的我方卡牌  //int 为pos
    private static Dictionary<int, Card> viewCardDic = new Dictionary<int, Card>();

    //5个卡位坐标
    private static Dictionary<int, Vector2> viewPosList = new Dictionary<int, Vector2>();

    //int 为pos
    //private static Dictionary<int, CardData> cardDataDic = null;

    //卡牌的父节点
    private static GameObject parentGame = null;

    private static float doSize = 1.0f;

    private static List<Card> choiceCardList = new List<Card>();

    float outCardPosY = 6.0f;//待出卡牌的位置
    //单列模式
    public static MineFightView instance = null;
    public static MineFightView getInstance()
    {
        if (instance == null)
        {
            instance = new MineFightView();
        }
        return instance;
    }

	// Use this for initialization
	void Start () 
    { 
         MineFightController.getInstance().initMineFightData();

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
         Dictionary<int, CardData>  cardDataDic = MineFightController.getInstance().getFristCards();
         for (int i = 1; i <= cardDataDic.Count; i++)
         { 
                Card card = Card.create( cardDataDic[i] );
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

                card.setScale(doSize);

                int key = cardDataDic[i].Pos;
                
                viewPosList.Add(key,new Vector2(fristX + (i-1) * (itemWidth + space), -middPanelHeight * 0.5f + itemHeight * 0.5f));

                viewCardDic.Add(key, card);

                card.setPosition(viewPosList[i]);  
         }
         outCardPosY = outCardPosY * doSize;
	}
    public void DestroyCardsObject(Card card)
    {
        int pos = card.cardData.Pos;
        int id = viewCardDic[pos].getCardData().getId();
        //清理数据
        MineFightController.getInstance().destroy(id);

        card.DestoryCard();
         
        //移除表现对象
        viewCardDic.Remove(pos);

        createCardObject(pos);
    }

     
    private void noOut()
    {
        for (int i = 1; i <= Const.FRIST_CARD_NUM; ++i)
        {
            if ( viewCardDic.ContainsKey(i) )
            {
                viewCardDic[i].setCardState(Card.CardState.none);
                viewCardDic[i].getObj().GetComponent<RectTransform>().anchoredPosition3D = viewPosList[i];
            }
        } 
    }
    //处理战斗view的事件
    public  void FightViewTouchFn( Const.FIGHT_BTN_TYPE state)
    {        
        if (Const.FIGHT_BTN_TYPE.NO_OUT == state)
        {
            noOut();
        }
        else if (Const.FIGHT_BTN_TYPE.TIP == state)
        {
            noOut();
            int posRand = Random.Range(1, 6);     
            Card card = viewCardDic[posRand];
            card.setCardState(Card.CardState.waitFight);
            GameObject cardObj = card.getObj(); 
            cardObj.GetComponent<RectTransform>().DOLocalMoveY(viewPosList[posRand].y + outCardPosY, 0.3f);     
        }
        else if (Const.FIGHT_BTN_TYPE.ATK == state)
        {
            for (int i = 1; i <= Const.FRIST_CARD_NUM; ++i)
            { 
                if ( viewCardDic.ContainsKey(i) && viewCardDic[i].getCardState() == Card.CardState.waitFight )
                {
                    int pos = viewCardDic[i].getCardData().Pos;                    
                    viewCardDic[i].getObj().GetComponent<RectTransform>().DOLocalMoveY(viewPosList[pos].y + outCardPosY * 20 , 0.3f) ;      
                }  
            }
          
        }
    }

    public  void createCardObject( int pos )
    {
        CardData cardData =  MineFightController.getInstance().substitute(pos);
        Card card = Card.create( cardData );
        card.setParent(parentGame);
        card.setScale(doSize);
        card.moveTo(viewPosList[pos]);
        viewCardDic.Add(pos, card);
    }  
}
