using System;
using System.Reflection;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000298 RID: 664
	internal class ArgInfo
	{
		// Token: 0x06001524 RID: 5412 RVA: 0x0004A5BC File Offset: 0x000487BC
		public ArgInfo(MethodBase method, ArgInfoType type)
		{
			this._method = method;
			ParameterInfo[] parameters = this._method.GetParameters();
			this._paramMap = new int[parameters.Length];
			this._inoutArgCount = 0;
			if (type == ArgInfoType.In)
			{
				for (int i = 0; i < parameters.Length; i++)
				{
					if (!parameters[i].ParameterType.IsByRef)
					{
						this._paramMap[this._inoutArgCount++] = i;
					}
				}
			}
			else
			{
				for (int j = 0; j < parameters.Length; j++)
				{
					if (parameters[j].ParameterType.IsByRef || parameters[j].IsOut)
					{
						this._paramMap[this._inoutArgCount++] = j;
					}
				}
			}
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x0004A690 File Offset: 0x00048890
		public int GetInOutArgIndex(int inoutArgNum)
		{
			return this._paramMap[inoutArgNum];
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x0004A69C File Offset: 0x0004889C
		public virtual string GetInOutArgName(int index)
		{
			return this._method.GetParameters()[this._paramMap[index]].Name;
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x0004A6B8 File Offset: 0x000488B8
		public int GetInOutArgCount()
		{
			return this._inoutArgCount;
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x0004A6C0 File Offset: 0x000488C0
		public object[] GetInOutArgs(object[] args)
		{
			object[] array = new object[this._inoutArgCount];
			for (int i = 0; i < this._inoutArgCount; i++)
			{
				array[i] = args[this._paramMap[i]];
			}
			return array;
		}

		// Token: 0x04000AE4 RID: 2788
		private int[] _paramMap;

		// Token: 0x04000AE5 RID: 2789
		private int _inoutArgCount;

		// Token: 0x04000AE6 RID: 2790
		private MethodBase _method;
	}
}
