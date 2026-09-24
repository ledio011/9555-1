using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x02000040 RID: 64
	public class Component : Object
	{
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000361 RID: 865 RVA: 0x00007F8C File Offset: 0x0000618C
		public Transform transform
		{
			get
			{
				return this.InternalGetTransform();
			}
		}

		// Token: 0x06000362 RID: 866
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal extern Transform InternalGetTransform();

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000363 RID: 867
		public extern Rigidbody rigidbody { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000364 RID: 868
		public extern Camera camera { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000365 RID: 869
		public extern Light light { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000366 RID: 870
		public extern Animation animation { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000367 RID: 871
		public extern Renderer renderer { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000368 RID: 872
		public extern AudioSource audio { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000369 RID: 873
		public extern GUIText guiText { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600036A RID: 874
		public extern GUITexture guiTexture { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600036B RID: 875
		public extern Collider collider { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600036C RID: 876
		public extern ParticleEmitter particleEmitter { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600036D RID: 877 RVA: 0x00007F94 File Offset: 0x00006194
		public GameObject gameObject
		{
			get
			{
				return this.InternalGetGameObject();
			}
		}

		// Token: 0x0600036E RID: 878
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal extern GameObject InternalGetGameObject();

		// Token: 0x0600036F RID: 879
		[WrapperlessIcall]
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		[MethodImpl(4096)]
		public extern Component GetComponent(Type type);

		// Token: 0x06000370 RID: 880 RVA: 0x00007F9C File Offset: 0x0000619C
		public T GetComponent<T>() where T : Component
		{
			return this.GetComponent(typeof(T)) as T;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00007FB8 File Offset: 0x000061B8
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponentInChildren(Type t)
		{
			return this.gameObject.GetComponentInChildren(t);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00007FC8 File Offset: 0x000061C8
		public T GetComponentInChildren<T>() where T : Component
		{
			return (T)((object)this.GetComponentInChildren(typeof(T)));
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00007FE0 File Offset: 0x000061E0
		public T[] GetComponentsInChildren<T>(bool includeInactive) where T : Component
		{
			return this.gameObject.GetComponentsInChildren<T>(includeInactive);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00007FF0 File Offset: 0x000061F0
		public void GetComponentsInChildren<T>(bool includeInactive, List<T> result) where T : Component
		{
			this.gameObject.GetComponentsInChildren<T>(includeInactive, result);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00008000 File Offset: 0x00006200
		public T[] GetComponentsInChildren<T>() where T : Component
		{
			return this.GetComponentsInChildren<T>(false);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000800C File Offset: 0x0000620C
		public void GetComponentsInChildren<T>(List<T> results) where T : Component
		{
			this.GetComponentsInChildren<T>(false, results);
		}

		// Token: 0x06000377 RID: 887
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern Component[] GetComponents(Type type);

		// Token: 0x06000378 RID: 888
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern Component[] GetComponentsWithCorrectReturnType(Type type);

		// Token: 0x06000379 RID: 889
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void GetComponentsForListInternal(Type searchType, Type listElementType, bool recursive, bool includeInactive, object resultList);

		// Token: 0x0600037A RID: 890 RVA: 0x00008018 File Offset: 0x00006218
		public T[] GetComponents<T>() where T : Component
		{
			return (T[])this.GetComponentsWithCorrectReturnType(typeof(T));
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00008030 File Offset: 0x00006230
		public void GetComponents(Type type, List<Component> results)
		{
			this.GetComponentsForListInternal(type, typeof(Component), false, true, results);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00008048 File Offset: 0x00006248
		public void GetComponents<T>(List<T> results) where T : Component
		{
			this.GetComponentsForListInternal(typeof(T), typeof(T), false, true, results);
		}

		// Token: 0x0600037D RID: 893
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern bool CompareTag(string tag);

		// Token: 0x0600037E RID: 894
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SendMessage(string methodName, [DefaultValue("null")] object value, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		// Token: 0x0600037F RID: 895 RVA: 0x00008068 File Offset: 0x00006268
		[ExcludeFromDocs]
		public void SendMessage(string methodName)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			object value = null;
			this.SendMessage(methodName, value, options);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00008084 File Offset: 0x00006284
		public void SendMessage(string methodName, SendMessageOptions options)
		{
			this.SendMessage(methodName, null, options);
		}

		// Token: 0x06000381 RID: 897
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void BroadcastMessage(string methodName, [DefaultValue("null")] object parameter, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		// Token: 0x06000382 RID: 898 RVA: 0x00008090 File Offset: 0x00006290
		public void BroadcastMessage(string methodName, SendMessageOptions options)
		{
			this.BroadcastMessage(methodName, null, options);
		}
	}
}
