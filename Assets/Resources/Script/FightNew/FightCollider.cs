using UnityEngine;
using System.Collections;
using DG.Tweening;

public class FightCollider : MonoBehaviour
{
    [SerializeField]
    private GameObject Canvas;

    [SerializeField]
    private GameObject mainPanel;

    [SerializeField]
    FightMineCard fightMineCard;
    [SerializeField]
    FightEnemyCard fightEnemyCard;

    void OnTriggerEnter(Collider coll)
    {  
        if( this.gameObject.name == "EnemyPos" && coll.gameObject.name == "mineCard")
        {
            StartCoroutine(DestroyCard(coll.gameObject));
            return;
        }

        if(this.gameObject.name == "minePos" && coll.gameObject.name == "EnemyCard")
        {
            StartCoroutine(DestroyEnemyCard(coll.gameObject));
            return;
        }
    }

    IEnumerator DestroyCard(GameObject vObj)
    {
        mainPanel.transform.DOShakePosition(0.2f, 30, 20, 180);
        FightCard fightCard = vObj.GetComponent<FightCard>();
        int hp = fightCard.GetHurt();
        this.fightEnemyCard.subHp(hp);
        yield return new WaitForSeconds(0.1f);
        GameObject.DestroyObject(vObj);   
    }

    IEnumerator DestroyEnemyCard(GameObject vObj)
    {
        mainPanel.transform.DOShakePosition(0.2f, 30, 20, 180);
        FightCard fightCard = vObj.GetComponent<FightCard>();
        yield return new WaitForSeconds(0.1f); 
        int hp = fightCard.GetHurt();
        this.fightMineCard.subHp(hp); 
        GameObject.DestroyObject(vObj); 
    } 
}
