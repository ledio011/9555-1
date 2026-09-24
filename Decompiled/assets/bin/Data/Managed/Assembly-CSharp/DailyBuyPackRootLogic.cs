using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020008DC RID: 2268
public class DailyBuyPackRootLogic : SingletonUnity<DailyBuyPackRootLogic>
{
	// Token: 0x06003D48 RID: 15688 RVA: 0x001116CC File Offset: 0x0010F8CC
	public void EnableReset()
	{
		for (int i = 0; i < this.buyItems.Count; i++)
		{
			UnityVersionUtil.SetActiveRecursive(this.buyItems[i].gameObject, false);
		}
		this.InitTexture();
	}

	// Token: 0x06003D49 RID: 15689 RVA: 0x00111714 File Offset: 0x0010F914
	public void InitTexture()
	{
		if (this.BannerTexture.mainTexture == null && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
		{
			base.StartCoroutine(BundleManager.LoadTexture(GameDefine.TextureBannerDaily, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
		}
	}

	// Token: 0x06003D4A RID: 15690 RVA: 0x00111764 File Offset: 0x0010F964
	private void TextureLoadFinish(string name, Texture textureObj)
	{
		this.BannerTexture.mainTexture = textureObj;
	}

	// Token: 0x06003D4B RID: 15691 RVA: 0x00111774 File Offset: 0x0010F974
	public void Reset(ret_request_daily_buy.request request)
	{
		this.daily_buy_list = new List<daily_buy>(request.daily_buys.Values);
		this.daily_buy_list.Sort((daily_buy x, daily_buy y) => int.Parse(x.ID) - int.Parse(y.ID));
		int num = this.daily_buy_list.Count - this.buyItems.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.buyItems[0].gameObject) as GameObject;
				DailyBuyItem component = gameObject.GetComponent<DailyBuyItem>();
				gameObject.name = string.Format("dailybuyitem{0:D2}", this.buyItems.Count);
				gameObject.transform.parent = this.parentGrid.transform;
				gameObject.transform.localScale = Vector3.one;
				this.buyItems.Add(component);
			}
		}
		for (int j = 0; j < this.buyItems.Count; j++)
		{
			if (j < this.daily_buy_list.Count)
			{
				UnityVersionUtil.SetActiveRecursive(this.buyItems[j].gameObject, true);
				this.buyItems[j].UpdateInfo(this.daily_buy_list[j]);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.buyItems[j].gameObject, false);
			}
		}
		this.parentGrid.Reposition();
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Welfare", "DailyBuy", "open");
	}

	// Token: 0x06003D4C RID: 15692 RVA: 0x00111910 File Offset: 0x0010FB10
	public void UpdateInfo(string id)
	{
		for (int i = 0; i < this.daily_buy_list.Count; i++)
		{
			if (this.daily_buy_list[i].ID.Equals(id))
			{
				this.daily_buy_list[i].state = 2L;
				this.buyItems[i].UpdateInfo(this.daily_buy_list[i]);
				SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Welfare", "DailyBuy", string.Format("require_{0}", id));
				break;
			}
		}
	}

	// Token: 0x040028B9 RID: 10425
	public UITexture BannerTexture;

	// Token: 0x040028BA RID: 10426
	public List<DailyBuyItem> buyItems;

	// Token: 0x040028BB RID: 10427
	private List<daily_buy> daily_buy_list;

	// Token: 0x040028BC RID: 10428
	public UIGrid parentGrid;
}
