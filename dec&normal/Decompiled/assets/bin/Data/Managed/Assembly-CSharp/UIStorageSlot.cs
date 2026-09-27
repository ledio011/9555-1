using System;
using UnityEngine;

// Token: 0x0200001E RID: 30
[AddComponentMenu("NGUI/Examples/UI Storage Slot")]
public class UIStorageSlot : UIItemSlot
{
	// Token: 0x17000009 RID: 9
	// (get) Token: 0x0600008A RID: 138 RVA: 0x00004BCC File Offset: 0x00002DCC
	protected override InvGameItem observedItem
	{
		get
		{
			return (!(this.storage != null)) ? null : this.storage.GetItem(this.slot);
		}
	}

	// Token: 0x0600008B RID: 139 RVA: 0x00004C04 File Offset: 0x00002E04
	protected override InvGameItem Replace(InvGameItem item)
	{
		return (!(this.storage != null)) ? item : this.storage.Replace(this.slot, item);
	}

	// Token: 0x04000080 RID: 128
	public UIItemStorage storage;

	// Token: 0x04000081 RID: 129
	public int slot;
}
