using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;
/**战斗的卡牌攻击动画
* 作者:tony  */
public class FightAtkAnim : MonoBehaviour {

    //攻击目标位置
    Vector3 tragetPos = Vector3.zero;

    //播放攻击动作延时时间
    float delayTime = 0;

    /// <summary>
    /// 播放卡牌攻击动作
    /// </summary>
    public IEnumerator PlayAtk()
    {
        yield return new WaitForSeconds(this.delayTime);
        this.transform.DOMove(this.tragetPos, 0.5f).SetEase(Ease.InBack).SetUpdate(true);
    }
    /// <summary>
    /// 延时vDelayTime播放攻击动作
    /// </summary>
    /// <param name="vDelayTime"></param>
    public void PlayAtk( float vDelayTime )
    {
        this.delayTime = vDelayTime;
        StartCoroutine(this.PlayAtk()); 
    }

    /// <summary>
    /// 设置攻击的目标位置
    /// </summary>
    /// <param name="vVet3"></param>
    public void SetTragetPos(Vector3 vVet3)
    {
        this.tragetPos = vVet3;
    }
}
