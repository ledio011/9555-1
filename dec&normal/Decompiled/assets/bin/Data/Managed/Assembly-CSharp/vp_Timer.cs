using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A90 RID: 2704
public class vp_Timer : MonoBehaviour
{
	// Token: 0x17000FE6 RID: 4070
	// (get) Token: 0x06004EA8 RID: 20136 RVA: 0x001AF2C0 File Offset: 0x001AD4C0
	public bool WasAddedCorrectly
	{
		get
		{
			return Application.isPlaying && !(base.gameObject != vp_Timer.m_MainObject);
		}
	}

	// Token: 0x06004EA9 RID: 20137 RVA: 0x001AF2F4 File Offset: 0x001AD4F4
	private void Awake()
	{
		if (!this.WasAddedCorrectly)
		{
			Object.Destroy(this);
			return;
		}
	}

	// Token: 0x06004EAA RID: 20138 RVA: 0x001AF308 File Offset: 0x001AD508
	private void Update()
	{
		vp_Timer.m_EventBatch = 0;
		while (vp_Timer.m_Active.Count > 0 && vp_Timer.m_EventBatch < vp_Timer.MaxEventsPerFrame)
		{
			if (vp_Timer.m_EventIterator < 0)
			{
				vp_Timer.m_EventIterator = vp_Timer.m_Active.Count - 1;
				break;
			}
			if (vp_Timer.m_EventIterator > vp_Timer.m_Active.Count - 1)
			{
				vp_Timer.m_EventIterator = vp_Timer.m_Active.Count - 1;
			}
			if (Time.time >= vp_Timer.m_Active[vp_Timer.m_EventIterator].DueTime || vp_Timer.m_Active[vp_Timer.m_EventIterator].Id == 0)
			{
				vp_Timer.m_Active[vp_Timer.m_EventIterator].Execute();
			}
			else if (vp_Timer.m_Active[vp_Timer.m_EventIterator].Paused)
			{
				vp_Timer.m_Active[vp_Timer.m_EventIterator].DueTime += Time.deltaTime;
			}
			else
			{
				vp_Timer.m_Active[vp_Timer.m_EventIterator].LifeTime += Time.deltaTime;
			}
			vp_Timer.m_EventIterator--;
			vp_Timer.m_EventBatch++;
		}
	}

	// Token: 0x06004EAB RID: 20139 RVA: 0x001AF450 File Offset: 0x001AD650
	public static void In(float delay, vp_Timer.Callback callback, vp_Timer.Handle timerHandle = null)
	{
		vp_Timer.Schedule(delay, callback, null, null, timerHandle, 1, -1f);
	}

	// Token: 0x06004EAC RID: 20140 RVA: 0x001AF464 File Offset: 0x001AD664
	public static void In(float delay, vp_Timer.Callback callback, int iterations, vp_Timer.Handle timerHandle = null)
	{
		vp_Timer.Schedule(delay, callback, null, null, timerHandle, iterations, -1f);
	}

	// Token: 0x06004EAD RID: 20141 RVA: 0x001AF478 File Offset: 0x001AD678
	public static void In(float delay, vp_Timer.Callback callback, int iterations, float interval, vp_Timer.Handle timerHandle = null)
	{
		vp_Timer.Schedule(delay, callback, null, null, timerHandle, iterations, interval);
	}

	// Token: 0x06004EAE RID: 20142 RVA: 0x001AF488 File Offset: 0x001AD688
	public static void In(float delay, vp_Timer.ArgCallback callback, object arguments, vp_Timer.Handle timerHandle = null)
	{
		vp_Timer.Schedule(delay, null, callback, arguments, timerHandle, 1, -1f);
	}

	// Token: 0x06004EAF RID: 20143 RVA: 0x001AF49C File Offset: 0x001AD69C
	public static void In(float delay, vp_Timer.ArgCallback callback, object arguments, int iterations, vp_Timer.Handle timerHandle = null)
	{
		vp_Timer.Schedule(delay, null, callback, arguments, timerHandle, iterations, -1f);
	}

	// Token: 0x06004EB0 RID: 20144 RVA: 0x001AF4B0 File Offset: 0x001AD6B0
	public static void In(float delay, vp_Timer.ArgCallback callback, object arguments, int iterations, float interval, vp_Timer.Handle timerHandle = null)
	{
		vp_Timer.Schedule(delay, null, callback, arguments, timerHandle, iterations, interval);
	}

	// Token: 0x06004EB1 RID: 20145 RVA: 0x001AF4C0 File Offset: 0x001AD6C0
	public static void Start(vp_Timer.Handle timerHandle)
	{
		vp_Timer.Schedule(315360000f, delegate
		{
		}, null, null, timerHandle, 1, -1f);
	}

	// Token: 0x06004EB2 RID: 20146 RVA: 0x001AF500 File Offset: 0x001AD700
	private static void Schedule(float time, vp_Timer.Callback func, vp_Timer.ArgCallback argFunc, object args, vp_Timer.Handle timerHandle, int iterations, float interval)
	{
		if (func == null && argFunc == null)
		{
			Debug.LogError("Error: (vp_Timer) Aborted event because function is null.");
			return;
		}
		if (vp_Timer.m_MainObject == null)
		{
			vp_Timer.m_MainObject = new GameObject("Timers");
			vp_Timer.m_MainObject.AddComponent<vp_Timer>();
			Object.DontDestroyOnLoad(vp_Timer.m_MainObject);
		}
		time = Mathf.Max(0f, time);
		iterations = Mathf.Max(0, iterations);
		interval = ((interval != -1f) ? Mathf.Max(0f, interval) : time);
		vp_Timer.m_NewEvent = null;
		if (vp_Timer.m_Pool.Count > 0)
		{
			vp_Timer.m_NewEvent = vp_Timer.m_Pool[0];
			vp_Timer.m_Pool.Remove(vp_Timer.m_NewEvent);
		}
		else
		{
			vp_Timer.m_NewEvent = new vp_Timer.Event();
		}
		vp_Timer.m_EventCount++;
		vp_Timer.m_NewEvent.Id = vp_Timer.m_EventCount;
		if (func != null)
		{
			vp_Timer.m_NewEvent.Function = func;
		}
		else if (argFunc != null)
		{
			vp_Timer.m_NewEvent.ArgFunction = argFunc;
			vp_Timer.m_NewEvent.Arguments = args;
		}
		vp_Timer.m_NewEvent.StartTime = Time.time;
		vp_Timer.m_NewEvent.DueTime = Time.time + time;
		vp_Timer.m_NewEvent.Iterations = iterations;
		vp_Timer.m_NewEvent.Interval = interval;
		vp_Timer.m_NewEvent.LifeTime = 0f;
		vp_Timer.m_NewEvent.Paused = false;
		vp_Timer.m_Active.Add(vp_Timer.m_NewEvent);
		if (timerHandle != null)
		{
			if (timerHandle.Active)
			{
				timerHandle.Cancel();
			}
			timerHandle.Id = vp_Timer.m_NewEvent.Id;
		}
	}

	// Token: 0x06004EB3 RID: 20147 RVA: 0x001AF6B0 File Offset: 0x001AD8B0
	private static void Cancel(vp_Timer.Handle handle)
	{
		if (handle == null)
		{
			return;
		}
		if (handle.Active)
		{
			handle.Id = 0;
			return;
		}
	}

	// Token: 0x06004EB4 RID: 20148 RVA: 0x001AF6CC File Offset: 0x001AD8CC
	public static void CancelAll()
	{
		for (int i = vp_Timer.m_Active.Count - 1; i > -1; i--)
		{
			vp_Timer.m_Active[i].Id = 0;
		}
	}

	// Token: 0x06004EB5 RID: 20149 RVA: 0x001AF708 File Offset: 0x001AD908
	public static void CancelAll(string methodName)
	{
		for (int i = vp_Timer.m_Active.Count - 1; i > -1; i--)
		{
			if (vp_Timer.m_Active[i].MethodName == methodName)
			{
				vp_Timer.m_Active[i].Id = 0;
			}
		}
	}

	// Token: 0x06004EB6 RID: 20150 RVA: 0x001AF760 File Offset: 0x001AD960
	public static void DestroyAll()
	{
		vp_Timer.m_Active.Clear();
		vp_Timer.m_Pool.Clear();
	}

	// Token: 0x06004EB7 RID: 20151 RVA: 0x001AF778 File Offset: 0x001AD978
	public static vp_Timer.Stats EditorGetStats()
	{
		vp_Timer.Stats result;
		result.Created = vp_Timer.m_Active.Count + vp_Timer.m_Pool.Count;
		result.Inactive = vp_Timer.m_Pool.Count;
		result.Active = vp_Timer.m_Active.Count;
		return result;
	}

	// Token: 0x06004EB8 RID: 20152 RVA: 0x001AF7C4 File Offset: 0x001AD9C4
	public static string EditorGetMethodInfo(int eventIndex)
	{
		if (eventIndex < 0 || eventIndex > vp_Timer.m_Active.Count - 1)
		{
			return "Argument out of range.";
		}
		return vp_Timer.m_Active[eventIndex].MethodInfo;
	}

	// Token: 0x06004EB9 RID: 20153 RVA: 0x001AF7F8 File Offset: 0x001AD9F8
	public static int EditorGetMethodId(int eventIndex)
	{
		if (eventIndex < 0 || eventIndex > vp_Timer.m_Active.Count - 1)
		{
			return 0;
		}
		return vp_Timer.m_Active[eventIndex].Id;
	}

	// Token: 0x04003D35 RID: 15669
	private static GameObject m_MainObject = null;

	// Token: 0x04003D36 RID: 15670
	private static List<vp_Timer.Event> m_Active = new List<vp_Timer.Event>();

	// Token: 0x04003D37 RID: 15671
	private static List<vp_Timer.Event> m_Pool = new List<vp_Timer.Event>();

	// Token: 0x04003D38 RID: 15672
	private static vp_Timer.Event m_NewEvent = null;

	// Token: 0x04003D39 RID: 15673
	private static int m_EventCount = 0;

	// Token: 0x04003D3A RID: 15674
	private static int m_EventBatch = 0;

	// Token: 0x04003D3B RID: 15675
	private static int m_EventIterator = 0;

	// Token: 0x04003D3C RID: 15676
	public static int MaxEventsPerFrame = 500;

	// Token: 0x02000A91 RID: 2705
	public struct Stats
	{
		// Token: 0x04003D3E RID: 15678
		public int Created;

		// Token: 0x04003D3F RID: 15679
		public int Inactive;

		// Token: 0x04003D40 RID: 15680
		public int Active;
	}

	// Token: 0x02000A92 RID: 2706
	private class Event
	{
		// Token: 0x06004EBC RID: 20156 RVA: 0x001AF848 File Offset: 0x001ADA48
		public void Execute()
		{
			if (this.Id == 0 || this.DueTime == 0f)
			{
				this.Recycle();
				return;
			}
			if (this.Function != null)
			{
				this.Function();
			}
			else
			{
				if (this.ArgFunction == null)
				{
					this.Error("Aborted event because function is null.");
					this.Recycle();
					return;
				}
				this.ArgFunction(this.Arguments);
			}
			if (this.Iterations > 0)
			{
				this.Iterations--;
				if (this.Iterations < 1)
				{
					this.Recycle();
					return;
				}
			}
			this.DueTime = Time.time + this.Interval;
		}

		// Token: 0x06004EBD RID: 20157 RVA: 0x001AF904 File Offset: 0x001ADB04
		private void Recycle()
		{
			this.Id = 0;
			this.DueTime = 0f;
			this.StartTime = 0f;
			this.Function = null;
			this.ArgFunction = null;
			this.Arguments = null;
			if (vp_Timer.m_Active.Remove(this))
			{
				vp_Timer.m_Pool.Add(this);
			}
		}

		// Token: 0x06004EBE RID: 20158 RVA: 0x001AF960 File Offset: 0x001ADB60
		private void Destroy()
		{
			vp_Timer.m_Active.Remove(this);
			vp_Timer.m_Pool.Remove(this);
		}

		// Token: 0x06004EBF RID: 20159 RVA: 0x001AF97C File Offset: 0x001ADB7C
		private void Error(string message)
		{
			string text = "Error: (vp_Timer.Event) " + message;
			Debug.LogError(text);
		}

		// Token: 0x17000FE7 RID: 4071
		// (get) Token: 0x06004EC0 RID: 20160 RVA: 0x001AF99C File Offset: 0x001ADB9C
		public string MethodName
		{
			get
			{
				if (this.Function != null)
				{
					if (this.Function.Method != null)
					{
						if (this.Function.Method.Name.get_Chars(0) == '<')
						{
							return "delegate";
						}
						return this.Function.Method.Name;
					}
				}
				else if (this.ArgFunction != null && this.ArgFunction.Method != null)
				{
					if (this.ArgFunction.Method.Name.get_Chars(0) == '<')
					{
						return "delegate";
					}
					return this.ArgFunction.Method.Name;
				}
				return null;
			}
		}

		// Token: 0x17000FE8 RID: 4072
		// (get) Token: 0x06004EC1 RID: 20161 RVA: 0x001AFA50 File Offset: 0x001ADC50
		public string MethodInfo
		{
			get
			{
				string text = this.MethodName;
				if (!string.IsNullOrEmpty(text))
				{
					text += "(";
					if (this.Arguments != null)
					{
						if (this.Arguments.GetType().IsArray)
						{
							object[] array = (object[])this.Arguments;
							foreach (object obj in array)
							{
								text += obj.ToString();
								if (Array.IndexOf<object>(array, obj) < array.Length - 1)
								{
									text += ", ";
								}
							}
						}
						else
						{
							text += this.Arguments;
						}
					}
					text += ")";
				}
				else
				{
					text = "(function = null)";
				}
				return text;
			}
		}

		// Token: 0x04003D41 RID: 15681
		public int Id;

		// Token: 0x04003D42 RID: 15682
		public vp_Timer.Callback Function;

		// Token: 0x04003D43 RID: 15683
		public vp_Timer.ArgCallback ArgFunction;

		// Token: 0x04003D44 RID: 15684
		public object Arguments;

		// Token: 0x04003D45 RID: 15685
		public int Iterations = 1;

		// Token: 0x04003D46 RID: 15686
		public float Interval = -1f;

		// Token: 0x04003D47 RID: 15687
		public float DueTime;

		// Token: 0x04003D48 RID: 15688
		public float StartTime;

		// Token: 0x04003D49 RID: 15689
		public float LifeTime;

		// Token: 0x04003D4A RID: 15690
		public bool Paused;
	}

	// Token: 0x02000A93 RID: 2707
	public class Handle
	{
		// Token: 0x17000FE9 RID: 4073
		// (get) Token: 0x06004EC3 RID: 20163 RVA: 0x001AFB2C File Offset: 0x001ADD2C
		// (set) Token: 0x06004EC4 RID: 20164 RVA: 0x001AFB48 File Offset: 0x001ADD48
		public bool Paused
		{
			get
			{
				return this.Active && this.m_Event.Paused;
			}
			set
			{
				if (this.Active)
				{
					this.m_Event.Paused = value;
				}
			}
		}

		// Token: 0x17000FEA RID: 4074
		// (get) Token: 0x06004EC5 RID: 20165 RVA: 0x001AFB64 File Offset: 0x001ADD64
		public float TimeOfInitiation
		{
			get
			{
				if (this.Active)
				{
					return this.m_Event.StartTime;
				}
				return 0f;
			}
		}

		// Token: 0x17000FEB RID: 4075
		// (get) Token: 0x06004EC6 RID: 20166 RVA: 0x001AFB84 File Offset: 0x001ADD84
		public float TimeOfFirstIteration
		{
			get
			{
				if (this.Active)
				{
					return this.m_FirstDueTime;
				}
				return 0f;
			}
		}

		// Token: 0x17000FEC RID: 4076
		// (get) Token: 0x06004EC7 RID: 20167 RVA: 0x001AFBA0 File Offset: 0x001ADDA0
		public float TimeOfNextIteration
		{
			get
			{
				if (this.Active)
				{
					return this.m_Event.DueTime;
				}
				return 0f;
			}
		}

		// Token: 0x17000FED RID: 4077
		// (get) Token: 0x06004EC8 RID: 20168 RVA: 0x001AFBC0 File Offset: 0x001ADDC0
		public float TimeOfLastIteration
		{
			get
			{
				if (this.Active)
				{
					return Time.time + this.DurationLeft;
				}
				return 0f;
			}
		}

		// Token: 0x17000FEE RID: 4078
		// (get) Token: 0x06004EC9 RID: 20169 RVA: 0x001AFBE0 File Offset: 0x001ADDE0
		public float Delay
		{
			get
			{
				return Mathf.Round((this.m_FirstDueTime - this.TimeOfInitiation) * 1000f) / 1000f;
			}
		}

		// Token: 0x17000FEF RID: 4079
		// (get) Token: 0x06004ECA RID: 20170 RVA: 0x001AFC00 File Offset: 0x001ADE00
		public float Interval
		{
			get
			{
				if (this.Active)
				{
					return this.m_Event.Interval;
				}
				return 0f;
			}
		}

		// Token: 0x17000FF0 RID: 4080
		// (get) Token: 0x06004ECB RID: 20171 RVA: 0x001AFC20 File Offset: 0x001ADE20
		public float TimeUntilNextIteration
		{
			get
			{
				if (this.Active)
				{
					return this.m_Event.DueTime - Time.time;
				}
				return 0f;
			}
		}

		// Token: 0x17000FF1 RID: 4081
		// (get) Token: 0x06004ECC RID: 20172 RVA: 0x001AFC50 File Offset: 0x001ADE50
		public float DurationLeft
		{
			get
			{
				if (this.Active)
				{
					return this.TimeUntilNextIteration + (float)(this.m_Event.Iterations - 1) * this.m_Event.Interval;
				}
				return 0f;
			}
		}

		// Token: 0x17000FF2 RID: 4082
		// (get) Token: 0x06004ECD RID: 20173 RVA: 0x001AFC90 File Offset: 0x001ADE90
		public float DurationTotal
		{
			get
			{
				if (this.Active)
				{
					return this.Delay + (float)this.m_StartIterations * ((this.m_StartIterations <= 1) ? 0f : this.Interval);
				}
				return 0f;
			}
		}

		// Token: 0x17000FF3 RID: 4083
		// (get) Token: 0x06004ECE RID: 20174 RVA: 0x001AFCDC File Offset: 0x001ADEDC
		public float Duration
		{
			get
			{
				if (this.Active)
				{
					return this.m_Event.LifeTime;
				}
				return 0f;
			}
		}

		// Token: 0x17000FF4 RID: 4084
		// (get) Token: 0x06004ECF RID: 20175 RVA: 0x001AFCFC File Offset: 0x001ADEFC
		public int IterationsTotal
		{
			get
			{
				return this.m_StartIterations;
			}
		}

		// Token: 0x17000FF5 RID: 4085
		// (get) Token: 0x06004ED0 RID: 20176 RVA: 0x001AFD04 File Offset: 0x001ADF04
		public int IterationsLeft
		{
			get
			{
				if (this.Active)
				{
					return this.m_Event.Iterations;
				}
				return 0;
			}
		}

		// Token: 0x17000FF6 RID: 4086
		// (get) Token: 0x06004ED1 RID: 20177 RVA: 0x001AFD20 File Offset: 0x001ADF20
		// (set) Token: 0x06004ED2 RID: 20178 RVA: 0x001AFD28 File Offset: 0x001ADF28
		public int Id
		{
			get
			{
				return this.m_Id;
			}
			set
			{
				this.m_Id = value;
				if (this.m_Id == 0)
				{
					this.m_Event.DueTime = 0f;
					return;
				}
				this.m_Event = null;
				for (int i = vp_Timer.m_Active.Count - 1; i > -1; i--)
				{
					if (vp_Timer.m_Active[i].Id == this.m_Id)
					{
						this.m_Event = vp_Timer.m_Active[i];
						break;
					}
				}
				if (this.m_Event == null)
				{
					Debug.LogError("Error: (vp_Timer.Handle) Failed to assign event with Id '" + this.m_Id + "'.");
				}
				this.m_StartIterations = this.m_Event.Iterations;
				this.m_FirstDueTime = this.m_Event.DueTime;
			}
		}

		// Token: 0x17000FF7 RID: 4087
		// (get) Token: 0x06004ED3 RID: 20179 RVA: 0x001AFDFC File Offset: 0x001ADFFC
		public bool Active
		{
			get
			{
				return this.m_Event != null && this.Id != 0 && this.m_Event.Id != 0 && this.m_Event.Id == this.Id;
			}
		}

		// Token: 0x17000FF8 RID: 4088
		// (get) Token: 0x06004ED4 RID: 20180 RVA: 0x001AFE3C File Offset: 0x001AE03C
		public string MethodName
		{
			get
			{
				return this.m_Event.MethodName;
			}
		}

		// Token: 0x17000FF9 RID: 4089
		// (get) Token: 0x06004ED5 RID: 20181 RVA: 0x001AFE4C File Offset: 0x001AE04C
		public string MethodInfo
		{
			get
			{
				return this.m_Event.MethodInfo;
			}
		}

		// Token: 0x06004ED6 RID: 20182 RVA: 0x001AFE5C File Offset: 0x001AE05C
		public void Cancel()
		{
			vp_Timer.Cancel(this);
		}

		// Token: 0x06004ED7 RID: 20183 RVA: 0x001AFE64 File Offset: 0x001AE064
		public void Execute()
		{
			this.m_Event.DueTime = Time.time;
		}

		// Token: 0x04003D4B RID: 15691
		private vp_Timer.Event m_Event;

		// Token: 0x04003D4C RID: 15692
		private int m_Id;

		// Token: 0x04003D4D RID: 15693
		private int m_StartIterations = 1;

		// Token: 0x04003D4E RID: 15694
		private float m_FirstDueTime;
	}

	// Token: 0x02000B0A RID: 2826
	// (Invoke) Token: 0x060050B1 RID: 20657
	public delegate void Callback();

	// Token: 0x02000B0B RID: 2827
	// (Invoke) Token: 0x060050B5 RID: 20661
	public delegate void ArgCallback(object args);
}
