using System;

namespace System.Runtime.Serialization
{
	// Token: 0x020002E5 RID: 741
	internal class ArrayFixupRecord : BaseFixupRecord
	{
		// Token: 0x06001731 RID: 5937 RVA: 0x00051960 File Offset: 0x0004FB60
		public ArrayFixupRecord(ObjectRecord objectToBeFixed, int index, ObjectRecord objectRequired) : base(objectToBeFixed, objectRequired)
		{
			this._index = index;
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x00051974 File Offset: 0x0004FB74
		protected override void FixupImpl(ObjectManager manager)
		{
			Array array = (Array)this.ObjectToBeFixed.ObjectInstance;
			array.SetValue(this.ObjectRequired.ObjectInstance, this._index);
		}

		// Token: 0x04000BE3 RID: 3043
		private int _index;
	}
}
