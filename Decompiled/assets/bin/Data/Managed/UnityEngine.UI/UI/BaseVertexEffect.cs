using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x02000032 RID: 50
	[ExecuteInEditMode]
	public abstract class BaseVertexEffect : UIBehaviour, IVertexModifier
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00005440 File Offset: 0x00003640
		protected Graphic graphic
		{
			get
			{
				if (this.m_Graphic == null)
				{
					this.m_Graphic = base.GetComponent<Graphic>();
				}
				return this.m_Graphic;
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00005468 File Offset: 0x00003668
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.graphic != null)
			{
				this.graphic.SetVerticesDirty();
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000548C File Offset: 0x0000368C
		protected override void OnDisable()
		{
			if (this.graphic != null)
			{
				this.graphic.SetVerticesDirty();
			}
			base.OnDisable();
		}

		// Token: 0x0600013E RID: 318
		public abstract void ModifyVertices(List<UIVertex> verts);

		// Token: 0x0400009A RID: 154
		[NonSerialized]
		private Graphic m_Graphic;
	}
}
