using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x02000079 RID: 121
	[AddComponentMenu("UI/Raw Image", 12)]
	public class RawImage : MaskableGraphic
	{
		// Token: 0x060003A5 RID: 933 RVA: 0x0000F66C File Offset: 0x0000D86C
		protected RawImage()
		{
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x0000F694 File Offset: 0x0000D894
		public override Texture mainTexture
		{
			get
			{
				return (!(this.m_Texture == null)) ? this.m_Texture : Graphic.s_WhiteTexture;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x0000F6B8 File Offset: 0x0000D8B8
		// (set) Token: 0x060003A8 RID: 936 RVA: 0x0000F6C0 File Offset: 0x0000D8C0
		public Texture texture
		{
			get
			{
				return this.m_Texture;
			}
			set
			{
				if (this.m_Texture == value)
				{
					return;
				}
				this.m_Texture = value;
				this.SetVerticesDirty();
				this.SetMaterialDirty();
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x0000F6E8 File Offset: 0x0000D8E8
		// (set) Token: 0x060003AA RID: 938 RVA: 0x0000F6F0 File Offset: 0x0000D8F0
		public Rect uvRect
		{
			get
			{
				return this.m_UVRect;
			}
			set
			{
				if (this.m_UVRect == value)
				{
					return;
				}
				this.m_UVRect = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000F714 File Offset: 0x0000D914
		public override void SetNativeSize()
		{
			Texture mainTexture = this.mainTexture;
			if (mainTexture != null)
			{
				int num = Mathf.RoundToInt((float)mainTexture.width * this.uvRect.width);
				int num2 = Mathf.RoundToInt((float)mainTexture.height * this.uvRect.height);
				base.rectTransform.anchorMax = base.rectTransform.anchorMin;
				base.rectTransform.sizeDelta = new Vector2((float)num, (float)num2);
			}
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000F798 File Offset: 0x0000D998
		protected override void OnFillVBO(List<UIVertex> vbo)
		{
			Texture mainTexture = this.mainTexture;
			if (mainTexture != null)
			{
				Vector4 zero = Vector4.zero;
				int num = Mathf.RoundToInt((float)mainTexture.width * this.uvRect.width);
				int num2 = Mathf.RoundToInt((float)mainTexture.height * this.uvRect.height);
				float num3 = (float)(((num & 1) != 0) ? (num + 1) : num);
				float num4 = (float)(((num2 & 1) != 0) ? (num2 + 1) : num2);
				zero.x = 0f;
				zero.y = 0f;
				zero.z = (float)num / num3;
				zero.w = (float)num2 / num4;
				zero.x -= base.rectTransform.pivot.x;
				zero.y -= base.rectTransform.pivot.y;
				zero.z -= base.rectTransform.pivot.x;
				zero.w -= base.rectTransform.pivot.y;
				zero.x *= base.rectTransform.rect.width;
				zero.y *= base.rectTransform.rect.height;
				zero.z *= base.rectTransform.rect.width;
				zero.w *= base.rectTransform.rect.height;
				vbo.Clear();
				UIVertex simpleVert = UIVertex.simpleVert;
				simpleVert.position = new Vector2(zero.x, zero.y);
				simpleVert.uv0 = new Vector2(this.m_UVRect.xMin, this.m_UVRect.yMin);
				simpleVert.color = base.color;
				vbo.Add(simpleVert);
				simpleVert.position = new Vector2(zero.x, zero.w);
				simpleVert.uv0 = new Vector2(this.m_UVRect.xMin, this.m_UVRect.yMax);
				simpleVert.color = base.color;
				vbo.Add(simpleVert);
				simpleVert.position = new Vector2(zero.z, zero.w);
				simpleVert.uv0 = new Vector2(this.m_UVRect.xMax, this.m_UVRect.yMax);
				simpleVert.color = base.color;
				vbo.Add(simpleVert);
				simpleVert.position = new Vector2(zero.z, zero.y);
				simpleVert.uv0 = new Vector2(this.m_UVRect.xMax, this.m_UVRect.yMin);
				simpleVert.color = base.color;
				vbo.Add(simpleVert);
			}
		}

		// Token: 0x040001D7 RID: 471
		[SerializeField]
		[FormerlySerializedAs("m_Tex")]
		private Texture m_Texture;

		// Token: 0x040001D8 RID: 472
		[SerializeField]
		private Rect m_UVRect = new Rect(0f, 0f, 1f, 1f);
	}
}
