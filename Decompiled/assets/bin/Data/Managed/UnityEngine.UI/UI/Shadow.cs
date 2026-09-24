using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x02000086 RID: 134
	[AddComponentMenu("UI/Effects/Shadow", 14)]
	public class Shadow : BaseVertexEffect
	{
		// Token: 0x06000454 RID: 1108 RVA: 0x00012580 File Offset: 0x00010780
		protected Shadow()
		{
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x000125D0 File Offset: 0x000107D0
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x000125D8 File Offset: 0x000107D8
		public Color effectColor
		{
			get
			{
				return this.m_EffectColor;
			}
			set
			{
				this.m_EffectColor = value;
				if (base.graphic != null)
				{
					base.graphic.SetVerticesDirty();
				}
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x00012600 File Offset: 0x00010800
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x00012608 File Offset: 0x00010808
		public Vector2 effectDistance
		{
			get
			{
				return this.m_EffectDistance;
			}
			set
			{
				if (value.x > 600f)
				{
					value.x = 600f;
				}
				if (value.x < -600f)
				{
					value.x = -600f;
				}
				if (value.y > 600f)
				{
					value.y = 600f;
				}
				if (value.y < -600f)
				{
					value.y = -600f;
				}
				if (this.m_EffectDistance == value)
				{
					return;
				}
				this.m_EffectDistance = value;
				if (base.graphic != null)
				{
					base.graphic.SetVerticesDirty();
				}
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x000126C0 File Offset: 0x000108C0
		// (set) Token: 0x0600045A RID: 1114 RVA: 0x000126C8 File Offset: 0x000108C8
		public bool useGraphicAlpha
		{
			get
			{
				return this.m_UseGraphicAlpha;
			}
			set
			{
				this.m_UseGraphicAlpha = value;
				if (base.graphic != null)
				{
					base.graphic.SetVerticesDirty();
				}
			}
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x000126F0 File Offset: 0x000108F0
		protected void ApplyShadow(List<UIVertex> verts, Color32 color, int start, int end, float x, float y)
		{
			int num = verts.Count * 2;
			if (verts.Capacity < num)
			{
				verts.Capacity = num;
			}
			for (int i = start; i < end; i++)
			{
				UIVertex uivertex = verts[i];
				verts.Add(uivertex);
				Vector3 position = uivertex.position;
				position.x += x;
				position.y += y;
				uivertex.position = position;
				Color32 color2 = color;
				if (this.m_UseGraphicAlpha)
				{
					color2.a = color2.a * verts[i].color.a / byte.MaxValue;
				}
				uivertex.color = color2;
				verts[i] = uivertex;
			}
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x000127B8 File Offset: 0x000109B8
		public override void ModifyVertices(List<UIVertex> verts)
		{
			if (!this.IsActive())
			{
				return;
			}
			this.ApplyShadow(verts, this.effectColor, 0, verts.Count, this.effectDistance.x, this.effectDistance.y);
		}

		// Token: 0x04000226 RID: 550
		[SerializeField]
		private Color m_EffectColor = new Color(0f, 0f, 0f, 0.5f);

		// Token: 0x04000227 RID: 551
		[SerializeField]
		private Vector2 m_EffectDistance = new Vector2(1f, -1f);

		// Token: 0x04000228 RID: 552
		[SerializeField]
		private bool m_UseGraphicAlpha = true;
	}
}
