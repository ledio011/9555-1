using System;
using SprotoType;
using UnityEngine;

// Token: 0x020008E0 RID: 2272
public class InvestLineItem : MonoBehaviour
{
	// Token: 0x06003D68 RID: 15720 RVA: 0x0011293C File Offset: 0x00110B3C
	public void refershinfo(invest_pack curpark, InvestData curdata, int index)
	{
		if (curpark.ID.Equals(this.curInfo.ID))
		{
			this.UpdateInfo(curpark, curdata, index);
		}
	}

	// Token: 0x06003D69 RID: 15721 RVA: 0x00112970 File Offset: 0x00110B70
	public void UpdateInfo(invest_pack curpark, InvestData curdata, int index)
	{
		this.curInfo = curpark;
		this.curData = curdata;
		this.ItmeIndex = index;
		this.InfoLabel.text = StrDictionary.GetDictionaryString("#{300401}", new object[]
		{
			this.curData.LvTarget
		});
		this.completeFlag.enabled = false;
		long state = this.curInfo.state;
		if (state >= -1L && state <= 2L)
		{
			switch ((int)(state - -1L))
			{
			case 0:
				this.btnSp.spriteName = GameDefine.BtnIconNew[1];
				this.BtnLabel.text = StrDictionary.GetDictionaryString("#{300402}", new object[0]);
				break;
			case 1:
				this.btnSp.spriteName = GameDefine.BtnIconNew[1];
				this.BtnLabel.text = StrDictionary.GetDictionaryString("#{300402}", new object[0]);
				break;
			case 2:
				this.btnSp.spriteName = GameDefine.BtnIconNew[0];
				this.BtnLabel.text = StrDictionary.GetDictionaryString("#{300402}", new object[0]);
				break;
			case 3:
				this.btnSp.spriteName = GameDefine.BtnIconNew[1];
				this.BtnLabel.text = StrDictionary.GetDictionaryString("#{300403}", new object[0]);
				this.completeFlag.enabled = true;
				break;
			}
		}
		this.DiamondLabel.text = string.Format("{0}", this.curData.ItemCount1);
	}

	// Token: 0x06003D6A RID: 15722 RVA: 0x00112B00 File Offset: 0x00110D00
	public void OnClickBtn()
	{
		if (this.curInfo.state == -1L)
		{
			return;
		}
		if (this.curInfo.state == 1L)
		{
			WaitResponseUIRootLogic.OpenWaitBox(263, 10f, 0f, null);
			require_invest_reward.request request = new require_invest_reward.request();
			request.ID = this.curInfo.ID;
			NetLogic.GetInstance().Send<Protocol.require_invest_reward>(request, null);
		}
		else if (this.curInfo.state == 0L)
		{
			NoticeLogic.AddNotifyData("#{300404}", true, false);
		}
	}

	// Token: 0x040028DE RID: 10462
	public UILabel InfoLabel;

	// Token: 0x040028DF RID: 10463
	private invest_pack curInfo;

	// Token: 0x040028E0 RID: 10464
	private InvestData curData;

	// Token: 0x040028E1 RID: 10465
	public UILabel BtnLabel;

	// Token: 0x040028E2 RID: 10466
	public UISprite btnSp;

	// Token: 0x040028E3 RID: 10467
	public UILabel DiamondLabel;

	// Token: 0x040028E4 RID: 10468
	public int ItmeIndex;

	// Token: 0x040028E5 RID: 10469
	public UISprite completeFlag;
}
