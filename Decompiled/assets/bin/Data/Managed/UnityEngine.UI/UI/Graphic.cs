using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI.CoroutineTween;

namespace UnityEngine.UI
{
	// Token: 0x0200004A RID: 74
	[RequireComponent(typeof(RectTransform))]
	[DisallowMultipleComponent]
	[RequireComponent(typeof(CanvasRenderer))]
	[ExecuteInEditMode]
	public abstract class Graphic : UIBehaviour, ICanvasElement
	{
		// Token: 0x060001E3 RID: 483 RVA: 0x00006A48 File Offset: 0x00004C48
		protected Graphic()
		{
			if (this.m_ColorTweenRunner == null)
			{
				this.m_ColorTweenRunner = new TweenRunner<ColorTween>();
			}
			this.m_ColorTweenRunner.Init(this);
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00006AE0 File Offset: 0x00004CE0
		public static Material defaultGraphicMaterial
		{
			get
			{
				if (Graphic.s_DefaultUI == null)
				{
					Graphic.s_DefaultUI = Canvas.GetDefaultCanvasMaterial();
				}
				return Graphic.s_DefaultUI;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00006B04 File Offset: 0x00004D04
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x00006B0C File Offset: 0x00004D0C
		public Color color
		{
			get
			{
				return this.m_Color;
			}
			set
			{
				if (SetPropertyUtility.SetColor(ref this.m_Color, value))
				{
					this.SetVerticesDirty();
				}
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00006B28 File Offset: 0x00004D28
		public virtual void SetAllDirty()
		{
			this.SetLayoutDirty();
			this.SetVerticesDirty();
			this.SetMaterialDirty();
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00006B3C File Offset: 0x00004D3C
		public virtual void SetLayoutDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			if (this.m_OnDirtyLayoutCallback != null)
			{
				this.m_OnDirtyLayoutCallback();
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00006B6C File Offset: 0x00004D6C
		public virtual void SetVerticesDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			this.m_VertsDirty = true;
			CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild(this);
			if (this.m_OnDirtyVertsCallback != null)
			{
				this.m_OnDirtyVertsCallback();
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00006BA0 File Offset: 0x00004DA0
		public virtual void SetMaterialDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			this.m_MaterialDirty = true;
			CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild(this);
			if (this.m_OnDirtyMaterialCallback != null)
			{
				this.m_OnDirtyMaterialCallback();
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00006BD4 File Offset: 0x00004DD4
		protected override void OnRectTransformDimensionsChange()
		{
			if (base.gameObject.activeInHierarchy)
			{
				if (CanvasUpdateRegistry.IsRebuildingLayout())
				{
					this.SetVerticesDirty();
				}
				else
				{
					this.SetVerticesDirty();
					this.SetLayoutDirty();
				}
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00006C08 File Offset: 0x00004E08
		protected override void OnBeforeTransformParentChanged()
		{
			GraphicRegistry.UnregisterGraphicForCanvas(this.canvas, this);
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00006C24 File Offset: 0x00004E24
		protected override void OnTransformParentChanged()
		{
			if (!this.IsActive())
			{
				return;
			}
			this.CacheCanvas();
			GraphicRegistry.RegisterGraphicForCanvas(this.canvas, this);
			this.SetAllDirty();
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00006C4C File Offset: 0x00004E4C
		public int depth
		{
			get
			{
				return this.canvasRenderer.absoluteDepth;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00006C5C File Offset: 0x00004E5C
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

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00006C88 File Offset: 0x00004E88
		public Canvas canvas
		{
			get
			{
				if (this.m_Canvas == null)
				{
					this.CacheCanvas();
				}
				return this.m_Canvas;
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00006CA8 File Offset: 0x00004EA8
		private void CacheCanvas()
		{
			List<Canvas> list = CanvasListPool.Get();
			base.gameObject.GetComponentsInParent<Canvas>(false, list);
			if (list.Count > 0)
			{
				this.m_Canvas = list[0];
			}
			CanvasListPool.Release(list);
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00006CE8 File Offset: 0x00004EE8
		public CanvasRenderer canvasRenderer
		{
			get
			{
				if (this.m_CanvasRender == null)
				{
					this.m_CanvasRender = base.GetComponent<CanvasRenderer>();
				}
				return this.m_CanvasRender;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00006D10 File Offset: 0x00004F10
		public virtual Material defaultMaterial
		{
			get
			{
				return Graphic.defaultGraphicMaterial;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00006D18 File Offset: 0x00004F18
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x00006D3C File Offset: 0x00004F3C
		public virtual Material material
		{
			get
			{
				return (!(this.m_Material != null)) ? this.defaultMaterial : this.m_Material;
			}
			set
			{
				if (this.m_Material == value)
				{
					return;
				}
				this.m_Material = value;
				this.SetMaterialDirty();
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00006D60 File Offset: 0x00004F60
		public virtual Material materialForRendering
		{
			get
			{
				List<Component> list = ComponentListPool.Get();
				base.GetComponents(typeof(IMaterialModifier), list);
				Material material = this.material;
				for (int i = 0; i < list.Count; i++)
				{
					material = (list[i] as IMaterialModifier).GetModifiedMaterial(material);
				}
				ComponentListPool.Release(list);
				return material;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00006DBC File Offset: 0x00004FBC
		public virtual Texture mainTexture
		{
			get
			{
				return Graphic.s_WhiteTexture;
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00006DC4 File Offset: 0x00004FC4
		protected override void OnEnable()
		{
			base.OnEnable();
			this.CacheCanvas();
			GraphicRegistry.RegisterGraphicForCanvas(this.canvas, this);
			if (Graphic.s_WhiteTexture == null)
			{
				Graphic.s_WhiteTexture = Texture2D.whiteTexture;
			}
			this.SetAllDirty();
			this.SendGraphicEnabledDisabled();
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00006E04 File Offset: 0x00005004
		protected override void OnDisable()
		{
			GraphicRegistry.UnregisterGraphicForCanvas(this.canvas, this);
			CanvasUpdateRegistry.UnRegisterCanvasElementForRebuild(this);
			if (this.canvasRenderer != null)
			{
				this.canvasRenderer.Clear();
			}
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			this.SendGraphicEnabledDisabled();
			base.OnDisable();
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00006E58 File Offset: 0x00005058
		private void SendGraphicEnabledDisabled()
		{
			List<Component> list = ComponentListPool.Get();
			base.GetComponents(typeof(IGraphicEnabledDisabled), list);
			for (int i = 0; i < list.Count; i++)
			{
				((IGraphicEnabledDisabled)list[i]).OnSiblingGraphicEnabledDisabled();
			}
			ComponentListPool.Release(list);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00006EAC File Offset: 0x000050AC
		protected override void OnCanvasHierarchyChanged()
		{
			if (!this.IsActive())
			{
				return;
			}
			Canvas canvas = this.m_Canvas;
			this.CacheCanvas();
			if (canvas != this.m_Canvas)
			{
				GraphicRegistry.UnregisterGraphicForCanvas(canvas, this);
				GraphicRegistry.RegisterGraphicForCanvas(this.canvas, this);
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00006EF8 File Offset: 0x000050F8
		public virtual void Rebuild(CanvasUpdate update)
		{
			if (update == CanvasUpdate.PreRender)
			{
				if (this.m_VertsDirty)
				{
					this.UpdateGeometry();
					this.m_VertsDirty = false;
				}
				if (this.m_MaterialDirty)
				{
					this.UpdateMaterial();
					this.m_MaterialDirty = false;
				}
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00006F48 File Offset: 0x00005148
		protected virtual void UpdateGeometry()
		{
			List<UIVertex> list = Graphic.s_VboPool.Get();
			if (this.rectTransform != null && this.rectTransform.rect.width >= 0f && this.rectTransform.rect.height >= 0f)
			{
				this.OnFillVBO(list);
			}
			List<Component> list2 = ComponentListPool.Get();
			base.GetComponents(typeof(IVertexModifier), list2);
			for (int i = 0; i < list2.Count; i++)
			{
				(list2[i] as IVertexModifier).ModifyVertices(list);
			}
			ComponentListPool.Release(list2);
			this.canvasRenderer.SetVertices(list);
			Graphic.s_VboPool.Release(list);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00007010 File Offset: 0x00005210
		protected virtual void UpdateMaterial()
		{
			if (this.IsActive())
			{
				this.canvasRenderer.SetMaterial(this.materialForRendering, this.mainTexture);
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00007034 File Offset: 0x00005234
		protected virtual void OnFillVBO(List<UIVertex> vbo)
		{
			Rect pixelAdjustedRect = this.GetPixelAdjustedRect();
			Vector4 vector = new Vector4(pixelAdjustedRect.x, pixelAdjustedRect.y, pixelAdjustedRect.x + pixelAdjustedRect.width, pixelAdjustedRect.y + pixelAdjustedRect.height);
			UIVertex simpleVert = UIVertex.simpleVert;
			simpleVert.color = this.color;
			simpleVert.position = new Vector3(vector.x, vector.y);
			simpleVert.uv0 = new Vector2(0f, 0f);
			vbo.Add(simpleVert);
			simpleVert.position = new Vector3(vector.x, vector.w);
			simpleVert.uv0 = new Vector2(0f, 1f);
			vbo.Add(simpleVert);
			simpleVert.position = new Vector3(vector.z, vector.w);
			simpleVert.uv0 = new Vector2(1f, 1f);
			vbo.Add(simpleVert);
			simpleVert.position = new Vector3(vector.z, vector.y);
			simpleVert.uv0 = new Vector2(1f, 0f);
			vbo.Add(simpleVert);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00007170 File Offset: 0x00005370
		protected override void OnDidApplyAnimationProperties()
		{
			this.SetAllDirty();
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00007178 File Offset: 0x00005378
		public virtual void SetNativeSize()
		{
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000717C File Offset: 0x0000537C
		public virtual bool Raycast(Vector2 sp, Camera eventCamera)
		{
			Transform transform = base.transform;
			List<Component> list = ComponentListPool.Get();
			bool flag = false;
			while (transform != null)
			{
				transform.GetComponents<Component>(list);
				for (int i = 0; i < list.Count; i++)
				{
					ICanvasRaycastFilter canvasRaycastFilter = list[i] as ICanvasRaycastFilter;
					if (canvasRaycastFilter != null)
					{
						bool flag2 = true;
						CanvasGroup canvasGroup = list[i] as CanvasGroup;
						if (canvasGroup != null)
						{
							if (!flag && canvasGroup.ignoreParentGroups)
							{
								flag = true;
								flag2 = canvasRaycastFilter.IsRaycastLocationValid(sp, eventCamera);
							}
							else if (!flag)
							{
								flag2 = canvasRaycastFilter.IsRaycastLocationValid(sp, eventCamera);
							}
						}
						else
						{
							flag2 = canvasRaycastFilter.IsRaycastLocationValid(sp, eventCamera);
						}
						if (!flag2)
						{
							ComponentListPool.Release(list);
							return false;
						}
					}
				}
				transform = transform.parent;
			}
			ComponentListPool.Release(list);
			return true;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00007264 File Offset: 0x00005464
		public Vector2 PixelAdjustPoint(Vector2 point)
		{
			if (!this.canvas || !this.canvas.pixelPerfect)
			{
				return point;
			}
			return RectTransformUtility.PixelAdjustPoint(point, base.transform, this.canvas);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000729C File Offset: 0x0000549C
		public Rect GetPixelAdjustedRect()
		{
			if (!this.canvas || !this.canvas.pixelPerfect)
			{
				return this.rectTransform.rect;
			}
			return RectTransformUtility.PixelAdjustRect(this.rectTransform, this.canvas);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x000072DC File Offset: 0x000054DC
		public void CrossFadeColor(Color targetColor, float duration, bool ignoreTimeScale, bool useAlpha)
		{
			this.CrossFadeColor(targetColor, duration, ignoreTimeScale, useAlpha, true);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x000072EC File Offset: 0x000054EC
		private void CrossFadeColor(Color targetColor, float duration, bool ignoreTimeScale, bool useAlpha, bool useRGB)
		{
			if (this.canvasRenderer == null || (!useRGB && !useAlpha))
			{
				return;
			}
			if (this.canvasRenderer.GetColor().Equals(targetColor))
			{
				return;
			}
			ColorTween.ColorTweenMode tweenMode = (!useRGB || !useAlpha) ? ((!useRGB) ? ColorTween.ColorTweenMode.Alpha : ColorTween.ColorTweenMode.RGB) : ColorTween.ColorTweenMode.All;
			ColorTween info = new ColorTween
			{
				duration = duration,
				startColor = this.canvasRenderer.GetColor(),
				targetColor = targetColor
			};
			info.AddOnChangedCallback(new UnityAction<Color>(this.canvasRenderer.SetColor));
			info.ignoreTimeScale = ignoreTimeScale;
			info.tweenMode = tweenMode;
			this.m_ColorTweenRunner.StartTween(info);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x000073C0 File Offset: 0x000055C0
		private static Color CreateColorFromAlpha(float alpha)
		{
			Color black = Color.black;
			black.a = alpha;
			return black;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x000073DC File Offset: 0x000055DC
		public void CrossFadeAlpha(float alpha, float duration, bool ignoreTimeScale)
		{
			this.CrossFadeColor(Graphic.CreateColorFromAlpha(alpha), duration, ignoreTimeScale, true, false);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x000073F0 File Offset: 0x000055F0
		public void RegisterDirtyLayoutCallback(UnityAction action)
		{
			this.m_OnDirtyLayoutCallback = (UnityAction)Delegate.Combine(this.m_OnDirtyLayoutCallback, action);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000740C File Offset: 0x0000560C
		public void UnregisterDirtyLayoutCallback(UnityAction action)
		{
			this.m_OnDirtyLayoutCallback = (UnityAction)Delegate.Remove(this.m_OnDirtyLayoutCallback, action);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00007428 File Offset: 0x00005628
		public void RegisterDirtyVerticesCallback(UnityAction action)
		{
			this.m_OnDirtyVertsCallback = (UnityAction)Delegate.Combine(this.m_OnDirtyVertsCallback, action);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00007444 File Offset: 0x00005644
		public void UnregisterDirtyVerticesCallback(UnityAction action)
		{
			this.m_OnDirtyVertsCallback = (UnityAction)Delegate.Remove(this.m_OnDirtyVertsCallback, action);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00007460 File Offset: 0x00005660
		public void RegisterDirtyMaterialCallback(UnityAction action)
		{
			this.m_OnDirtyMaterialCallback = (UnityAction)Delegate.Combine(this.m_OnDirtyMaterialCallback, action);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000747C File Offset: 0x0000567C
		public void UnregisterDirtyMaterialCallback(UnityAction action)
		{
			this.m_OnDirtyMaterialCallback = (UnityAction)Delegate.Remove(this.m_OnDirtyMaterialCallback, action);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x000074C0 File Offset: 0x000056C0
		virtual bool IsDestroyed()
		{
			return base.IsDestroyed();
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000074C8 File Offset: 0x000056C8
		virtual Transform get_transform()
		{
			return base.transform;
		}

		// Token: 0x040000FE RID: 254
		protected static Material s_DefaultUI = null;

		// Token: 0x040000FF RID: 255
		protected static Texture2D s_WhiteTexture = null;

		// Token: 0x04000100 RID: 256
		private static readonly ObjectPool<List<UIVertex>> s_VboPool = new ObjectPool<List<UIVertex>>(delegate(List<UIVertex> x)
		{
			if (x.Capacity < 300)
			{
				x.Capacity = 300;
			}
		}, delegate(List<UIVertex> l)
		{
			l.Clear();
		});

		// Token: 0x04000101 RID: 257
		[SerializeField]
		[FormerlySerializedAs("m_Mat")]
		protected Material m_Material;

		// Token: 0x04000102 RID: 258
		[SerializeField]
		private Color m_Color = Color.white;

		// Token: 0x04000103 RID: 259
		[NonSerialized]
		private RectTransform m_RectTransform;

		// Token: 0x04000104 RID: 260
		[NonSerialized]
		private CanvasRenderer m_CanvasRender;

		// Token: 0x04000105 RID: 261
		[NonSerialized]
		private Canvas m_Canvas;

		// Token: 0x04000106 RID: 262
		[NonSerialized]
		private bool m_VertsDirty;

		// Token: 0x04000107 RID: 263
		[NonSerialized]
		private bool m_MaterialDirty;

		// Token: 0x04000108 RID: 264
		[NonSerialized]
		protected UnityAction m_OnDirtyLayoutCallback;

		// Token: 0x04000109 RID: 265
		[NonSerialized]
		protected UnityAction m_OnDirtyVertsCallback;

		// Token: 0x0400010A RID: 266
		[NonSerialized]
		protected UnityAction m_OnDirtyMaterialCallback;

		// Token: 0x0400010B RID: 267
		[NonSerialized]
		private readonly TweenRunner<ColorTween> m_ColorTweenRunner;
	}
}
