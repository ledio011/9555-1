using System;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x02000051 RID: 81
	internal abstract class BaseInvokableCall
	{
		// Token: 0x060003FC RID: 1020 RVA: 0x00009438 File Offset: 0x00007638
		protected BaseInvokableCall()
		{
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00009440 File Offset: 0x00007640
		protected BaseInvokableCall(object target, MethodInfo function)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (function == null)
			{
				throw new ArgumentNullException("function");
			}
		}

		// Token: 0x060003FE RID: 1022
		public abstract void Invoke(object[] args);

		// Token: 0x060003FF RID: 1023 RVA: 0x0000946C File Offset: 0x0000766C
		protected static void ThrowOnInvalidArg<T>(object arg)
		{
			if (arg != null && !(arg is T))
			{
				throw new ArgumentException(UnityString.Format("Passed argument 'args[0]' is of the wrong type. Type:{0} Expected:{1}", new object[]
				{
					arg.GetType(),
					typeof(T)
				}));
			}
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x000094AC File Offset: 0x000076AC
		protected static bool AllowInvoke(Delegate @delegate)
		{
			return @delegate.Method.IsStatic || @delegate.Target != null;
		}

		// Token: 0x06000401 RID: 1025
		public abstract bool Find(object targetObj, MethodInfo method);
	}
}
