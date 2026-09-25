using System;
using System.Reflection.Emit;

namespace Boo.Lang.Runtime.DynamicDispatching.Emitters
{
	// Token: 0x0200000C RID: 12
	public abstract class DispatcherEmitter
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00002940 File Offset: 0x00000B40
		public DispatcherEmitter(Type owner, string dynamicMethodName)
		{
			this._dynamicMethod = new DynamicMethod(owner.Name + "$" + dynamicMethodName, typeof(object), new Type[]
			{
				typeof(object),
				typeof(object[])
			}, owner);
			this._il = this._dynamicMethod.GetILGenerator();
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000029AC File Offset: 0x00000BAC
		public Dispatcher Emit()
		{
			this.EmitMethodBody();
			return this.CreateMethodDispatcher();
		}

		// Token: 0x06000053 RID: 83
		protected abstract void EmitMethodBody();

		// Token: 0x06000054 RID: 84 RVA: 0x000029BC File Offset: 0x00000BBC
		protected Dispatcher CreateMethodDispatcher()
		{
			return (Dispatcher)this._dynamicMethod.CreateDelegate(typeof(Dispatcher));
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000029D8 File Offset: 0x00000BD8
		protected void EmitCastOrUnbox(Type type)
		{
			if (type.IsValueType)
			{
				this._il.Emit(OpCodes.Unbox, type);
				this._il.Emit(OpCodes.Ldobj, type);
			}
			else
			{
				this._il.Emit(OpCodes.Castclass, type);
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002A28 File Offset: 0x00000C28
		protected void BoxIfNeeded(Type returnType)
		{
			if (returnType.IsValueType)
			{
				this._il.Emit(OpCodes.Box, returnType);
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002A48 File Offset: 0x00000C48
		protected void EmitReturn(Type typeOnStack)
		{
			if (typeOnStack == typeof(void))
			{
				this._il.Emit(OpCodes.Ldnull);
			}
			else
			{
				this.BoxIfNeeded(typeOnStack);
			}
			this._il.Emit(OpCodes.Ret);
		}

		// Token: 0x04000011 RID: 17
		private DynamicMethod _dynamicMethod;

		// Token: 0x04000012 RID: 18
		protected readonly ILGenerator _il;
	}
}
