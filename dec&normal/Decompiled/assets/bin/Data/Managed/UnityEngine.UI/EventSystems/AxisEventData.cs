using System;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000002 RID: 2
	public class AxisEventData : BaseEventData
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public AxisEventData(EventSystem eventSystem) : base(eventSystem)
		{
			this.moveVector = Vector2.zero;
			this.moveDir = MoveDirection.None;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x0000206C File Offset: 0x0000026C
		// (set) Token: 0x06000003 RID: 3 RVA: 0x00002074 File Offset: 0x00000274
		public Vector2 moveVector { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002080 File Offset: 0x00000280
		// (set) Token: 0x06000005 RID: 5 RVA: 0x00002088 File Offset: 0x00000288
		public MoveDirection moveDir { get; set; }
	}
}
