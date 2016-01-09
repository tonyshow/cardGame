using UnityEngine;
using System.Collections;
public delegate void EnemyCardCollisionCB(CardEnemy card);
public class EnemyCollider : MonoBehaviour {

    EnemyCardCollisionCB touchEve = new EnemyCardCollisionCB(EnemyFightView.getInstance().DestroyCardsObject);
    void OnCollisionEnter2D(Collision2D coll)
    { 
        CardData otherCardData = coll.gameObject.GetComponent<Card>().getCardData();
        if (otherCardData.getCosplay() != CardCosplay.Enemy)
        {
            CardEnemy enemyCard = this.gameObject.GetComponent<CardEnemy>();
            touchEve(enemyCard);
        }
        else
        {
            CardEnemy enemyCard = this.gameObject.GetComponent<CardEnemy>();
            enemyCard.correctRotate();
        }
    }
    void OnCollisionExit2D(Collision2D coll)
    {
        Debug.Log("结束碰撞");
    }

}
