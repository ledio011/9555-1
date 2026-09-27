using System;

namespace UnityEngine
{
	// Token: 0x02000105 RID: 261
	internal struct SliderHandler
	{
		// Token: 0x060009A7 RID: 2471 RVA: 0x000154A8 File Offset: 0x000136A8
		public SliderHandler(Rect position, float currentValue, float size, float start, float end, GUIStyle slider, GUIStyle thumb, bool horiz, int id)
		{
			this.position = position;
			this.currentValue = currentValue;
			this.size = size;
			this.start = start;
			this.end = end;
			this.slider = slider;
			this.thumb = thumb;
			this.horiz = horiz;
			this.id = id;
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x000154FC File Offset: 0x000136FC
		public float Handle()
		{
			if (this.slider == null || this.thumb == null)
			{
				return this.currentValue;
			}
			switch (this.CurrentEventType())
			{
			case EventType.MouseDown:
				return this.OnMouseDown();
			case EventType.MouseUp:
				return this.OnMouseUp();
			case EventType.MouseDrag:
				return this.OnMouseDrag();
			case EventType.Repaint:
				return this.OnRepaint();
			}
			return this.currentValue;
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0001557C File Offset: 0x0001377C
		private float OnMouseDown()
		{
			if (!this.position.Contains(this.CurrentEvent().mousePosition) || this.IsEmptySlider())
			{
				return this.currentValue;
			}
			GUI.scrollTroughSide = 0;
			GUIUtility.hotControl = this.id;
			this.CurrentEvent().Use();
			if (this.ThumbSelectionRect().Contains(this.CurrentEvent().mousePosition))
			{
				this.StartDraggingWithValue(this.ClampedCurrentValue());
				return this.currentValue;
			}
			GUI.changed = true;
			if (this.SupportsPageMovements())
			{
				this.SliderState().isDragging = false;
				GUI.nextScrollStepTime = SystemClock.now.AddMilliseconds(250.0);
				GUI.scrollTroughSide = this.CurrentScrollTroughSide();
				return this.PageMovementValue();
			}
			float num = this.ValueForCurrentMousePosition();
			this.StartDraggingWithValue(num);
			return this.Clamp(num);
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x00015668 File Offset: 0x00013868
		private float OnMouseDrag()
		{
			if (GUIUtility.hotControl != this.id)
			{
				return this.currentValue;
			}
			SliderState sliderState = this.SliderState();
			if (!sliderState.isDragging)
			{
				return this.currentValue;
			}
			GUI.changed = true;
			this.CurrentEvent().Use();
			float num = this.MousePosition() - sliderState.dragStartPos;
			float value = sliderState.dragStartValue + num / this.ValuesPerPixel();
			return this.Clamp(value);
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x000156DC File Offset: 0x000138DC
		private float OnMouseUp()
		{
			if (GUIUtility.hotControl == this.id)
			{
				this.CurrentEvent().Use();
				GUIUtility.hotControl = 0;
			}
			return this.currentValue;
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x00015708 File Offset: 0x00013908
		private float OnRepaint()
		{
			this.slider.Draw(this.position, GUIContent.none, this.id);
			this.thumb.Draw(this.ThumbRect(), GUIContent.none, this.id);
			if (GUIUtility.hotControl != this.id || !this.position.Contains(this.CurrentEvent().mousePosition) || this.IsEmptySlider())
			{
				return this.currentValue;
			}
			if (this.ThumbRect().Contains(this.CurrentEvent().mousePosition))
			{
				if (GUI.scrollTroughSide != 0)
				{
					GUIUtility.hotControl = 0;
				}
				return this.currentValue;
			}
			GUI.InternalRepaintEditorWindow();
			if (SystemClock.now < GUI.nextScrollStepTime)
			{
				return this.currentValue;
			}
			if (this.CurrentScrollTroughSide() != GUI.scrollTroughSide)
			{
				return this.currentValue;
			}
			GUI.nextScrollStepTime = SystemClock.now.AddMilliseconds(30.0);
			if (this.SupportsPageMovements())
			{
				this.SliderState().isDragging = false;
				GUI.changed = true;
				return this.PageMovementValue();
			}
			return this.ClampedCurrentValue();
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x00015840 File Offset: 0x00013A40
		private EventType CurrentEventType()
		{
			return this.CurrentEvent().GetTypeForControl(this.id);
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x00015854 File Offset: 0x00013A54
		private int CurrentScrollTroughSide()
		{
			float num = (!this.horiz) ? this.CurrentEvent().mousePosition.y : this.CurrentEvent().mousePosition.x;
			float num2 = (!this.horiz) ? this.ThumbRect().y : this.ThumbRect().x;
			return (num <= num2) ? -1 : 1;
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x000158D8 File Offset: 0x00013AD8
		private bool IsEmptySlider()
		{
			return this.start == this.end;
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x000158E8 File Offset: 0x00013AE8
		private bool SupportsPageMovements()
		{
			return this.size != 0f && GUI.usePageScrollbars;
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x00015904 File Offset: 0x00013B04
		private float PageMovementValue()
		{
			float num = this.currentValue;
			int num2 = (this.start <= this.end) ? 1 : -1;
			if (this.MousePosition() > this.PageUpMovementBound())
			{
				num += this.size * (float)num2 * 0.9f;
			}
			else
			{
				num -= this.size * (float)num2 * 0.9f;
			}
			return this.Clamp(num);
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x00015974 File Offset: 0x00013B74
		private float PageUpMovementBound()
		{
			if (this.horiz)
			{
				return this.ThumbRect().xMax - this.position.x;
			}
			return this.ThumbRect().yMax - this.position.y;
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x000159C8 File Offset: 0x00013BC8
		private Event CurrentEvent()
		{
			return Event.current;
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x000159D0 File Offset: 0x00013BD0
		private float ValueForCurrentMousePosition()
		{
			if (this.horiz)
			{
				return (this.MousePosition() - this.ThumbRect().width * 0.5f) / this.ValuesPerPixel() + this.start - this.size * 0.5f;
			}
			return (this.MousePosition() - this.ThumbRect().height * 0.5f) / this.ValuesPerPixel() + this.start - this.size * 0.5f;
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x00015A58 File Offset: 0x00013C58
		private float Clamp(float value)
		{
			return Mathf.Clamp(value, this.MinValue(), this.MaxValue());
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x00015A6C File Offset: 0x00013C6C
		private Rect ThumbSelectionRect()
		{
			return this.ThumbRect();
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00015A84 File Offset: 0x00013C84
		private void StartDraggingWithValue(float dragStartValue)
		{
			SliderState sliderState = this.SliderState();
			sliderState.dragStartPos = this.MousePosition();
			sliderState.dragStartValue = dragStartValue;
			sliderState.isDragging = true;
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00015AB4 File Offset: 0x00013CB4
		private SliderState SliderState()
		{
			return (SliderState)GUIUtility.GetStateObject(typeof(SliderState), this.id);
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00015AD0 File Offset: 0x00013CD0
		private Rect ThumbRect()
		{
			return (!this.horiz) ? this.VerticalThumbRect() : this.HorizontalThumbRect();
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x00015AF0 File Offset: 0x00013CF0
		private Rect VerticalThumbRect()
		{
			float num = this.ValuesPerPixel();
			if (this.start < this.end)
			{
				return new Rect(this.position.x + (float)this.slider.padding.left, (this.ClampedCurrentValue() - this.start) * num + this.position.y + (float)this.slider.padding.top, this.position.width - (float)this.slider.padding.horizontal, this.size * num + this.ThumbSize());
			}
			return new Rect(this.position.x + (float)this.slider.padding.left, (this.ClampedCurrentValue() + this.size - this.start) * num + this.position.y + (float)this.slider.padding.top, this.position.width - (float)this.slider.padding.horizontal, this.size * -num + this.ThumbSize());
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x00015C2C File Offset: 0x00013E2C
		private Rect HorizontalThumbRect()
		{
			float num = this.ValuesPerPixel();
			if (this.start < this.end)
			{
				return new Rect((this.ClampedCurrentValue() - this.start) * num + this.position.x + (float)this.slider.padding.left, this.position.y + (float)this.slider.padding.top, this.size * num + this.ThumbSize(), this.position.height - (float)this.slider.padding.vertical);
			}
			return new Rect((this.ClampedCurrentValue() + this.size - this.start) * num + this.position.x + (float)this.slider.padding.left, this.position.y, this.size * -num + this.ThumbSize(), this.position.height);
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x00015D44 File Offset: 0x00013F44
		private float ClampedCurrentValue()
		{
			return this.Clamp(this.currentValue);
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x00015D54 File Offset: 0x00013F54
		private float MousePosition()
		{
			if (this.horiz)
			{
				return this.CurrentEvent().mousePosition.x - this.position.x;
			}
			return this.CurrentEvent().mousePosition.y - this.position.y;
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x00015DB4 File Offset: 0x00013FB4
		private float ValuesPerPixel()
		{
			if (this.horiz)
			{
				return (this.position.width - (float)this.slider.padding.horizontal - this.ThumbSize()) / (this.end - this.start);
			}
			return (this.position.height - (float)this.slider.padding.vertical - this.ThumbSize()) / (this.end - this.start);
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x00015E38 File Offset: 0x00014038
		private float ThumbSize()
		{
			if (this.horiz)
			{
				return (this.thumb.fixedWidth == 0f) ? ((float)this.thumb.padding.horizontal) : this.thumb.fixedWidth;
			}
			return (this.thumb.fixedHeight == 0f) ? ((float)this.thumb.padding.vertical) : this.thumb.fixedHeight;
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x00015EC0 File Offset: 0x000140C0
		private float MaxValue()
		{
			return Mathf.Max(this.start, this.end) - this.size;
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x00015EDC File Offset: 0x000140DC
		private float MinValue()
		{
			return Mathf.Min(this.start, this.end);
		}

		// Token: 0x040003C2 RID: 962
		private readonly Rect position;

		// Token: 0x040003C3 RID: 963
		private readonly float currentValue;

		// Token: 0x040003C4 RID: 964
		private readonly float size;

		// Token: 0x040003C5 RID: 965
		private readonly float start;

		// Token: 0x040003C6 RID: 966
		private readonly float end;

		// Token: 0x040003C7 RID: 967
		private readonly GUIStyle slider;

		// Token: 0x040003C8 RID: 968
		private readonly GUIStyle thumb;

		// Token: 0x040003C9 RID: 969
		private readonly bool horiz;

		// Token: 0x040003CA RID: 970
		private readonly int id;
	}
}
