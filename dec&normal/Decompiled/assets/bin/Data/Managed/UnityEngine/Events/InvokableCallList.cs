using System;
using System.Collections.Generic;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x02000055 RID: 85
	internal class InvokableCallList
	{
		// Token: 0x0600040C RID: 1036 RVA: 0x00009674 File Offset: 0x00007874
		public void AddPersistentInvokableCall(BaseInvokableCall call)
		{
			this.m_PersistentCalls.Add(call);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00009684 File Offset: 0x00007884
		public void AddListener(BaseInvokableCall call)
		{
			this.m_RuntimeCalls.Add(call);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00009694 File Offset: 0x00007894
		public void RemoveListener(object targetObj, MethodInfo method)
		{
			List<BaseInvokableCall> list = new List<BaseInvokableCall>();
			for (int i = 0; i < this.m_RuntimeCalls.Count; i++)
			{
				if (this.m_RuntimeCalls[i].Find(targetObj, method))
				{
					list.Add(this.m_RuntimeCalls[i]);
				}
			}
			this.m_RuntimeCalls.RemoveAll(new Predicate<BaseInvokableCall>(list.Contains));
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00009708 File Offset: 0x00007908
		public void ClearPersistent()
		{
			this.m_PersistentCalls.Clear();
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00009718 File Offset: 0x00007918
		public void Invoke(object[] parameters)
		{
			this.m_ExecutingCalls.AddRange(this.m_PersistentCalls);
			this.m_ExecutingCalls.AddRange(this.m_RuntimeCalls);
			for (int i = 0; i < this.m_ExecutingCalls.Count; i++)
			{
				this.m_ExecutingCalls[i].Invoke(parameters);
			}
			this.m_ExecutingCalls.Clear();
		}

		// Token: 0x040000A9 RID: 169
		private readonly List<BaseInvokableCall> m_PersistentCalls = new List<BaseInvokableCall>();

		// Token: 0x040000AA RID: 170
		private readonly List<BaseInvokableCall> m_RuntimeCalls = new List<BaseInvokableCall>();

		// Token: 0x040000AB RID: 171
		private readonly List<BaseInvokableCall> m_ExecutingCalls = new List<BaseInvokableCall>();
	}
}
