using System;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000003 RID: 3
	public class BaseEventData
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002094 File Offset: 0x00000294
		public BaseEventData(EventSystem eventSystem)
		{
			this.m_EventSystem = eventSystem;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020A4 File Offset: 0x000002A4
		public void Reset()
		{
			this.m_Used = false;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020B0 File Offset: 0x000002B0
		public void Use()
		{
			this.m_Used = true;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020BC File Offset: 0x000002BC
		public bool used
		{
			get
			{
				return this.m_Used;
			}
		}

		// Token: 0x17000004 RID: 4
		// (set) Token: 0x0600000A RID: 10 RVA: 0x000020C4 File Offset: 0x000002C4
		public GameObject selectedObject
		{
			set
			{
				this.m_EventSystem.SetSelectedGameObject(value, this);
			}
		}

		// Token: 0x04000003 RID: 3
		private readonly EventSystem m_EventSystem;

		// Token: 0x04000004 RID: 4
		private bool m_Used;
	}
}
