using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x02000087 RID: 135
	[RequireComponent(typeof(RectTransform))]
	[AddComponentMenu("UI/Slider", 34)]
	public class Slider : Selectable, IDragHandler, IEventSystemHandler, IInitializePotentialDragHandler, ICanvasElement
	{
		// Token: 0x0600045D RID: 1117 RVA: 0x00012808 File Offset: 0x00010A08
		protected Slider()
		{
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x0001283C File Offset: 0x00010A3C
		// (set) Token: 0x0600045F RID: 1119 RVA: 0x00012844 File Offset: 0x00010A44
		public RectTransform fillRect
		{
			get
			{
				return this.m_FillRect;
			}
			set
			{
				if (SetPropertyUtility.SetClass<RectTransform>(ref this.m_FillRect, value))
				{
					this.UpdateCachedReferences();
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x00012864 File Offset: 0x00010A64
		// (set) Token: 0x06000461 RID: 1121 RVA: 0x0001286C File Offset: 0x00010A6C
		public RectTransform handleRect
		{
			get
			{
				return this.m_HandleRect;
			}
			set
			{
				if (SetPropertyUtility.SetClass<RectTransform>(ref this.m_HandleRect, value))
				{
					this.UpdateCachedReferences();
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x0001288C File Offset: 0x00010A8C
		// (set) Token: 0x06000463 RID: 1123 RVA: 0x00012894 File Offset: 0x00010A94
		public Slider.Direction direction
		{
			get
			{
				return this.m_Direction;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<Slider.Direction>(ref this.m_Direction, value))
				{
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x000128B0 File Offset: 0x00010AB0
		// (set) Token: 0x06000465 RID: 1125 RVA: 0x000128B8 File Offset: 0x00010AB8
		public float minValue
		{
			get
			{
				return this.m_MinValue;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_MinValue, value))
				{
					this.Set(this.m_Value);
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x000128E0 File Offset: 0x00010AE0
		// (set) Token: 0x06000467 RID: 1127 RVA: 0x000128E8 File Offset: 0x00010AE8
		public float maxValue
		{
			get
			{
				return this.m_MaxValue;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_MaxValue, value))
				{
					this.Set(this.m_Value);
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x00012910 File Offset: 0x00010B10
		// (set) Token: 0x06000469 RID: 1129 RVA: 0x00012918 File Offset: 0x00010B18
		public bool wholeNumbers
		{
			get
			{
				return this.m_WholeNumbers;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<bool>(ref this.m_WholeNumbers, value))
				{
					this.Set(this.m_Value);
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x00012940 File Offset: 0x00010B40
		// (set) Token: 0x0600046B RID: 1131 RVA: 0x00012960 File Offset: 0x00010B60
		public float value
		{
			get
			{
				if (this.wholeNumbers)
				{
					return Mathf.Round(this.m_Value);
				}
				return this.m_Value;
			}
			set
			{
				this.Set(value);
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x0001296C File Offset: 0x00010B6C
		// (set) Token: 0x0600046D RID: 1133 RVA: 0x000129A4 File Offset: 0x00010BA4
		public float normalizedValue
		{
			get
			{
				if (Mathf.Approximately(this.minValue, this.maxValue))
				{
					return 0f;
				}
				return Mathf.InverseLerp(this.minValue, this.maxValue, this.value);
			}
			set
			{
				this.value = Mathf.Lerp(this.minValue, this.maxValue, value);
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x000129C0 File Offset: 0x00010BC0
		// (set) Token: 0x0600046F RID: 1135 RVA: 0x000129C8 File Offset: 0x00010BC8
		public Slider.SliderEvent onValueChanged
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

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x000129D4 File Offset: 0x00010BD4
		private float stepSize
		{
			get
			{
				return (!this.wholeNumbers) ? ((this.maxValue - this.minValue) * 0.1f) : 1f;
			}
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00012A00 File Offset: 0x00010C00
		public virtual void Rebuild(CanvasUpdate executing)
		{
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00012A04 File Offset: 0x00010C04
		protected override void OnEnable()
		{
			base.OnEnable();
			this.UpdateCachedReferences();
			this.Set(this.m_Value, false);
			this.UpdateVisuals();
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00012A28 File Offset: 0x00010C28
		protected override void OnDisable()
		{
			this.m_Tracker.Clear();
			base.OnDisable();
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00012A3C File Offset: 0x00010C3C
		private void UpdateCachedReferences()
		{
			if (this.m_FillRect)
			{
				this.m_FillTransform = this.m_FillRect.transform;
				this.m_FillImage = this.m_FillRect.GetComponent<Image>();
				if (this.m_FillTransform.parent != null)
				{
					this.m_FillContainerRect = this.m_FillTransform.parent.GetComponent<RectTransform>();
				}
			}
			else
			{
				this.m_FillContainerRect = null;
				this.m_FillImage = null;
			}
			if (this.m_HandleRect)
			{
				this.m_HandleTransform = this.m_HandleRect.transform;
				if (this.m_HandleTransform.parent != null)
				{
					this.m_HandleContainerRect = this.m_HandleTransform.parent.GetComponent<RectTransform>();
				}
			}
			else
			{
				this.m_HandleContainerRect = null;
			}
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00012B14 File Offset: 0x00010D14
		private void Set(float input)
		{
			this.Set(input, true);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00012B20 File Offset: 0x00010D20
		private void Set(float input, bool sendCallback)
		{
			float num = Mathf.Clamp(input, this.minValue, this.maxValue);
			if (this.wholeNumbers)
			{
				num = Mathf.Round(num);
			}
			if (this.m_Value == num)
			{
				return;
			}
			this.m_Value = num;
			this.UpdateVisuals();
			if (sendCallback)
			{
				this.m_OnValueChanged.Invoke(num);
			}
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00012B80 File Offset: 0x00010D80
		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
			if (!this.IsActive())
			{
				return;
			}
			this.UpdateVisuals();
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x00012B9C File Offset: 0x00010D9C
		private Slider.Axis axis
		{
			get
			{
				return (this.m_Direction != Slider.Direction.LeftToRight && this.m_Direction != Slider.Direction.RightToLeft) ? Slider.Axis.Vertical : Slider.Axis.Horizontal;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x00012BBC File Offset: 0x00010DBC
		private bool reverseValue
		{
			get
			{
				return this.m_Direction == Slider.Direction.RightToLeft || this.m_Direction == Slider.Direction.TopToBottom;
			}
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00012BD8 File Offset: 0x00010DD8
		private void UpdateVisuals()
		{
			this.m_Tracker.Clear();
			if (this.m_FillContainerRect != null)
			{
				this.m_Tracker.Add(this, this.m_FillRect, DrivenTransformProperties.Anchors);
				Vector2 zero = Vector2.zero;
				Vector2 one = Vector2.one;
				if (this.m_FillImage != null && this.m_FillImage.type == Image.Type.Filled)
				{
					this.m_FillImage.fillAmount = this.normalizedValue;
				}
				else if (this.reverseValue)
				{
					zero[(int)this.axis] = 1f - this.normalizedValue;
				}
				else
				{
					one[(int)this.axis] = this.normalizedValue;
				}
				this.m_FillRect.anchorMin = zero;
				this.m_FillRect.anchorMax = one;
			}
			if (this.m_HandleContainerRect != null)
			{
				this.m_Tracker.Add(this, this.m_HandleRect, DrivenTransformProperties.Anchors);
				Vector2 zero2 = Vector2.zero;
				Vector2 one2 = Vector2.one;
				int axis = (int)this.axis;
				float value = (!this.reverseValue) ? this.normalizedValue : (1f - this.normalizedValue);
				one2[(int)this.axis] = value;
				zero2[axis] = value;
				this.m_HandleRect.anchorMin = zero2;
				this.m_HandleRect.anchorMax = one2;
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00012D40 File Offset: 0x00010F40
		private void UpdateDrag(PointerEventData eventData, Camera cam)
		{
			RectTransform rectTransform = this.m_HandleContainerRect ?? this.m_FillContainerRect;
			if (rectTransform != null && rectTransform.rect.size[(int)this.axis] > 0f)
			{
				Vector2 a;
				if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, cam, out a))
				{
					return;
				}
				a -= rectTransform.rect.position;
				float num = Mathf.Clamp01((a - this.m_Offset)[(int)this.axis] / rectTransform.rect.size[(int)this.axis]);
				this.normalizedValue = ((!this.reverseValue) ? num : (1f - num));
			}
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00012E20 File Offset: 0x00011020
		private bool MayDrag(PointerEventData eventData)
		{
			return this.IsActive() && this.IsInteractable() && eventData.button == PointerEventData.InputButton.Left;
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00012E44 File Offset: 0x00011044
		public override void OnPointerDown(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			base.OnPointerDown(eventData);
			this.m_Offset = Vector2.zero;
			if (this.m_HandleContainerRect != null && RectTransformUtility.RectangleContainsScreenPoint(this.m_HandleRect, eventData.position, eventData.enterEventCamera))
			{
				Vector2 offset;
				if (RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_HandleRect, eventData.position, eventData.pressEventCamera, out offset))
				{
					this.m_Offset = offset;
				}
			}
			else
			{
				this.UpdateDrag(eventData, eventData.pressEventCamera);
			}
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00012ED4 File Offset: 0x000110D4
		public virtual void OnDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			this.UpdateDrag(eventData, eventData.pressEventCamera);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00012EF0 File Offset: 0x000110F0
		public override void OnMove(AxisEventData eventData)
		{
			if (!this.IsActive() || !this.IsInteractable())
			{
				base.OnMove(eventData);
				return;
			}
			switch (eventData.moveDir)
			{
			case MoveDirection.Left:
				if (this.axis == Slider.Axis.Horizontal && this.FindSelectableOnLeft() == null)
				{
					this.Set((!this.reverseValue) ? (this.value - this.stepSize) : (this.value + this.stepSize));
				}
				else
				{
					base.OnMove(eventData);
				}
				break;
			case MoveDirection.Up:
				if (this.axis == Slider.Axis.Vertical && this.FindSelectableOnUp() == null)
				{
					this.Set((!this.reverseValue) ? (this.value + this.stepSize) : (this.value - this.stepSize));
				}
				else
				{
					base.OnMove(eventData);
				}
				break;
			case MoveDirection.Right:
				if (this.axis == Slider.Axis.Horizontal && this.FindSelectableOnRight() == null)
				{
					this.Set((!this.reverseValue) ? (this.value + this.stepSize) : (this.value - this.stepSize));
				}
				else
				{
					base.OnMove(eventData);
				}
				break;
			case MoveDirection.Down:
				if (this.axis == Slider.Axis.Vertical && this.FindSelectableOnDown() == null)
				{
					this.Set((!this.reverseValue) ? (this.value - this.stepSize) : (this.value + this.stepSize));
				}
				else
				{
					base.OnMove(eventData);
				}
				break;
			}
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x000130B4 File Offset: 0x000112B4
		public override Selectable FindSelectableOnLeft()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Slider.Axis.Horizontal)
			{
				return null;
			}
			return base.FindSelectableOnLeft();
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x000130E8 File Offset: 0x000112E8
		public override Selectable FindSelectableOnRight()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Slider.Axis.Horizontal)
			{
				return null;
			}
			return base.FindSelectableOnRight();
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0001311C File Offset: 0x0001131C
		public override Selectable FindSelectableOnUp()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Slider.Axis.Vertical)
			{
				return null;
			}
			return base.FindSelectableOnUp();
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00013154 File Offset: 0x00011354
		public override Selectable FindSelectableOnDown()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Slider.Axis.Vertical)
			{
				return null;
			}
			return base.FindSelectableOnDown();
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0001318C File Offset: 0x0001138C
		public virtual void OnInitializePotentialDrag(PointerEventData eventData)
		{
			eventData.useDragThreshold = false;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00013198 File Offset: 0x00011398
		public void SetDirection(Slider.Direction direction, bool includeRectLayouts)
		{
			Slider.Axis axis = this.axis;
			bool reverseValue = this.reverseValue;
			this.direction = direction;
			if (!includeRectLayouts)
			{
				return;
			}
			if (this.axis != axis)
			{
				RectTransformUtility.FlipLayoutAxes(base.transform as RectTransform, true, true);
			}
			if (this.reverseValue != reverseValue)
			{
				RectTransformUtility.FlipLayoutOnAxis(base.transform as RectTransform, (int)this.axis, true, true);
			}
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00013204 File Offset: 0x00011404
		virtual bool IsDestroyed()
		{
			return base.IsDestroyed();
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0001320C File Offset: 0x0001140C
		virtual Transform get_transform()
		{
			return base.transform;
		}

		// Token: 0x04000229 RID: 553
		[SerializeField]
		private RectTransform m_FillRect;

		// Token: 0x0400022A RID: 554
		[SerializeField]
		private RectTransform m_HandleRect;

		// Token: 0x0400022B RID: 555
		[SerializeField]
		[Space(6f)]
		private Slider.Direction m_Direction;

		// Token: 0x0400022C RID: 556
		[SerializeField]
		private float m_MinValue;

		// Token: 0x0400022D RID: 557
		[SerializeField]
		private float m_MaxValue = 1f;

		// Token: 0x0400022E RID: 558
		[SerializeField]
		private bool m_WholeNumbers;

		// Token: 0x0400022F RID: 559
		[SerializeField]
		private float m_Value = 1f;

		// Token: 0x04000230 RID: 560
		[SerializeField]
		[Space(6f)]
		private Slider.SliderEvent m_OnValueChanged = new Slider.SliderEvent();

		// Token: 0x04000231 RID: 561
		private Image m_FillImage;

		// Token: 0x04000232 RID: 562
		private Transform m_FillTransform;

		// Token: 0x04000233 RID: 563
		private RectTransform m_FillContainerRect;

		// Token: 0x04000234 RID: 564
		private Transform m_HandleTransform;

		// Token: 0x04000235 RID: 565
		private RectTransform m_HandleContainerRect;

		// Token: 0x04000236 RID: 566
		private Vector2 m_Offset = Vector2.zero;

		// Token: 0x04000237 RID: 567
		private DrivenRectTransformTracker m_Tracker;

		// Token: 0x02000088 RID: 136
		private enum Axis
		{
			// Token: 0x04000239 RID: 569
			Horizontal,
			// Token: 0x0400023A RID: 570
			Vertical
		}

		// Token: 0x02000089 RID: 137
		public enum Direction
		{
			// Token: 0x0400023C RID: 572
			LeftToRight,
			// Token: 0x0400023D RID: 573
			RightToLeft,
			// Token: 0x0400023E RID: 574
			BottomToTop,
			// Token: 0x0400023F RID: 575
			TopToBottom
		}

		// Token: 0x0200008A RID: 138
		[Serializable]
		public class SliderEvent : UnityEvent<float>
		{
		}
	}
}
