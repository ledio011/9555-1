using System;

namespace System.Runtime.Serialization
{
	// Token: 0x020002E7 RID: 743
	internal class DelayedFixupRecord : BaseFixupRecord
	{
		// Token: 0x06001736 RID: 5942 RVA: 0x00051A70 File Offset: 0x0004FC70
		public DelayedFixupRecord(ObjectRecord objectToBeFixed, string memberName, ObjectRecord objectRequired) : base(objectToBeFixed, objectRequired)
		{
			this._memberName = memberName;
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x00051A84 File Offset: 0x0004FC84
		protected override void FixupImpl(ObjectManager manager)
		{
			this.ObjectToBeFixed.SetMemberValue(manager, this._memberName, this.ObjectRequired.ObjectInstance);
		}

		// Token: 0x04000BE8 RID: 3048
		public string _memberName;
	}
}
