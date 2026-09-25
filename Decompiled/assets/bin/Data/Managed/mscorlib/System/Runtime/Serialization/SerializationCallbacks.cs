using System;
using System.Collections;
using System.Reflection;

namespace System.Runtime.Serialization
{
	// Token: 0x02000311 RID: 785
	internal sealed class SerializationCallbacks
	{
		// Token: 0x060017F6 RID: 6134 RVA: 0x0005779C File Offset: 0x0005599C
		public SerializationCallbacks(Type type)
		{
			this.onSerializingList = SerializationCallbacks.GetMethodsByAttribute(type, typeof(OnSerializingAttribute));
			this.onSerializedList = SerializationCallbacks.GetMethodsByAttribute(type, typeof(OnSerializedAttribute));
			this.onDeserializingList = SerializationCallbacks.GetMethodsByAttribute(type, typeof(OnDeserializingAttribute));
			this.onDeserializedList = SerializationCallbacks.GetMethodsByAttribute(type, typeof(OnDeserializedAttribute));
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x060017F8 RID: 6136 RVA: 0x00057820 File Offset: 0x00055A20
		public bool HasSerializedCallbacks
		{
			get
			{
				return this.onSerializedList != null;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x060017F9 RID: 6137 RVA: 0x00057830 File Offset: 0x00055A30
		public bool HasDeserializedCallbacks
		{
			get
			{
				return this.onDeserializedList != null;
			}
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x00057840 File Offset: 0x00055A40
		private static ArrayList GetMethodsByAttribute(Type type, Type attr)
		{
			ArrayList arrayList = new ArrayList();
			for (Type type2 = type; type2 != typeof(object); type2 = type2.BaseType)
			{
				int num = 0;
				foreach (MethodInfo methodInfo in type2.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
				{
					if (methodInfo.IsDefined(attr, false))
					{
						arrayList.Add(methodInfo);
						num++;
					}
				}
				if (num > 1)
				{
					throw new TypeLoadException(string.Format("Type '{0}' has more than one method with the following attribute: '{1}'.", type.AssemblyQualifiedName, attr.FullName));
				}
			}
			return (arrayList.Count != 0) ? arrayList : null;
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x000578EC File Offset: 0x00055AEC
		private static void Invoke(ArrayList list, object target, StreamingContext context)
		{
			if (list == null)
			{
				return;
			}
			SerializationCallbacks.CallbackHandler callbackHandler = null;
			foreach (object obj in list)
			{
				MethodInfo method = (MethodInfo)obj;
				callbackHandler = (SerializationCallbacks.CallbackHandler)Delegate.Combine(Delegate.CreateDelegate(typeof(SerializationCallbacks.CallbackHandler), target, method), callbackHandler);
			}
			callbackHandler(context);
		}

		// Token: 0x060017FC RID: 6140 RVA: 0x00057970 File Offset: 0x00055B70
		public void RaiseOnSerializing(object target, StreamingContext contex)
		{
			SerializationCallbacks.Invoke(this.onSerializingList, target, contex);
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x00057980 File Offset: 0x00055B80
		public void RaiseOnSerialized(object target, StreamingContext contex)
		{
			SerializationCallbacks.Invoke(this.onSerializedList, target, contex);
		}

		// Token: 0x060017FE RID: 6142 RVA: 0x00057990 File Offset: 0x00055B90
		public void RaiseOnDeserializing(object target, StreamingContext contex)
		{
			SerializationCallbacks.Invoke(this.onDeserializingList, target, contex);
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x000579A0 File Offset: 0x00055BA0
		public void RaiseOnDeserialized(object target, StreamingContext contex)
		{
			SerializationCallbacks.Invoke(this.onDeserializedList, target, contex);
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x000579B0 File Offset: 0x00055BB0
		public static SerializationCallbacks GetSerializationCallbacks(Type t)
		{
			SerializationCallbacks serializationCallbacks = (SerializationCallbacks)SerializationCallbacks.cache[t];
			if (serializationCallbacks != null)
			{
				return serializationCallbacks;
			}
			object obj = SerializationCallbacks.cache_lock;
			SerializationCallbacks result;
			lock (obj)
			{
				serializationCallbacks = (SerializationCallbacks)SerializationCallbacks.cache[t];
				if (serializationCallbacks == null)
				{
					Hashtable hashtable = (Hashtable)SerializationCallbacks.cache.Clone();
					serializationCallbacks = new SerializationCallbacks(t);
					hashtable[t] = serializationCallbacks;
					SerializationCallbacks.cache = hashtable;
				}
				result = serializationCallbacks;
			}
			return result;
		}

		// Token: 0x04000C7B RID: 3195
		private const BindingFlags DefaultBindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x04000C7C RID: 3196
		private readonly ArrayList onSerializingList;

		// Token: 0x04000C7D RID: 3197
		private readonly ArrayList onSerializedList;

		// Token: 0x04000C7E RID: 3198
		private readonly ArrayList onDeserializingList;

		// Token: 0x04000C7F RID: 3199
		private readonly ArrayList onDeserializedList;

		// Token: 0x04000C80 RID: 3200
		private static Hashtable cache = new Hashtable();

		// Token: 0x04000C81 RID: 3201
		private static object cache_lock = new object();

		// Token: 0x02000312 RID: 786
		// (Invoke) Token: 0x06001802 RID: 6146
		public delegate void CallbackHandler(StreamingContext context);
	}
}
