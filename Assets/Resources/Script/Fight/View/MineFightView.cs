using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
public class MineFightView : MonoBehaviour {

    ShakeMainPanel shake = new ShakeMainPanel(FightView.shake);

    public Text mineCardNumsObj;
    public Text hpObj;
    //场上征战的我方卡牌  //int 为pos
    private static Dictionary<int, Card> viewCardDic = new Dictionary<int, Card>();

    //5个卡位坐标
    private static Dictionary<int, Vector2> viewPosList = new Dictionary<int, Vector2>();
       
    //卡牌的父节点
    private static GameObject parentGame = null;

    private static float doSize = 1.0f;

    private static List<Card> choiceCardList = new List<Card>();

    private static Text _mineCardNumsObj;
    private static Text _hpObj;

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
       // StartCoroutine(atkCard);
        MineFightController.getInstance().initMineFightData();
        _mineCardNumsObj = mineCardNumsObj;
        _mineCardNumsObj.text = "剩余卡牌数量" + MineFightData.getInstance().cardsNumber().ToString();

        _hpObj = hpObj;
        _hpObj.text = MineFightData.getInstance().getHp().ToString();

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

                EventListener.Get(card.getObj()).onDown = BtnCallBack;
         }
         outCardPosY = outCardPosY * doSize;
	}

     private void BtnCallBack(GameObject obj)
     {
         if (FightController.getInstance().RightToPlay == FightController.RIGHTTOPLAY.MINE)
         {
             Card card = obj.gameObject.GetComponent<Card>();
             if (card.getCardState() == Card.CardState.waitFight)
             {
                 card.setCardState(Card.CardState.none);
                 cardToNonePos(card.getCardData().Pos);
             }
             else if (card.getCardState() == Card.CardState.none)
             {
                 card.setCardState(Card.CardState.waitFight);
                 cardToWaitPos(card.getCardData().Pos);
             }
         }
     }

    //遭受攻击
    static public void Atkend()
    {
        _hpObj.text = MineFightData.getInstance().getHp().ToString();
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

        _mineCardNumsObj.text = "剩余卡牌数量" + (MineFightData.getInstance().cardsNumber() + MineFightData.getInstance().usingCardsNumber()).ToString();
         
        shake();
    }

    //将卡牌移动到待战位置
    private void cardToWaitPos( int pos )
    {
        Card card = viewCardDic[pos];
        card.setCardState(Card.CardState.waitFight);
        GameObject cardObj = card.getObj();
        cardObj.GetComponent<RectTransform>().DOLocalMoveY(viewPosList[pos].y + outCardPosY, 0.1f);   
    }

    //将卡牌移动到卡槽位置
    private void cardToNonePos(int pos)
    {
        Card card = viewCardDic[pos];
        card.setCardState(Card.CardState.none);
        GameObject cardObj = card.getObj();
        cardObj.GetComponent<RectTransform>().DOLocalMoveY(viewPosList[pos].y, 0.1f);
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
     
        //场上必须得有卡牌才能响应
        if (MineFightData.getInstance().usingCardsNumber() > 0 && FightController.getInstance().RightToPlay == FightController.RIGHTTOPLAY.MINE )
        {
            if (Const.FIGHT_BTN_TYPE.NO_OUT == state)
            {
                noOut();
            }
            else if (Const.FIGHT_BTN_TYPE.TIP == state)
            {
                noOut();
                int posRand = Random.Range(1, 6);
                cardToWaitPos(posRand); 
            }
               
            //出击
            else if (Const.FIGHT_BTN_TYPE.ATK == state)
            {
                atkCard();
            }
        }
    }


    IEnumerator atkCard()
    {
        for (int i = 1; i <= Const.FRIST_CARD_NUM; ++i)
        {
            if (viewCardDic.ContainsKey(i) && viewCardDic[i].getCardState() == Card.CardState.waitFight)
            {
                viewCardDic[i].getObj().GetComponent<RectTransform>().DOLocalMove(FightUIData.getInstance().EnemyVec3, 0.5f);
                viewCardDic[i].getObj().transform.SetAsLastSibling();
                FightController.getInstance().RightToPlay = FightController.RIGHTTOPLAY.MINEING;
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
    public  void createCardObject( int pos )
    {
        if (MineFightData.getInstance().cardsNumber() > 0)
        {
            CardData cardData = MineFightController.getInstance().substitute(pos);
            Card card = Card.create(cardData);
            card.setParent(parentGame);
            card.setPosition(new Vector3(Screen.width, 0, 0));
            card.setScale(doSize);
            card.moveTo(viewPosList[pos], () =>
            {
                FightController.getInstance().RightToPlay = FightController.RIGHTTOPLAY.ENEMY; 
            });
            viewCardDic.Add(pos, card);
            EventListener.Get(card.getObj()).onClick = BtnCallBack;
        }        
    }  
}
