using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace UnityEngine.Events
{
	// Token: 0x02000057 RID: 87
	[Serializable]
	internal class PersistentCallGroup
	{
		// Token: 0x06000419 RID: 1049 RVA: 0x000099A8 File Offset: 0x00007BA8
		public PersistentCallGroup()
		{
			this.m_Calls = new List<PersistentCall>();
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x000099BC File Offset: 0x00007BBC
		public void Initialize(InvokableCallList invokableList, UnityEventBase unityEventBase)
		{
			foreach (PersistentCall persistentCall in this.m_Calls)
			{
				if (persistentCall.IsValid())
				{
					BaseInvokableCall runtimeCall = persistentCall.GetRuntimeCall(unityEventBase);
					if (runtimeCall != null)
					{
						invokableList.AddPersistentInvokableCall(runtimeCall);
					}
				}
			}
		}

		// Token: 0x040000B1 RID: 177
		[SerializeField]
		[FormerlySerializedAs("m_Listeners")]
		private List<PersistentCall> m_Calls;
	}
}
