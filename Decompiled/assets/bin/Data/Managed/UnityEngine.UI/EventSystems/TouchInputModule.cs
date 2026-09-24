using System;
using System.Collections.Generic;
using System.Text;

namespace UnityEngine.EventSystems
{
	// Token: 0x0200002D RID: 45
	[AddComponentMenu("Event/Touch Input Module")]
	public class TouchInputModule : PointerInputModule
	{
		// Token: 0x0600010B RID: 267 RVA: 0x00004B7C File Offset: 0x00002D7C
		protected TouchInputModule()
		{
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00004B84 File Offset: 0x00002D84
		// (set) Token: 0x0600010D RID: 269 RVA: 0x00004B8C File Offset: 0x00002D8C
		public bool allowActivationOnStandalone
		{
			get
			{
				return this.m_AllowActivationOnStandalone;
			}
			set
			{
				this.m_AllowActivationOnStandalone = value;
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004B98 File Offset: 0x00002D98
		public override void UpdateModule()
		{
			this.m_LastMousePosition = this.m_MousePosition;
			this.m_MousePosition = Input.mousePosition;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00004BB8 File Offset: 0x00002DB8
		public override bool IsModuleSupported()
		{
			return this.m_AllowActivationOnStandalone || Input.touchSupported;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00004BD0 File Offset: 0x00002DD0
		public override bool ShouldActivateModule()
		{
			if (!base.ShouldActivateModule())
			{
				return false;
			}
			if (this.UseFakeInput())
			{
				bool mouseButtonDown = Input.GetMouseButtonDown(0);
				return mouseButtonDown | (this.m_MousePosition - this.m_LastMousePosition).sqrMagnitude > 0f;
			}
			for (int i = 0; i < Input.touchCount; i++)
			{
				Touch touch = Input.GetTouch(i);
				if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00004C68 File Offset: 0x00002E68
		private bool UseFakeInput()
		{
			return !Input.touchSupported;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00004C74 File Offset: 0x00002E74
		public override void Process()
		{
			if (this.UseFakeInput())
			{
				this.FakeTouches();
			}
			else
			{
				this.ProcessTouchEvents();
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004C94 File Offset: 0x00002E94
		private void FakeTouches()
		{
			PointerInputModule.MouseState mousePointerEventData = this.GetMousePointerEventData(0);
			PointerInputModule.MouseButtonEventData eventData = mousePointerEventData.GetButtonState(PointerEventData.InputButton.Left).eventData;
			if (eventData.PressedThisFrame())
			{
				eventData.buttonData.delta = Vector2.zero;
			}
			this.ProcessTouchPress(eventData.buttonData, eventData.PressedThisFrame(), eventData.ReleasedThisFrame());
			if (Input.GetMouseButton(0))
			{
				this.ProcessMove(eventData.buttonData);
				this.ProcessDrag(eventData.buttonData);
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004D0C File Offset: 0x00002F0C
		private void ProcessTouchEvents()
		{
			for (int i = 0; i < Input.touchCount; i++)
			{
				Touch touch = Input.GetTouch(i);
				bool pressed;
				bool flag;
				PointerEventData touchPointerEventData = base.GetTouchPointerEventData(touch, out pressed, out flag);
				this.ProcessTouchPress(touchPointerEventData, pressed, flag);
				if (!flag)
				{
					this.ProcessMove(touchPointerEventData);
					this.ProcessDrag(touchPointerEventData);
				}
				else
				{
					base.RemovePointerData(touchPointerEventData);
				}
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00004D70 File Offset: 0x00002F70
		private void ProcessTouchPress(PointerEventData pointerEvent, bool pressed, bool released)
		{
			GameObject gameObject = pointerEvent.pointerCurrentRaycast.gameObject;
			if (pressed)
			{
				pointerEvent.eligibleForClick = true;
				pointerEvent.delta = Vector2.zero;
				pointerEvent.dragging = false;
				pointerEvent.useDragThreshold = true;
				pointerEvent.pressPosition = pointerEvent.position;
				pointerEvent.pointerPressRaycast = pointerEvent.pointerCurrentRaycast;
				base.DeselectIfSelectionChanged(gameObject, pointerEvent);
				if (pointerEvent.pointerEnter != gameObject)
				{
					base.HandlePointerExitAndEnter(pointerEvent, gameObject);
					pointerEvent.pointerEnter = gameObject;
				}
				GameObject gameObject2 = ExecuteEvents.ExecuteHierarchy<IPointerDownHandler>(gameObject, pointerEvent, ExecuteEvents.pointerDownHandler);
				if (gameObject2 == null)
				{
					gameObject2 = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
				}
				float unscaledTime = Time.unscaledTime;
				if (gameObject2 == pointerEvent.lastPress)
				{
					float num = unscaledTime - pointerEvent.clickTime;
					if (num < 0.3f)
					{
						pointerEvent.clickCount++;
					}
					else
					{
						pointerEvent.clickCount = 1;
					}
					pointerEvent.clickTime = unscaledTime;
				}
				else
				{
					pointerEvent.clickCount = 1;
				}
				pointerEvent.pointerPress = gameObject2;
				pointerEvent.rawPointerPress = gameObject;
				pointerEvent.clickTime = unscaledTime;
				pointerEvent.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(gameObject);
				if (pointerEvent.pointerDrag != null)
				{
					ExecuteEvents.Execute<IInitializePotentialDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.initializePotentialDrag);
				}
			}
			if (released)
			{
				ExecuteEvents.Execute<IPointerUpHandler>(pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerUpHandler);
				GameObject eventHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
				if (pointerEvent.pointerPress == eventHandler && pointerEvent.eligibleForClick)
				{
					ExecuteEvents.Execute<IPointerClickHandler>(pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerClickHandler);
				}
				else if (pointerEvent.pointerDrag != null)
				{
					ExecuteEvents.ExecuteHierarchy<IDropHandler>(gameObject, pointerEvent, ExecuteEvents.dropHandler);
				}
				pointerEvent.eligibleForClick = false;
				pointerEvent.pointerPress = null;
				pointerEvent.rawPointerPress = null;
				if (pointerEvent.pointerDrag != null && pointerEvent.dragging)
				{
					ExecuteEvents.Execute<IEndDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.endDragHandler);
				}
				pointerEvent.dragging = false;
				pointerEvent.pointerDrag = null;
				if (pointerEvent.pointerDrag != null)
				{
					ExecuteEvents.Execute<IEndDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.endDragHandler);
				}
				pointerEvent.pointerDrag = null;
				ExecuteEvents.ExecuteHierarchy<IPointerExitHandler>(pointerEvent.pointerEnter, pointerEvent, ExecuteEvents.pointerExitHandler);
				pointerEvent.pointerEnter = null;
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004FB8 File Offset: 0x000031B8
		public override void DeactivateModule()
		{
			base.DeactivateModule();
			base.ClearSelection();
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004FC8 File Offset: 0x000031C8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine((!this.UseFakeInput()) ? "Input: Touch" : "Input: Faked");
			if (this.UseFakeInput())
			{
				PointerEventData lastPointerEventData = base.GetLastPointerEventData(-1);
				if (lastPointerEventData != null)
				{
					stringBuilder.AppendLine(lastPointerEventData.ToString());
				}
			}
			else
			{
				foreach (KeyValuePair<int, PointerEventData> keyValuePair in this.m_PointerData)
				{
					stringBuilder.AppendLine(keyValuePair.ToString());
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000085 RID: 133
		private Vector2 m_LastMousePosition;

		// Token: 0x04000086 RID: 134
		private Vector2 m_MousePosition;

		// Token: 0x04000087 RID: 135
		[SerializeField]
		private bool m_AllowActivationOnStandalone;
	}
}
