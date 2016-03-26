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

    static Dictionary<OUTTYPE, List<List<int>>> listOutCard = new Dictionary<OUTTYPE, List<List<int>>>();

    //当前出牌组
    static List<List<int>> currOutCardRule = new List<List<int>>();
    //卡牌的父节点
    private static GameObject parentGame = null;

    private static float doSize = 1.0f;
     

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

	//清理战斗中的静态数据
    void clearnDictionary()
    {
        viewCardDic.Clear();
        viewPosList.Clear();
        listOutCard.Clear();
        currOutCardRule.Clear();
    }
	// Use this for initialization
	void Start () 
    {
        clearnDictionary();
        MineFightController.getInstance().initMineFightData();
        _mineCardNumsObj = mineCardNumsObj;
        _mineCardNumsObj.text = MineFightData.getInstance().cardsNumber().ToString();

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

         initOutCardRule();
	}

	//按钮点击回调
     private void BtnCallBack(GameObject obj)
     {
         Debug.Log("sss");
         if (FightController.getInstance().RightToPlay == FightController.RIGHTTOPLAY.MINE)
         {
             Card card = obj.gameObject.GetComponent<Card>();
             if (card.getCardState() == Card.CardState.waitFight)
             {
                 card.Set(Card.CardState.none);
                 cardToNonePos(card.getCardData().Pos);
             }
             else if (card.getCardState() == Card.CardState.none)
             {
                 card.Set(Card.CardState.waitFight);
                 cardToWaitPos(card.getCardData().Pos);
             }
         }
     }

    //遭受攻击
    static public void Atkend() 
    {
        _hpObj.text = MineFightData.getInstance().getHp().ToString();
        FightController.getInstance().RightToPlay = FightController.RIGHTTOPLAY.MINE;
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

        _mineCardNumsObj.text = (MineFightData.getInstance().cardsNumber() + MineFightData.getInstance().usingCardsNumber()).ToString();
         
        shake();
    }

    //将卡牌移动到待战位置
    private void cardToWaitPos( int pos )
	{   
        Card card = viewCardDic[pos];
		card.Set(Card.CardState.waitFight);
        GameObject cardObj = card.getObj();
		cardObj.GetComponent<RectTransform>().DOLocalMoveY(outCardPosY, 0.1f).SetUpdate(true).SetRelative().SetRecyclable();
		cardObj.GetComponent<RectTransform>().DOShakeScale(0.5f, 0.1f,10,1).SetUpdate(true).SetRelative();
    }

    //将卡牌移动到初始位置
    private void cardToNonePos(int pos)
    {
        Card card = viewCardDic[pos];
        card.Set(Card.CardState.none);
        GameObject cardObj = card.getObj();
		cardObj.GetComponent<RectTransform>().DOLocalMoveY(viewPosList[pos].y, 0.1f).SetUpdate(true);
    }

	//不出
    private void noOut()
    {
        for (int i = 1; i <= Const.FRIST_CARD_NUM; ++i)
        {
            if ( viewCardDic.ContainsKey(i) )
            {
                viewCardDic[i].Set(Card.CardState.none);
                viewCardDic[i].getObj().GetComponent<RectTransform>().anchoredPosition3D = viewPosList[i];
                Card card = viewCardDic[i].getObj().GetComponent<Card>();
                card.Set(Card.CardState.none);
            }
        } 
    }

    //初始化出牌规则
    private void initOutCardRule()
    {

        listOutCard = OutCardRule.getInstance().getTip(OUTCARDRULE.ONE, viewCardDic);
        currOutCardRule.Clear();
    }

    //获得一组出牌规则
    private List<List<int>> getItemOutCardRule()
    {
        if (listOutCard.Count == 0)
        {
            initOutCardRule();
        }
        OUTTYPE key = OUTTYPE.None;
        foreach (var _key in listOutCard.Keys)
        {
            key = _key;
            break;
        }
        List<List<int>> temp = listOutCard[key];
        listOutCard.Remove(key);
        return temp;
    }

    //处理战斗view的事件
    public  IEnumerator FightViewTouchFn( Const.FIGHT_BTN_TYPE state)
    {
       
        //场上必须得有卡牌才能响应
        if (MineFightData.getInstance().usingCardsNumber() > 0 && FightController.getInstance().RightToPlay == FightController.RIGHTTOPLAY.MINE )
        {
            if (Const.FIGHT_BTN_TYPE.NO_OUT == state)
            {
                noOut();
				FightController.getInstance().RightToPlay = FightController.RIGHTTOPLAY.ENEMY; 
            }
            else if (Const.FIGHT_BTN_TYPE.TIP == state)
            {
                noOut();
                if (currOutCardRule.Count == 0)
                {
                    currOutCardRule = getItemOutCardRule();
                }
                if (currOutCardRule.Count > 0)
                { 
                    List<int> l = new List<int>();       
                    l = currOutCardRule[0];
                    currOutCardRule.RemoveAt(0);
                    foreach (var pos in l)
                    {
                        cardToWaitPos(pos);
                    } 
                } 
            } 
            //出击
            else if (Const.FIGHT_BTN_TYPE.ATK == state)
            {   
				//此时是否有卡片可以出
				bool isHaveCardCanOut = false;
                for (int i = 1; i <= Const.FRIST_CARD_NUM; ++i)
                {
                    if (viewCardDic.ContainsKey(i) && viewCardDic[i].getCardState() == Card.CardState.waitFight)
                    {
						isHaveCardCanOut = true;
						FightController.getInstance().RightToPlay = FightController.RIGHTTOPLAY.MINEING;
                     
                        viewCardDic[i].getObj().transform.SetAsLastSibling();
						viewCardDic [i].getObj ().GetComponent<RectTransform> ().DOLocalMove (FightUIData.getInstance ().EnemyVec3 + new Vector3 (0, Screen.height * 0.2f, 0), 0.1f)
							.SetEase(Ease.InBack).SetUpdate(true);
                        yield return new WaitForSeconds(0.3f);
                    }
                }
				if (isHaveCardCanOut) {
					yield return new WaitForSeconds (0.6f);
					FightController.getInstance ().RightToPlay = FightController.RIGHTTOPLAY.ENEMY;
				} else {
					MsgPrompts.create ("请选择至少一张卡牌",Color.red);
				}
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
            card.moveTo(viewPosList[pos]);
            viewCardDic.Add(pos, card);

            EventListener eve = EventListener.Get(card.getObj());
			eve.onDown = BtnCallBack; 
			eve.DOKill ();
            initOutCardRule();
        }        
    }  
}
