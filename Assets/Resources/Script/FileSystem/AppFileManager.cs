using UnityEngine;
using System.Collections;

/** 文件资源管理器，
 * 作者：林艺斌 */
public class AppFileManager {

	/** 设备文件存储路径 */
	static string _deviceDataPath;
	/** 设备文件存储路径 */
	public static string DeviceDataPath {
		get {
			if (string.IsNullOrEmpty (_deviceDataPath)) {
				if (Application.platform == RuntimePlatform.IPhonePlayer) {
					string vPath = Application.dataPath.Substring (0, Application.dataPath.Length - 5);
					vPath = vPath.Substring (0, vPath.LastIndexOf ('/'));
					_deviceDataPath = vPath + "/Documents";
				} else if (Application.platform == RuntimePlatform.Android) {
					_deviceDataPath = Application.persistentDataPath + "/";
				} else {
					_deviceDataPath = Application.dataPath;
				}
			}
			return _deviceDataPath;
		}
	}

	/** 提取指定的路径的文本资源 */
	public static TextAsset LoadTextAsset (string vResPath) {
		TextAsset vTextAsset = Resources.Load (vResPath) as TextAsset;
		return vTextAsset;
	}

	/** 提取指定的路径的预设体资源 */
	public static GameObject LoadPrefab (string vResPath) {
		GameObject vGo = Resources.Load (vResPath) as GameObject;
		return vGo;
	}

	/** 提取指定的路径的贴图资源 */
	public static Texture LoadTexture (string vResPath) {
		Texture vTexture = Resources.Load (vResPath) as Texture;
		return vTexture;
	}

	/** 提取指定的路径的材质资源 */
	public static Material LoadMaterial (string vResPath) {
		Material vMat = Resources.Load (vResPath) as Material;
		return vMat;
	}

	/** 提取指定的路径的音频资源 */
	public static AudioClip LoadAudioClip (string vResPath) {
		AudioClip vAudioClip = Resources.Load (vResPath) as AudioClip;
		return vAudioClip;
	}

	/** 提取指定的路径的着色器资源 */
	public static Shader LoadShader (string vResPath) {
		Shader vShader = Resources.Load (vResPath) as Shader;
		return vShader;
	}

    /** 提取指定的路径的sprite */
    public static Sprite LoadSprite(string vResPath)
    {
        Sprite s = new Sprite();
        Sprite vSprite = Resources.Load(vResPath , s.GetType() ) as Sprite;
        return vSprite;
    }

}
