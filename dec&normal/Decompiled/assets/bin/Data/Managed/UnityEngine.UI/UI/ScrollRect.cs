using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x0200007F RID: 127
	[ExecuteInEditMode]
	[AddComponentMenu("UI/Scroll Rect", 33)]
	[RequireComponent(typeof(RectTransform))]
	[SelectionBase]
	public class ScrollRect : UIBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IEventSystemHandler, IInitializePotentialDragHandler, IScrollHandler, ICanvasElement
	{
		// Token: 0x060003DC RID: 988 RVA: 0x000105D8 File Offset: 0x0000E7D8
		protected ScrollRect()
		{
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060003DD RID: 989 RVA: 0x00010660 File Offset: 0x0000E860
		// (set) Token: 0x060003DE RID: 990 RVA: 0x00010668 File Offset: 0x0000E868
		public RectTransform content
		{
			get
			{
				return this.m_Content;
			}
			set
			{
				this.m_Content = value;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060003DF RID: 991 RVA: 0x00010674 File Offset: 0x0000E874
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x0001067C File Offset: 0x0000E87C
		public bool horizontal
		{
			get
			{
				return this.m_Horizontal;
			}
			set
			{
				this.m_Horizontal = value;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x00010688 File Offset: 0x0000E888
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x00010690 File Offset: 0x0000E890
		public bool vertical
		{
			get
			{
				return this.m_Vertical;
			}
			set
			{
				this.m_Vertical = value;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x0001069C File Offset: 0x0000E89C
		// (set) Token: 0x060003E4 RID: 996 RVA: 0x000106A4 File Offset: 0x0000E8A4
		public ScrollRect.MovementType movementType
		{
			get
			{
				return this.m_MovementType;
			}
			set
			{
				this.m_MovementType = value;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x000106B0 File Offset: 0x0000E8B0
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x000106B8 File Offset: 0x0000E8B8
		public float elasticity
		{
			get
			{
				return this.m_Elasticity;
			}
			set
			{
				this.m_Elasticity = value;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x000106C4 File Offset: 0x0000E8C4
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x000106CC File Offset: 0x0000E8CC
		public bool inertia
		{
			get
			{
				return this.m_Inertia;
			}
			set
			{
				this.m_Inertia = value;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x000106D8 File Offset: 0x0000E8D8
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x000106E0 File Offset: 0x0000E8E0
		public float decelerationRate
		{
			get
			{
				return this.m_DecelerationRate;
			}
			set
			{
				this.m_DecelerationRate = value;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x000106EC File Offset: 0x0000E8EC
		// (set) Token: 0x060003EC RID: 1004 RVA: 0x000106F4 File Offset: 0x0000E8F4
		public float scrollSensitivity
		{
			get
			{
				return this.m_ScrollSensitivity;
			}
			set
			{
				this.m_ScrollSensitivity = value;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x00010700 File Offset: 0x0000E900
		// (set) Token: 0x060003EE RID: 1006 RVA: 0x00010708 File Offset: 0x0000E908
		public Scrollbar horizontalScrollbar
		{
			get
			{
				return this.m_HorizontalScrollbar;
			}
			set
			{
				if (this.m_HorizontalScrollbar)
				{
					this.m_HorizontalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.SetHorizontalNormalizedPosition));
				}
				this.m_HorizontalScrollbar = value;
				if (this.m_HorizontalScrollbar)
				{
					this.m_HorizontalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.SetHorizontalNormalizedPosition));
				}
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x00010774 File Offset: 0x0000E974
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x0001077C File Offset: 0x0000E97C
		public Scrollbar verticalScrollbar
		{
			get
			{
				return this.m_VerticalScrollbar;
			}
			set
			{
				if (this.m_VerticalScrollbar)
				{
					this.m_VerticalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.SetVerticalNormalizedPosition));
				}
				this.m_VerticalScrollbar = value;
				if (this.m_VerticalScrollbar)
				{
					this.m_VerticalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.SetVerticalNormalizedPosition));
				}
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x000107E8 File Offset: 0x0000E9E8
		// (set) Token: 0x060003F2 RID: 1010 RVA: 0x000107F0 File Offset: 0x0000E9F0
		public ScrollRect.ScrollRectEvent onValueChanged
		{
			get
			{
				return this.m_OnValueChanged;
			}
			set
			{
				this.m_OnValueChanged = value;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x000107FC File Offset: 0x0000E9FC
		protected RectTransform viewRect
		{
			get
			{
				if (this.m_ViewRect == null)
				{
					this.m_ViewRect = (RectTransform)base.transform;
				}
				return this.m_ViewRect;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00010828 File Offset: 0x0000EA28
		// (set) Token: 0x060003F5 RID: 1013 RVA: 0x00010830 File Offset: 0x0000EA30
		public Vector2 velocity
		{
			get
			{
				return this.m_Velocity;
			}
			set
			{
				this.m_Velocity = value;
			}
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0001083C File Offset: 0x0000EA3C
		public virtual void Rebuild(CanvasUpdate executing)
		{
			if (executing != CanvasUpdate.PostLayout)
			{
				return;
			}
			this.UpdateBounds();
			this.UpdateScrollbars(Vector2.zero);
			this.UpdatePrevData();
			this.m_HasRebuiltLayout = true;
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00010864 File Offset: 0x0000EA64
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.m_HorizontalScrollbar)
			{
				this.m_HorizontalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.SetHorizontalNormalizedPosition));
			}
			if (this.m_VerticalScrollbar)
			{
				this.m_VerticalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.SetVerticalNormalizedPosition));
			}
			CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(this);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x000108D8 File Offset: 0x0000EAD8
		protected override void OnDisable()
		{
			CanvasUpdateRegistry.UnRegisterCanvasElementForRebuild(this);
			if (this.m_HorizontalScrollbar)
			{
				this.m_HorizontalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.SetHorizontalNormalizedPosition));
			}
			if (this.m_VerticalScrollbar)
			{
				this.m_VerticalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.SetVerticalNormalizedPosition));
			}
			this.m_HasRebuiltLayout = false;
			base.OnDisable();
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00010950 File Offset: 0x0000EB50
		public override bool IsActive()
		{
			return base.IsActive() && this.m_Content != null;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0001096C File Offset: 0x0000EB6C
		private void EnsureLayoutHasRebuilt()
		{
			if (!this.m_HasRebuiltLayout && !CanvasUpdateRegistry.IsRebuildingLayout())
			{
				Canvas.ForceUpdateCanvases();
			}
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00010988 File Offset: 0x0000EB88
		public virtual void StopMovement()
		{
			this.m_Velocity = Vector2.zero;
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00010998 File Offset: 0x0000EB98
		public virtual void OnScroll(PointerEventData data)
		{
			if (!this.IsActive())
			{
				return;
			}
			this.EnsureLayoutHasRebuilt();
			this.UpdateBounds();
			Vector2 scrollDelta = data.scrollDelta;
			scrollDelta.y *= -1f;
			if (this.vertical && !this.horizontal)
			{
				if (Mathf.Abs(scrollDelta.x) > Mathf.Abs(scrollDelta.y))
				{
					scrollDelta.y = scrollDelta.x;
				}
				scrollDelta.x = 0f;
			}
			if (this.horizontal && !this.vertical)
			{
				if (Mathf.Abs(scrollDelta.y) > Mathf.Abs(scrollDelta.x))
				{
					scrollDelta.x = scrollDelta.y;
				}
				scrollDelta.y = 0f;
			}
			Vector2 vector = this.m_Content.anchoredPosition;
			vector += scrollDelta * this.m_ScrollSensitivity;
			if (this.m_MovementType == ScrollRect.MovementType.Clamped)
			{
				vector += this.CalculateOffset(vector - this.m_Content.anchoredPosition);
			}
			this.SetContentAnchoredPosition(vector);
			this.UpdateBounds();
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00010AC8 File Offset: 0x0000ECC8
		public virtual void OnInitializePotentialDrag(PointerEventData eventData)
		{
			this.m_Velocity = Vector2.zero;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00010AD8 File Offset: 0x0000ECD8
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			if (!this.IsActive())
			{
				return;
			}
			this.UpdateBounds();
			this.m_PointerStartLocalCursor = Vector2.zero;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.viewRect, eventData.position, eventData.pressEventCamera, out this.m_PointerStartLocalCursor);
			this.m_ContentStartPosition = this.m_Content.anchoredPosition;
			this.m_Dragging = true;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00010B44 File Offset: 0x0000ED44
		public virtual void OnEndDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.m_Dragging = false;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00010B5C File Offset: 0x0000ED5C
		public virtual void OnDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			if (!this.IsActive())
			{
				return;
			}
			Vector2 a;
			if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(this.viewRect, eventData.position, eventData.pressEventCamera, out a))
			{
				return;
			}
			this.UpdateBounds();
			Vector2 b = a - this.m_PointerStartLocalCursor;
			Vector2 vector = this.m_ContentStartPosition + b;
			Vector2 b2 = this.CalculateOffset(vector - this.m_Content.anchoredPosition);
			vector += b2;
			if (this.m_MovementType == ScrollRect.MovementType.Elastic)
			{
				if (b2.x != 0f)
				{
					vector.x -= ScrollRect.RubberDelta(b2.x, this.m_ViewBounds.size.x);
				}
				if (b2.y != 0f)
				{
					vector.y -= ScrollRect.RubberDelta(b2.y, this.m_ViewBounds.size.y);
				}
			}
			this.SetContentAnchoredPosition(vector);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00010C74 File Offset: 0x0000EE74
		protected virtual void SetContentAnchoredPosition(Vector2 position)
		{
			if (!this.m_Horizontal)
			{
				position.x = this.m_Content.anchoredPosition.x;
			}
			if (!this.m_Vertical)
			{
				position.y = this.m_Content.anchoredPosition.y;
			}
			if (position != this.m_Content.anchoredPosition)
			{
				this.m_Content.anchoredPosition = position;
				this.UpdateBounds();
			}
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00010CF4 File Offset: 0x0000EEF4
		protected virtual void LateUpdate()
		{
			if (!this.m_Content)
			{
				return;
			}
			this.EnsureLayoutHasRebuilt();
			this.UpdateBounds();
			float unscaledDeltaTime = Time.unscaledDeltaTime;
			Vector2 vector = this.CalculateOffset(Vector2.zero);
			if (!this.m_Dragging && (vector != Vector2.zero || this.m_Velocity != Vector2.zero))
			{
				Vector2 vector2 = this.m_Content.anchoredPosition;
				for (int i = 0; i < 2; i++)
				{
					if (this.m_MovementType == ScrollRect.MovementType.Elastic && vector[i] != 0f)
					{
						float value = this.m_Velocity[i];
						vector2[i] = Mathf.SmoothDamp(this.m_Content.anchoredPosition[i], this.m_Content.anchoredPosition[i] + vector[i], ref value, this.m_Elasticity, float.PositiveInfinity, unscaledDeltaTime);
						this.m_Velocity[i] = value;
					}
					else if (this.m_Inertia)
					{
						ref Vector2 ptr = ref this.m_Velocity;
						int index2;
						int index = index2 = i;
						float num = ptr[index2];
						this.m_Velocity[index] = num * Mathf.Pow(this.m_DecelerationRate, unscaledDeltaTime);
						if (Mathf.Abs(this.m_Velocity[i]) < 1f)
						{
							this.m_Velocity[i] = 0f;
						}
						ref Vector2 ptr2 = ref vector2;
						int index3 = index2 = i;
						num = ptr2[index2];
						vector2[index3] = num + this.m_Velocity[i] * unscaledDeltaTime;
					}
					else
					{
						this.m_Velocity[i] = 0f;
					}
				}
				if (this.m_Velocity != Vector2.zero)
				{
					if (this.m_MovementType == ScrollRect.MovementType.Clamped)
					{
						vector = this.CalculateOffset(vector2 - this.m_Content.anchoredPosition);
						vector2 += vector;
					}
					this.SetContentAnchoredPosition(vector2);
				}
			}
			if (this.m_Dragging && this.m_Inertia)
			{
				Vector3 to = (this.m_Content.anchoredPosition - this.m_PrevPosition) / unscaledDeltaTime;
				this.m_Velocity = Vector3.Lerp(this.m_Velocity, to, unscaledDeltaTime * 10f);
			}
			if (this.m_ViewBounds != this.m_PrevViewBounds || this.m_ContentBounds != this.m_PrevContentBounds || this.m_Content.anchoredPosition != this.m_PrevPosition)
			{
				this.UpdateScrollbars(vector);
				this.m_OnValueChanged.Invoke(this.normalizedPosition);
				this.UpdatePrevData();
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00010FB8 File Offset: 0x0000F1B8
		private void UpdatePrevData()
		{
			if (this.m_Content == null)
			{
				this.m_PrevPosition = Vector2.zero;
			}
			else
			{
				this.m_PrevPosition = this.m_Content.anchoredPosition;
			}
			this.m_PrevViewBounds = this.m_ViewBounds;
			this.m_PrevContentBounds = this.m_ContentBounds;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00011010 File Offset: 0x0000F210
		private void UpdateScrollbars(Vector2 offset)
		{
			if (this.m_HorizontalScrollbar)
			{
				if (this.m_ContentBounds.size.x > 0f)
				{
					this.m_HorizontalScrollbar.size = Mathf.Clamp01((this.m_ViewBounds.size.x - Mathf.Abs(offset.x)) / this.m_ContentBounds.size.x);
				}
				else
				{
					this.m_HorizontalScrollbar.size = 1f;
				}
				this.m_HorizontalScrollbar.value = this.horizontalNormalizedPosition;
			}
			if (this.m_VerticalScrollbar)
			{
				if (this.m_ContentBounds.size.y > 0f)
				{
					this.m_VerticalScrollbar.size = Mathf.Clamp01((this.m_ViewBounds.size.y - Mathf.Abs(offset.y)) / this.m_ContentBounds.size.y);
				}
				else
				{
					this.m_VerticalScrollbar.size = 1f;
				}
				this.m_VerticalScrollbar.value = this.verticalNormalizedPosition;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x00011150 File Offset: 0x0000F350
		// (set) Token: 0x06000406 RID: 1030 RVA: 0x00011164 File Offset: 0x0000F364
		public Vector2 normalizedPosition
		{
			get
			{
				return new Vector2(this.horizontalNormalizedPosition, this.verticalNormalizedPosition);
			}
			set
			{
				this.SetNormalizedPosition(value.x, 0);
				this.SetNormalizedPosition(value.y, 1);
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x00011184 File Offset: 0x0000F384
		// (set) Token: 0x06000408 RID: 1032 RVA: 0x0001124C File Offset: 0x0000F44C
		public float horizontalNormalizedPosition
		{
			get
			{
				this.UpdateBounds();
				if (this.m_ContentBounds.size.x <= this.m_ViewBounds.size.x)
				{
					return (float)((this.m_ViewBounds.min.x <= this.m_ContentBounds.min.x) ? 0 : 1);
				}
				return (this.m_ViewBounds.min.x - this.m_ContentBounds.min.x) / (this.m_ContentBounds.size.x - this.m_ViewBounds.size.x);
			}
			set
			{
				this.SetNormalizedPosition(value, 0);
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x00011258 File Offset: 0x0000F458
		// (set) Token: 0x0600040A RID: 1034 RVA: 0x00011320 File Offset: 0x0000F520
		public float verticalNormalizedPosition
		{
			get
			{
				this.UpdateBounds();
				if (this.m_ContentBounds.size.y <= this.m_ViewBounds.size.y)
				{
					return (float)((this.m_ViewBounds.min.y <= this.m_ContentBounds.min.y) ? 0 : 1);
				}
				return (this.m_ViewBounds.min.y - this.m_ContentBounds.min.y) / (this.m_ContentBounds.size.y - this.m_ViewBounds.size.y);
			}
			set
			{
				this.SetNormalizedPosition(value, 1);
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0001132C File Offset: 0x0000F52C
		private void SetHorizontalNormalizedPosition(float value)
		{
			this.SetNormalizedPosition(value, 0);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00011338 File Offset: 0x0000F538
		private void SetVerticalNormalizedPosition(float value)
		{
			this.SetNormalizedPosition(value, 1);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00011344 File Offset: 0x0000F544
		private void SetNormalizedPosition(float value, int axis)
		{
			this.EnsureLayoutHasRebuilt();
			this.UpdateBounds();
			float num = this.m_ContentBounds.size[axis] - this.m_ViewBounds.size[axis];
			float num2 = this.m_ViewBounds.min[axis] - value * num;
			float num3 = this.m_Content.localPosition[axis] + num2 - this.m_ContentBounds.min[axis];
			Vector3 localPosition = this.m_Content.localPosition;
			if (Mathf.Abs(localPosition[axis] - num3) > 0.01f)
			{
				localPosition[axis] = num3;
				this.m_Content.localPosition = localPosition;
				this.m_Velocity[axis] = 0f;
				this.UpdateBounds();
			}
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00011424 File Offset: 0x0000F624
		private static float RubberDelta(float overStretching, float viewSize)
		{
			return (1f - 1f / (Mathf.Abs(overStretching) * 0.55f / viewSize + 1f)) * viewSize * Mathf.Sign(overStretching);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00011450 File Offset: 0x0000F650
		private void UpdateBounds()
		{
			this.m_ViewBounds = new Bounds(this.viewRect.rect.center, this.viewRect.rect.size);
			this.m_ContentBounds = this.GetBounds();
			if (this.m_Content == null)
			{
				return;
			}
			Vector3 size = this.m_ContentBounds.size;
			Vector3 center = this.m_ContentBounds.center;
			Vector3 vector = this.m_ViewBounds.size - size;
			if (vector.x > 0f)
			{
				center.x -= vector.x * (this.m_Content.pivot.x - 0.5f);
				size.x = this.m_ViewBounds.size.x;
			}
			if (vector.y > 0f)
			{
				center.y -= vector.y * (this.m_Content.pivot.y - 0.5f);
				size.y = this.m_ViewBounds.size.y;
			}
			this.m_ContentBounds.size = size;
			this.m_ContentBounds.center = center;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x000115B4 File Offset: 0x0000F7B4
		private Bounds GetBounds()
		{
			if (this.m_Content == null)
			{
				return default(Bounds);
			}
			Vector3 vector = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
			Vector3 vector2 = new Vector3(float.MinValue, float.MinValue, float.MinValue);
			Matrix4x4 worldToLocalMatrix = this.viewRect.worldToLocalMatrix;
			this.m_Content.GetWorldCorners(this.m_Corners);
			for (int i = 0; i < 4; i++)
			{
				Vector3 lhs = worldToLocalMatrix.MultiplyPoint3x4(this.m_Corners[i]);
				vector = Vector3.Min(lhs, vector);
				vector2 = Vector3.Max(lhs, vector2);
			}
			Bounds result = new Bounds(vector, Vector3.zero);
			result.Encapsulate(vector2);
			return result;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0001167C File Offset: 0x0000F87C
		private Vector2 CalculateOffset(Vector2 delta)
		{
			Vector2 zero = Vector2.zero;
			if (this.m_MovementType == ScrollRect.MovementType.Unrestricted)
			{
				return zero;
			}
			Vector2 vector = this.m_ContentBounds.min;
			Vector2 vector2 = this.m_ContentBounds.max;
			if (this.m_Horizontal)
			{
				vector.x += delta.x;
				vector2.x += delta.x;
				if (vector.x > this.m_ViewBounds.min.x)
				{
					zero.x = this.m_ViewBounds.min.x - vector.x;
				}
				else if (vector2.x < this.m_ViewBounds.max.x)
				{
					zero.x = this.m_ViewBounds.max.x - vector2.x;
				}
			}
			if (this.m_Vertical)
			{
				vector.y += delta.y;
				vector2.y += delta.y;
				if (vector2.y < this.m_ViewBounds.max.y)
				{
					zero.y = this.m_ViewBounds.max.y - vector2.y;
				}
				else if (vector.y > this.m_ViewBounds.min.y)
				{
					zero.y = this.m_ViewBounds.min.y - vector.y;
				}
			}
			return zero;
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00011840 File Offset: 0x0000FA40
		virtual bool IsDestroyed()
		{
			return base.IsDestroyed();
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00011848 File Offset: 0x0000FA48
		virtual Transform get_transform()
		{
			return base.transform;
		}

		// Token: 0x040001F3 RID: 499
		[SerializeField]
		private RectTransform m_Content;

		// Token: 0x040001F4 RID: 500
		[SerializeField]
		private bool m_Horizontal = true;

		// Token: 0x040001F5 RID: 501
		[SerializeField]
		private bool m_Vertical = true;

		// Token: 0x040001F6 RID: 502
		[SerializeField]
		private ScrollRect.MovementType m_MovementType = ScrollRect.MovementType.Elastic;

		// Token: 0x040001F7 RID: 503
		[SerializeField]
		private float m_Elasticity = 0.1f;

		// Token: 0x040001F8 RID: 504
		[SerializeField]
		private bool m_Inertia = true;

		// Token: 0x040001F9 RID: 505
		[SerializeField]
		private float m_DecelerationRate = 0.135f;

		// Token: 0x040001FA RID: 506
		[SerializeField]
		private float m_ScrollSensitivity = 1f;

		// Token: 0x040001FB RID: 507
		[SerializeField]
		private Scrollbar m_HorizontalScrollbar;

		// Token: 0x040001FC RID: 508
		[SerializeField]
		private Scrollbar m_VerticalScrollbar;

		// Token: 0x040001FD RID: 509
		[SerializeField]
		private ScrollRect.ScrollRectEvent m_OnValueChanged = new ScrollRect.ScrollRectEvent();

		// Token: 0x040001FE RID: 510
		private Vector2 m_PointerStartLocalCursor = Vector2.zero;

		// Token: 0x040001FF RID: 511
		private Vector2 m_ContentStartPosition = Vector2.zero;

		// Token: 0x04000200 RID: 512
		private RectTransform m_ViewRect;

		// Token: 0x04000201 RID: 513
		private Bounds m_ContentBounds;

		// Token: 0x04000202 RID: 514
		private Bounds m_ViewBounds;

		// Token: 0x04000203 RID: 515
		private Vector2 m_Velocity;

		// Token: 0x04000204 RID: 516
		private bool m_Dragging;

		// Token: 0x04000205 RID: 517
		private Vector2 m_PrevPosition = Vector2.zero;

		// Token: 0x04000206 RID: 518
		private Bounds m_PrevContentBounds;

		// Token: 0x04000207 RID: 519
		private Bounds m_PrevViewBounds;

		// Token: 0x04000208 RID: 520
		[NonSerialized]
		private bool m_HasRebuiltLayout;

		// Token: 0x04000209 RID: 521
		private readonly Vector3[] m_Corners = new Vector3[4];

		// Token: 0x02000080 RID: 128
		public enum MovementType
		{
			// Token: 0x0400020B RID: 523
			Unrestricted,
			// Token: 0x0400020C RID: 524
			Elastic,
			// Token: 0x0400020D RID: 525
			Clamped
		}

		// Token: 0x02000081 RID: 129
		[Serializable]
		public class ScrollRectEvent : UnityEvent<Vector2>
		{
		}
	}
}
