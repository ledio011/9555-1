using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200001D RID: 29
[AddComponentMenu("NGUI/Examples/UI Item Storage")]
public class UIItemStorage : MonoBehaviour
{
	// Token: 0x17000008 RID: 8
	// (get) Token: 0x06000085 RID: 133 RVA: 0x000049B4 File Offset: 0x00002BB4
	public List<InvGameItem> items
	{
		get
		{
			while (this.mItems.Count < this.maxItemCount)
			{
				this.mItems.Add(null);
			}
			return this.mItems;
		}
	}

	// Token: 0x06000086 RID: 134 RVA: 0x000049E4 File Offset: 0x00002BE4
	public InvGameItem GetItem(int slot)
	{
		return (slot >= this.items.Count) ? null : this.mItems[slot];
	}

	// Token: 0x06000087 RID: 135 RVA: 0x00004A0C File Offset: 0x00002C0C
	public InvGameItem Replace(int slot, InvGameItem item)
	{
		if (slot < this.maxItemCount)
		{
			InvGameItem result = this.items[slot];
			this.mItems[slot] = item;
			return result;
		}
		return item;
	}

	// Token: 0x06000088 RID: 136 RVA: 0x00004A44 File Offset: 0x00002C44
	private void Start()
	{
		if (this.template != null)
		{
			int num = 0;
			Bounds bounds = default(Bounds);
			for (int i = 0; i < this.maxRows; i++)
			{
				for (int j = 0; j < this.maxColumns; j++)
				{
					GameObject gameObject = NGUITools.AddChild(base.gameObject, this.template);
					Transform transform = gameObject.transform;
					transform.localPosition = new Vector3((float)this.padding + ((float)j + 0.5f) * (float)this.spacing, (float)(-(float)this.padding) - ((float)i + 0.5f) * (float)this.spacing, 0f);
					UIStorageSlot component = gameObject.GetComponent<UIStorageSlot>();
					if (component != null)
					{
						component.storage = this;
						component.slot = num;
					}
					bounds.Encapsulate(new Vector3((float)this.padding * 2f + (float)((j + 1) * this.spacing), (float)(-(float)this.padding) * 2f - (float)((i + 1) * this.spacing), 0f));
					if (++num >= this.maxItemCount)
					{
						if (this.background != null)
						{
							this.background.transform.localScale = bounds.size;
						}
						return;
					}
				}
			}
			if (this.background != null)
			{
				this.background.transform.localScale = bounds.size;
			}
		}
	}

	// Token: 0x04000078 RID: 120
	public int maxItemCount = 8;

	// Token: 0x04000079 RID: 121
	public int maxRows = 4;

	// Token: 0x0400007A RID: 122
	public int maxColumns = 4;

	// Token: 0x0400007B RID: 123
	public GameObject template;

	// Token: 0x0400007C RID: 124
	public UIWidget background;

	// Token: 0x0400007D RID: 125
	public int spacing = 128;

	// Token: 0x0400007E RID: 126
	public int padding = 10;

	// Token: 0x0400007F RID: 127
	private List<InvGameItem> mItems = new List<InvGameItem>();
}
