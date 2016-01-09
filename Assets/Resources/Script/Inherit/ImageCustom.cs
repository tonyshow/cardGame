using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace UnityEngine.UI
{ 
    [AddComponentMenu("UI/ImageCustom", 11)]
    public class ImageCustom : Image 
    {  
         private bool isGray = false;
         public bool isOK = false;
         private Material defaultMeterial = null;
         void Start()
         {
             defaultMeterial = this.m_Material;
         }
         virtual public bool gray
         {
             get{ return isGray;}
             set {   
                     isGray = value;
                     if (isGray)
                     {
                         Material m = this.m_Material;
                         m = Resources.Load("Material/gray") as Material;
                         this.m_Material = m;
                     }
                     else
                     {
                         this.m_Material = defaultMeterial;
                     } 
             } 
         } 
    }
}