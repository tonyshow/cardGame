using UnityEngine;
using System.Collections;

public enum DEVICE_SIZE_TYPE
{ 
    None = 0,
    Compare =  1,   //等比设备
    Leng =  2,   //偏长
    Short = 3   //偏短
}
public class AdaptationResolutionMG
{
    private float rawWidth  = 960.0f;
    private float rawHeight = 640.0f;
    private float rawCompare = 1.5f;
    private DEVICE_SIZE_TYPE deviceSizeType;
    static AdaptationResolutionMG instance = null;
    public static AdaptationResolutionMG getInstance()
    {
        if( null == instance )
        {
            instance = new AdaptationResolutionMG(); 
        }
        return instance;
    }
    AdaptationResolutionMG()
    {
        deviceSizeType = DEVICE_SIZE_TYPE.None;
    }
    //只按照设备宽度适配
    public float getDoSizeWidth()
    {
        return Screen.width / rawWidth;
    }
    //只按照设备高度适配
    public float getDoSizeHeight()
    {
        return Screen.height / rawHeight;
    }
    public float setDoSizeMSG(GameObject obj)
    {
        float dosize = 1.0f;
        Vector2 size = obj.transform.GetComponent<RectTransform>().sizeDelta;
        if (Screen.width / Screen.height > size.x / size.y)
        {
            dosize = Screen.width / size.x;
        }
        else
        {
            dosize = Screen.height / size.y;
        }
        return dosize;
    }

    //按照设备高度UI始终保持在界面内
    public float getDoSizeMSG()
    {
       float dosize = 1.0f;
       if (Screen.width / Screen.height > 1.5)
       {
           dosize = Screen.width / rawWidth;
       }
       else
       {
           dosize = Screen.height / rawHeight;
       }
       return dosize;
    }


    public float setDoSizeBg(GameObject obj)
    {
        float dosize = 1.0f;
        Vector2 size = obj.transform.GetComponent<RectTransform>().sizeDelta;
        if (Screen.width / Screen.height < size.x / size.y)
        {
            dosize = Screen.width / size.x;
        }
        else
        {
            dosize = Screen.height / size.y;
        }
        return dosize;
    }
    //按照设备高度UI始终布满屏幕
    public float getDoSizeBg()
    {
        float dosize = 1.0f;
        if (Screen.width / Screen.height < 1.5)
        {
            dosize = Screen.width / rawWidth;
        }
        else
        {
            dosize = Screen.height / rawHeight;
        }
        return dosize;
    }

    //适配特殊情况
    public float getUIDoSize()
    {
        float dosize = 1.0f; 
        float compare = ((float)Screen.width / (float)Screen.height);
        //设备尺寸比较偏向正方形
        if (compare <= rawCompare)
        {
            dosize = Screen.width / rawWidth;
        }
        //设备尺寸比较偏向长方形
        else
        {
            dosize = Screen.height / rawHeight;
        }
        return dosize;
    }

    //设备宽高比
    public float getDeviceWidthCompareHeight()
    {
        return Screen.width / (float)Screen.height;
    }

    //原始设计宽高比
    public float getRawCompare()
    {
        return rawCompare;
    }

    public float getRawWidth()
    {
        return rawWidth;
    }
    public float getRawHeight()
    {
        return rawHeight;
    }

    public DEVICE_SIZE_TYPE getDeviceSizeType()
    {
        if (DEVICE_SIZE_TYPE.None != deviceSizeType)
        {
            return deviceSizeType;
        }
        if (this.getDeviceWidthCompareHeight() == this.getRawCompare())
        {
            deviceSizeType = DEVICE_SIZE_TYPE.Compare;
        }
        else if (this.getDeviceWidthCompareHeight() > this.getRawCompare())
        {
            deviceSizeType = DEVICE_SIZE_TYPE.Leng;
        }
        else
        {
            deviceSizeType = DEVICE_SIZE_TYPE.Short;
        }
        return deviceSizeType;
    }
}
