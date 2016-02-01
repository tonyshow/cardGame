using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using DG.Tweening;
public delegate void EnemyCardCollisionCB(CardEnemy card);
public class EnemyCollider : MonoBehaviour {
    ShakeMainPanel atk = new ShakeMainPanel(MineFightView.Atkend);
    EnemyCardCollisionCB touchEve = new EnemyCardCollisionCB(EnemyFightView.getInstance().DestroyCardsObject);
    void OnTriggerEnter(Collider coll)
    { 
        if (coll.gameObject.name == "myPanel")
        {
            CardEnemy enemyCard = this.gameObject.GetComponent<CardEnemy>();

            //爆炸效果
            GameObject par = Instantiate(Resources.Load("Prefab/ColliderParticle") as GameObject);
            par.transform.parent = this.transform.parent;
            par.transform.localPosition = FightUIData.getInstance().MineVec3;
             
            //掉血效果
            GameObject HpObj = Instantiate(Resources.Load("Prefab/Fight/dropBlood") as GameObject);
            Text Hp = HpObj.transform.GetComponent<Text>();
            Debug.Log(Hp);
            Hp.transform.parent = this.transform.parent;
			Hp.transform.localPosition = FightUIData.getInstance ().MineVec3;// - new Vector3(0,100,0);
            Hp.text = "-" + enemyCard.cardData.getNumber();
		



			Sequence sq = DOTween.Sequence ();
			sq.SetRelative ();
			Tweener tw = HpObj.transform.DOLocalMoveY( Screen.height * 0.1f, 0.5f).SetEase(Ease.OutExpo).SetRelative();
			tw.OnComplete(delegate()
				{
					DestroyObject(HpObj);
				});  

			sq.Join (tw);
			sq.Join (HpObj.transform.DOScale(new Vector3(1.5f,1.5f,1.5f) ,0.5f )  );



            touchEve(enemyCard); 
            atk();
        }
        else
        {
            CardEnemy enemyCard = this.gameObject.GetComponent<CardEnemy>();
            enemyCard.correctRotate();
        }
    }
}
