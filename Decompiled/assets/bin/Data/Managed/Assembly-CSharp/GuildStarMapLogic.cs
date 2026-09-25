using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000A17 RID: 2583
public class GuildStarMapLogic : MonoBehaviour
{
	// Token: 0x06004A47 RID: 19015 RVA: 0x00184CB4 File Offset: 0x00182EB4
	public void UpdateInfo(Dictionary<string, guild_star> GuildstarsDic, int CurMapId)
	{
		this.MapName = string.Empty;
		this.DataList.Clear();
		this.DataList = DataManager.GetGuildStarDataListByMapID(CurMapId);
		if (this.DataList.Count > 0)
		{
			this.MapName = this.DataList[0].PicName;
		}
		this.InitTexture();
		int num = this.DataList.Count - this.StarItems.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.StarItems[0].gameObject) as GameObject;
				UISprite component = gameObject.GetComponent<UISprite>();
				gameObject.name = string.Format("{0:D2}", this.StarItems.Count);
				gameObject.transform.parent = base.transform;
				gameObject.transform.localScale = Vector3.one;
				this.StarItems.Add(component);
			}
		}
		NGUITools.SetActive(this.NextTra.gameObject, false);
		int num2 = -1;
		for (int j = 0; j < this.StarItems.Count; j++)
		{
			if (j < this.DataList.Count)
			{
				NGUITools.SetActive(this.StarItems[j].gameObject, true);
				this.StarItems[j].transform.localPosition = new Vector3(this.DataList[j].PosX, this.DataList[j].PosY);
				if (GuildstarsDic.ContainsKey(this.DataList[j].ID) && GuildstarsDic[this.DataList[j].ID].state == 1L)
				{
					this.StarItems[j].enabled = true;
				}
				else
				{
					this.StarItems[j].enabled = false;
					if (num2 == -1)
					{
						num2 = j;
						this.NextTra.localPosition = new Vector3(this.DataList[j].PosX, this.DataList[j].PosY);
						NGUITools.SetActive(this.NextTra.gameObject, true);
					}
				}
			}
			else
			{
				NGUITools.SetActive(this.StarItems[j].gameObject, false);
			}
		}
	}

	// Token: 0x06004A48 RID: 19016 RVA: 0x00184F2C File Offset: 0x0018312C
	public void InitTexture()
	{
		if (string.IsNullOrEmpty(this.MapName))
		{
			return;
		}
		if ((this.mainTexture.mainTexture == null || !this.mainTexture.mainTexture.name.Equals(this.MapName)) && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
		{
			base.StartCoroutine(BundleManager.LoadTexture(this.MapName, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
		}
	}

	// Token: 0x06004A49 RID: 19017 RVA: 0x00184FB0 File Offset: 0x001831B0
	private void TextureLoadFinish(string name, Texture textureObj)
	{
		if (textureObj != null)
		{
			this.mainTexture.mainTexture = textureObj;
		}
	}

	// Token: 0x040037C7 RID: 14279
	public UITexture mainTexture;

	// Token: 0x040037C8 RID: 14280
	public List<UISprite> StarItems;

	// Token: 0x040037C9 RID: 14281
	public Transform NextTra;

	// Token: 0x040037CA RID: 14282
	private List<GuildStarData> DataList = new List<GuildStarData>();

	// Token: 0x040037CB RID: 14283
	private string MapName;
}
