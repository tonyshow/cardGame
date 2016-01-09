using UnityEngine;
using System.Collections;

public class PlayerData : MonoBehaviour 
{

    static PlayerData instance;
    private int level = 100;
    public static PlayerData getInstance()
    { 
        if (instance == null)
        {
            instance = new PlayerData();
        }
        return instance;
         
    }
    
    public int Level
    {
        get { return level; }
        set { level = value; }
    }
}
