using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000007 RID: 7
	[AddComponentMenu("Event/Event Trigger")]
	public class EventTrigger : MonoBehaviour, IBeginDragHandler, ICancelHandler, IDeselectHandler, IDragHandler, IDropHandler, IEndDragHandler, IEventSystemHandler, IInitializePotentialDragHandler, IMoveHandler, IPointerClickHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IScrollHandler, ISelectHandler, ISubmitHandler, IUpdateSelectedHandler
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00002B18 File Offset: 0x00000D18
		protected EventTrigger()
		{
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002B20 File Offset: 0x00000D20
		private void Execute(EventTriggerType id, BaseEventData eventData)
		{
			if (this.delegates != null)
			{
				int i = 0;
				int count = this.delegates.Count;
				while (i < count)
				{
					EventTrigger.Entry entry = this.delegates[i];
					if (entry.eventID == id && entry.callback != null)
					{
						entry.callback.Invoke(eventData);
					}
					i++;
				}
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002B88 File Offset: 0x00000D88
		public virtual void OnPointerEnter(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerEnter, eventData);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002B94 File Offset: 0x00000D94
		public virtual void OnPointerExit(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerExit, eventData);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002BA0 File Offset: 0x00000DA0
		public virtual void OnDrag(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.Drag, eventData);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002BAC File Offset: 0x00000DAC
		public virtual void OnDrop(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.Drop, eventData);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002BB8 File Offset: 0x00000DB8
		public virtual void OnPointerDown(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerDown, eventData);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002BC4 File Offset: 0x00000DC4
		public virtual void OnPointerUp(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerUp, eventData);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002BD0 File Offset: 0x00000DD0
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerClick, eventData);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002BDC File Offset: 0x00000DDC
		public virtual void OnSelect(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.Select, eventData);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002BE8 File Offset: 0x00000DE8
		public virtual void OnDeselect(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.Deselect, eventData);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002BF4 File Offset: 0x00000DF4
		public virtual void OnScroll(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.Scroll, eventData);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002C00 File Offset: 0x00000E00
		public virtual void OnMove(AxisEventData eventData)
		{
			this.Execute(EventTriggerType.Move, eventData);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002C0C File Offset: 0x00000E0C
		public virtual void OnUpdateSelected(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.UpdateSelected, eventData);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002C18 File Offset: 0x00000E18
		public virtual void OnInitializePotentialDrag(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.InitializePotentialDrag, eventData);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002C24 File Offset: 0x00000E24
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.BeginDrag, eventData);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002C30 File Offset: 0x00000E30
		public virtual void OnEndDrag(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.EndDrag, eventData);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002C3C File Offset: 0x00000E3C
		public virtual void OnSubmit(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.Submit, eventData);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002C48 File Offset: 0x00000E48
		public virtual void OnCancel(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.Cancel, eventData);
		}

		// Token: 0x04000013 RID: 19
		public List<EventTrigger.Entry> delegates;

		// Token: 0x02000008 RID: 8
		[Serializable]
		public class Entry
		{
			// Token: 0x04000014 RID: 20
			public EventTriggerType eventID = EventTriggerType.PointerClick;

			// Token: 0x04000015 RID: 21
			public EventTrigger.TriggerEvent callback = new EventTrigger.TriggerEvent();
		}

		// Token: 0x02000009 RID: 9
		[Serializable]
		public class TriggerEvent : UnityEvent<BaseEventData>
		{
		}
	}
}
