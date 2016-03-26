using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ConfigCard : ConfigBasic
{ 
    //所有卡牌数据
    private Dictionary<int, CardDetail> _dic = new Dictionary<int, CardDetail>();

    public ConfigCard(string vResPath, ConfigBasic.PathType vType)
    {
        base.Config(vResPath, vType);
    }

    /** 转换一行数据 */
    protected override void transLine(string[] vRows)
    {
        CardDetail vSubNew;
        vSubNew = new CardDetail();
        vSubNew.configData(vRows);
        _dic.Add(vSubNew.id, vSubNew);
    }

    /** 通过牌的唯一id找卡牌数据 */
    public CardDetail GetCardDetail(int vId )
    {
        if(_dic.ContainsKey(vId))
        {
            return _dic[vId];
        } 
        Debug.LogWarning("错误的卡牌vId = " + vId);
        return null; 
    }

    /** 获得一副牌 */
    public Dictionary<int, CardDetail> GetAllCard()
    {
        return this._dic;
    }
}
