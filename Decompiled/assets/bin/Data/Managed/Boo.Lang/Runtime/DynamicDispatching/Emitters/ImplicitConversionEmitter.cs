using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Boo.Lang.Runtime.DynamicDispatching.Emitters
{
	// Token: 0x0200000D RID: 13
	internal class ImplicitConversionEmitter : DispatcherEmitter
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00002A88 File Offset: 0x00000C88
		public ImplicitConversionEmitter(MethodInfo conversion) : base(conversion.DeclaringType, conversion.Name)
		{
			this._conversion = conversion;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002AA4 File Offset: 0x00000CA4
		protected override void EmitMethodBody()
		{
			this._il.Emit(OpCodes.Ldarg_0);
			base.EmitCastOrUnbox(this._conversion.GetParameters()[0].ParameterType);
			this._il.Emit(OpCodes.Call, this._conversion);
			base.EmitReturn(this._conversion.ReturnType);
		}

		// Token: 0x04000013 RID: 19
		private MethodInfo _conversion;
	}
}
