using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A33 RID: 2611
public class WorldMapRoot : SingletonUnity<WorldMapRoot>
{
	// Token: 0x06004C20 RID: 19488 RVA: 0x0019C7FC File Offset: 0x0019A9FC
	private void OnEnable()
	{
		this.InitTexture();
	}

	// Token: 0x06004C21 RID: 19489 RVA: 0x0019C804 File Offset: 0x0019AA04
	public void EnableReset(bool isact = false)
	{
		this.isActflag = isact;
	}

	// Token: 0x06004C22 RID: 19490 RVA: 0x0019C810 File Offset: 0x0019AA10
	public void Reset()
	{
		this.InitWorld(this.isActflag);
	}

	// Token: 0x06004C23 RID: 19491 RVA: 0x0019C820 File Offset: 0x0019AA20
	public void InitTexture()
	{
		List<string> list = new List<string>();
		if (this.WorldMapPic.mainTexture == null)
		{
			list.Add(GameDefine.WorldMap);
		}
		if (list.Count == 0)
		{
			return;
		}
		if (UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
		{
			base.StartCoroutine(BundleManager.LoadTexture(list, new BundleManager.LoadTextureDicFinish(this.TextureLoadFinish)));
		}
	}

	// Token: 0x06004C24 RID: 19492 RVA: 0x0019C88C File Offset: 0x0019AA8C
	private void TextureLoadFinish(Dictionary<string, Texture> retdic)
	{
		if (retdic.Count == 0)
		{
			return;
		}
		if (retdic.ContainsKey(GameDefine.WorldMap))
		{
			this.WorldMapPic.mainTexture = retdic[GameDefine.WorldMap];
		}
	}

	// Token: 0x06004C25 RID: 19493 RVA: 0x0019C8CC File Offset: 0x0019AACC
	private void InitWorld(bool Isact)
	{
		List<MapInfoData> mapInfoDataListByType = DataManager.GetMapInfoDataListByType(MAPTYPE.BIG_WORLD);
		mapInfoDataListByType.Add(DataManager.GetMapInfoDataListByType(MAPTYPE.TUTORIAL_CAR)[0]);
		int num = mapInfoDataListByType.Count - this.worldItemLogicList.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.worldItemLogicList[0].gameObject) as GameObject;
				WorldItemLogic component = gameObject.GetComponent<WorldItemLogic>();
				gameObject.transform.parent = this.worldItemLogicList[0].gameObject.transform.parent;
				this.worldItemLogicList.Add(component);
			}
		}
		for (int j = 0; j < this.worldItemLogicList.Count; j++)
		{
			if (j < mapInfoDataListByType.Count)
			{
				NGUITools.SetActive(this.worldItemLogicList[j].gameObject, true);
				this.worldItemLogicList[j].ResetItem(mapInfoDataListByType[j], Isact);
			}
			else
			{
				NGUITools.SetActive(this.worldItemLogicList[j].gameObject, false);
			}
		}
	}

	// Token: 0x06004C26 RID: 19494 RVA: 0x0019C9F4 File Offset: 0x0019ABF4
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.WorldMapRoot);
	}

	// Token: 0x040039DF RID: 14815
	public UITexture WorldMapPic;

	// Token: 0x040039E0 RID: 14816
	public List<WorldItemLogic> worldItemLogicList = new List<WorldItemLogic>();

	// Token: 0x040039E1 RID: 14817
	private bool isActflag;
}
