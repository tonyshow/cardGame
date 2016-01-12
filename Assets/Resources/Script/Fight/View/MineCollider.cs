using UnityEngine;
using System.Collections;
using DG.Tweening;
//卡牌碰撞检测
//作者：tony

//collObj:碰撞体
//coverCollObj：被碰撞体
public delegate void CardCollisionCB(Card card);
public class MineCollider : MonoBehaviour {

        CardCollisionCB touchEve = new CardCollisionCB(MineFightView.getInstance().DestroyCardsObject);
        void OnCollisionEnter2D(Collision2D coll)
        {
            if(coll.gameObject.GetComponent<Card>()!= null)
            {
                CardData otherCardData = coll.gameObject.GetComponent<Card>().getCardData();
                if (otherCardData.getCosplay() != CardCosplay.Mine)
                {
                    Card card = this.gameObject.GetComponent<Card>();
                    GameObject par = Object.Instantiate(Resources.Load("Prefab/ColliderParticle") as GameObject);
                    par.transform.parent = card.getObj().transform.parent;
                    par.transform.localPosition = coll.gameObject.transform.GetComponent<RectTransform>().anchoredPosition3D;
                    Tweener tweener = par.transform.DOScale(1.0f, 0.1f);
                    tweener.OnComplete(delegate()
                    {
                        touchEve(card);
                    });
                }
                else
                {
                    Card card = this.gameObject.GetComponent<Card>();
                    card.correctRotate();
                }
            }
        } 
}

