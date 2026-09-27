using System;
using System.Collections.Generic;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000005 RID: 5
	public abstract class BaseRaycaster : UIBehaviour
	{
		// Token: 0x0600001E RID: 30
		public abstract void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList);

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001F RID: 31
		public abstract Camera eventCamera { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002410 File Offset: 0x00000610
		[Obsolete("Please use sortOrderPriority and renderOrderPriority", false)]
		public virtual int priority
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002414 File Offset: 0x00000614
		public virtual int sortOrderPriority
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000241C File Offset: 0x0000061C
		public virtual int renderOrderPriority
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002424 File Offset: 0x00000624
		protected override void OnEnable()
		{
			base.OnEnable();
			RaycasterManager.AddRaycaster(this);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002434 File Offset: 0x00000634
		protected override void OnDisable()
		{
			RaycasterManager.RemoveRaycasters(this);
			base.OnDisable();
		}
	}
}
