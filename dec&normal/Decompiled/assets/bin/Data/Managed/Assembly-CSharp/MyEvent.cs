using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x020001D6 RID: 470
public class MyEvent : SingletonUnity<MyEvent>
{
	// Token: 0x060010EA RID: 4330 RVA: 0x0006DAD0 File Offset: 0x0006BCD0
	public bool Register(string eventname, object own, string funname)
	{
		this.DeRegister(eventname, own, funname);
		List<MyEvent.Pair> list = null;
		MyEvent.Pair pair = default(MyEvent.Pair);
		pair.owner = own;
		pair.funcname = funname;
		pair.method = own.GetType().GetMethod(funname);
		if (pair.method == null)
		{
			Debug.LogError(string.Concat(new object[]
			{
				"MyEvent::Register ",
				own,
				" not found method[",
				funname,
				"]"
			}));
			return false;
		}
		if (!this.events.TryGetValue(eventname, ref list))
		{
			list = new List<MyEvent.Pair>();
			list.Add(pair);
			this.events.Add(eventname, list);
			return true;
		}
		list.Add(pair);
		return true;
	}

	// Token: 0x060010EB RID: 4331 RVA: 0x0006DB8C File Offset: 0x0006BD8C
	public bool DeRegister(string eventname, object own, string funname)
	{
		List<MyEvent.Pair> list = null;
		if (!this.events.TryGetValue(eventname, ref list))
		{
			return false;
		}
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].funcname == funname && list[i].owner == own)
			{
				list.RemoveAt(i);
				return true;
			}
		}
		return false;
	}

	// Token: 0x060010EC RID: 4332 RVA: 0x0006DC00 File Offset: 0x0006BE00
	public void Fire(string eventname, params object[] args)
	{
		List<MyEvent.Pair> list = null;
		if (!this.events.TryGetValue(eventname, ref list))
		{
			Debug.LogWarning("MyEvent::Fire " + eventname + " not found");
			return;
		}
		for (int i = list.Count - 1; i >= 0; i--)
		{
			try
			{
				list[i].method.Invoke(list[i].owner, args);
			}
			catch (Exception ex)
			{
				Debug.LogError("MyEvent::Fire " + eventname + " " + ex.ToString());
			}
		}
	}

	// Token: 0x060010ED RID: 4333 RVA: 0x0006DCBC File Offset: 0x0006BEBC
	public void DelayFire(string eventname, float delaytime, params object[] args)
	{
		if (delaytime <= 0f)
		{
			this.Fire(eventname, args);
		}
		List<MyEvent.Pair> list = null;
		if (!this.events.TryGetValue(eventname, ref list))
		{
			Debug.LogWarning("MyEvent::Fire " + eventname + " not found");
			return;
		}
		for (int i = 0; i < list.Count; i++)
		{
			MyEvent.EventInfo eventInfo = new MyEvent.EventInfo();
			eventInfo.delaytime = delaytime;
			eventInfo.info = list[i];
			eventInfo.args = args;
			this.delayevents.Add(eventInfo);
		}
	}

	// Token: 0x060010EE RID: 4334 RVA: 0x0006DD4C File Offset: 0x0006BF4C
	private void UpdateDelayEvents()
	{
		if (this.delayevents.Count > 0)
		{
			int i = 0;
			while (i < this.delayevents.Count)
			{
				MyEvent.EventInfo eventInfo = this.delayevents[i];
				eventInfo.delaytime -= Time.deltaTime;
				if (eventInfo.delaytime <= 0f)
				{
					try
					{
						eventInfo.info.method.Invoke(eventInfo.info.owner, eventInfo.args);
					}
					catch (Exception ex)
					{
						Debug.LogError("MyEvent::UpdateDelayEvents  " + ex.ToString());
					}
					this.delayevents.RemoveAt(i);
				}
				else
				{
					i++;
				}
			}
		}
	}

	// Token: 0x060010EF RID: 4335 RVA: 0x0006DE24 File Offset: 0x0006C024
	public bool HasRegister(string eventname)
	{
		return this.events.ContainsKey(eventname);
	}

	// Token: 0x060010F0 RID: 4336 RVA: 0x0006DE34 File Offset: 0x0006C034
	public void Clear()
	{
		this.delayevents.Clear();
		this.events.Clear();
	}

	// Token: 0x060010F1 RID: 4337 RVA: 0x0006DE4C File Offset: 0x0006C04C
	private void Update()
	{
		this.UpdateDelayEvents();
	}

	// Token: 0x0400147C RID: 5244
	private Dictionary<string, List<MyEvent.Pair>> events = new Dictionary<string, List<MyEvent.Pair>>();

	// Token: 0x0400147D RID: 5245
	private List<MyEvent.EventInfo> delayevents = new List<MyEvent.EventInfo>();

	// Token: 0x020001D7 RID: 471
	public struct Pair
	{
		// Token: 0x0400147E RID: 5246
		public object owner;

		// Token: 0x0400147F RID: 5247
		public string funcname;

		// Token: 0x04001480 RID: 5248
		public MethodInfo method;
	}

	// Token: 0x020001D8 RID: 472
	public class EventInfo
	{
		// Token: 0x04001481 RID: 5249
		public MyEvent.Pair info;

		// Token: 0x04001482 RID: 5250
		public object[] args;

		// Token: 0x04001483 RID: 5251
		public float delaytime;
	}
}
