using UnityEngine;
using System.Collections;

/** 单例化配置信息集合 */
public class ConfigDatas
{

    ConfigCard _card;

    /** 静态单例 */
    private static ConfigDatas _inst;
    /** 静态单例引用 */
    public static ConfigDatas Inst
    {
        get
        {
            if (_inst == null)
                _inst = new ConfigDatas();
            return _inst;
        }
    }

    private ConfigDatas()
    {
        if (_inst != null)
            Debug.LogError("不需要重复初始化");
        else
            ConfigFromResource();
    }

   

    /** 从默认包中提取配置初始化数据 */
    private void ConfigFromResource()
    { 
        _card = new ConfigCard("configs/Card", ConfigBasic.PathType.localResource);  
    }
    
    public ConfigCard card { get { return this._card; } }
}
