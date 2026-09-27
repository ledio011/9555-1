using System;
using UnityEngine;

// Token: 0x0200001B RID: 27
[AddComponentMenu("NGUI/Examples/UI Equipment Slot")]
public class UIEquipmentSlot : UIItemSlot
{
	// Token: 0x17000006 RID: 6
	// (get) Token: 0x06000079 RID: 121 RVA: 0x000044C4 File Offset: 0x000026C4
	protected override InvGameItem observedItem
	{
		get
		{
			return (!(this.equipment != null)) ? null : this.equipment.GetItem(this.slot);
		}
	}

	// Token: 0x0600007A RID: 122 RVA: 0x000044FC File Offset: 0x000026FC
	protected override InvGameItem Replace(InvGameItem item)
	{
		return (!(this.equipment != null)) ? item : this.equipment.Replace(this.slot, item);
	}

	// Token: 0x0400006D RID: 109
	public InvEquipment equipment;

	// Token: 0x0400006E RID: 110
	public InvBaseItem.Slot slot;
}
