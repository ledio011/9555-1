using System;

namespace System.Runtime.Serialization
{
	// Token: 0x02000306 RID: 774
	internal class MultiArrayFixupRecord : BaseFixupRecord
	{
		// Token: 0x060017D0 RID: 6096 RVA: 0x00056A04 File Offset: 0x00054C04
		public MultiArrayFixupRecord(ObjectRecord objectToBeFixed, int[] indices, ObjectRecord objectRequired) : base(objectToBeFixed, objectRequired)
		{
			this._indices = indices;
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x00056A18 File Offset: 0x00054C18
		protected override void FixupImpl(ObjectManager manager)
		{
			this.ObjectToBeFixed.SetArrayValue(manager, this.ObjectRequired.ObjectInstance, this._indices);
		}

		// Token: 0x04000C5C RID: 3164
		private int[] _indices;
	}
}
