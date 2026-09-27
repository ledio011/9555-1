using System;
using System.Collections.Generic;
using System.Text;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000025 RID: 37
	public abstract class PointerInputModule : BaseInputModule
	{
		// Token: 0x060000CB RID: 203 RVA: 0x00003A20 File Offset: 0x00001C20
		protected bool GetPointerData(int id, out PointerEventData data, bool create)
		{
			if (!this.m_PointerData.TryGetValue(id, out data) && create)
			{
				data = new PointerEventData(base.eventSystem)
				{
					pointerId = id
				};
				this.m_PointerData.Add(id, data);
				return true;
			}
			return false;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003A6C File Offset: 0x00001C6C
		protected void RemovePointerData(PointerEventData data)
		{
			this.m_PointerData.Remove(data.pointerId);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003A80 File Offset: 0x00001C80
		protected PointerEventData GetTouchPointerEventData(Touch input, out bool pressed, out bool released)
		{
			PointerEventData pointerEventData;
			bool pointerData = this.GetPointerData(input.fingerId, out pointerEventData, true);
			pointerEventData.Reset();
			pressed = (pointerData || input.phase == TouchPhase.Began);
			released = (input.phase == TouchPhase.Canceled || input.phase == TouchPhase.Ended);
			if (pointerData)
			{
				pointerEventData.position = input.position;
			}
			if (pressed)
			{
				pointerEventData.delta = Vector2.zero;
			}
			else
			{
				pointerEventData.delta = input.position - pointerEventData.position;
			}
			pointerEventData.position = input.position;
			pointerEventData.button = PointerEventData.InputButton.Left;
			base.eventSystem.RaycastAll(pointerEventData, this.m_RaycastResultCache);
			RaycastResult pointerCurrentRaycast = BaseInputModule.FindFirstRaycast(this.m_RaycastResultCache);
			pointerEventData.pointerCurrentRaycast = pointerCurrentRaycast;
			this.m_RaycastResultCache.Clear();
			return pointerEventData;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003B5C File Offset: 0x00001D5C
		protected void CopyFromTo(PointerEventData from, PointerEventData to)
		{
			to.position = from.position;
			to.delta = from.delta;
			to.scrollDelta = from.scrollDelta;
			to.pointerCurrentRaycast = from.pointerCurrentRaycast;
			to.pointerEnter = from.pointerEnter;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003B9C File Offset: 0x00001D9C
		protected static PointerEventData.FramePressState StateForMouseButton(int buttonId)
		{
			bool mouseButtonDown = Input.GetMouseButtonDown(buttonId);
			bool mouseButtonUp = Input.GetMouseButtonUp(buttonId);
			if (mouseButtonDown && mouseButtonUp)
			{
				return PointerEventData.FramePressState.PressedAndReleased;
			}
			if (mouseButtonDown)
			{
				return PointerEventData.FramePressState.Pressed;
			}
			if (mouseButtonUp)
			{
				return PointerEventData.FramePressState.Released;
			}
			return PointerEventData.FramePressState.NotChanged;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003BD8 File Offset: 0x00001DD8
		protected virtual PointerInputModule.MouseState GetMousePointerEventData()
		{
			return this.GetMousePointerEventData(0);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003BE4 File Offset: 0x00001DE4
		protected virtual PointerInputModule.MouseState GetMousePointerEventData(int id)
		{
			PointerEventData pointerEventData;
			bool pointerData = this.GetPointerData(-1, out pointerEventData, true);
			pointerEventData.Reset();
			if (pointerData)
			{
				pointerEventData.position = Input.mousePosition;
			}
			Vector2 vector = Input.mousePosition;
			pointerEventData.delta = vector - pointerEventData.position;
			pointerEventData.position = vector;
			pointerEventData.scrollDelta = Input.mouseScrollDelta;
			pointerEventData.button = PointerEventData.InputButton.Left;
			base.eventSystem.RaycastAll(pointerEventData, this.m_RaycastResultCache);
			RaycastResult pointerCurrentRaycast = BaseInputModule.FindFirstRaycast(this.m_RaycastResultCache);
			pointerEventData.pointerCurrentRaycast = pointerCurrentRaycast;
			this.m_RaycastResultCache.Clear();
			PointerEventData pointerEventData2;
			this.GetPointerData(-2, out pointerEventData2, true);
			this.CopyFromTo(pointerEventData, pointerEventData2);
			pointerEventData2.button = PointerEventData.InputButton.Right;
			PointerEventData pointerEventData3;
			this.GetPointerData(-3, out pointerEventData3, true);
			this.CopyFromTo(pointerEventData, pointerEventData3);
			pointerEventData3.button = PointerEventData.InputButton.Middle;
			this.m_MouseState.SetButtonState(PointerEventData.InputButton.Left, PointerInputModule.StateForMouseButton(0), pointerEventData);
			this.m_MouseState.SetButtonState(PointerEventData.InputButton.Right, PointerInputModule.StateForMouseButton(1), pointerEventData2);
			this.m_MouseState.SetButtonState(PointerEventData.InputButton.Middle, PointerInputModule.StateForMouseButton(2), pointerEventData3);
			return this.m_MouseState;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003D00 File Offset: 0x00001F00
		protected PointerEventData GetLastPointerEventData(int id)
		{
			PointerEventData result;
			this.GetPointerData(id, out result, false);
			return result;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003D1C File Offset: 0x00001F1C
		private static bool ShouldStartDrag(Vector2 pressPos, Vector2 currentPos, float threshold, bool useDragThreshold)
		{
			return !useDragThreshold || (pressPos - currentPos).sqrMagnitude >= threshold * threshold;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003D48 File Offset: 0x00001F48
		protected virtual void ProcessMove(PointerEventData pointerEvent)
		{
			GameObject gameObject = pointerEvent.pointerCurrentRaycast.gameObject;
			base.HandlePointerExitAndEnter(pointerEvent, gameObject);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003D6C File Offset: 0x00001F6C
		protected virtual void ProcessDrag(PointerEventData pointerEvent)
		{
			bool flag = pointerEvent.IsPointerMoving();
			if (flag && pointerEvent.pointerDrag != null && !pointerEvent.dragging && PointerInputModule.ShouldStartDrag(pointerEvent.pressPosition, pointerEvent.position, (float)base.eventSystem.pixelDragThreshold, pointerEvent.useDragThreshold))
			{
				ExecuteEvents.Execute<IBeginDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.beginDragHandler);
				pointerEvent.dragging = true;
			}
			if (pointerEvent.dragging && flag && pointerEvent.pointerDrag != null)
			{
				if (pointerEvent.pointerPress != pointerEvent.pointerDrag)
				{
					ExecuteEvents.Execute<IPointerUpHandler>(pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerUpHandler);
					pointerEvent.eligibleForClick = false;
					pointerEvent.pointerPress = null;
					pointerEvent.rawPointerPress = null;
				}
				ExecuteEvents.Execute<IDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.dragHandler);
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003E54 File Offset: 0x00002054
		public override bool IsPointerOverGameObject(int pointerId)
		{
			PointerEventData lastPointerEventData = this.GetLastPointerEventData(pointerId);
			return lastPointerEventData != null && lastPointerEventData.pointerEnter != null;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003E80 File Offset: 0x00002080
		protected void ClearSelection()
		{
			BaseEventData baseEventData = this.GetBaseEventData();
			foreach (PointerEventData currentPointerData in this.m_PointerData.Values)
			{
				base.HandlePointerExitAndEnter(currentPointerData, null);
			}
			this.m_PointerData.Clear();
			base.eventSystem.SetSelectedGameObject(null, baseEventData);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00003F00 File Offset: 0x00002100
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("<b>Pointer Input Module of type: </b>" + base.GetType());
			stringBuilder.AppendLine();
			foreach (KeyValuePair<int, PointerEventData> keyValuePair in this.m_PointerData)
			{
				if (keyValuePair.Value != null)
				{
					stringBuilder.AppendLine("<B>Pointer:</b> " + keyValuePair.Key);
					stringBuilder.AppendLine(keyValuePair.Value.ToString());
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00003FB8 File Offset: 0x000021B8
		protected void DeselectIfSelectionChanged(GameObject currentOverGo, BaseEventData pointerEvent)
		{
			GameObject eventHandler = ExecuteEvents.GetEventHandler<ISelectHandler>(currentOverGo);
			if (eventHandler != base.eventSystem.currentSelectedGameObject)
			{
				base.eventSystem.SetSelectedGameObject(null, pointerEvent);
			}
		}

		// Token: 0x04000063 RID: 99
		public const int kMouseLeftId = -1;

		// Token: 0x04000064 RID: 100
		public const int kMouseRightId = -2;

		// Token: 0x04000065 RID: 101
		public const int kMouseMiddleId = -3;

		// Token: 0x04000066 RID: 102
		public const int kFakeTouchesId = -4;

		// Token: 0x04000067 RID: 103
		protected Dictionary<int, PointerEventData> m_PointerData = new Dictionary<int, PointerEventData>();

		// Token: 0x04000068 RID: 104
		private readonly PointerInputModule.MouseState m_MouseState = new PointerInputModule.MouseState();

		// Token: 0x02000026 RID: 38
		protected class ButtonState
		{
			// Token: 0x1700003C RID: 60
			// (get) Token: 0x060000DB RID: 219 RVA: 0x00003FF8 File Offset: 0x000021F8
			// (set) Token: 0x060000DC RID: 220 RVA: 0x00004000 File Offset: 0x00002200
			public PointerInputModule.MouseButtonEventData eventData
			{
				get
				{
					return this.m_EventData;
				}
				set
				{
					this.m_EventData = value;
				}
			}

			// Token: 0x1700003D RID: 61
			// (get) Token: 0x060000DD RID: 221 RVA: 0x0000400C File Offset: 0x0000220C
			// (set) Token: 0x060000DE RID: 222 RVA: 0x00004014 File Offset: 0x00002214
			public PointerEventData.InputButton button
			{
				get
				{
					return this.m_Button;
				}
				set
				{
					this.m_Button = value;
				}
			}

			// Token: 0x04000069 RID: 105
			private PointerEventData.InputButton m_Button;

			// Token: 0x0400006A RID: 106
			private PointerInputModule.MouseButtonEventData m_EventData;
		}

		// Token: 0x02000027 RID: 39
		public class MouseButtonEventData
		{
			// Token: 0x060000E0 RID: 224 RVA: 0x00004028 File Offset: 0x00002228
			public bool PressedThisFrame()
			{
				return this.buttonState == PointerEventData.FramePressState.Pressed || this.buttonState == PointerEventData.FramePressState.PressedAndReleased;
			}

			// Token: 0x060000E1 RID: 225 RVA: 0x00004044 File Offset: 0x00002244
			public bool ReleasedThisFrame()
			{
				return this.buttonState == PointerEventData.FramePressState.Released || this.buttonState == PointerEventData.FramePressState.PressedAndReleased;
			}

			// Token: 0x0400006B RID: 107
			public PointerEventData.FramePressState buttonState;

			// Token: 0x0400006C RID: 108
			public PointerEventData buttonData;
		}

		// Token: 0x02000028 RID: 40
		protected class MouseState
		{
			// Token: 0x060000E3 RID: 227 RVA: 0x00004074 File Offset: 0x00002274
			public bool AnyPressesThisFrame()
			{
				for (int i = 0; i < this.m_TrackedButtons.Count; i++)
				{
					if (this.m_TrackedButtons[i].eventData.PressedThisFrame())
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x060000E4 RID: 228 RVA: 0x000040BC File Offset: 0x000022BC
			public bool AnyReleasesThisFrame()
			{
				for (int i = 0; i < this.m_TrackedButtons.Count; i++)
				{
					if (this.m_TrackedButtons[i].eventData.ReleasedThisFrame())
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x060000E5 RID: 229 RVA: 0x00004104 File Offset: 0x00002304
			public PointerInputModule.ButtonState GetButtonState(PointerEventData.InputButton button)
			{
				PointerInputModule.ButtonState buttonState = null;
				for (int i = 0; i < this.m_TrackedButtons.Count; i++)
				{
					if (this.m_TrackedButtons[i].button == button)
					{
						buttonState = this.m_TrackedButtons[i];
						break;
					}
				}
				if (buttonState == null)
				{
					buttonState = new PointerInputModule.ButtonState
					{
						button = button,
						eventData = new PointerInputModule.MouseButtonEventData()
					};
					this.m_TrackedButtons.Add(buttonState);
				}
				return buttonState;
			}

			// Token: 0x060000E6 RID: 230 RVA: 0x00004188 File Offset: 0x00002388
			public void SetButtonState(PointerEventData.InputButton button, PointerEventData.FramePressState stateForMouseButton, PointerEventData data)
			{
				PointerInputModule.ButtonState buttonState = this.GetButtonState(button);
				buttonState.eventData.buttonState = stateForMouseButton;
				buttonState.eventData.buttonData = data;
			}

			// Token: 0x0400006D RID: 109
			private List<PointerInputModule.ButtonState> m_TrackedButtons = new List<PointerInputModule.ButtonState>();
		}
	}
}
