using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class OtherFightData
{ 
    //单列模式
    public static OtherFightData instance = null;
    public static OtherFightData getInstance()
    {
        if (instance == null)
        {
            instance = new OtherFightData();
        }
        return instance; 
    }

   
}


