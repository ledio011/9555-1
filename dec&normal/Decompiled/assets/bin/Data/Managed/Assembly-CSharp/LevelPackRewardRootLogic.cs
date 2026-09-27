using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020008E3 RID: 2275
public class LevelPackRewardRootLogic : SingletonUnity<LevelPackRewardRootLogic>
{
	// Token: 0x06003D7D RID: 15741 RVA: 0x00113A1C File Offset: 0x00111C1C
	protected override void Awake()
	{
		base.Awake();
		this.uiWrapContent.enabled = false;
		UIWrapContentNew uiwrapContentNew = this.uiWrapContent;
		uiwrapContentNew.onInitializeItem = (UIWrapContentNew.OnInitializeItem)Delegate.Combine(uiwrapContentNew.onInitializeItem, new UIWrapContentNew.OnInitializeItem(this.OnInitializeItem));
	}

	// Token: 0x06003D7E RID: 15742 RVA: 0x00113A64 File Offset: 0x00111C64
	public void EnableReset()
	{
		for (int i = 0; i < this.LineItems.Count; i++)
		{
			UnityVersionUtil.SetActiveRecursive(this.LineItems[i].gameObject, false);
		}
		this.InitTexture();
	}

	// Token: 0x06003D7F RID: 15743 RVA: 0x00113AAC File Offset: 0x00111CAC
	public void InitTexture()
	{
		if (this.bannerTexture.mainTexture == null && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
		{
			base.StartCoroutine(BundleManager.LoadTexture(GameDefine.TextureBannerLevel, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
		}
	}

	// Token: 0x06003D80 RID: 15744 RVA: 0x00113AFC File Offset: 0x00111CFC
	private void TextureLoadFinish(string name, Texture textureObj)
	{
		this.bannerTexture.mainTexture = textureObj;
	}

	// Token: 0x06003D81 RID: 15745 RVA: 0x00113B0C File Offset: 0x00111D0C
	public void Reset(ret_request_level_pack.request request)
	{
		this.LevelDataList.Clear();
		this.Level_Dic = request.level_pack;
		this.Level_list = new List<level_pack>(request.level_pack.Values);
		for (int i = 0; i < this.Level_list.Count; i++)
		{
			this.LevelDataList.Add(DataManager.GetLevelPackageDataBuyId(this.Level_list[i].ID));
		}
		this.LevelDataList.Sort((LevelPackageData x, LevelPackageData y) => x.LvTarget - y.LvTarget);
		int num = Mathf.Min(this.LevelDataList.Count, this.lineMinCount) - this.LineItems.Count;
		if (num > 0)
		{
			for (int j = 0; j < num; j++)
			{
				GameObject gameObject = Object.Instantiate(this.LineItems[0].gameObject) as GameObject;
				LevelPackLineItem component = gameObject.GetComponent<LevelPackLineItem>();
				gameObject.name = string.Format("{0:D2}", this.LineItems.Count);
				gameObject.transform.parent = this.uiWrapContent.transform;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localPosition = Vector3.zero;
				this.LineItems.Add(component);
			}
		}
		for (int k = 0; k < this.LineItems.Count; k++)
		{
			if (k < this.LevelDataList.Count)
			{
				UnityVersionUtil.SetActiveRecursive(this.LineItems[k].gameObject, true);
				this.LineItems[k].UpdateInfo(this.Level_Dic[this.LevelDataList[k].ID], this.LevelDataList[k]);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.LineItems[k].gameObject, false);
			}
		}
		this.uiWrapContent.minIndex = 1 - this.LevelDataList.Count;
		this.WrapContentBottomWidget.height = this.LevelDataList.Count * this.uiWrapContent.itemSize;
		if (this.LevelDataList.Count == 1)
		{
			this.uiWrapContent.maxIndex = 1;
		}
		this.uiWrapContent.SortBasedOnScrollMovement();
		this.uiScrollView.ResetPosition();
		this.uiWrapContent.enabled = true;
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Welfare", "LevelGift", "open");
	}

	// Token: 0x06003D82 RID: 15746 RVA: 0x00113DA4 File Offset: 0x00111FA4
	public void UpdateInfo(string id)
	{
		if (this.Level_Dic.ContainsKey(id))
		{
			this.Level_Dic[id].state = 2L;
		}
		for (int i = 0; i < this.LevelDataList.Count; i++)
		{
			if (this.LevelDataList[i].ID.Equals(id))
			{
				for (int j = 0; j < this.LineItems.Count; j++)
				{
					this.LineItems[j].refershInfo(this.Level_Dic[this.LevelDataList[i].ID], this.LevelDataList[i]);
				}
				SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Welfare", "LevelGift", string.Format("require_{0}", id));
				break;
			}
		}
	}

	// Token: 0x06003D83 RID: 15747 RVA: 0x00113E88 File Offset: 0x00112088
	private void OnInitializeItem(GameObject obj, int index, int realIndex)
	{
		LevelPackLineItem itemLogic = this.LineItems[index];
		this.ResetItemLine(itemLogic, Mathf.Abs(realIndex));
	}

	// Token: 0x06003D84 RID: 15748 RVA: 0x00113EB0 File Offset: 0x001120B0
	private void ResetItemLine(LevelPackLineItem itemLogic, int idx)
	{
		if (idx < this.LevelDataList.Count)
		{
			itemLogic.UpdateInfo(this.Level_Dic[this.LevelDataList[idx].ID], this.LevelDataList[idx]);
		}
	}

	// Token: 0x040028FD RID: 10493
	public UITexture bannerTexture;

	// Token: 0x040028FE RID: 10494
	public UIWrapContentNew uiWrapContent;

	// Token: 0x040028FF RID: 10495
	private int lineMinCount = 6;

	// Token: 0x04002900 RID: 10496
	public UIWidget WrapContentBottomWidget;

	// Token: 0x04002901 RID: 10497
	public UIScrollView uiScrollView;

	// Token: 0x04002902 RID: 10498
	private Dictionary<string, level_pack> Level_Dic;

	// Token: 0x04002903 RID: 10499
	public List<LevelPackLineItem> LineItems;

	// Token: 0x04002904 RID: 10500
	private List<level_pack> Level_list;

	// Token: 0x04002905 RID: 10501
	private List<LevelPackageData> LevelDataList = new List<LevelPackageData>();
}
