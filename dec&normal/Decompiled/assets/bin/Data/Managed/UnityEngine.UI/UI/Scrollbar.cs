using System;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x0200007A RID: 122
	[AddComponentMenu("UI/Scrollbar", 32)]
	[RequireComponent(typeof(RectTransform))]
	public class Scrollbar : Selectable, IBeginDragHandler, IDragHandler, IEventSystemHandler, IInitializePotentialDragHandler, ICanvasElement
	{
		// Token: 0x060003AD RID: 941 RVA: 0x0000FADC File Offset: 0x0000DCDC
		protected Scrollbar()
		{
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060003AE RID: 942 RVA: 0x0000FB10 File Offset: 0x0000DD10
		// (set) Token: 0x060003AF RID: 943 RVA: 0x0000FB18 File Offset: 0x0000DD18
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

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x0000FB38 File Offset: 0x0000DD38
		// (set) Token: 0x060003B1 RID: 945 RVA: 0x0000FB40 File Offset: 0x0000DD40
		public Scrollbar.Direction direction
		{
			get
			{
				return this.m_Direction;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<Scrollbar.Direction>(ref this.m_Direction, value))
				{
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x0000FB5C File Offset: 0x0000DD5C
		// (set) Token: 0x060003B3 RID: 947 RVA: 0x0000FB98 File Offset: 0x0000DD98
		public float value
		{
			get
			{
				float num = this.m_Value;
				if (this.m_NumberOfSteps > 1)
				{
					num = Mathf.Round(num * (float)(this.m_NumberOfSteps - 1)) / (float)(this.m_NumberOfSteps - 1);
				}
				return num;
			}
			set
			{
				this.Set(value);
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0000FBA4 File Offset: 0x0000DDA4
		// (set) Token: 0x060003B5 RID: 949 RVA: 0x0000FBAC File Offset: 0x0000DDAC
		public float size
		{
			get
			{
				return this.m_Size;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_Size, Mathf.Clamp01(value)))
				{
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x0000FBCC File Offset: 0x0000DDCC
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x0000FBD4 File Offset: 0x0000DDD4
		public int numberOfSteps
		{
			get
			{
				return this.m_NumberOfSteps;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<int>(ref this.m_NumberOfSteps, value))
				{
					this.Set(this.m_Value);
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x0000FBFC File Offset: 0x0000DDFC
		// (set) Token: 0x060003B9 RID: 953 RVA: 0x0000FC04 File Offset: 0x0000DE04
		public Scrollbar.ScrollEvent onValueChanged
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

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0000FC10 File Offset: 0x0000DE10
		private float stepSize
		{
			get
			{
				return (this.m_NumberOfSteps <= 1) ? 0.1f : (1f / (float)(this.m_NumberOfSteps - 1));
			}
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000FC38 File Offset: 0x0000DE38
		public virtual void Rebuild(CanvasUpdate executing)
		{
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000FC3C File Offset: 0x0000DE3C
		protected override void OnEnable()
		{
			base.OnEnable();
			this.UpdateCachedReferences();
			this.Set(this.m_Value, false);
			this.UpdateVisuals();
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000FC60 File Offset: 0x0000DE60
		protected override void OnDisable()
		{
			this.m_Tracker.Clear();
			base.OnDisable();
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000FC74 File Offset: 0x0000DE74
		private void UpdateCachedReferences()
		{
			if (this.m_HandleRect && this.m_HandleRect.parent != null)
			{
				this.m_ContainerRect = this.m_HandleRect.parent.GetComponent<RectTransform>();
			}
			else
			{
				this.m_ContainerRect = null;
			}
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000FCCC File Offset: 0x0000DECC
		private void Set(float input)
		{
			this.Set(input, true);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000FCD8 File Offset: 0x0000DED8
		private void Set(float input, bool sendCallback)
		{
			float value = this.m_Value;
			this.m_Value = Mathf.Clamp01(input);
			if (value == this.value)
			{
				return;
			}
			this.UpdateVisuals();
			if (sendCallback)
			{
				this.m_OnValueChanged.Invoke(this.value);
			}
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000FD24 File Offset: 0x0000DF24
		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
			if (!this.IsActive())
			{
				return;
			}
			this.UpdateVisuals();
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0000FD40 File Offset: 0x0000DF40
		private Scrollbar.Axis axis
		{
			get
			{
				return (this.m_Direction != Scrollbar.Direction.LeftToRight && this.m_Direction != Scrollbar.Direction.RightToLeft) ? Scrollbar.Axis.Vertical : Scrollbar.Axis.Horizontal;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000FD60 File Offset: 0x0000DF60
		private bool reverseValue
		{
			get
			{
				return this.m_Direction == Scrollbar.Direction.RightToLeft || this.m_Direction == Scrollbar.Direction.TopToBottom;
			}
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000FD7C File Offset: 0x0000DF7C
		private void UpdateVisuals()
		{
			this.m_Tracker.Clear();
			if (this.m_ContainerRect != null)
			{
				this.m_Tracker.Add(this, this.m_HandleRect, DrivenTransformProperties.Anchors);
				Vector2 zero = Vector2.zero;
				Vector2 one = Vector2.one;
				float num = this.value * (1f - this.size);
				if (this.reverseValue)
				{
					zero[(int)this.axis] = 1f - num - this.size;
					one[(int)this.axis] = 1f - num;
				}
				else
				{
					zero[(int)this.axis] = num;
					one[(int)this.axis] = num + this.size;
				}
				this.m_HandleRect.anchorMin = zero;
				this.m_HandleRect.anchorMax = one;
			}
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000FE58 File Offset: 0x0000E058
		private void UpdateDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			if (this.m_ContainerRect == null)
			{
				return;
			}
			Vector2 a;
			if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_ContainerRect, eventData.position, eventData.pressEventCamera, out a))
			{
				return;
			}
			Vector2 a2 = a - this.m_Offset - this.m_ContainerRect.rect.position;
			Vector2 vector = a2 - (this.m_HandleRect.rect.size - this.m_HandleRect.sizeDelta) * 0.5f;
			float num = (this.axis != Scrollbar.Axis.Horizontal) ? this.m_ContainerRect.rect.height : this.m_ContainerRect.rect.width;
			float num2 = num * (1f - this.size);
			if (num2 <= 0f)
			{
				return;
			}
			switch (this.m_Direction)
			{
			case Scrollbar.Direction.LeftToRight:
				this.Set(vector.x / num2);
				break;
			case Scrollbar.Direction.RightToLeft:
				this.Set(1f - vector.x / num2);
				break;
			case Scrollbar.Direction.BottomToTop:
				this.Set(vector.y / num2);
				break;
			case Scrollbar.Direction.TopToBottom:
				this.Set(1f - vector.y / num2);
				break;
			}
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000FFD8 File Offset: 0x0000E1D8
		private bool MayDrag(PointerEventData eventData)
		{
			return this.IsActive() && this.IsInteractable() && eventData.button == PointerEventData.InputButton.Left;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000FFFC File Offset: 0x0000E1FC
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			this.isPointerDownAndNotDragging = false;
			if (!this.MayDrag(eventData))
			{
				return;
			}
			if (this.m_ContainerRect == null)
			{
				return;
			}
			this.m_Offset = Vector2.zero;
			Vector2 a;
			if (RectTransformUtility.RectangleContainsScreenPoint(this.m_HandleRect, eventData.position, eventData.enterEventCamera) && RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_HandleRect, eventData.position, eventData.pressEventCamera, out a))
			{
				this.m_Offset = a - this.m_HandleRect.rect.center;
			}
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00010094 File Offset: 0x0000E294
		public virtual void OnDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			if (this.m_ContainerRect != null)
			{
				this.UpdateDrag(eventData);
			}
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x000100BC File Offset: 0x0000E2BC
		public override void OnPointerDown(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			base.OnPointerDown(eventData);
			this.isPointerDownAndNotDragging = true;
			this.m_PointerDownRepeat = base.StartCoroutine(this.ClickRepeat(eventData));
		}

		// Token: 0x060003CA RID: 970 RVA: 0x000100EC File Offset: 0x0000E2EC
		protected IEnumerator ClickRepeat(PointerEventData eventData)
		{
			while (this.isPointerDownAndNotDragging)
			{
				Vector2 localMousePos;
				if (!RectTransformUtility.RectangleContainsScreenPoint(this.m_HandleRect, eventData.position, eventData.enterEventCamera) && RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_HandleRect, eventData.position, eventData.pressEventCamera, out localMousePos))
				{
					float axisCoordinate = (this.axis != Scrollbar.Axis.Horizontal) ? localMousePos.y : localMousePos.x;
					if (axisCoordinate < 0f)
					{
						this.value -= this.size;
					}
					else
					{
						this.value += this.size;
					}
				}
				yield return new WaitForEndOfFrame();
			}
			base.StopCoroutine(this.m_PointerDownRepeat);
			yield break;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00010118 File Offset: 0x0000E318
		public override void OnPointerUp(PointerEventData eventData)
		{
			base.OnPointerUp(eventData);
			this.isPointerDownAndNotDragging = false;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00010128 File Offset: 0x0000E328
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
				if (this.axis == Scrollbar.Axis.Horizontal && this.FindSelectableOnLeft() == null)
				{
					this.Set((!this.reverseValue) ? (this.value - this.stepSize) : (this.value + this.stepSize));
				}
				else
				{
					base.OnMove(eventData);
				}
				break;
			case MoveDirection.Up:
				if (this.axis == Scrollbar.Axis.Vertical && this.FindSelectableOnUp() == null)
				{
					this.Set((!this.reverseValue) ? (this.value + this.stepSize) : (this.value - this.stepSize));
				}
				else
				{
					base.OnMove(eventData);
				}
				break;
			case MoveDirection.Right:
				if (this.axis == Scrollbar.Axis.Horizontal && this.FindSelectableOnRight() == null)
				{
					this.Set((!this.reverseValue) ? (this.value + this.stepSize) : (this.value - this.stepSize));
				}
				else
				{
					base.OnMove(eventData);
				}
				break;
			case MoveDirection.Down:
				if (this.axis == Scrollbar.Axis.Vertical && this.FindSelectableOnDown() == null)
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

		// Token: 0x060003CD RID: 973 RVA: 0x000102EC File Offset: 0x0000E4EC
		public override Selectable FindSelectableOnLeft()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Scrollbar.Axis.Horizontal)
			{
				return null;
			}
			return base.FindSelectableOnLeft();
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00010320 File Offset: 0x0000E520
		public override Selectable FindSelectableOnRight()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Scrollbar.Axis.Horizontal)
			{
				return null;
			}
			return base.FindSelectableOnRight();
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00010354 File Offset: 0x0000E554
		public override Selectable FindSelectableOnUp()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Scrollbar.Axis.Vertical)
			{
				return null;
			}
			return base.FindSelectableOnUp();
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0001038C File Offset: 0x0000E58C
		public override Selectable FindSelectableOnDown()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Scrollbar.Axis.Vertical)
			{
				return null;
			}
			return base.FindSelectableOnDown();
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x000103C4 File Offset: 0x0000E5C4
		public virtual void OnInitializePotentialDrag(PointerEventData eventData)
		{
			eventData.useDragThreshold = false;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x000103D0 File Offset: 0x0000E5D0
		public void SetDirection(Scrollbar.Direction direction, bool includeRectLayouts)
		{
			Scrollbar.Axis axis = this.axis;
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

		// Token: 0x060003D3 RID: 979 RVA: 0x0001043C File Offset: 0x0000E63C
		virtual bool IsDestroyed()
		{
			return base.IsDestroyed();
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00010444 File Offset: 0x0000E644
		virtual Transform get_transform()
		{
			return base.transform;
		}

		// Token: 0x040001D9 RID: 473
		[SerializeField]
		private RectTransform m_HandleRect;

		// Token: 0x040001DA RID: 474
		[SerializeField]
		private Scrollbar.Direction m_Direction;

		// Token: 0x040001DB RID: 475
		[Range(0f, 1f)]
		[SerializeField]
		private float m_Value = 1f;

		// Token: 0x040001DC RID: 476
		[Range(0f, 1f)]
		[SerializeField]
		private float m_Size = 0.2f;

		// Token: 0x040001DD RID: 477
		[Range(0f, 11f)]
		[SerializeField]
		private int m_NumberOfSteps;

		// Token: 0x040001DE RID: 478
		[SerializeField]
		[Space(6f)]
		private Scrollbar.ScrollEvent m_OnValueChanged = new Scrollbar.ScrollEvent();

		// Token: 0x040001DF RID: 479
		private RectTransform m_ContainerRect;

		// Token: 0x040001E0 RID: 480
		private Vector2 m_Offset = Vector2.zero;

		// Token: 0x040001E1 RID: 481
		private DrivenRectTransformTracker m_Tracker;

		// Token: 0x040001E2 RID: 482
		private Coroutine m_PointerDownRepeat;

		// Token: 0x040001E3 RID: 483
		private bool isPointerDownAndNotDragging;

		// Token: 0x0200007C RID: 124
		private enum Axis
		{
			// Token: 0x040001EC RID: 492
			Horizontal,
			// Token: 0x040001ED RID: 493
			Vertical
		}

		// Token: 0x0200007D RID: 125
		public enum Direction
		{
			// Token: 0x040001EF RID: 495
			LeftToRight,
			// Token: 0x040001F0 RID: 496
			RightToLeft,
			// Token: 0x040001F1 RID: 497
			BottomToTop,
			// Token: 0x040001F2 RID: 498
			TopToBottom
		}

		// Token: 0x0200007E RID: 126
		[Serializable]
		public class ScrollEvent : UnityEvent<float>
		{
		}
	}
}
