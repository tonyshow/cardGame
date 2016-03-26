using UnityEngine;
using System.Collections;
using DG.Tweening;
public class FightCardAnim : MonoBehaviour {

    Vector3 initPos = new Vector3();
    Vector3 initScale = new Vector3();

    void Start()
    {
        this.initPos = this.transform.localPosition;
        this.initScale = this.transform.localScale;
    }

    public void PlayToWait()
    {
        this.GetComponent<RectTransform>().DOLocalMoveY(10, 0.1f).SetUpdate(true).SetRelative().SetRecyclable();
        this.GetComponent<RectTransform>().DOShakeScale(0.9f, 0.1f, 10, 1).SetUpdate(true).SetRelative(); 
    }

    public void PlayToInitPos()
    {
        this.transform.localPosition = this.initPos;
        this.transform.localScale = this.initScale; 
        FightCardState state = this.transform.GetComponent<FightCardState>();
        state.Set(Const.CardState.none);
    }

    //发牌动画
    public IEnumerator Licensing(float vDelayTime )
    {
        yield return new WaitForSeconds(vDelayTime);
        Vector3 temp = this.transform.position;
        this.transform.position = new Vector3(Screen.width * 1.5f, Screen.height*0.5f,0);
        this.gameObject.SetActive(true);
        transform.SetAsLastSibling();
        Tweener tw = this.transform.DOMove(temp, 1.5f).SetEase(Ease.OutQuint).SetUpdate(true);

        tw.OnComplete(delegate ()
        {
            this.initPos = this.transform.localPosition;
            this.initScale = this.transform.localScale;
        }); 
    }
}
