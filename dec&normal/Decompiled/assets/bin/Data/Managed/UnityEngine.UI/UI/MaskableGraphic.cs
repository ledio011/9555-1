using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x02000072 RID: 114
	public abstract class MaskableGraphic : Graphic, IMaskable
	{
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000F108 File Offset: 0x0000D308
		// (set) Token: 0x0600038A RID: 906 RVA: 0x0000F110 File Offset: 0x0000D310
		public bool maskable
		{
			get
			{
				return this.m_Maskable;
			}
			set
			{
				if (value == this.m_Maskable)
				{
					return;
				}
				this.m_Maskable = value;
				this.SetMaterialDirty();
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0000F12C File Offset: 0x0000D32C
		// (set) Token: 0x0600038C RID: 908 RVA: 0x0000F194 File Offset: 0x0000D394
		public override Material material
		{
			get
			{
				this.UpdateInternalState();
				if (this.m_IncludeForMasking)
				{
					if (this.m_MaskMaterial == null)
					{
						this.m_MaskMaterial = StencilMaterial.Add(base.material, (1 << this.m_StencilValue) - 1);
					}
					return this.m_MaskMaterial ?? base.material;
				}
				return base.material;
			}
			set
			{
				base.material = value;
			}
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000F1A0 File Offset: 0x0000D3A0
		private void UpdateInternalState()
		{
			if (!this.m_ShouldRecalculate)
			{
				return;
			}
			this.m_StencilValue = this.GetStencilForGraphic();
			Transform parent = base.transform.parent;
			this.m_IncludeForMasking = false;
			List<Component> list = ComponentListPool.Get();
			while (this.m_Maskable && parent != null)
			{
				parent.GetComponents(typeof(IMask), list);
				if (list.Count > 0)
				{
					this.m_IncludeForMasking = true;
					break;
				}
				parent = parent.parent;
			}
			this.m_ShouldRecalculate = false;
			ComponentListPool.Release(list);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000F238 File Offset: 0x0000D438
		protected override void OnEnable()
		{
			base.OnEnable();
			this.m_ShouldRecalculate = true;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000F248 File Offset: 0x0000D448
		protected override void OnDisable()
		{
			base.OnDisable();
			this.ClearMaskMaterial();
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000F258 File Offset: 0x0000D458
		protected override void OnTransformParentChanged()
		{
			base.OnTransformParentChanged();
			this.m_ShouldRecalculate = true;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000F268 File Offset: 0x0000D468
		public virtual void ParentMaskStateChanged()
		{
			this.m_ShouldRecalculate = true;
			this.SetMaterialDirty();
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000F278 File Offset: 0x0000D478
		private void ClearMaskMaterial()
		{
			StencilMaterial.Remove(this.m_MaskMaterial);
			this.m_MaskMaterial = null;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000F28C File Offset: 0x0000D48C
		public override void SetMaterialDirty()
		{
			base.SetMaterialDirty();
			this.ClearMaskMaterial();
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000F29C File Offset: 0x0000D49C
		private int GetStencilForGraphic()
		{
			int num = 0;
			Transform parent = base.transform.parent;
			List<Component> list = ComponentListPool.Get();
			while (parent != null)
			{
				parent.GetComponents(typeof(IMask), list);
				for (int i = 0; i < list.Count; i++)
				{
					IMask mask = list[i] as IMask;
					if (mask != null && mask.MaskEnabled())
					{
						num++;
						num = Mathf.Clamp(num, 0, 8);
						break;
					}
				}
				parent = parent.parent;
			}
			ComponentListPool.Release(list);
			return num;
		}

		// Token: 0x040001C3 RID: 451
		[NonSerialized]
		private bool m_Maskable = true;

		// Token: 0x040001C4 RID: 452
		[NonSerialized]
		protected Material m_MaskMaterial;

		// Token: 0x040001C5 RID: 453
		[NonSerialized]
		protected bool m_IncludeForMasking;

		// Token: 0x040001C6 RID: 454
		[NonSerialized]
		protected int m_StencilValue;

		// Token: 0x040001C7 RID: 455
		[NonSerialized]
		protected bool m_ShouldRecalculate = true;
	}
}
