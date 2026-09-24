using System;
using SprotoType;
using UnityEngine;

// Token: 0x02000969 RID: 2409
public class PlayerCarIconLogic : MonoBehaviour
{
	// Token: 0x17000FA4 RID: 4004
	// (get) Token: 0x060043A6 RID: 17318 RVA: 0x0014EE4C File Offset: 0x0014D04C
	public string CurCarId
	{
		get
		{
			return this.mCurCarId;
		}
	}

	// Token: 0x060043A7 RID: 17319 RVA: 0x0014EE54 File Offset: 0x0014D054
	public void Reset(mount curCarData, DelegateDefine.StringGameObjectDelegate func = null)
	{
		this.mCurCarId = curCarData.ID;
		this.onClickCarIcon = func;
		MountData mountDataById = DataManager.GetMountDataById(this.mCurCarId);
		this.Icon.spriteName = mountDataById.CarIcon;
		NGUITools.SetActive(this.NewLabel.gameObject, curCarData.state == 3L);
	}

	// Token: 0x060043A8 RID: 17320 RVA: 0x0014EEAC File Offset: 0x0014D0AC
	public void OnClickBtn()
	{
		if (UnityVersionUtil.IsActive(this.NewLabel.gameObject))
		{
			NGUITools.SetActive(this.NewLabel.gameObject, false);
		}
		if (this.onClickCarIcon != null)
		{
			this.onClickCarIcon(this.mCurCarId, base.gameObject);
		}
	}

	// Token: 0x04003047 RID: 12359
	public UISprite Icon;

	// Token: 0x04003048 RID: 12360
	public UISprite NewLabel;

	// Token: 0x04003049 RID: 12361
	public DelegateDefine.StringGameObjectDelegate onClickCarIcon;

	// Token: 0x0400304A RID: 12362
	private string mCurCarId = string.Empty;
}
