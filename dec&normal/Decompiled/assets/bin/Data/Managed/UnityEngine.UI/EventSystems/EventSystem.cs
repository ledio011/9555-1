using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Serialization;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000006 RID: 6
	[AddComponentMenu("Event/Event System")]
	public class EventSystem : UIBehaviour
	{
		// Token: 0x06000025 RID: 37 RVA: 0x00002444 File Offset: 0x00000644
		protected EventSystem()
		{
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000247C File Offset: 0x0000067C
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002484 File Offset: 0x00000684
		public static EventSystem current { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000248C File Offset: 0x0000068C
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002494 File Offset: 0x00000694
		public bool sendNavigationEvents
		{
			get
			{
				return this.m_sendNavigationEvents;
			}
			set
			{
				this.m_sendNavigationEvents = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000024A0 File Offset: 0x000006A0
		// (set) Token: 0x0600002C RID: 44 RVA: 0x000024A8 File Offset: 0x000006A8
		public int pixelDragThreshold
		{
			get
			{
				return this.m_DragThreshold;
			}
			set
			{
				this.m_DragThreshold = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000024B4 File Offset: 0x000006B4
		public BaseInputModule currentInputModule
		{
			get
			{
				return this.m_CurrentInputModule;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000024BC File Offset: 0x000006BC
		// (set) Token: 0x0600002F RID: 47 RVA: 0x000024C4 File Offset: 0x000006C4
		public GameObject firstSelectedGameObject
		{
			get
			{
				return this.m_FirstSelected;
			}
			set
			{
				this.m_FirstSelected = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000024D0 File Offset: 0x000006D0
		public GameObject currentSelectedGameObject
		{
			get
			{
				return this.m_CurrentSelected;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000024D8 File Offset: 0x000006D8
		[Obsolete("lastSelectedGameObject is no longer supported")]
		public GameObject lastSelectedGameObject
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000024DC File Offset: 0x000006DC
		public void UpdateModules()
		{
			base.GetComponents<BaseInputModule>(this.m_SystemInputModules);
			for (int i = this.m_SystemInputModules.Count - 1; i >= 0; i--)
			{
				if (!this.m_SystemInputModules[i] || !this.m_SystemInputModules[i].IsActive())
				{
					this.m_SystemInputModules.RemoveAt(i);
				}
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002550 File Offset: 0x00000750
		public bool alreadySelecting
		{
			get
			{
				return this.m_SelectionGuard;
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002558 File Offset: 0x00000758
		public void SetSelectedGameObject(GameObject selected, BaseEventData pointer)
		{
			if (this.m_SelectionGuard)
			{
				Debug.LogError("Attempting to select " + selected + "while already selecting an object.");
				return;
			}
			this.m_SelectionGuard = true;
			if (selected == this.m_CurrentSelected)
			{
				this.m_SelectionGuard = false;
				return;
			}
			ExecuteEvents.Execute<IDeselectHandler>(this.m_CurrentSelected, pointer, ExecuteEvents.deselectHandler);
			this.m_CurrentSelected = selected;
			ExecuteEvents.Execute<ISelectHandler>(this.m_CurrentSelected, pointer, ExecuteEvents.selectHandler);
			this.m_SelectionGuard = false;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000025D8 File Offset: 0x000007D8
		private BaseEventData baseEventDataCache
		{
			get
			{
				if (this.m_DummyData == null)
				{
					this.m_DummyData = new BaseEventData(this);
				}
				return this.m_DummyData;
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000025F8 File Offset: 0x000007F8
		public void SetSelectedGameObject(GameObject selected)
		{
			this.SetSelectedGameObject(selected, this.baseEventDataCache);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002608 File Offset: 0x00000808
		private static int RaycastComparer(RaycastResult lhs, RaycastResult rhs)
		{
			if (lhs.module != rhs.module)
			{
				if (lhs.module.eventCamera != null && rhs.module.eventCamera != null && lhs.module.eventCamera.depth != rhs.module.eventCamera.depth)
				{
					if (lhs.module.eventCamera.depth < rhs.module.eventCamera.depth)
					{
						return 1;
					}
					if (lhs.module.eventCamera.depth == rhs.module.eventCamera.depth)
					{
						return 0;
					}
					return -1;
				}
				else
				{
					if (lhs.module.sortOrderPriority != rhs.module.sortOrderPriority)
					{
						return rhs.module.sortOrderPriority.CompareTo(lhs.module.sortOrderPriority);
					}
					if (lhs.module.renderOrderPriority != rhs.module.renderOrderPriority)
					{
						return rhs.module.renderOrderPriority.CompareTo(lhs.module.renderOrderPriority);
					}
				}
			}
			if (lhs.sortingLayer != rhs.sortingLayer)
			{
				return rhs.sortingLayer.CompareTo(lhs.sortingLayer);
			}
			if (lhs.sortingOrder != rhs.sortingOrder)
			{
				return rhs.sortingOrder.CompareTo(lhs.sortingOrder);
			}
			if (lhs.depth != rhs.depth)
			{
				return rhs.depth.CompareTo(lhs.depth);
			}
			if (lhs.distance != rhs.distance)
			{
				return lhs.distance.CompareTo(rhs.distance);
			}
			return lhs.index.CompareTo(rhs.index);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002804 File Offset: 0x00000A04
		public void RaycastAll(PointerEventData eventData, List<RaycastResult> raycastResults)
		{
			raycastResults.Clear();
			List<BaseRaycaster> raycasters = RaycasterManager.GetRaycasters();
			for (int i = 0; i < raycasters.Count; i++)
			{
				BaseRaycaster baseRaycaster = raycasters[i];
				if (!(baseRaycaster == null) && baseRaycaster.IsActive())
				{
					baseRaycaster.Raycast(eventData, raycastResults);
				}
			}
			raycastResults.Sort(EventSystem.s_RaycastComparer);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000286C File Offset: 0x00000A6C
		public bool IsPointerOverGameObject()
		{
			return this.IsPointerOverGameObject(-1);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002878 File Offset: 0x00000A78
		public bool IsPointerOverGameObject(int pointerId)
		{
			return !(this.m_CurrentInputModule == null) && this.m_CurrentInputModule.IsPointerOverGameObject(pointerId);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000289C File Offset: 0x00000A9C
		protected override void OnEnable()
		{
			base.OnEnable();
			if (EventSystem.current == null)
			{
				EventSystem.current = this;
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000028BC File Offset: 0x00000ABC
		protected override void OnDisable()
		{
			if (this.m_CurrentInputModule != null)
			{
				this.m_CurrentInputModule.DeactivateModule();
				this.m_CurrentInputModule = null;
			}
			if (EventSystem.current == this)
			{
				EventSystem.current = null;
			}
			base.OnDisable();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002908 File Offset: 0x00000B08
		private void TickModules()
		{
			for (int i = 0; i < this.m_SystemInputModules.Count; i++)
			{
				if (this.m_SystemInputModules[i] != null)
				{
					this.m_SystemInputModules[i].UpdateModule();
				}
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000295C File Offset: 0x00000B5C
		protected virtual void Update()
		{
			if (EventSystem.current != this)
			{
				return;
			}
			this.TickModules();
			bool flag = false;
			for (int i = 0; i < this.m_SystemInputModules.Count; i++)
			{
				BaseInputModule baseInputModule = this.m_SystemInputModules[i];
				if (baseInputModule.IsModuleSupported() && baseInputModule.ShouldActivateModule())
				{
					if (this.m_CurrentInputModule != baseInputModule)
					{
						this.ChangeEventModule(baseInputModule);
						flag = true;
					}
					break;
				}
			}
			if (this.m_CurrentInputModule == null)
			{
				for (int j = 0; j < this.m_SystemInputModules.Count; j++)
				{
					BaseInputModule baseInputModule2 = this.m_SystemInputModules[j];
					if (baseInputModule2.IsModuleSupported())
					{
						this.ChangeEventModule(baseInputModule2);
						flag = true;
						break;
					}
				}
			}
			if (!flag && this.m_CurrentInputModule != null)
			{
				this.m_CurrentInputModule.Process();
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002A58 File Offset: 0x00000C58
		private void ChangeEventModule(BaseInputModule module)
		{
			if (this.m_CurrentInputModule == module)
			{
				return;
			}
			if (this.m_CurrentInputModule != null)
			{
				this.m_CurrentInputModule.DeactivateModule();
			}
			if (module != null)
			{
				module.ActivateModule();
			}
			this.m_CurrentInputModule = module;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002AAC File Offset: 0x00000CAC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("<b>Selected:</b>" + this.currentSelectedGameObject);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			stringBuilder.AppendLine((!(this.m_CurrentInputModule != null)) ? "No module" : this.m_CurrentInputModule.ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x04000009 RID: 9
		private List<BaseInputModule> m_SystemInputModules = new List<BaseInputModule>();

		// Token: 0x0400000A RID: 10
		private BaseInputModule m_CurrentInputModule;

		// Token: 0x0400000B RID: 11
		[SerializeField]
		[FormerlySerializedAs("m_Selected")]
		private GameObject m_FirstSelected;

		// Token: 0x0400000C RID: 12
		[SerializeField]
		private bool m_sendNavigationEvents = true;

		// Token: 0x0400000D RID: 13
		[SerializeField]
		private int m_DragThreshold = 5;

		// Token: 0x0400000E RID: 14
		private GameObject m_CurrentSelected;

		// Token: 0x0400000F RID: 15
		private bool m_SelectionGuard;

		// Token: 0x04000010 RID: 16
		private BaseEventData m_DummyData;

		// Token: 0x04000011 RID: 17
		private static readonly Comparison<RaycastResult> s_RaycastComparer = new Comparison<RaycastResult>(EventSystem.RaycastComparer);
	}
}
