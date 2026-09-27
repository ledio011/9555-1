using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x02000069 RID: 105
	public sealed class GameObject : Object
	{
		// Token: 0x0600045C RID: 1116 RVA: 0x00009F3C File Offset: 0x0000813C
		public GameObject(string name)
		{
			GameObject.Internal_CreateGameObject(this, name);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00009F4C File Offset: 0x0000814C
		public GameObject()
		{
			GameObject.Internal_CreateGameObject(this, null);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00009F5C File Offset: 0x0000815C
		public GameObject(string name, params Type[] components)
		{
			GameObject.Internal_CreateGameObject(this, name);
			foreach (Type componentType in components)
			{
				this.AddComponent(componentType);
			}
		}

		// Token: 0x0600045F RID: 1119
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SampleAnimation(AnimationClip animation, float time);

		// Token: 0x06000460 RID: 1120
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern GameObject CreatePrimitive(PrimitiveType type);

		// Token: 0x06000461 RID: 1121
		[WrapperlessIcall]
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		[MethodImpl(4096)]
		public extern Component GetComponent(Type type);

		// Token: 0x06000462 RID: 1122 RVA: 0x00009F98 File Offset: 0x00008198
		public T GetComponent<T>() where T : Component
		{
			return this.GetComponent(typeof(T)) as T;
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00009FB4 File Offset: 0x000081B4
		public Component GetComponent(string type)
		{
			return this.GetComponentByName(type);
		}

		// Token: 0x06000464 RID: 1124
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern Component GetComponentByName(string type);

		// Token: 0x06000465 RID: 1125 RVA: 0x00009FC0 File Offset: 0x000081C0
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponentInChildren(Type type)
		{
			if (this.activeInHierarchy)
			{
				Component component = this.GetComponent(type);
				if (component != null)
				{
					return component;
				}
			}
			Transform transform = this.transform;
			if (transform != null)
			{
				foreach (object obj in transform)
				{
					Transform transform2 = (Transform)obj;
					Component componentInChildren = transform2.gameObject.GetComponentInChildren(type);
					if (componentInChildren != null)
					{
						return componentInChildren;
					}
				}
			}
			return null;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0000A078 File Offset: 0x00008278
		public T GetComponentInChildren<T>() where T : Component
		{
			return this.GetComponentInChildren(typeof(T)) as T;
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0000A094 File Offset: 0x00008294
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponentInParent(Type type)
		{
			if (this.activeInHierarchy)
			{
				Component component = this.GetComponent(type);
				if (component != null)
				{
					return component;
				}
			}
			Transform parent = this.transform.parent;
			if (parent != null)
			{
				while (parent != null)
				{
					if (parent.gameObject.activeInHierarchy)
					{
						Component component2 = parent.gameObject.GetComponent(type);
						if (component2 != null)
						{
							return component2;
						}
					}
					parent = parent.parent;
				}
			}
			return null;
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0000A120 File Offset: 0x00008320
		public T GetComponentInParent<T>() where T : Component
		{
			return this.GetComponentInParent(typeof(T)) as T;
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0000A13C File Offset: 0x0000833C
		public void GetComponentsInParent<T>(bool includeInactive, List<T> results) where T : Component
		{
			this.GetComponentsForListInternal(typeof(T), typeof(T), true, includeInactive, true, results);
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600046A RID: 1130
		// (set) Token: 0x0600046B RID: 1131
		public extern bool isStatic { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600046C RID: 1132
		internal extern bool isStaticBatchable { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x0600046D RID: 1133 RVA: 0x0000A15C File Offset: 0x0000835C
		[CanConvertToFlash]
		public Component[] GetComponents(Type type)
		{
			return this.GetComponentsInternal(type, false, false, true, false);
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0000A16C File Offset: 0x0000836C
		public T[] GetComponents<T>() where T : Component
		{
			return (T[])this.GetComponentsInternal(typeof(T), true, false, true, false);
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0000A188 File Offset: 0x00008388
		public void GetComponents(Type type, List<Component> results)
		{
			this.GetComponentsForListInternal(type, typeof(Component), false, true, false, results);
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0000A1A0 File Offset: 0x000083A0
		public void GetComponents<T>(List<T> results) where T : Component
		{
			this.GetComponentsForListInternal(typeof(T), typeof(T), false, true, false, results);
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000A1C0 File Offset: 0x000083C0
		[ExcludeFromDocs]
		public Component[] GetComponentsInChildren(Type type)
		{
			bool includeInactive = false;
			return this.GetComponentsInChildren(type, includeInactive);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0000A1D8 File Offset: 0x000083D8
		public Component[] GetComponentsInChildren(Type type, [DefaultValue("false")] bool includeInactive)
		{
			return this.GetComponentsInternal(type, false, true, includeInactive, false);
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0000A1E8 File Offset: 0x000083E8
		public T[] GetComponentsInChildren<T>(bool includeInactive) where T : Component
		{
			return (T[])this.GetComponentsInternal(typeof(T), true, true, includeInactive, false);
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0000A204 File Offset: 0x00008404
		public void GetComponentsInChildren<T>(bool includeInactive, List<T> results) where T : Component
		{
			this.GetComponentsForListInternal(typeof(T), typeof(T), true, includeInactive, false, results);
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000A224 File Offset: 0x00008424
		public T[] GetComponentsInChildren<T>() where T : Component
		{
			return this.GetComponentsInChildren<T>(false);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0000A230 File Offset: 0x00008430
		public void GetComponentsInChildren<T>(List<T> results) where T : Component
		{
			this.GetComponentsInChildren<T>(false, results);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0000A23C File Offset: 0x0000843C
		[ExcludeFromDocs]
		public Component[] GetComponentsInParent(Type type)
		{
			bool includeInactive = false;
			return this.GetComponentsInParent(type, includeInactive);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000A254 File Offset: 0x00008454
		public Component[] GetComponentsInParent(Type type, [DefaultValue("false")] bool includeInactive)
		{
			return this.GetComponentsInternal(type, false, true, includeInactive, true);
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0000A264 File Offset: 0x00008464
		public T[] GetComponentsInParent<T>(bool includeInactive) where T : Component
		{
			return (T[])this.GetComponentsInternal(typeof(T), true, true, includeInactive, true);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0000A280 File Offset: 0x00008480
		public T[] GetComponentsInParent<T>() where T : Component
		{
			return this.GetComponentsInParent<T>(false);
		}

		// Token: 0x0600047B RID: 1147
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void GetComponentsForListInternal(Type searchType, Type listElementType, bool recursive, bool includeInactive, bool reverse, object resultList);

		// Token: 0x0600047C RID: 1148
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern Component[] GetComponentsInternal(Type type, bool isGenericTypeArray, bool recursive, bool includeInactive, bool reverse);

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600047D RID: 1149
		public extern Transform transform { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600047E RID: 1150
		public extern Rigidbody rigidbody { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600047F RID: 1151
		public extern Rigidbody2D rigidbody2D { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000480 RID: 1152
		public extern Camera camera { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000481 RID: 1153
		public extern Light light { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000482 RID: 1154
		public extern Animation animation { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000483 RID: 1155
		public extern ConstantForce constantForce { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000484 RID: 1156
		public extern Renderer renderer { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000485 RID: 1157
		public extern AudioSource audio { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000486 RID: 1158
		public extern GUIText guiText { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000487 RID: 1159
		public extern NetworkView networkView { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000488 RID: 1160
		[Obsolete("Please use guiTexture instead")]
		public extern GUIElement guiElement { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000489 RID: 1161
		public extern GUITexture guiTexture { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600048A RID: 1162
		public extern Collider collider { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600048B RID: 1163
		public extern Collider2D collider2D { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600048C RID: 1164
		public extern HingeJoint hingeJoint { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600048D RID: 1165
		public extern ParticleEmitter particleEmitter { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600048E RID: 1166
		public extern ParticleSystem particleSystem { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600048F RID: 1167
		// (set) Token: 0x06000490 RID: 1168
		public extern int layer { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000491 RID: 1169
		// (set) Token: 0x06000492 RID: 1170
		[Obsolete("GameObject.active is obsolete. Use GameObject.SetActive(), GameObject.activeSelf or GameObject.activeInHierarchy.")]
		public extern bool active { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x06000493 RID: 1171
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SetActive(bool value);

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000494 RID: 1172
		public extern bool activeSelf { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000495 RID: 1173
		public extern bool activeInHierarchy { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x06000496 RID: 1174
		[WrapperlessIcall]
		[Obsolete("gameObject.SetActiveRecursively() is obsolete. Use GameObject.SetActive(), which is now inherited by children.")]
		[MethodImpl(4096)]
		public extern void SetActiveRecursively(bool state);

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000497 RID: 1175
		// (set) Token: 0x06000498 RID: 1176
		public extern string tag { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x06000499 RID: 1177
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern bool CompareTag(string tag);

		// Token: 0x0600049A RID: 1178
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern GameObject FindGameObjectWithTag(string tag);

		// Token: 0x0600049B RID: 1179 RVA: 0x0000A28C File Offset: 0x0000848C
		public static GameObject FindWithTag(string tag)
		{
			return GameObject.FindGameObjectWithTag(tag);
		}

		// Token: 0x0600049C RID: 1180
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern GameObject[] FindGameObjectsWithTag(string tag);

		// Token: 0x0600049D RID: 1181
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SendMessageUpwards(string methodName, [DefaultValue("null")] object value, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		// Token: 0x0600049E RID: 1182 RVA: 0x0000A294 File Offset: 0x00008494
		[ExcludeFromDocs]
		public void SendMessageUpwards(string methodName, object value)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			this.SendMessageUpwards(methodName, value, options);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0000A2AC File Offset: 0x000084AC
		[ExcludeFromDocs]
		public void SendMessageUpwards(string methodName)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			object value = null;
			this.SendMessageUpwards(methodName, value, options);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0000A2C8 File Offset: 0x000084C8
		public void SendMessageUpwards(string methodName, SendMessageOptions options)
		{
			this.SendMessageUpwards(methodName, null, options);
		}

		// Token: 0x060004A1 RID: 1185
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SendMessage(string methodName, [DefaultValue("null")] object value, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		// Token: 0x060004A2 RID: 1186 RVA: 0x0000A2D4 File Offset: 0x000084D4
		[ExcludeFromDocs]
		public void SendMessage(string methodName, object value)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			this.SendMessage(methodName, value, options);
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0000A2EC File Offset: 0x000084EC
		[ExcludeFromDocs]
		public void SendMessage(string methodName)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			object value = null;
			this.SendMessage(methodName, value, options);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0000A308 File Offset: 0x00008508
		public void SendMessage(string methodName, SendMessageOptions options)
		{
			this.SendMessage(methodName, null, options);
		}

		// Token: 0x060004A5 RID: 1189
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void BroadcastMessage(string methodName, [DefaultValue("null")] object parameter, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		// Token: 0x060004A6 RID: 1190 RVA: 0x0000A314 File Offset: 0x00008514
		[ExcludeFromDocs]
		public void BroadcastMessage(string methodName, object parameter)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			this.BroadcastMessage(methodName, parameter, options);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0000A32C File Offset: 0x0000852C
		[ExcludeFromDocs]
		public void BroadcastMessage(string methodName)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			object parameter = null;
			this.BroadcastMessage(methodName, parameter, options);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0000A348 File Offset: 0x00008548
		public void BroadcastMessage(string methodName, SendMessageOptions options)
		{
			this.BroadcastMessage(methodName, null, options);
		}

		// Token: 0x060004A9 RID: 1193
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern Component AddComponent(string className);

		// Token: 0x060004AA RID: 1194 RVA: 0x0000A354 File Offset: 0x00008554
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component AddComponent(Type componentType)
		{
			return this.Internal_AddComponentWithType(componentType);
		}

		// Token: 0x060004AB RID: 1195
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern Component Internal_AddComponentWithType(Type componentType);

		// Token: 0x060004AC RID: 1196 RVA: 0x0000A360 File Offset: 0x00008560
		public T AddComponent<T>() where T : Component
		{
			return this.AddComponent(typeof(T)) as T;
		}

		// Token: 0x060004AD RID: 1197
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_CreateGameObject([Writable] GameObject mono, string name);

		// Token: 0x060004AE RID: 1198
		[Obsolete("gameObject.PlayAnimation is not supported anymore. Use animation.Play")]
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void PlayAnimation(AnimationClip animation);

		// Token: 0x060004AF RID: 1199
		[WrapperlessIcall]
		[Obsolete("gameObject.StopAnimation is not supported anymore. Use animation.Stop")]
		[MethodImpl(4096)]
		public extern void StopAnimation();

		// Token: 0x060004B0 RID: 1200
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern GameObject Find(string name);

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x0000A37C File Offset: 0x0000857C
		public GameObject gameObject
		{
			get
			{
				return this;
			}
		}
	}
}
