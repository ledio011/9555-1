using System;

namespace UnityEngine.EventSystems
{
	// Token: 0x0200002A RID: 42
	public struct RaycastResult
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000EB RID: 235 RVA: 0x0000420C File Offset: 0x0000240C
		// (set) Token: 0x060000EC RID: 236 RVA: 0x00004214 File Offset: 0x00002414
		public GameObject gameObject
		{
			get
			{
				return this.m_GameObject;
			}
			set
			{
				this.m_GameObject = value;
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004220 File Offset: 0x00002420
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"Name: ",
				this.gameObject.name,
				"\nmodule: ",
				this.module.camera,
				"\ndistance: ",
				this.distance,
				"\nindex: ",
				this.index,
				"\ndepth: ",
				this.depth,
				"\nworldNormal: ",
				this.worldNormal,
				"\nworldPosition: ",
				this.worldPosition,
				"\nscreenPosition: ",
				this.screenPosition,
				"\nmodule.sortOrderPriority: ",
				this.module.sortOrderPriority,
				"\nmodule.renderOrderPriority: ",
				this.module.renderOrderPriority,
				"\nsortingLayer: ",
				this.sortingLayer,
				"\nsortingOrder: ",
				this.sortingOrder
			});
		}

		// Token: 0x0400006F RID: 111
		private GameObject m_GameObject;

		// Token: 0x04000070 RID: 112
		public BaseRaycaster module;

		// Token: 0x04000071 RID: 113
		public float distance;

		// Token: 0x04000072 RID: 114
		public float index;

		// Token: 0x04000073 RID: 115
		public int depth;

		// Token: 0x04000074 RID: 116
		public int sortingLayer;

		// Token: 0x04000075 RID: 117
		public int sortingOrder;

		// Token: 0x04000076 RID: 118
		public Vector3 worldPosition;

		// Token: 0x04000077 RID: 119
		public Vector3 worldNormal;

		// Token: 0x04000078 RID: 120
		public Vector2 screenPosition;
	}
}
