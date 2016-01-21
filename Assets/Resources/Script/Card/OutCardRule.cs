/*卡牌数据
 * 
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum OUTTYPE
{
    Single,            //单牌
    Sub,               //对子    
    threeAndTwo,       //三带二
    threeAndOne,       //三带一
    twoSub,            //两对
    straight,          //顺子  
    bomb,              //炸弹 
    kingBomb,          //王炸  
}

//出牌方案
public enum OUTCARDRULE
{
    ONE,    //单牌（根据牌面大小，不是伤害大小）先出小牌，然后是对子，三带二，三带一，双对，顺子，炸弹，王炸。
    TWO,    //三带二，三带一，双对，顺子，对子，单牌（根据牌面大小，不是伤害大小）先出小牌，炸弹（炸弹不压玩家的单牌和王炸），王炸（玩家一个回合打出大于等于4张牌才出王炸）
    THREE,  //三带二，三带一，双对，顺子，对子，单牌（根据牌面大小，不是伤害大小）先出K以下的大牌（包括K），炸弹（摸到炸弹后必定保留到下回合，待蓄力后首次出牌打出3倍伤害），王炸（无论先抽到小王还是             大王必定优先保留，凑齐王炸以后必定保留到下回合，待蓄力后首次出牌打出3倍伤害）
    FOUR,
    FIVE
}


public class OutCardRule
{
    OutCardRule()
    {      
        List<OUTTYPE> list = new List<OUTTYPE>();
        list.Add(OUTTYPE.Single);
        list.Add(OUTTYPE.Sub);
        list.Add(OUTTYPE.threeAndTwo);
        list.Add(OUTTYPE.threeAndOne);
        list.Add(OUTTYPE.twoSub);
        list.Add(OUTTYPE.straight);
        list.Add(OUTTYPE.bomb);
        list.Add(OUTTYPE.kingBomb);  
    }
    static private Dictionary<OUTCARDRULE, List<OUTTYPE>> AllRule = new Dictionary<OUTCARDRULE, List<OUTTYPE>>();
    static public List<OUTTYPE> getOutCardRule( OUTCARDRULE rule )
    {
        return AllRule[rule];        
    }

}
