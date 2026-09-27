using System;

namespace UnityEngine
{
	// Token: 0x0200009D RID: 157
	public interface ISerializationCallbackReceiver
	{
		// Token: 0x0600070B RID: 1803
		void OnBeforeSerialize();

		// Token: 0x0600070C RID: 1804
		void OnAfterDeserialize();
	}
}
