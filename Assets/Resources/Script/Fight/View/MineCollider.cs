using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;
using System;
//卡牌碰撞检测
//作者：tony

//collObj:碰撞体
//coverCollObj：被碰撞体
public delegate void CardCollisionCB(Card card);
public class MineCollider : MonoBehaviour {
        ShakeMainPanel atk = new ShakeMainPanel(EnemyFightView.Atkend);
        CardCollisionCB touchEve = new CardCollisionCB(MineFightView.getInstance().DestroyCardsObject);
        void OnTriggerEnter(Collider coll)
        {
            if (coll.gameObject.name == "enemyPanel")
            {
                Card card = this.gameObject.GetComponent<Card>();
                GameObject par = Instantiate(Resources.Load("Prefab/ColliderParticle") as GameObject);
                par.transform.parent = card.getObj().transform.parent;
                par.transform.localPosition = FightUIData.getInstance().EnemyVec3;
                Tweener tweener = par.transform.DOScale(1.0f, 0.1f);
                tweener.OnComplete(delegate()
                {
                    touchEve(card);
					atk();
                });
                 
                //掉血效果 
                GameObject HpObj = Instantiate(Resources.Load("Prefab/Fight/dropBlood") as GameObject);
                Text Hp = HpObj.transform.GetComponent<Text>();
                Debug.Log(Hp);
                Hp.transform.parent = this.transform.parent;
				Hp.transform.localPosition = FightUIData.getInstance().EnemyVec3 ; //FightUIData.getInstance().EnemyVec3 + new Vector3(0, 100, 0);
                Hp.text = "-" + card.cardData.getNumber();

			    Sequence sq = DOTween.Sequence ();
				sq.SetRelative ();
				Tweener tw = HpObj.transform.DOLocalMoveY( Screen.height * 0.1f, 0.5f).SetEase(Ease.OutExpo).SetRelative();
                tw.OnComplete(delegate()
                {
                    DestroyObject(HpObj);
                });  
			 
				sq.Join (tw);
				sq.Join (HpObj.transform.DOScale(new Vector3(1.5f,1.5f,1.5f) ,0.5f )  );
            }
            else
            {
                Card card = this.gameObject.GetComponent<Card>();
                card.correctRotate();
            }
        }     
}

