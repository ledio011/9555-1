using System;
using SprotoType;
using UnityEngine;

// Token: 0x02000904 RID: 2308
public class DonateItem : MonoBehaviour
{
	// Token: 0x06003F1E RID: 16158 RVA: 0x0012467C File Offset: 0x0012287C
	public void Init(GuildDonateData data, donate_record record = null)
	{
		this.curDonateData = data;
		this.curRecod = record;
		this.curItemData = DataManager.GetItemDataByID(this.curDonateData.ItemID);
		this.donateLabel.text = string.Format("+{0}", this.curDonateData.Contribute);
		this.contributeLabel.text = string.Format("+{0}", this.curDonateData.GuildExp);
		this.costLabel.text = GameMoneyHelper.GetMoneyValStr(this.curDonateData.ItemCount, this.curDonateData.ItemID);
		if (record.DonateCount <= 0L)
		{
			this.BkSprite.alpha = 0.5f;
		}
		else
		{
			this.BkSprite.alpha = 1f;
		}
	}

	// Token: 0x06003F1F RID: 16159 RVA: 0x00124750 File Offset: 0x00122950
	public void OnClickDonate()
	{
		if (this.curRecod == null)
		{
			return;
		}
		if (this.curRecod.DonateCount > 0L)
		{
			Singleton<ObjManager>.Instance.MainPlayer.GuildDonate(this.curDonateData);
		}
	}

	// Token: 0x04002AD2 RID: 10962
	public UILabel donateLabel;

	// Token: 0x04002AD3 RID: 10963
	public UILabel contributeLabel;

	// Token: 0x04002AD4 RID: 10964
	public UILabel costLabel;

	// Token: 0x04002AD5 RID: 10965
	public UISprite BkSprite;

	// Token: 0x04002AD6 RID: 10966
	private GuildDonateData curDonateData;

	// Token: 0x04002AD7 RID: 10967
	private donate_record curRecod;

	// Token: 0x04002AD8 RID: 10968
	private ItemData curItemData;
}
