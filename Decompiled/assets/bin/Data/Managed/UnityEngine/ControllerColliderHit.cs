using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x02000044 RID: 68
	[StructLayout(0)]
	public sealed class ControllerColliderHit
	{
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000385 RID: 901 RVA: 0x000080B4 File Offset: 0x000062B4
		public Collider collider
		{
			get
			{
				return this.m_Collider;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000386 RID: 902 RVA: 0x000080BC File Offset: 0x000062BC
		public Vector3 point
		{
			get
			{
				return this.m_Point;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000387 RID: 903 RVA: 0x000080C4 File Offset: 0x000062C4
		public Vector3 normal
		{
			get
			{
				return this.m_Normal;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000388 RID: 904 RVA: 0x000080CC File Offset: 0x000062CC
		public Vector3 moveDirection
		{
			get
			{
				return this.m_MoveDirection;
			}
		}

		// Token: 0x04000069 RID: 105
		internal CharacterController m_Controller;

		// Token: 0x0400006A RID: 106
		internal Collider m_Collider;

		// Token: 0x0400006B RID: 107
		internal Vector3 m_Point;

		// Token: 0x0400006C RID: 108
		internal Vector3 m_Normal;

		// Token: 0x0400006D RID: 109
		internal Vector3 m_MoveDirection;

		// Token: 0x0400006E RID: 110
		internal float m_MoveLength;

		// Token: 0x0400006F RID: 111
		internal int m_Push;
	}
}
