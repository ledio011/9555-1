using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x02000037 RID: 55
	[AddComponentMenu("Layout/Canvas Scaler", 101)]
	[RequireComponent(typeof(Canvas))]
	[ExecuteInEditMode]
	public class CanvasScaler : UIBehaviour
	{
		// Token: 0x06000151 RID: 337 RVA: 0x00005694 File Offset: 0x00003894
		protected CanvasScaler()
		{
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00005710 File Offset: 0x00003910
		// (set) Token: 0x06000153 RID: 339 RVA: 0x00005718 File Offset: 0x00003918
		public CanvasScaler.ScaleMode uiScaleMode
		{
			get
			{
				return this.m_UiScaleMode;
			}
			set
			{
				this.m_UiScaleMode = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00005724 File Offset: 0x00003924
		// (set) Token: 0x06000155 RID: 341 RVA: 0x0000572C File Offset: 0x0000392C
		public float referencePixelsPerUnit
		{
			get
			{
				return this.m_ReferencePixelsPerUnit;
			}
			set
			{
				this.m_ReferencePixelsPerUnit = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00005738 File Offset: 0x00003938
		// (set) Token: 0x06000157 RID: 343 RVA: 0x00005740 File Offset: 0x00003940
		public float scaleFactor
		{
			get
			{
				return this.m_ScaleFactor;
			}
			set
			{
				this.m_ScaleFactor = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000158 RID: 344 RVA: 0x0000574C File Offset: 0x0000394C
		// (set) Token: 0x06000159 RID: 345 RVA: 0x00005754 File Offset: 0x00003954
		public Vector2 referenceResolution
		{
			get
			{
				return this.m_ReferenceResolution;
			}
			set
			{
				this.m_ReferenceResolution = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00005760 File Offset: 0x00003960
		// (set) Token: 0x0600015B RID: 347 RVA: 0x00005768 File Offset: 0x00003968
		public CanvasScaler.ScreenMatchMode screenMatchMode
		{
			get
			{
				return this.m_ScreenMatchMode;
			}
			set
			{
				this.m_ScreenMatchMode = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00005774 File Offset: 0x00003974
		// (set) Token: 0x0600015D RID: 349 RVA: 0x0000577C File Offset: 0x0000397C
		public float matchWidthOrHeight
		{
			get
			{
				return this.m_MatchWidthOrHeight;
			}
			set
			{
				this.m_MatchWidthOrHeight = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00005788 File Offset: 0x00003988
		// (set) Token: 0x0600015F RID: 351 RVA: 0x00005790 File Offset: 0x00003990
		public CanvasScaler.Unit physicalUnit
		{
			get
			{
				return this.m_PhysicalUnit;
			}
			set
			{
				this.m_PhysicalUnit = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000160 RID: 352 RVA: 0x0000579C File Offset: 0x0000399C
		// (set) Token: 0x06000161 RID: 353 RVA: 0x000057A4 File Offset: 0x000039A4
		public float fallbackScreenDPI
		{
			get
			{
				return this.m_FallbackScreenDPI;
			}
			set
			{
				this.m_FallbackScreenDPI = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000162 RID: 354 RVA: 0x000057B0 File Offset: 0x000039B0
		// (set) Token: 0x06000163 RID: 355 RVA: 0x000057B8 File Offset: 0x000039B8
		public float defaultSpriteDPI
		{
			get
			{
				return this.m_DefaultSpriteDPI;
			}
			set
			{
				this.m_DefaultSpriteDPI = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000164 RID: 356 RVA: 0x000057C4 File Offset: 0x000039C4
		// (set) Token: 0x06000165 RID: 357 RVA: 0x000057CC File Offset: 0x000039CC
		public float dynamicPixelsPerUnit
		{
			get
			{
				return this.m_DynamicPixelsPerUnit;
			}
			set
			{
				this.m_DynamicPixelsPerUnit = value;
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000057D8 File Offset: 0x000039D8
		protected override void OnEnable()
		{
			base.OnEnable();
			this.m_Canvas = base.GetComponent<Canvas>();
			this.Handle();
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000057F4 File Offset: 0x000039F4
		protected override void OnDisable()
		{
			this.SetScaleFactor(1f);
			this.SetReferencePixelsPerUnit(100f);
			base.OnDisable();
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00005814 File Offset: 0x00003A14
		protected virtual void Update()
		{
			this.Handle();
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000581C File Offset: 0x00003A1C
		protected virtual void Handle()
		{
			if (this.m_Canvas == null || !this.m_Canvas.isRootCanvas)
			{
				return;
			}
			if (this.m_Canvas.renderMode == RenderMode.WorldSpace)
			{
				this.HandleWorldCanvas();
				return;
			}
			switch (this.m_UiScaleMode)
			{
			case CanvasScaler.ScaleMode.ConstantPixelSize:
				this.HandleConstantPixelSize();
				break;
			case CanvasScaler.ScaleMode.ScaleWithScreenSize:
				this.HandleScaleWithScreenSize();
				break;
			case CanvasScaler.ScaleMode.ConstantPhysicalSize:
				this.HandleConstantPhysicalSize();
				break;
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000058A4 File Offset: 0x00003AA4
		protected virtual void HandleWorldCanvas()
		{
			this.SetScaleFactor(this.m_DynamicPixelsPerUnit);
			this.SetReferencePixelsPerUnit(this.m_ReferencePixelsPerUnit);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000058C0 File Offset: 0x00003AC0
		protected virtual void HandleConstantPixelSize()
		{
			this.SetScaleFactor(this.m_ScaleFactor);
			this.SetReferencePixelsPerUnit(this.m_ReferencePixelsPerUnit);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000058DC File Offset: 0x00003ADC
		protected virtual void HandleScaleWithScreenSize()
		{
			Vector2 vector = new Vector2((float)Screen.width, (float)Screen.height);
			float scaleFactor = 0f;
			switch (this.m_ScreenMatchMode)
			{
			case CanvasScaler.ScreenMatchMode.MatchWidthOrHeight:
			{
				float from = Mathf.Log(vector.x / this.m_ReferenceResolution.x, 2f);
				float to = Mathf.Log(vector.y / this.m_ReferenceResolution.y, 2f);
				float p = Mathf.Lerp(from, to, this.m_MatchWidthOrHeight);
				scaleFactor = Mathf.Pow(2f, p);
				break;
			}
			case CanvasScaler.ScreenMatchMode.Expand:
				scaleFactor = Mathf.Min(vector.x / this.m_ReferenceResolution.x, vector.y / this.m_ReferenceResolution.y);
				break;
			case CanvasScaler.ScreenMatchMode.Shrink:
				scaleFactor = Mathf.Max(vector.x / this.m_ReferenceResolution.x, vector.y / this.m_ReferenceResolution.y);
				break;
			}
			this.SetScaleFactor(scaleFactor);
			this.SetReferencePixelsPerUnit(this.m_ReferencePixelsPerUnit);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000059F4 File Offset: 0x00003BF4
		protected virtual void HandleConstantPhysicalSize()
		{
			float dpi = Screen.dpi;
			float num = (dpi != 0f) ? dpi : this.m_FallbackScreenDPI;
			float num2 = 1f;
			switch (this.m_PhysicalUnit)
			{
			case CanvasScaler.Unit.Centimeters:
				num2 = 2.54f;
				break;
			case CanvasScaler.Unit.Millimeters:
				num2 = 25.4f;
				break;
			case CanvasScaler.Unit.Inches:
				num2 = 1f;
				break;
			case CanvasScaler.Unit.Points:
				num2 = 72f;
				break;
			case CanvasScaler.Unit.Picas:
				num2 = 6f;
				break;
			}
			this.SetScaleFactor(num / num2);
			this.SetReferencePixelsPerUnit(this.m_ReferencePixelsPerUnit * num2 / this.m_DefaultSpriteDPI);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00005AA0 File Offset: 0x00003CA0
		protected void SetScaleFactor(float scaleFactor)
		{
			if (scaleFactor == this.m_PrevScaleFactor)
			{
				return;
			}
			this.m_Canvas.scaleFactor = scaleFactor;
			this.m_PrevScaleFactor = scaleFactor;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00005AC4 File Offset: 0x00003CC4
		protected void SetReferencePixelsPerUnit(float referencePixelsPerUnit)
		{
			if (referencePixelsPerUnit == this.m_PrevReferencePixelsPerUnit)
			{
				return;
			}
			this.m_Canvas.referencePixelsPerUnit = referencePixelsPerUnit;
			this.m_PrevReferencePixelsPerUnit = referencePixelsPerUnit;
		}

		// Token: 0x040000A3 RID: 163
		private const float kLogBase = 2f;

		// Token: 0x040000A4 RID: 164
		[SerializeField]
		[Tooltip("Determines how UI elements in the Canvas are scaled.")]
		private CanvasScaler.ScaleMode m_UiScaleMode;

		// Token: 0x040000A5 RID: 165
		[SerializeField]
		[Tooltip("If a sprite has this 'Pixels Per Unit' setting, then one pixel in the sprite will cover one unit in the UI.")]
		protected float m_ReferencePixelsPerUnit = 100f;

		// Token: 0x040000A6 RID: 166
		[SerializeField]
		[Tooltip("Scales all UI elements in the Canvas by this factor.")]
		protected float m_ScaleFactor = 1f;

		// Token: 0x040000A7 RID: 167
		[SerializeField]
		[Tooltip("The resolution the UI layout is designed for. If the screen resolution is larger, the UI will be scaled up, and if it's smaller, the UI will be scaled down. This is done in accordance with the Screen Match Mode.")]
		protected Vector2 m_ReferenceResolution = new Vector2(800f, 600f);

		// Token: 0x040000A8 RID: 168
		[Tooltip("A mode used to scale the canvas area if the aspect ratio of the current resolution doesn't fit the reference resolution.")]
		[SerializeField]
		protected CanvasScaler.ScreenMatchMode m_ScreenMatchMode;

		// Token: 0x040000A9 RID: 169
		[SerializeField]
		[Range(0f, 1f)]
		[Tooltip("Determines if the scaling is using the width or height as reference, or a mix in between.")]
		protected float m_MatchWidthOrHeight;

		// Token: 0x040000AA RID: 170
		[Tooltip("The physical unit to specify positions and sizes in.")]
		[SerializeField]
		protected CanvasScaler.Unit m_PhysicalUnit = CanvasScaler.Unit.Points;

		// Token: 0x040000AB RID: 171
		[SerializeField]
		[Tooltip("The DPI to assume if the screen DPI is not known.")]
		protected float m_FallbackScreenDPI = 96f;

		// Token: 0x040000AC RID: 172
		[Tooltip("The pixels per inch to use for sprites that have a 'Pixels Per Unit' setting that matches the 'Reference Pixels Per Unit' setting.")]
		[SerializeField]
		protected float m_DefaultSpriteDPI = 96f;

		// Token: 0x040000AD RID: 173
		[SerializeField]
		[Tooltip("The amount of pixels per unit to use for dynamically created bitmaps in the UI, such as Text.")]
		protected float m_DynamicPixelsPerUnit = 1f;

		// Token: 0x040000AE RID: 174
		private Canvas m_Canvas;

		// Token: 0x040000AF RID: 175
		[NonSerialized]
		private float m_PrevScaleFactor = 1f;

		// Token: 0x040000B0 RID: 176
		[NonSerialized]
		private float m_PrevReferencePixelsPerUnit = 100f;

		// Token: 0x02000038 RID: 56
		public enum ScaleMode
		{
			// Token: 0x040000B2 RID: 178
			ConstantPixelSize,
			// Token: 0x040000B3 RID: 179
			ScaleWithScreenSize,
			// Token: 0x040000B4 RID: 180
			ConstantPhysicalSize
		}

		// Token: 0x02000039 RID: 57
		public enum ScreenMatchMode
		{
			// Token: 0x040000B6 RID: 182
			MatchWidthOrHeight,
			// Token: 0x040000B7 RID: 183
			Expand,
			// Token: 0x040000B8 RID: 184
			Shrink
		}

		// Token: 0x0200003A RID: 58
		public enum Unit
		{
			// Token: 0x040000BA RID: 186
			Centimeters,
			// Token: 0x040000BB RID: 187
			Millimeters,
			// Token: 0x040000BC RID: 188
			Inches,
			// Token: 0x040000BD RID: 189
			Points,
			// Token: 0x040000BE RID: 190
			Picas
		}
	}
}
