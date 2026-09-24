using System;
using System.Reflection;

namespace System.Runtime.Serialization
{
	// Token: 0x020002E8 RID: 744
	internal class FixupRecord : BaseFixupRecord
	{
		// Token: 0x06001738 RID: 5944 RVA: 0x00051AA4 File Offset: 0x0004FCA4
		public FixupRecord(ObjectRecord objectToBeFixed, MemberInfo member, ObjectRecord objectRequired) : base(objectToBeFixed, objectRequired)
		{
			this._member = member;
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x00051AB8 File Offset: 0x0004FCB8
		protected override void FixupImpl(ObjectManager manager)
		{
			this.ObjectToBeFixed.SetMemberValue(manager, this._member, this.ObjectRequired.ObjectInstance);
		}

		// Token: 0x04000BE9 RID: 3049
		public MemberInfo _member;
	}
}
