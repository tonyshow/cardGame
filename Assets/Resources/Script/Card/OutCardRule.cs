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

public class sortCardRule
{
    public OUTTYPE type;
    public Dictionary<int,int> data;
}
public class OutCardRule
{

    private static Dictionary<OUTCARDRULE, Dictionary<OUTTYPE, int>> AllRule = new Dictionary<OUTCARDRULE, Dictionary<OUTTYPE, int>>();
    //单列模式
    public static OutCardRule instance = null;
    public static OutCardRule getInstance()
    {
        if (instance == null)
        {
            instance = new OutCardRule();
            instance.init();
        }
        return instance;
    }

    private void init()
    {
        Dictionary<OUTTYPE, int> dic = new Dictionary<OUTTYPE, int>();
        dic.Add(OUTTYPE.Single, 1);
        dic.Add(OUTTYPE.threeAndTwo, 3);
        dic.Add(OUTTYPE.Sub, 2); 
        dic.Add(OUTTYPE.threeAndOne, 4);
        dic.Add(OUTTYPE.twoSub, 5);
        dic.Add(OUTTYPE.straight, 6);
        dic.Add(OUTTYPE.bomb, 7);
        dic.Add(OUTTYPE.kingBomb, 8);
        AllRule.Add(OUTCARDRULE.ONE, dic);  
    }



    private Dictionary<int, int> subNumber(Dictionary<int, int> data)
    {
        Dictionary<int, int> temp = new Dictionary<int, int>(); 
        foreach (var item in data)
        {
            int key = item.Key;
            int v = item.Value;
            foreach (var item1 in data)
            {
                int key1 = item1.Key;
                int v1 = item1.Value;
                if (key != key1 && v == v1)
                {
                    temp.Add(key, v);
                }
            }
        }
        return temp;
    }

     

    private Dictionary<int, int> isHaveThree(Dictionary<int, int> data)
    {
        Dictionary<int, int> sub = subNumber(data);
        Dictionary<int, int> temp = new Dictionary<int, int>();
        
        if ( sub.Count == 1)
        {
            int haveNum=-1;
            foreach (var item in data)
            {
                haveNum = item.Value;
            }
          
            foreach (var item1 in data)
            {
                if (item1.Value == haveNum )
                {
                    temp.Add(item1.Key, item1.Value);
                }
            }
        }
        return temp;
    }

   
    private Dictionary<int, int> isHaveStraight(Dictionary<int, int> data)
    {
        Dictionary<int, int> temp = new Dictionary<int, int>();
        if (isHaveThree(data).Count == 0 )
        {
            foreach (var item in data)
            {
                int num = item.Value;

                int key1_2 = -1;
                int key1_3 = -1;
                int pos1_2 = -1;
                int pos1_3 = -1;

                foreach (var item1 in data)
                {
                    //为顺子第一位
                    int tempNum1_2= num +1;
                    int tempNum1_3= num+2;

                    if (item1.Value == tempNum1_2)
                    {
                        pos1_2 = item1.Value;
                    }
                    if (item1.Value == tempNum1_3)
                    {
                        pos1_3 = item1.Value;
                    }

                    //存在顺子
                    if (key1_2 > 1 && pos1_2 != -1 && pos1_3 != -1)
                    {
                        temp.Add(item.Key, item.Value);
                        temp.Add(key1_2, pos1_2);
                        temp.Add(key1_3, pos1_2);
                        break;
                    }

                    //为顺子第二位
                    tempNum1_2 = num - 1;
                    tempNum1_3 = num + 1;
                    if (item1.Value == tempNum1_2)
                    {
                        pos1_2 = item1.Value;
                    }
                    if (item1.Value == tempNum1_3)
                    {
                        pos1_3 = item1.Value;
                    }

                    //存在顺子
                    if (tempNum1_2 > 0 && pos1_2 != -1 && pos1_3 != -1)
                    {
                        temp.Add(item.Key, item.Value);
                        temp.Add(key1_2, pos1_2);
                        temp.Add(key1_3, pos1_2);
                        break;
                    }

                    //为顺子第二位
                    tempNum1_2 = num - 1;
                    tempNum1_3 = num - 2;
                    if (item1.Value == tempNum1_2)
                    {
                        pos1_2 = item1.Value;
                    }
                    if (item1.Value == tempNum1_3)
                    {
                        pos1_3 = item1.Value;
                    }

                    //存在顺子
                    if (tempNum1_2 > 1 && pos1_2 != -1 && pos1_3 != -1)
                    {
                        temp.Add(item.Key, item.Value);
                        temp.Add(key1_2, pos1_2);
                        temp.Add(key1_3, pos1_2);
                        break;
                    }

                }
            }
        }
        return temp;
    }

    private Dictionary<int, int> isHaveBomb(Dictionary<int, int> data)
    {
        Dictionary<int, int> temp = new Dictionary<int, int>();
        return temp;
    }
    
    
    //获得出牌规则
    //返回的是出牌的规则
    public void getTip( OUTCARDRULE outCardRule , Dictionary<int,int> data )
    {
        //出牌顺序
        Dictionary<OUTTYPE, int> outRule = AllRule[outCardRule];

        List<OUTTYPE> outType = new List<OUTTYPE>();
        //单必有
        foreach (var key in outRule.Keys)
        { 
            OUTTYPE type =  (OUTTYPE)key;
            //必须有单
            if (  OUTTYPE.Single == type )
            {
                outType.Add(type);
            }
            else if (OUTTYPE.Sub == type)
            {
                if (this.subNumber(data).Count > 0)
                {
                    outType.Add(type);
                }
            }
            else if (OUTTYPE.threeAndTwo == type)
            {
                if ( data.Count ==5 && this.isHaveThree(data).Count > 0)
                {
                    outType.Add(type);
                }
            }
            else if (OUTTYPE.threeAndOne == type)
            {
                if (data.Count >= 4 && this.isHaveThree(data).Count > 0)
                {
                    outType.Add(type);
                }
            }
            else if (OUTTYPE.twoSub == type)
            {
                if ( subNumber(data).Count == 2 )
                {
                    outType.Add(type);
                }
            }
            else if (OUTTYPE.straight == type)
            {
                if (isHaveStraight(data).Count >= 3)
                {
                    outType.Add(type);
                }
            }            
        } 
    }
}
