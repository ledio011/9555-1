using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020008E7 RID: 2279
public class RetrieveRootLogic : SingletonUnity<RetrieveRootLogic>
{
	// Token: 0x06003D9F RID: 15775 RVA: 0x00114EF4 File Offset: 0x001130F4
	protected override void Awake()
	{
		base.Awake();
		this.uiWrapContent.enabled = false;
		UIWrapContentNew uiwrapContentNew = this.uiWrapContent;
		uiwrapContentNew.onInitializeItem = (UIWrapContentNew.OnInitializeItem)Delegate.Combine(uiwrapContentNew.onInitializeItem, new UIWrapContentNew.OnInitializeItem(this.OnInitializeItem));
	}

	// Token: 0x06003DA0 RID: 15776 RVA: 0x00114F3C File Offset: 0x0011313C
	public void EnableReset()
	{
		for (int i = 0; i < this.LineItems.Count; i++)
		{
			UnityVersionUtil.SetActiveRecursive(this.LineItems[i].gameObject, false);
		}
		this.InitTexture();
		if (SingletonUnity<AutoPopUIRoot>.Exists && UnityVersionUtil.IsActive(SingletonUnity<AutoPopUIRoot>.Instance.gameObject))
		{
			SingletonUnity<AutoPopUIRoot>.Instance.RemoveUI(GameDefine.AUTOPOPTYPE.RETRIEVE);
		}
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Welfare", "Retrive", "open");
	}

	// Token: 0x06003DA1 RID: 15777 RVA: 0x00114FC4 File Offset: 0x001131C4
	public void InitTexture()
	{
		if (this.bannerTexture.mainTexture == null && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
		{
			base.StartCoroutine(BundleManager.LoadTexture(GameDefine.TextureBannerRetrieve, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
		}
	}

	// Token: 0x06003DA2 RID: 15778 RVA: 0x00115014 File Offset: 0x00113214
	private void TextureLoadFinish(string name, Texture textureObj)
	{
		this.bannerTexture.mainTexture = textureObj;
	}

	// Token: 0x06003DA3 RID: 15779 RVA: 0x00115024 File Offset: 0x00113224
	public void Reset(ret_request_retrieve_info.request request)
	{
		this.RetrieveDataList.Clear();
		this.Retrieve_Dic = request.info;
		this.Retrieve_list = new List<retrieve_info>(request.info.Values);
		for (int i = this.Retrieve_list.Count - 1; i >= 0; i--)
		{
			if (this.Retrieve_list[i].state == 1L)
			{
				this.Retrieve_list.RemoveAt(i);
			}
			else
			{
				RetrieveData retrieveDataBuyId = DataManager.GetRetrieveDataBuyId(this.Retrieve_list[i].ID);
				if (retrieveDataBuyId == null || (retrieveDataBuyId.isGuildDance && !SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild()))
				{
					this.Retrieve_list.RemoveAt(i);
				}
			}
		}
		for (int j = 0; j < this.Retrieve_list.Count; j++)
		{
			this.RetrieveDataList.Add(DataManager.GetRetrieveDataBuyId(this.Retrieve_list[j].ID));
		}
		this.RetrieveDataList.Sort(delegate(RetrieveData x, RetrieveData y)
		{
			if (this.Retrieve_Dic[x.ID].state != this.Retrieve_Dic[y.ID].state)
			{
				return (int)this.Retrieve_Dic[x.ID].state - (int)this.Retrieve_Dic[y.ID].state;
			}
			if (x.ID.Length != y.ID.Length)
			{
				return x.ID.Length - y.ID.Length;
			}
			return x.ID.CompareTo(y.ID);
		});
		int num = Mathf.Min(this.RetrieveDataList.Count, this.lineMinCount) - this.LineItems.Count;
		if (num > 0)
		{
			for (int k = 0; k < num; k++)
			{
				GameObject gameObject = Object.Instantiate(this.LineItems[0].gameObject) as GameObject;
				RetrieveLineLogic component = gameObject.GetComponent<RetrieveLineLogic>();
				gameObject.name = string.Format("{0:D2}", this.LineItems.Count);
				gameObject.transform.parent = this.uiWrapContent.transform;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localPosition = Vector3.zero;
				this.LineItems.Add(component);
			}
		}
		for (int l = 0; l < this.LineItems.Count; l++)
		{
			if (l < this.RetrieveDataList.Count)
			{
				UnityVersionUtil.SetActiveRecursive(this.LineItems[l].gameObject, true);
				this.LineItems[l].UpdateInfo(this.Retrieve_Dic[this.RetrieveDataList[l].ID], this.RetrieveDataList[l]);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.LineItems[l].gameObject, false);
			}
		}
		this.uiWrapContent.minIndex = 1 - this.RetrieveDataList.Count;
		this.WrapContentBottomWidget.height = this.RetrieveDataList.Count * this.uiWrapContent.itemSize;
		this.uiWrapContent.SortBasedOnScrollMovement();
		this.uiScrollView.ResetPosition();
		this.uiWrapContent.enabled = true;
	}

	// Token: 0x06003DA4 RID: 15780 RVA: 0x00115310 File Offset: 0x00113510
	private void OnInitializeItem(GameObject obj, int index, int realIndex)
	{
		RetrieveLineLogic itemLogic = this.LineItems[index];
		this.ResetItemLine(itemLogic, Mathf.Abs(realIndex));
	}

	// Token: 0x06003DA5 RID: 15781 RVA: 0x00115338 File Offset: 0x00113538
	private void ResetItemLine(RetrieveLineLogic itemLogic, int idx)
	{
		if (idx < this.RetrieveDataList.Count)
		{
			itemLogic.UpdateInfo(this.Retrieve_Dic[this.RetrieveDataList[idx].ID], this.RetrieveDataList[idx]);
		}
	}

	// Token: 0x0400292D RID: 10541
	public UITexture bannerTexture;

	// Token: 0x0400292E RID: 10542
	public UIWrapContentNew uiWrapContent;

	// Token: 0x0400292F RID: 10543
	private int lineMinCount = 6;

	// Token: 0x04002930 RID: 10544
	public UIWidget WrapContentBottomWidget;

	// Token: 0x04002931 RID: 10545
	public UIScrollView uiScrollView;

	// Token: 0x04002932 RID: 10546
	private Dictionary<string, retrieve_info> Retrieve_Dic;

	// Token: 0x04002933 RID: 10547
	public List<RetrieveLineLogic> LineItems;

	// Token: 0x04002934 RID: 10548
	private List<retrieve_info> Retrieve_list;

	// Token: 0x04002935 RID: 10549
	private List<RetrieveData> RetrieveDataList = new List<RetrieveData>();
}
