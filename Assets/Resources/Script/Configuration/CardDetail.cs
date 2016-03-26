//Create Time :2016-3-25 22:39:17	
using UnityEngine;
using System.Collections;

public class CardDetail
{
	private int _id;
	private int _number;
	private int _type;
	private int _hurt;
	private string _iconPath;
	private string _typeSprite;
	private string _numberSprite;

	public void configData(string[] vRows)
	{
		//int
		_id=int.Parse(vRows[0]);

		//int
		_number=int.Parse(vRows[1]);

		//int
		_type=int.Parse(vRows[2]);

		//int
		_hurt=int.Parse(vRows[3]);

		//string
		_iconPath=vRows[4];

		//string
		_typeSprite=vRows[5];

		//string
		_numberSprite=vRows[6];

	}

	/*唯一id*/
	public int id{ get { return _id;} }
	/*数字*/
	public int number{ get { return _number;} }
	/*花色*/
	public int type{ get { return _type;} }
	/*伤害值*/
	public int hurt{ get { return _hurt;} }
	/*资源路径*/
	public string iconPath{ get { return _iconPath;} }
	/**/
	public string typeSprite{ get { return _typeSprite;} }
	/**/
	public string numberSprite{ get { return _numberSprite;} }

}