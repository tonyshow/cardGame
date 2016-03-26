using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/** 通用的单个配置文件的处理，通用数据转换处理 */
public class ConfigBasic {

	/** 资源路径 */
	protected string resPath;
	/** 数据集 */
	protected string[] lines;
	/** 字段名集 */
	protected string[] names;
	/** 注释集 */
	protected string[] docs;
	/** 行间隔体 */
	protected string[] lineStep = new string[] { "\r\n" };
	/** 段间隔体 */
	protected string[] rowStep = new string[] { "\t" };
	/** 字段量 */
	protected int _totalRow = 0;
	/** 使用的资源来源 */
	protected PathType useType = ConfigBasic.PathType.localResource;
	/** 资源来源 */
	public enum PathType {
		localResource,
		urlRes,
		realData
	}

	/** 初始化处理 */
	protected virtual void Config (string vRes, PathType vType) {
		if (vType == PathType.localResource)
		{
			resPath = vRes;
			loadLinesFromResource ();
			readLines ();
			lines = null; // 处理完后清理源缓存
		}
	}

	/** 从本地打包资源中提取信息 */
	protected void loadLinesFromResource () {
//		TextAsset txtAsset = Resources.Load (resPath) as TextAsset;
		TextAsset txtAsset = AppFileManager.LoadTextAsset (resPath);
		if (txtAsset == null) {
			Debug.LogError("在 Resource 目录里找不到相应文本文件：" + resPath);
			return;
		}
		lines = txtAsset.text.Split (lineStep, System.StringSplitOptions.None);
	}

	/** 分行处理 */
	protected void readLines () {
		if (lines.Length < 2) {
			Debug.LogError("配置：" + resPath + " 没有足够的可识别信息");
			return;
		}
		names = splitLine (lines [0]);
		_totalRow = names.Length;
		docs = splitLine (lines [1]);
		int l = lines.Length;
		if (l > 2)
		{
			int i = 0;
			string vSubLine;
			string[] vRows;
			for (i=2;i<l;i++)
			{
				vSubLine = lines[i];
				if (vSubLine == "")
					continue;
				vRows = splitLine(vSubLine);
				transLine(vRows);
			}
		}
	}

	/** 切换单行 */
	protected string[] splitLine (string vSubLine) {
		return vSubLine.Split(rowStep, System.StringSplitOptions.None);
	}

	/** 字段量 */
//	public int totalRow { get { return _totalRow; } }

	/** 转换一行数据 */
	protected virtual void transLine (string[] vRows) {

	}

 
     
}
