using System;

namespace System.Runtime.Serialization
{
	// Token: 0x020002E6 RID: 742
	internal abstract class BaseFixupRecord
	{
		// Token: 0x06001733 RID: 5939 RVA: 0x000519AC File Offset: 0x0004FBAC
		public BaseFixupRecord(ObjectRecord objectToBeFixed, ObjectRecord objectRequired)
		{
			this.ObjectToBeFixed = objectToBeFixed;
			this.ObjectRequired = objectRequired;
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x000519C4 File Offset: 0x0004FBC4
		public bool DoFixup(ObjectManager manager, bool strict)
		{
			if (this.ObjectToBeFixed.IsRegistered && this.ObjectRequired.IsInstanceReady)
			{
				this.FixupImpl(manager);
				return true;
			}
			if (!strict)
			{
				return false;
			}
			if (!this.ObjectToBeFixed.IsRegistered)
			{
				throw new SerializationException("An object with ID " + this.ObjectToBeFixed.ObjectID + " was included in a fixup, but it has not been registered");
			}
			if (!this.ObjectRequired.IsRegistered)
			{
				throw new SerializationException("An object with ID " + this.ObjectRequired.ObjectID + " was included in a fixup, but it has not been registered");
			}
			return false;
		}

		// Token: 0x06001735 RID: 5941
		protected abstract void FixupImpl(ObjectManager manager);

		// Token: 0x04000BE4 RID: 3044
		protected internal ObjectRecord ObjectToBeFixed;

		// Token: 0x04000BE5 RID: 3045
		protected internal ObjectRecord ObjectRequired;

		// Token: 0x04000BE6 RID: 3046
		public BaseFixupRecord NextSameContainer;

		// Token: 0x04000BE7 RID: 3047
		public BaseFixupRecord NextSameRequired;
	}
}
