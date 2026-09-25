using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020009B1 RID: 2481
public class StrongerItemLogic : MonoBehaviour
{
	// Token: 0x06004677 RID: 18039 RVA: 0x00164E78 File Offset: 0x00163078
	public StrongerItemLogic()
	{
		List<int> list = new List<int>();
		list.Add(1);
		list.Add(2);
		list.Add(4);
		list.Add(6);
		list.Add(7);
		list.Add(11);
		list.Add(12);
		this.RecommendList = list;
		base..ctor();
	}

	// Token: 0x06004678 RID: 18040 RVA: 0x00164ECC File Offset: 0x001630CC
	public void Reset(int type, DelegateDefine.OneIntParamDelegate clickback = null)
	{
		this.curType = type;
		this.ClickFun = clickback;
		this.NameLabel.text = StrDictionary.GetDictionaryString(GameDefine.STRONGER_TYPE_NAME[this.curType], new object[0]);
		if (this.RecommendList.Contains(type))
		{
			this.RecommendFlag.alpha = 1f;
		}
		else
		{
			this.RecommendFlag.alpha = 0f;
		}
	}

	// Token: 0x06004679 RID: 18041 RVA: 0x00164F44 File Offset: 0x00163144
	public void RefershSelect(int selectid)
	{
		if (selectid == this.curType)
		{
			this.btnSp.spriteName = "CZ_huaDongBG_1";
		}
		else
		{
			this.btnSp.spriteName = "CZ_huaDongBG";
		}
	}

	// Token: 0x0600467A RID: 18042 RVA: 0x00164F78 File Offset: 0x00163178
	public void OnClickItemBtn()
	{
		if (this.ClickFun != null)
		{
			this.ClickFun(this.curType);
		}
	}

	// Token: 0x04003380 RID: 13184
	public UISprite btnSp;

	// Token: 0x04003381 RID: 13185
	public UILabel NameLabel;

	// Token: 0x04003382 RID: 13186
	public UISprite RecommendFlag;

	// Token: 0x04003383 RID: 13187
	private DelegateDefine.OneIntParamDelegate ClickFun;

	// Token: 0x04003384 RID: 13188
	private int curType;

	// Token: 0x04003385 RID: 13189
	private List<int> RecommendList;
}
