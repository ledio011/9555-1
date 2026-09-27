using System;
using UnityEngine;

// Token: 0x020009D0 RID: 2512
public class TitleItemLogic : MonoBehaviour
{
	// Token: 0x06004771 RID: 18289 RVA: 0x0016C484 File Offset: 0x0016A684
	public void reset(int key, TitleData curdata, int playerlevel, DelegateDefine.OneIntParamDelegate clickfun)
	{
		this.TitleSp.spriteName = curdata.Icon;
		this.Titilename.text = StrDictionary.GetDictionaryString(curdata.Name, new object[0]);
		this.curkey = key;
		this.clickitemfun = clickfun;
		this.updateitem(playerlevel);
	}

	// Token: 0x06004772 RID: 18290 RVA: 0x0016C4D4 File Offset: 0x0016A6D4
	public void updateitem(int level)
	{
		if (this.curkey <= level)
		{
			this.TitleSp.color = Color.white;
			this.GetFlag.enabled = true;
		}
		else
		{
			this.TitleSp.color = Color.gray;
			this.GetFlag.enabled = false;
		}
	}

	// Token: 0x06004773 RID: 18291 RVA: 0x0016C52C File Offset: 0x0016A72C
	public void SetSelect(Transform seltra)
	{
		seltra.parent = base.transform;
		seltra.localPosition = Vector3.zero;
		seltra.localRotation = Quaternion.identity;
		seltra.localScale = Vector3.one;
	}

	// Token: 0x06004774 RID: 18292 RVA: 0x0016C568 File Offset: 0x0016A768
	public void OnClickItemBtn()
	{
		if (this.clickitemfun != null)
		{
			this.clickitemfun(this.curkey);
		}
	}

	// Token: 0x040034B2 RID: 13490
	public UISprite GetFlag;

	// Token: 0x040034B3 RID: 13491
	public UISprite TitleSp;

	// Token: 0x040034B4 RID: 13492
	public UILabel Titilename;

	// Token: 0x040034B5 RID: 13493
	public DelegateDefine.OneIntParamDelegate clickitemfun;

	// Token: 0x040034B6 RID: 13494
	private int curkey;
}
