using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x02000085 RID: 133
[Serializable]
public class EventDelegate
{
	// Token: 0x060002BE RID: 702 RVA: 0x00013274 File Offset: 0x00011474
	public EventDelegate()
	{
	}

	// Token: 0x060002BF RID: 703 RVA: 0x0001327C File Offset: 0x0001147C
	public EventDelegate(EventDelegate.Callback call)
	{
		this.Set(call);
	}

	// Token: 0x060002C0 RID: 704 RVA: 0x0001328C File Offset: 0x0001148C
	public EventDelegate(MonoBehaviour target, string methodName)
	{
		this.Set(target, methodName);
	}

	// Token: 0x17000054 RID: 84
	// (get) Token: 0x060002C2 RID: 706 RVA: 0x000132B0 File Offset: 0x000114B0
	// (set) Token: 0x060002C3 RID: 707 RVA: 0x000132B8 File Offset: 0x000114B8
	public MonoBehaviour target
	{
		get
		{
			return this.mTarget;
		}
		set
		{
			this.mTarget = value;
			this.mCachedCallback = null;
			this.mRawDelegate = false;
			this.mCached = false;
			this.mMethod = null;
			this.mParameters = null;
		}
	}

	// Token: 0x17000055 RID: 85
	// (get) Token: 0x060002C4 RID: 708 RVA: 0x000132F0 File Offset: 0x000114F0
	// (set) Token: 0x060002C5 RID: 709 RVA: 0x000132F8 File Offset: 0x000114F8
	public string methodName
	{
		get
		{
			return this.mMethodName;
		}
		set
		{
			this.mMethodName = value;
			this.mCachedCallback = null;
			this.mRawDelegate = false;
			this.mCached = false;
			this.mMethod = null;
			this.mParameters = null;
		}
	}

	// Token: 0x17000056 RID: 86
	// (get) Token: 0x060002C6 RID: 710 RVA: 0x00013330 File Offset: 0x00011530
	public EventDelegate.Parameter[] parameters
	{
		get
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			return this.mParameters;
		}
	}

	// Token: 0x17000057 RID: 87
	// (get) Token: 0x060002C7 RID: 711 RVA: 0x0001334C File Offset: 0x0001154C
	public bool isValid
	{
		get
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			return (this.mRawDelegate && this.mCachedCallback != null) || (this.mTarget != null && !string.IsNullOrEmpty(this.mMethodName));
		}
	}

	// Token: 0x17000058 RID: 88
	// (get) Token: 0x060002C8 RID: 712 RVA: 0x000133A8 File Offset: 0x000115A8
	public bool isEnabled
	{
		get
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			if (this.mRawDelegate && this.mCachedCallback != null)
			{
				return true;
			}
			if (this.mTarget == null)
			{
				return false;
			}
			MonoBehaviour monoBehaviour = this.mTarget;
			return monoBehaviour == null || monoBehaviour.enabled;
		}
	}

	// Token: 0x060002C9 RID: 713 RVA: 0x00013410 File Offset: 0x00011610
	private static string GetMethodName(EventDelegate.Callback callback)
	{
		return callback.Method.Name;
	}

	// Token: 0x060002CA RID: 714 RVA: 0x00013420 File Offset: 0x00011620
	private static bool IsValid(EventDelegate.Callback callback)
	{
		return callback != null && callback.Method != null;
	}

	// Token: 0x060002CB RID: 715 RVA: 0x00013438 File Offset: 0x00011638
	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return !this.isValid;
		}
		if (obj is EventDelegate.Callback)
		{
			EventDelegate.Callback callback = obj as EventDelegate.Callback;
			if (callback.Equals(this.mCachedCallback))
			{
				return true;
			}
			MonoBehaviour monoBehaviour = callback.Target as MonoBehaviour;
			return this.mTarget == monoBehaviour && string.Equals(this.mMethodName, EventDelegate.GetMethodName(callback));
		}
		else
		{
			if (obj is EventDelegate)
			{
				EventDelegate eventDelegate = obj as EventDelegate;
				return this.mTarget == eventDelegate.mTarget && string.Equals(this.mMethodName, eventDelegate.mMethodName);
			}
			return false;
		}
	}

	// Token: 0x060002CC RID: 716 RVA: 0x000134EC File Offset: 0x000116EC
	public override int GetHashCode()
	{
		return EventDelegate.s_Hash;
	}

	// Token: 0x060002CD RID: 717 RVA: 0x000134F4 File Offset: 0x000116F4
	private void Set(EventDelegate.Callback call)
	{
		this.Clear();
		if (call != null && EventDelegate.IsValid(call))
		{
			this.mTarget = (call.Target as MonoBehaviour);
			if (this.mTarget == null)
			{
				this.mRawDelegate = true;
				this.mCachedCallback = call;
				this.mMethodName = null;
			}
			else
			{
				this.mMethodName = EventDelegate.GetMethodName(call);
				this.mRawDelegate = false;
			}
		}
	}

	// Token: 0x060002CE RID: 718 RVA: 0x00013568 File Offset: 0x00011768
	public void Set(MonoBehaviour target, string methodName)
	{
		this.Clear();
		this.mTarget = target;
		this.mMethodName = methodName;
	}

	// Token: 0x060002CF RID: 719 RVA: 0x00013580 File Offset: 0x00011780
	private void Cache()
	{
		this.mCached = true;
		if (this.mRawDelegate)
		{
			return;
		}
		if ((this.mCachedCallback == null || this.mCachedCallback.Target as MonoBehaviour != this.mTarget || EventDelegate.GetMethodName(this.mCachedCallback) != this.mMethodName) && this.mTarget != null && !string.IsNullOrEmpty(this.mMethodName))
		{
			Type type = this.mTarget.GetType();
			try
			{
				this.mMethod = type.GetMethod(this.mMethodName, 52);
			}
			catch (Exception ex)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"Failed to bind ",
					type,
					".",
					this.mMethodName,
					"\n",
					ex.Message
				}));
				return;
			}
			if (this.mMethod == null)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"Could not find method '",
					this.mMethodName,
					"' on ",
					this.mTarget.GetType()
				}), this.mTarget);
				return;
			}
			if (this.mMethod.ReturnType != typeof(void))
			{
				Debug.LogError(string.Concat(new object[]
				{
					this.mTarget.GetType(),
					".",
					this.mMethodName,
					" must have a 'void' return type."
				}), this.mTarget);
				return;
			}
			ParameterInfo[] parameters = this.mMethod.GetParameters();
			if (parameters.Length == 0)
			{
				this.mCachedCallback = (EventDelegate.Callback)Delegate.CreateDelegate(typeof(EventDelegate.Callback), this.mTarget, this.mMethodName);
				this.mArgs = null;
				this.mParameters = null;
				return;
			}
			this.mCachedCallback = null;
			if (this.mParameters == null || this.mParameters.Length != parameters.Length)
			{
				this.mParameters = new EventDelegate.Parameter[parameters.Length];
				int i = 0;
				int num = this.mParameters.Length;
				while (i < num)
				{
					this.mParameters[i] = new EventDelegate.Parameter();
					i++;
				}
			}
			int j = 0;
			int num2 = this.mParameters.Length;
			while (j < num2)
			{
				this.mParameters[j].expectedType = parameters[j].ParameterType;
				j++;
			}
		}
	}

	// Token: 0x060002D0 RID: 720 RVA: 0x00013810 File Offset: 0x00011A10
	public bool Execute()
	{
		if (!this.mCached)
		{
			this.Cache();
		}
		if (this.mCachedCallback != null)
		{
			this.mCachedCallback();
			return true;
		}
		if (this.mMethod != null)
		{
			if (this.mParameters == null || this.mParameters.Length == 0)
			{
				this.mMethod.Invoke(this.mTarget, null);
			}
			else
			{
				if (this.mArgs == null || this.mArgs.Length != this.mParameters.Length)
				{
					this.mArgs = new object[this.mParameters.Length];
				}
				int i = 0;
				int num = this.mParameters.Length;
				while (i < num)
				{
					this.mArgs[i] = this.mParameters[i].value;
					i++;
				}
				try
				{
					this.mMethod.Invoke(this.mTarget, this.mArgs);
				}
				catch (ArgumentException ex)
				{
					string text = ex.Message;
					text += "\nExpected: ";
					ParameterInfo[] parameters = this.mMethod.GetParameters();
					if (parameters.Length == 0)
					{
						text += "no arguments";
					}
					else
					{
						text += parameters[0];
						for (int j = 1; j < parameters.Length; j++)
						{
							text = text + ", " + parameters[j].ParameterType;
						}
					}
					text += "\nGot: ";
					if (this.mParameters.Length == 0)
					{
						text += "no arguments";
					}
					else
					{
						text += this.mParameters[0].type;
						for (int k = 1; k < this.mParameters.Length; k++)
						{
							text = text + ", " + this.mParameters[k].type;
						}
					}
					text += "\n";
					Debug.LogError(text);
				}
				int l = 0;
				int num2 = this.mArgs.Length;
				while (l < num2)
				{
					this.mArgs[l] = null;
					l++;
				}
			}
			return true;
		}
		return false;
	}

	// Token: 0x060002D1 RID: 721 RVA: 0x00013A68 File Offset: 0x00011C68
	public void Clear()
	{
		this.mTarget = null;
		this.mMethodName = null;
		this.mRawDelegate = false;
		this.mCachedCallback = null;
		this.mParameters = null;
		this.mCached = false;
		this.mMethod = null;
		this.mArgs = null;
	}

	// Token: 0x060002D2 RID: 722 RVA: 0x00013AB0 File Offset: 0x00011CB0
	public override string ToString()
	{
		if (!(this.mTarget != null))
		{
			return (!this.mRawDelegate) ? null : "[delegate]";
		}
		string text = this.mTarget.GetType().ToString();
		int num = text.LastIndexOf('.');
		if (num > 0)
		{
			text = text.Substring(num + 1);
		}
		if (!string.IsNullOrEmpty(this.methodName))
		{
			return text + "." + this.methodName;
		}
		return text + ".[delegate]";
	}

	// Token: 0x060002D3 RID: 723 RVA: 0x00013B40 File Offset: 0x00011D40
	public static void Execute(List<EventDelegate> list)
	{
		if (list != null)
		{
			for (int i = 0; i < list.Count; i++)
			{
				EventDelegate eventDelegate = list[i];
				if (eventDelegate != null)
				{
					eventDelegate.Execute();
					if (i >= list.Count)
					{
						break;
					}
					if (list[i] != eventDelegate)
					{
						continue;
					}
					if (eventDelegate.oneShot)
					{
						list.RemoveAt(i);
						continue;
					}
				}
			}
		}
	}

	// Token: 0x060002D4 RID: 724 RVA: 0x00013BBC File Offset: 0x00011DBC
	public static bool IsValid(List<EventDelegate> list)
	{
		if (list != null)
		{
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				EventDelegate eventDelegate = list[i];
				if (eventDelegate != null && eventDelegate.isValid)
				{
					return true;
				}
				i++;
			}
		}
		return false;
	}

	// Token: 0x060002D5 RID: 725 RVA: 0x00013C04 File Offset: 0x00011E04
	public static void Set(List<EventDelegate> list, EventDelegate.Callback callback)
	{
		if (list != null)
		{
			list.Clear();
			list.Add(new EventDelegate(callback));
		}
	}

	// Token: 0x060002D6 RID: 726 RVA: 0x00013C20 File Offset: 0x00011E20
	public static void Set(List<EventDelegate> list, EventDelegate del)
	{
		if (list != null)
		{
			list.Clear();
			list.Add(del);
		}
	}

	// Token: 0x060002D7 RID: 727 RVA: 0x00013C38 File Offset: 0x00011E38
	public static void Add(List<EventDelegate> list, EventDelegate.Callback callback)
	{
		EventDelegate.Add(list, callback, false);
	}

	// Token: 0x060002D8 RID: 728 RVA: 0x00013C44 File Offset: 0x00011E44
	public static void Add(List<EventDelegate> list, EventDelegate.Callback callback, bool oneShot)
	{
		if (list != null)
		{
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				EventDelegate eventDelegate = list[i];
				if (eventDelegate != null && eventDelegate.Equals(callback))
				{
					return;
				}
				i++;
			}
			list.Add(new EventDelegate(callback)
			{
				oneShot = oneShot
			});
		}
		else
		{
			Debug.LogWarning("Attempting to add a callback to a list that's null");
		}
	}

	// Token: 0x060002D9 RID: 729 RVA: 0x00013CB0 File Offset: 0x00011EB0
	public static void Add(List<EventDelegate> list, EventDelegate ev)
	{
		EventDelegate.Add(list, ev, ev.oneShot);
	}

	// Token: 0x060002DA RID: 730 RVA: 0x00013CC0 File Offset: 0x00011EC0
	public static void Add(List<EventDelegate> list, EventDelegate ev, bool oneShot)
	{
		if (ev.mRawDelegate || ev.target == null || string.IsNullOrEmpty(ev.methodName))
		{
			EventDelegate.Add(list, ev.mCachedCallback, oneShot);
		}
		else if (list != null)
		{
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				EventDelegate eventDelegate = list[i];
				if (eventDelegate != null && eventDelegate.Equals(ev))
				{
					return;
				}
				i++;
			}
			EventDelegate eventDelegate2 = new EventDelegate(ev.target, ev.methodName);
			eventDelegate2.oneShot = oneShot;
			if (ev.mParameters != null && ev.mParameters.Length > 0)
			{
				eventDelegate2.mParameters = new EventDelegate.Parameter[ev.mParameters.Length];
				for (int j = 0; j < ev.mParameters.Length; j++)
				{
					eventDelegate2.mParameters[j] = ev.mParameters[j];
				}
			}
			list.Add(eventDelegate2);
		}
		else
		{
			Debug.LogWarning("Attempting to add a callback to a list that's null");
		}
	}

	// Token: 0x060002DB RID: 731 RVA: 0x00013DD0 File Offset: 0x00011FD0
	public static bool Remove(List<EventDelegate> list, EventDelegate.Callback callback)
	{
		if (list != null)
		{
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				EventDelegate eventDelegate = list[i];
				if (eventDelegate != null && eventDelegate.Equals(callback))
				{
					list.RemoveAt(i);
					return true;
				}
				i++;
			}
		}
		return false;
	}

	// Token: 0x060002DC RID: 732 RVA: 0x00013E20 File Offset: 0x00012020
	public static bool Remove(List<EventDelegate> list, EventDelegate ev)
	{
		if (list != null)
		{
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				EventDelegate eventDelegate = list[i];
				if (eventDelegate != null && eventDelegate.Equals(ev))
				{
					list.RemoveAt(i);
					return true;
				}
				i++;
			}
		}
		return false;
	}

	// Token: 0x04000309 RID: 777
	[SerializeField]
	private MonoBehaviour mTarget;

	// Token: 0x0400030A RID: 778
	[SerializeField]
	private string mMethodName;

	// Token: 0x0400030B RID: 779
	[SerializeField]
	private EventDelegate.Parameter[] mParameters;

	// Token: 0x0400030C RID: 780
	public bool oneShot;

	// Token: 0x0400030D RID: 781
	private EventDelegate.Callback mCachedCallback;

	// Token: 0x0400030E RID: 782
	private bool mRawDelegate;

	// Token: 0x0400030F RID: 783
	private bool mCached;

	// Token: 0x04000310 RID: 784
	private MethodInfo mMethod;

	// Token: 0x04000311 RID: 785
	private object[] mArgs;

	// Token: 0x04000312 RID: 786
	private static int s_Hash = "EventDelegate".GetHashCode();

	// Token: 0x02000086 RID: 134
	[Serializable]
	public class Parameter
	{
		// Token: 0x060002DD RID: 733 RVA: 0x00013E70 File Offset: 0x00012070
		public Parameter()
		{
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00013E88 File Offset: 0x00012088
		public Parameter(Object obj, string field)
		{
			this.obj = obj;
			this.field = field;
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060002DF RID: 735 RVA: 0x00013EBC File Offset: 0x000120BC
		public object value
		{
			get
			{
				if (!this.cached)
				{
					this.cached = true;
					this.fieldInfo = null;
					this.propInfo = null;
					if (this.obj != null && !string.IsNullOrEmpty(this.field))
					{
						Type type = this.obj.GetType();
						this.propInfo = type.GetProperty(this.field);
						if (this.propInfo == null)
						{
							this.fieldInfo = type.GetField(this.field);
						}
					}
				}
				if (this.propInfo != null)
				{
					return this.propInfo.GetValue(this.obj, null);
				}
				if (this.fieldInfo != null)
				{
					return this.fieldInfo.GetValue(this.obj);
				}
				return this.obj;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x00013F88 File Offset: 0x00012188
		public Type type
		{
			get
			{
				if (this.obj == null)
				{
					return typeof(void);
				}
				return this.obj.GetType();
			}
		}

		// Token: 0x04000313 RID: 787
		public Object obj;

		// Token: 0x04000314 RID: 788
		public string field;

		// Token: 0x04000315 RID: 789
		[NonSerialized]
		public Type expectedType = typeof(void);

		// Token: 0x04000316 RID: 790
		[NonSerialized]
		public bool cached;

		// Token: 0x04000317 RID: 791
		[NonSerialized]
		public PropertyInfo propInfo;

		// Token: 0x04000318 RID: 792
		[NonSerialized]
		public FieldInfo fieldInfo;
	}

	// Token: 0x02000A9B RID: 2715
	// (Invoke) Token: 0x06004EF5 RID: 20213
	public delegate void Callback();
}
