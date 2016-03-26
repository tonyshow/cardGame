/*卡牌数据
 * 
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum OUTTYPE
{
    None,
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

        dic.Clear();
        dic.Add(OUTTYPE.kingBomb, 8);
        dic.Add(OUTTYPE.bomb, 7);
        dic.Add(OUTTYPE.straight, 6); 
        dic.Add(OUTTYPE.twoSub, 5); 
        dic.Add(OUTTYPE.threeAndOne, 4); 
        dic.Add(OUTTYPE.threeAndTwo, 3);
        dic.Add(OUTTYPE.Sub, 2);
        dic.Add(OUTTYPE.Single, 1);
        AllRule.Add(OUTCARDRULE.TWO, dic);
    }


    //对子
    private List<List<int>> subNumber(Dictionary<int, Card> data)
    { 
        List<List<int>> l = new List<List<int>>();
        List <int> temp = new List <int>();
        foreach (var item in data)
        {
            int key = item.Key;
            int v = item.Value.cardData.getNumber();
            foreach (var item1 in data)
            {
                int key1 = item1.Key;
                int v1 = item1.Value.cardData.getNumber();
                if (key != key1 && v == v1)
                {
                    List<int> t = new List<int>();
                    t.Add(key);
                    t.Add(key1);
                    int num = key * key1;
                    if (!temp.Contains(num))
                    {
                        l.Add(t);
                        temp.Add(num);
                    } 
                }
            }
        }
        return l;
    }


    //三个
    private List<List<int>> threeFn(Dictionary<int, Card> data)
    {
        List<List<int>> sub = subNumber(data);
        if (sub.Count == 1 && data.Count >= 3 )
        {
            int key1 = sub[0][0];
            int key2 = sub[0][1];
            int num = data[key1].cardData.getNumber();
            foreach (var item1 in data)
            {
                int key = item1.Key;
                if (item1.Value.cardData.getNumber() == num && key != key1 && key != key2)
                {
                    sub[0].Add(key);
                }
            } 
        }
        return sub;
    }

    //连子
    private List<List<int>> straightFn(Dictionary<int, Card> data)
    {
        List<List<int>> l = new List<List<int>>();
        List<int> tempList = new List<int>();
        if (threeFn(data).Count == 0 )
        {
            foreach (var item in data)
            {
                int num1 = item.Value.cardData.getNumber();
                int num2 = item.Value.cardData.getNumber() + 1;
                int num3 = item.Value.cardData.getNumber() + 2;
                bool isNum2 = false;
                bool isNum3 = false;

                foreach (var items in data)
                {
                    if (items.Value.cardData.getNumber() == num2)
                    {
                        isNum2 = true;
                    }
                    if (items.Value.cardData.getNumber() == num3)
                    {
                        isNum3 = true;
                    }
                }
                //小
                if (num3 < 14 && isNum2 && isNum3)
                {
                    List<int> d = new List<int>();
                    bool find1 = false;
                    bool find2 = false;
                    int temp =0;
                    foreach (int key in data.Keys)
                    {
                        if (data[key].cardData.getNumber() == num2 && !find1)
                        {
                            d.Add(key);
                            find1 = true;
                            temp = temp + key;
                        }
                        else if (data[key].cardData.getNumber() == num3 && !find2)
                        {
                            d.Add(key);
                            find2 = true;
                            temp = temp + key;
                        }
                    }
                    if (d.Count > 1)
                    {
                        d.Add(item.Key);
                        d.Sort(); 
                        temp = temp + item.Key;
                        if (!tempList.Contains(temp))
                        {
                            l.Add(d); 
                            tempList.Add(temp);
                        }
                    }
                }

                //中
                num1 = item.Value.cardData.getNumber();
                num2 = item.Value.cardData.getNumber() - 1;
                num3 = item.Value.cardData.getNumber() + 1;
                isNum2 = false;
                isNum3 = false;
                foreach (var items in data)
                {
                    if (items.Value.cardData.getNumber() == num2)
                    {
                        isNum2 = true;
                    }
                    if (items.Value.cardData.getNumber() == num3)
                    {
                        isNum3 = true;
                    }
                }
                if (num3 < 14 && isNum2 && isNum3)
                {
                    List<int> d = new List<int>();
                    bool find1 = false;
                    bool find2 = false;
                    int temp = 0;
                    foreach (int key in data.Keys)
                    {
                        if (data[key].cardData.getNumber() == num2 && !find1)
                        {
                            d.Add(key);
                            find1 = true;
                            temp = temp + key;
                        }
                        else if (data[key].cardData.getNumber() == num3 && !find2)
                        {
                            d.Add(key);
                            find2 = true;
                            temp = temp + key;
                        }
                    }
                    if (d.Count > 1)
                    {
                        d.Add(item.Key);
                        d.Sort();
                        temp = temp + item.Key;
                        if (!tempList.Contains(temp))
                        {
                            l.Add(d);
                            tempList.Add(temp);
                        }
                    }
                }

                //大
                num1 = item.Value.cardData.getNumber();
                num2 = item.Value.cardData.getNumber() - 1;
                num3 = item.Value.cardData.getNumber() - 2;
                isNum2 = false;
                isNum3 = false;
                foreach (var items in data)
                {
                    if (items.Value.cardData.getNumber() == num2)
                    {
                        isNum2 = true;
                    }
                    if (items.Value.cardData.getNumber() == num3)
                    {
                        isNum3 = true;
                    }
                }
                if (num1 < 14 && isNum2 && isNum3 )
                {
                    List<int> d = new List<int>();
                    bool find1 = false;
                    bool find2 = false;
                    int temp = 0;
                    foreach (int key in data.Keys)
                    {
                        if (data[key].cardData.getNumber() == num2 && !find1)
                        {
                            d.Add(key);
                            find1 = true;
                            temp = temp + key;
                        }
                        else if (data[key].cardData.getNumber() == num3 && !find2)
                        {
                            d.Add(key);
                            find2 = true;
                            temp = temp + key;
                        }
                    }
                    if (d.Count > 1)
                    {
                        d.Add(item.Key);
                        d.Sort();
                        temp = temp + item.Key;
                        if (!tempList.Contains(temp))
                        {
                            l.Add(d);
                            tempList.Add(temp);
                        }
                    }
                }   
            }
        }
    
        return l;
    }

    //炸弹
    private List<List<int>> bombFn(Dictionary<int, Card> data)
    {
        if (data.Count < 4)
        {
            return new List<List<int>>();
        }

        List<List<int>> temp = new List<List<int>>();
        List<int> list = new List<int>();
        foreach (var item in data)
        {
            list.Add(item.Value.cardData.getNumber());
        } 
        list.Sort();

        if (data.Count == 4)
        {
            if (list[0] == list[1] &&  list[1] == list[2] &&  list[2] == list[3] ) 
            {
                List<int> bombL = new List<int>();
                for (int i = 0; i < 4; ++i)
                {
                    bombL.Add(list[i]);
                }
                temp.Add(bombL);
            }
        }
        else if (data.Count == 5)
        {
            if ( list[1] == list[2] &&  list[2] == list[3] && list[3] == list[4]  ) 
            {
                List<int> bombL = new List<int>();
                for (int i = 1; i < 5; ++i)
                {
                    bombL.Add(list[i]);
                }
                temp.Add(bombL);
            }
            if (list[0] == list[1] && list[1] == list[2] && list[2] == list[3])
            {
                List<int> bombL = new List<int>();
                for (int i = 0; i < 4; ++i)
                {
                    bombL.Add(list[i]);
                }
                temp.Add(bombL);
            }
        }
       
        return temp;
    }

    //王炸
    private List<List<int>> kingBombFn(Dictionary<int, Card> data)
    {
        List<List<int>> temp = new List<List<int>>();

        bool isNum2 = false;
        bool isNum3 = false;
        foreach (var items in data)
        {
            if (items.Value.cardData.getNumber() == 14)
            {
                isNum2 = true;
            }
            if (items.Value.cardData.getNumber() == 15)
            {
                isNum3 = true;
            }
        }

        if ( isNum2 && isNum3 )
        {
            List<int> l = new List<int>();
            foreach (var item in data)
            {
                if (item.Value.cardData.getNumber() == 14)
                {
                    l.Add(item.Key);
                }
                if (item.Value.cardData.getNumber() == 15)
                {
                    l.Add(item.Key);
                }
            }
            temp.Add(l);
        }
        return temp;
    }

    //获得出牌规则
    //返回的是出牌的规则
    public Dictionary<OUTTYPE, List<List<int>>> getTip(OUTCARDRULE outCardRule, Dictionary<int, Card> data)
    {
        //出牌顺序
        Dictionary<OUTTYPE, int> outRule = AllRule[outCardRule];

        List<OUTTYPE> outType = new List<OUTTYPE>();

        Dictionary<OUTTYPE, List<List<int>>> dicList = new Dictionary<OUTTYPE, List<List<int>>>();
        //单必有
        foreach (var key in outRule.Keys)
        { 
            OUTTYPE type =  (OUTTYPE)key;
            //必须有单
            if (  OUTTYPE.Single == type )
            {
                outType.Add(type);
                List<List<int>> temp = new List<List<int>>();
                List<int> l = new List<int>();
                foreach (var item in data)
                { 
                    l.Add(item.Key);
                } 
                l.Sort();
                for (int i = 0; i < l.Count; ++i)
                {
                    List<int> lis = new List<int>();
                    lis.Add(l[i]);
                    temp.Add(lis);
                }
                dicList.Add(type, temp); 
            }
            //对子
            else if (OUTTYPE.Sub == type)
            {
                List<List<int>> temp = this.subNumber(data);
                if (temp.Count > 0)
                {
                    outType.Add(type);
                    dicList.Add(type, temp); 
                }
            }
            //三带二
            else if (OUTTYPE.threeAndTwo == type)
            {
                if ( data.Count ==5 && this.threeFn(data).Count > 0)
                {
                    outType.Add(type);
                    List<List<int>> temp = new List<List<int>>();
                    List<int> l = new List<int>();
                    foreach (var item in data)
                    {
                        l.Add(item.Key);
                    }
                    temp.Add(l);
                    dicList.Add(type, temp); 
                }
            }
            //三带一
            else if (OUTTYPE.threeAndOne == type)
            {
                
                List<List<int>> threeList = this.threeFn(data);
                if (data.Count >= 4 && threeList.Count > 0)
                {
                    outType.Add(type);
                    if (data.Count == 4)
                    {
                        List<List<int>> temp = new List<List<int>>();
                        List<int> l = new List<int>();
                        foreach (var item in data)
                        {
                            l.Add(item.Key);
                        }
                        temp.Add(l);
                        dicList.Add(type, temp); 
                    }
                    else if (data.Count == 5)
                    {
                        List<List<int>> temp = new List<List<int>>();
                        List<int> AllPosList = new List<int>();
                        AllPosList.Add(1);
                        AllPosList.Add(2);
                        AllPosList.Add(3);
                        AllPosList.Add(4);
                        AllPosList.Add(5);
                        foreach (var items in threeList)
                        {
                            int v = items[0];
                            AllPosList.Remove(v); 
                        }

                        List<int> temp1 = threeList[0];
                        temp1.Add(AllPosList[0]);
                        List<int> temp2 = threeList[0];
                        temp2.Add(AllPosList[1]);
                        temp.Add(temp1);
                        temp.Add(temp2);
                        dicList.Add(type, temp); 
                    } 
                }
            }
            //两对
            else if (OUTTYPE.twoSub == type)
            {
                List<List<int>> l = subNumber(data);
                if ( l.Count == 2 )
                {
                    int num1 = l[0][0];
                    int num2 = l[1][0];
                    if (Mathf.Abs(num1 - num2) == 1)
                    {
                        outType.Add(type);
                        List<int> temp = new List<int>();
                        temp.Add(l[0][0]);
                        temp.Add(l[0][1]);
                        temp.Add(l[1][0]);
                        temp.Add(l[1][1]); 
                        List<List<int>> s = new List<List<int>>();
                        s.Add(temp);
                        dicList.Add(type, s); 
                    } 
                }
            }
            //连子
            else if (OUTTYPE.straight == type)
            {
                List<List<int>> l = straightFn(data);
                if (l.Count>0)
                {
                    outType.Add(type);
                    dicList.Add(type,l);
                }
            }
            //炸弹
            else if (OUTTYPE.bomb == type)
            {
                List<List<int>> l = bombFn(data);
                if (l.Count>0)
                {
                    outType.Add(type);
                    dicList.Add(type,l);
                }
            }
            //王炸
            else if (OUTTYPE.kingBomb == type)
            {
                List<List<int>> l = kingBombFn(data);
                if (l.Count>0)
                {
                    outType.Add(type);
                    dicList.Add(type,l);
                }
            }  
        }
        return dicList;
    }
}
