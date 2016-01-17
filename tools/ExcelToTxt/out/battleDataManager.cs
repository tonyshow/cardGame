
//Create Time :2016-1-17 9:17:0
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;
public class battleDataManager
{
	private static Dictionary<string, int> indexList = new Dictionary<string, int>()
	{
		{"id",0},		//关卡ID
		{"rule",1},		//出牌规则
		{"oder",2},		//顺序
		{"playerId",3},		//玩家id
	};

	public static battleDataManager instance = null;
	public static battleDataManager getInstance()
	{
    	if (instance == null)
    	{
     	   instance = new battleDataManager();
    	}
		return instance;
	}

	public int getIndex(string keys){
		if(null==indexList[keys]){
			Debug.Error("no find keys: "+keys);
			return 0;
		}
		return indexList[keys];
	}

	public int getNrows(){
		return 12;
	}

	public int getNcols(){
		return 4;
	}

}