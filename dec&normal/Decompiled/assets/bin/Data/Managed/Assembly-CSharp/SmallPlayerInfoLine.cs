using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020009BD RID: 2493
public class SmallPlayerInfoLine : MonoBehaviour
{
	// Token: 0x060046FC RID: 18172 RVA: 0x00169038 File Offset: 0x00167238
	public void Reset(PlayerInfoItemData leftData, PlayerInfoItemData rightData, string btnName, string disableBtnName, DelegateDefine.OneLongParamDelegate onClickBtn)
	{
		if (leftData == null)
		{
			NGUITools.SetActive(this.ItemList[0].gameObject, false);
		}
		else
		{
			NGUITools.SetActive(this.ItemList[0].gameObject, true);
			this.ItemList[0].Reset(leftData.Profession, leftData.Name, leftData.ComboVal, leftData.Level, btnName, disableBtnName, leftData.IsEnable, leftData.Key, leftData.GuildId, leftData.GuildName, onClickBtn);
		}
		if (rightData == null)
		{
			NGUITools.SetActive(this.ItemList[1].gameObject, false);
		}
		else
		{
			NGUITools.SetActive(this.ItemList[1].gameObject, true);
			this.ItemList[1].Reset(rightData.Profession, rightData.Name, rightData.ComboVal, rightData.Level, btnName, disableBtnName, rightData.IsEnable, rightData.Key, rightData.GuildId, rightData.GuildName, onClickBtn);
		}
	}

	// Token: 0x04003416 RID: 13334
	public List<SmallPlayerInfoItem> ItemList;
}
