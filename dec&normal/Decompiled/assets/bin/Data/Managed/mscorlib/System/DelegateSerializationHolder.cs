using System;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000D0 RID: 208
	[Serializable]
	internal class DelegateSerializationHolder : IObjectReference, ISerializable
	{
		// Token: 0x06000874 RID: 2164 RVA: 0x00020B28 File Offset: 0x0001ED28
		private DelegateSerializationHolder(SerializationInfo info, StreamingContext ctx)
		{
			DelegateSerializationHolder.DelegateEntry delegateEntry = (DelegateSerializationHolder.DelegateEntry)info.GetValue("Delegate", typeof(DelegateSerializationHolder.DelegateEntry));
			int num = 0;
			DelegateSerializationHolder.DelegateEntry delegateEntry2 = delegateEntry;
			while (delegateEntry2 != null)
			{
				delegateEntry2 = delegateEntry2.delegateEntry;
				num++;
			}
			if (num == 1)
			{
				this._delegate = delegateEntry.DeserializeDelegate(info);
			}
			else
			{
				Delegate[] array = new Delegate[num];
				delegateEntry2 = delegateEntry;
				for (int i = 0; i < num; i++)
				{
					array[i] = delegateEntry2.DeserializeDelegate(info);
					delegateEntry2 = delegateEntry2.delegateEntry;
				}
				this._delegate = Delegate.Combine(array);
			}
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00020BC8 File Offset: 0x0001EDC8
		public static void GetDelegateData(Delegate instance, SerializationInfo info, StreamingContext ctx)
		{
			Delegate[] invocationList = instance.GetInvocationList();
			DelegateSerializationHolder.DelegateEntry delegateEntry = null;
			for (int i = 0; i < invocationList.Length; i++)
			{
				Delegate @delegate = invocationList[i];
				string text = (@delegate.Target == null) ? null : ("target" + i);
				DelegateSerializationHolder.DelegateEntry delegateEntry2 = new DelegateSerializationHolder.DelegateEntry(@delegate, text);
				if (delegateEntry == null)
				{
					info.AddValue("Delegate", delegateEntry2);
				}
				else
				{
					delegateEntry.delegateEntry = delegateEntry2;
				}
				delegateEntry = delegateEntry2;
				if (@delegate.Target != null)
				{
					info.AddValue(text, @delegate.Target);
				}
			}
			info.SetType(typeof(DelegateSerializationHolder));
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00020C70 File Offset: 0x0001EE70
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x00020C78 File Offset: 0x0001EE78
		public object GetRealObject(StreamingContext context)
		{
			return this._delegate;
		}

		// Token: 0x040002C5 RID: 709
		private Delegate _delegate;

		// Token: 0x020000D1 RID: 209
		[Serializable]
		private class DelegateEntry
		{
			// Token: 0x06000878 RID: 2168 RVA: 0x00020C80 File Offset: 0x0001EE80
			public DelegateEntry(Delegate del, string targetLabel)
			{
				this.type = del.GetType().FullName;
				this.assembly = del.GetType().Assembly.FullName;
				this.target = targetLabel;
				this.targetTypeAssembly = del.Method.DeclaringType.Assembly.FullName;
				this.targetTypeName = del.Method.DeclaringType.FullName;
				this.methodName = del.Method.Name;
			}

			// Token: 0x06000879 RID: 2169 RVA: 0x00020D04 File Offset: 0x0001EF04
			public Delegate DeserializeDelegate(SerializationInfo info)
			{
				object obj = null;
				if (this.target != null)
				{
					obj = info.GetValue(this.target.ToString(), typeof(object));
				}
				Assembly assembly = Assembly.Load(this.assembly);
				Type type = assembly.GetType(this.type);
				Delegate result;
				if (obj != null)
				{
					if (RemotingServices.IsTransparentProxy(obj))
					{
						Assembly assembly2 = Assembly.Load(this.targetTypeAssembly);
						Type type2 = assembly2.GetType(this.targetTypeName);
						if (!type2.IsInstanceOfType(obj))
						{
							throw new RemotingException("Unexpected proxy type.");
						}
					}
					result = Delegate.CreateDelegate(type, obj, this.methodName);
				}
				else
				{
					Assembly assembly3 = Assembly.Load(this.targetTypeAssembly);
					Type type3 = assembly3.GetType(this.targetTypeName);
					result = Delegate.CreateDelegate(type, type3, this.methodName);
				}
				return result;
			}

			// Token: 0x040002C6 RID: 710
			private string type;

			// Token: 0x040002C7 RID: 711
			private string assembly;

			// Token: 0x040002C8 RID: 712
			public object target;

			// Token: 0x040002C9 RID: 713
			private string targetTypeAssembly;

			// Token: 0x040002CA RID: 714
			private string targetTypeName;

			// Token: 0x040002CB RID: 715
			private string methodName;

			// Token: 0x040002CC RID: 716
			public DelegateSerializationHolder.DelegateEntry delegateEntry;
		}
	}
}
