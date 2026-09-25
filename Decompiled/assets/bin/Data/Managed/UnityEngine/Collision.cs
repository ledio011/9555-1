using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x02000039 RID: 57
	[StructLayout(0)]
	public class Collision
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000341 RID: 833 RVA: 0x000079DC File Offset: 0x00005BDC
		public Vector3 relativeVelocity
		{
			get
			{
				return this.m_RelativeVelocity;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000342 RID: 834 RVA: 0x000079E4 File Offset: 0x00005BE4
		public Rigidbody rigidbody
		{
			get
			{
				return this.m_Rigidbody;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000343 RID: 835 RVA: 0x000079EC File Offset: 0x00005BEC
		public GameObject gameObject
		{
			get
			{
				return (!(this.m_Rigidbody != null)) ? this.m_Collider.gameObject : this.m_Rigidbody.gameObject;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000344 RID: 836 RVA: 0x00007A1C File Offset: 0x00005C1C
		public ContactPoint[] contacts
		{
			get
			{
				return this.m_Contacts;
			}
		}

		// Token: 0x04000049 RID: 73
		internal Vector3 m_RelativeVelocity;

		// Token: 0x0400004A RID: 74
		internal Rigidbody m_Rigidbody;

		// Token: 0x0400004B RID: 75
		internal Collider m_Collider;

		// Token: 0x0400004C RID: 76
		internal ContactPoint[] m_Contacts;
	}
}
