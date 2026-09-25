using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x02000071 RID: 113
	[ExecuteInEditMode]
	[AddComponentMenu("UI/Mask", 13)]
	public class Mask : UIBehaviour, IGraphicEnabledDisabled, IMask, IMaterialModifier, ICanvasRaycastFilter
	{
		// Token: 0x0600037B RID: 891 RVA: 0x0000EE6C File Offset: 0x0000D06C
		protected Mask()
		{
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600037C RID: 892 RVA: 0x0000EE7C File Offset: 0x0000D07C
		private Graphic graphic
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

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0000EEA4 File Offset: 0x0000D0A4
		// (set) Token: 0x0600037E RID: 894 RVA: 0x0000EEAC File Offset: 0x0000D0AC
		public bool showMaskGraphic
		{
			get
			{
				return this.m_ShowMaskGraphic;
			}
			set
			{
				if (this.m_ShowMaskGraphic == value)
				{
					return;
				}
				this.m_ShowMaskGraphic = value;
				if (this.graphic != null)
				{
					this.graphic.SetMaterialDirty();
				}
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0000EEE0 File Offset: 0x0000D0E0
		public RectTransform rectTransform
		{
			get
			{
				RectTransform result;
				if ((result = this.m_RectTransform) == null)
				{
					result = (this.m_RectTransform = base.GetComponent<RectTransform>());
				}
				return result;
			}
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000EF0C File Offset: 0x0000D10C
		public virtual bool MaskEnabled()
		{
			return this.IsActive() && this.graphic != null;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000EF28 File Offset: 0x0000D128
		public virtual void OnSiblingGraphicEnabledDisabled()
		{
			this.NotifyMaskStateChanged();
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000EF30 File Offset: 0x0000D130
		private void NotifyMaskStateChanged()
		{
			if (this.graphic != null)
			{
				this.graphic.canvasRenderer.isMask = this.IsActive();
				this.graphic.SetMaterialDirty();
			}
			List<Component> list = ComponentListPool.Get();
			base.GetComponentsInChildren<Component>(list);
			for (int i = 0; i < list.Count; i++)
			{
				if (!(list[i] == null) && !(list[i].gameObject == base.gameObject))
				{
					IMaskable maskable = list[i] as IMaskable;
					if (maskable != null)
					{
						maskable.ParentMaskStateChanged();
					}
				}
			}
			ComponentListPool.Release(list);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000EFE8 File Offset: 0x0000D1E8
		private void ClearCachedMaterial()
		{
			if (this.m_RenderMaterial != null)
			{
				Misc.DestroyImmediate(this.m_RenderMaterial);
			}
			this.m_RenderMaterial = null;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000F010 File Offset: 0x0000D210
		protected override void OnEnable()
		{
			base.OnEnable();
			this.NotifyMaskStateChanged();
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000F020 File Offset: 0x0000D220
		protected override void OnDisable()
		{
			base.OnDisable();
			this.ClearCachedMaterial();
			this.NotifyMaskStateChanged();
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000F034 File Offset: 0x0000D234
		public virtual bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
		{
			return RectTransformUtility.RectangleContainsScreenPoint(this.rectTransform, sp, eventCamera);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000F044 File Offset: 0x0000D244
		public virtual Material GetModifiedMaterial(Material baseMaterial)
		{
			this.ClearCachedMaterial();
			if (!this.IsActive())
			{
				return baseMaterial;
			}
			this.m_RenderMaterial = new Material(baseMaterial)
			{
				name = "Mask  (" + baseMaterial.name + ")",
				hideFlags = HideFlags.HideAndDontSave
			};
			if (this.m_RenderMaterial.HasProperty("_ColorMask"))
			{
				this.m_RenderMaterial.SetInt("_ColorMask", (!this.m_ShowMaskGraphic) ? 0 : 15);
			}
			else
			{
				Debug.LogWarning("Material " + baseMaterial + " doesn't have color mask", baseMaterial);
			}
			return this.m_RenderMaterial;
		}

		// Token: 0x040001BF RID: 447
		[SerializeField]
		[FormerlySerializedAs("m_ShowGraphic")]
		private bool m_ShowMaskGraphic = true;

		// Token: 0x040001C0 RID: 448
		private Material m_RenderMaterial;

		// Token: 0x040001C1 RID: 449
		private Graphic m_Graphic;

		// Token: 0x040001C2 RID: 450
		private RectTransform m_RectTransform;
	}
}
