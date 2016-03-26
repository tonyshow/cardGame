using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/** 卡牌数据分析
 * 作者：tony */
public class CardDataAnalysis : MonoBehaviour {
    
    public OUTTYPE GetOtherStyle( List<int> vList )
    {
        return OUTTYPE.Single;
    }

    /// <summary>
    /// 通过当前选中的方式计算是否还有其他的出牌方式
    /// </summary>
    /// <param name="vList">当前场上的牌</param>
    /// <returns></returns>
    public List<int> GetOtherOutType(List<int> vList)
    {
        return new List<int>();
    }
}
