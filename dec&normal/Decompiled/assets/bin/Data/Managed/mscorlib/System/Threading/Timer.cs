using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Threading
{
	// Token: 0x020003C3 RID: 963
	[ComVisible(true)]
	public sealed class Timer : MarshalByRefObject, IDisposable
	{
		// Token: 0x06001D33 RID: 7475 RVA: 0x0006E7F8 File Offset: 0x0006C9F8
		public Timer(TimerCallback callback, object state, int dueTime, int period)
		{
			this.Init(callback, state, (long)dueTime, (long)period);
		}

		// Token: 0x06001D34 RID: 7476 RVA: 0x0006E810 File Offset: 0x0006CA10
		public Timer(TimerCallback callback, object state, TimeSpan dueTime, TimeSpan period)
		{
			this.Init(callback, state, (long)dueTime.TotalMilliseconds, (long)period.TotalMilliseconds);
		}

		// Token: 0x06001D36 RID: 7478 RVA: 0x0006E83C File Offset: 0x0006CA3C
		private void Init(TimerCallback callback, object state, long dueTime, long period)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			this.callback = callback;
			this.state = state;
			this.Change(dueTime, period, true);
		}

		// Token: 0x06001D37 RID: 7479 RVA: 0x0006E868 File Offset: 0x0006CA68
		public bool Change(TimeSpan dueTime, TimeSpan period)
		{
			return this.Change((long)dueTime.TotalMilliseconds, (long)period.TotalMilliseconds, false);
		}

		// Token: 0x06001D38 RID: 7480 RVA: 0x0006E884 File Offset: 0x0006CA84
		public void Dispose()
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			Timer.scheduler.Remove(this);
		}

		// Token: 0x06001D39 RID: 7481 RVA: 0x0006E8A4 File Offset: 0x0006CAA4
		private bool Change(long dueTime, long period, bool first)
		{
			if (dueTime > (long)((ulong)-2))
			{
				throw new ArgumentOutOfRangeException("Due time too large");
			}
			if (period > (long)((ulong)-2))
			{
				throw new ArgumentOutOfRangeException("Period too large");
			}
			if (dueTime < -1L)
			{
				throw new ArgumentOutOfRangeException("dueTime");
			}
			if (period < -1L)
			{
				throw new ArgumentOutOfRangeException("period");
			}
			if (this.disposed)
			{
				return false;
			}
			this.due_time_ms = dueTime;
			this.period_ms = period;
			long new_next_run;
			if (dueTime == 0L)
			{
				new_next_run = 0L;
			}
			else if (dueTime < 0L)
			{
				new_next_run = long.MaxValue;
				if (first)
				{
					this.next_run = new_next_run;
					return true;
				}
			}
			else
			{
				new_next_run = dueTime * 10000L + DateTime.GetTimeMonotonic();
			}
			Timer.scheduler.Change(this, new_next_run);
			return true;
		}

		// Token: 0x04000F6B RID: 3947
		private const long MaxValue = 4294967294L;

		// Token: 0x04000F6C RID: 3948
		private static Timer.Scheduler scheduler = Timer.Scheduler.Instance;

		// Token: 0x04000F6D RID: 3949
		private TimerCallback callback;

		// Token: 0x04000F6E RID: 3950
		private object state;

		// Token: 0x04000F6F RID: 3951
		private long due_time_ms;

		// Token: 0x04000F70 RID: 3952
		private long period_ms;

		// Token: 0x04000F71 RID: 3953
		private long next_run;

		// Token: 0x04000F72 RID: 3954
		private bool disposed;

		// Token: 0x020003C4 RID: 964
		private sealed class Scheduler
		{
			// Token: 0x06001D3A RID: 7482 RVA: 0x0006E96C File Offset: 0x0006CB6C
			private Scheduler()
			{
				this.list = new SortedList(new Timer.TimerComparer(), 1024);
				new Thread(new ThreadStart(this.SchedulerThread))
				{
					IsBackground = true
				}.Start();
			}

			// Token: 0x170004FD RID: 1277
			// (get) Token: 0x06001D3C RID: 7484 RVA: 0x0006E9C0 File Offset: 0x0006CBC0
			public static Timer.Scheduler Instance
			{
				get
				{
					return Timer.Scheduler.instance;
				}
			}

			// Token: 0x06001D3D RID: 7485 RVA: 0x0006E9C8 File Offset: 0x0006CBC8
			public void Remove(Timer timer)
			{
				if (timer.next_run == 0L || timer.next_run == 9223372036854775807L)
				{
					return;
				}
				lock (this)
				{
					this.InternalRemove(timer);
				}
			}

			// Token: 0x06001D3E RID: 7486 RVA: 0x0006EA24 File Offset: 0x0006CC24
			public void Change(Timer timer, long new_next_run)
			{
				lock (this)
				{
					this.InternalRemove(timer);
					if (new_next_run == 9223372036854775807L)
					{
						timer.next_run = new_next_run;
					}
					else if (!timer.disposed)
					{
						timer.next_run = new_next_run;
						this.Add(timer);
						if (this.list.GetByIndex(0) == timer)
						{
							Monitor.Pulse(this);
						}
					}
				}
			}

			// Token: 0x06001D3F RID: 7487 RVA: 0x0006EAAC File Offset: 0x0006CCAC
			private void Add(Timer timer)
			{
				int num = this.list.IndexOfKey(timer);
				if (num != -1)
				{
					bool flag = long.MaxValue - timer.next_run > 20000L;
					Timer timer2;
					do
					{
						num++;
						if (flag)
						{
							timer.next_run += 1L;
						}
						else
						{
							timer.next_run -= 1L;
						}
						if (num >= this.list.Count)
						{
							break;
						}
						timer2 = (Timer)this.list.GetByIndex(num);
					}
					while (timer2.next_run == timer.next_run);
				}
				this.list.Add(timer, timer);
			}

			// Token: 0x06001D40 RID: 7488 RVA: 0x0006EB70 File Offset: 0x0006CD70
			private int InternalRemove(Timer timer)
			{
				int num = this.list.IndexOfKey(timer);
				if (num >= 0)
				{
					this.list.RemoveAt(num);
				}
				return num;
			}

			// Token: 0x06001D41 RID: 7489 RVA: 0x0006EBA0 File Offset: 0x0006CDA0
			private void SchedulerThread()
			{
				Thread.CurrentThread.Name = "Timer-Scheduler";
				ArrayList arrayList = new ArrayList(512);
				for (;;)
				{
					long timeMonotonic = DateTime.GetTimeMonotonic();
					lock (this)
					{
						int num = this.list.Count;
						for (int i = 0; i < num; i++)
						{
							Timer timer = (Timer)this.list.GetByIndex(i);
							if (timer.next_run > timeMonotonic)
							{
								break;
							}
							this.list.RemoveAt(i);
							num--;
							i--;
							ThreadPool.QueueUserWorkItem(new WaitCallback(timer.callback.Invoke), timer.state);
							long period_ms = timer.period_ms;
							long due_time_ms = timer.due_time_ms;
							bool flag = period_ms == -1L || ((period_ms == 0L || period_ms == -1L) && due_time_ms != -1L);
							if (flag)
							{
								timer.next_run = long.MaxValue;
							}
							else
							{
								timer.next_run = DateTime.GetTimeMonotonic() + 10000L * timer.period_ms;
								arrayList.Add(timer);
							}
						}
						num = arrayList.Count;
						for (int i = 0; i < num; i++)
						{
							Timer timer2 = (Timer)arrayList[i];
							this.Add(timer2);
						}
						arrayList.Clear();
						this.ShrinkIfNeeded(arrayList, 512);
						int capacity = this.list.Capacity;
						num = this.list.Count;
						if (capacity > 1024 && num > 0 && capacity / num > 3)
						{
							this.list.Capacity = num * 2;
						}
						long num2 = long.MaxValue;
						if (this.list.Count > 0)
						{
							num2 = ((Timer)this.list.GetByIndex(0)).next_run;
						}
						int num3 = -1;
						if (num2 != 9223372036854775807L)
						{
							long num4 = num2 - DateTime.GetTimeMonotonic();
							num3 = (int)(num4 / 10000L);
							if (num3 < 0)
							{
								num3 = 0;
							}
						}
						Monitor.Wait(this, num3);
					}
				}
			}

			// Token: 0x06001D42 RID: 7490 RVA: 0x0006EDF4 File Offset: 0x0006CFF4
			private void ShrinkIfNeeded(ArrayList list, int initial)
			{
				int capacity = list.Capacity;
				int count = list.Count;
				if (capacity > initial && count > 0 && capacity / count > 3)
				{
					list.Capacity = count * 2;
				}
			}

			// Token: 0x04000F73 RID: 3955
			private static Timer.Scheduler instance = new Timer.Scheduler();

			// Token: 0x04000F74 RID: 3956
			private SortedList list;
		}

		// Token: 0x020003C5 RID: 965
		private sealed class TimerComparer : IComparer
		{
			// Token: 0x06001D44 RID: 7492 RVA: 0x0006EE38 File Offset: 0x0006D038
			public int Compare(object x, object y)
			{
				Timer timer = x as Timer;
				if (timer == null)
				{
					return -1;
				}
				Timer timer2 = y as Timer;
				if (timer2 == null)
				{
					return 1;
				}
				long num = timer.next_run - timer2.next_run;
				if (num == 0L)
				{
					return (x != y) ? -1 : 0;
				}
				return (num <= 0L) ? -1 : 1;
			}
		}
	}
}
