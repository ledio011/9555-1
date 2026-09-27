using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200000E RID: 14
public abstract class GestureRecognizerTS<T> : GestureRecognizer where T : Gesture, new()
{
	// Token: 0x14000002 RID: 2
	// (add) Token: 0x0600004C RID: 76 RVA: 0x00002C78 File Offset: 0x00000E78
	// (remove) Token: 0x0600004D RID: 77 RVA: 0x00002C94 File Offset: 0x00000E94
	public event GestureRecognizerTS<T>.GestureEventHandler OnGesture;

	// Token: 0x0600004E RID: 78 RVA: 0x00002CB0 File Offset: 0x00000EB0
	protected override void Start()
	{
		base.Start();
		this.InitGestures();
	}

	// Token: 0x0600004F RID: 79 RVA: 0x00002CC0 File Offset: 0x00000EC0
	protected override void OnEnable()
	{
		base.OnEnable();
	}

	// Token: 0x06000050 RID: 80 RVA: 0x00002CC8 File Offset: 0x00000EC8
	private void InitGestures()
	{
		if (this.gestures == null)
		{
			this.gestures = new List<T>();
			for (int i = 0; i < this.MaxSimultaneousGestures; i++)
			{
				this.AddGesture();
			}
		}
	}

	// Token: 0x06000051 RID: 81 RVA: 0x00002D0C File Offset: 0x00000F0C
	protected T AddGesture()
	{
		T t = this.CreateGesture();
		t.Recognizer = this;
		t.OnStateChanged += this.OnStateChanged;
		this.gestures.Add(t);
		return t;
	}

	// Token: 0x17000014 RID: 20
	// (get) Token: 0x06000052 RID: 82 RVA: 0x00002D58 File Offset: 0x00000F58
	public List<T> Gestures
	{
		get
		{
			return this.gestures;
		}
	}

	// Token: 0x06000053 RID: 83 RVA: 0x00002D60 File Offset: 0x00000F60
	protected virtual bool CanBegin(T gesture, FingerGestures.IFingerList touches)
	{
		return touches.Count == this.RequiredFingerCount && (!this.IsExclusive || FingerGestures.Touches.Count == this.RequiredFingerCount) && (!this.Delegate || !this.Delegate.enabled || this.Delegate.CanBegin(gesture, touches));
	}

	// Token: 0x06000054 RID: 84
	protected abstract void OnBegin(T gesture, FingerGestures.IFingerList touches);

	// Token: 0x06000055 RID: 85
	protected abstract GestureRecognitionState OnRecognize(T gesture, FingerGestures.IFingerList touches);

	// Token: 0x06000056 RID: 86 RVA: 0x00002DDC File Offset: 0x00000FDC
	protected virtual GameObject GetDefaultSelectionForSendMessage(T gesture)
	{
		return gesture.Selection;
	}

	// Token: 0x06000057 RID: 87 RVA: 0x00002DEC File Offset: 0x00000FEC
	protected virtual T CreateGesture()
	{
		return Activator.CreateInstance<T>();
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00002DF4 File Offset: 0x00000FF4
	public override Type GetGestureType()
	{
		return typeof(T);
	}

	// Token: 0x06000059 RID: 89 RVA: 0x00002E00 File Offset: 0x00001000
	protected virtual void OnStateChanged(Gesture gesture)
	{
	}

	// Token: 0x0600005A RID: 90 RVA: 0x00002E04 File Offset: 0x00001004
	protected virtual T FindGestureByCluster(FingerClusterManager.Cluster cluster)
	{
		return this.gestures.Find((T g) => g.ClusterId == cluster.Id);
	}

	// Token: 0x0600005B RID: 91 RVA: 0x00002E38 File Offset: 0x00001038
	protected virtual T MatchActiveGestureToCluster(FingerClusterManager.Cluster cluster)
	{
		return (T)((object)null);
	}

	// Token: 0x0600005C RID: 92 RVA: 0x00002E40 File Offset: 0x00001040
	protected virtual T FindFreeGesture()
	{
		return this.gestures.Find((T g) => g.State == GestureRecognitionState.Ready);
	}

	// Token: 0x0600005D RID: 93 RVA: 0x00002E78 File Offset: 0x00001078
	protected virtual void Reset(T gesture)
	{
		this.ReleaseFingers(gesture);
		gesture.ClusterId = 0;
		gesture.Fingers.Clear();
		gesture.State = GestureRecognitionState.Ready;
	}

	// Token: 0x0600005E RID: 94 RVA: 0x00002EB8 File Offset: 0x000010B8
	public virtual void Update()
	{
		if (this.IsExclusive)
		{
			this.UpdateExclusive();
		}
		else if (this.RequiredFingerCount == 1)
		{
			this.UpdatePerFinger();
		}
		else if (this.SupportFingerClustering && this.ClusterManager)
		{
			this.UpdateUsingClusters();
		}
		else
		{
			this.UpdateExclusive();
		}
	}

	// Token: 0x0600005F RID: 95 RVA: 0x00002F20 File Offset: 0x00001120
	private void UpdateExclusive()
	{
		T gesture = this.gestures[0];
		FingerGestures.IFingerList touches = FingerGestures.Touches;
		if (gesture.State == GestureRecognitionState.Ready && this.CanBegin(gesture, touches))
		{
			this.Begin(gesture, 0, touches);
		}
		this.UpdateGesture(gesture, touches);
	}

	// Token: 0x06000060 RID: 96 RVA: 0x00002F70 File Offset: 0x00001170
	private void UpdatePerFinger()
	{
		int num = 0;
		while (num < FingerGestures.Instance.MaxFingers && num < this.MaxSimultaneousGestures && num < this.gestures.Count)
		{
			FingerGestures.Finger finger = FingerGestures.GetFinger(num);
			T gesture = this.gestures[num];
			FingerGestures.FingerList fingerList = GestureRecognizerTS<T>.tempTouchList;
			fingerList.Clear();
			if (finger.IsDown)
			{
				fingerList.Add(finger);
			}
			if (gesture.State == GestureRecognitionState.Ready && this.CanBegin(gesture, fingerList))
			{
				this.Begin(gesture, 0, fingerList);
			}
			this.UpdateGesture(gesture, fingerList);
			num++;
		}
	}

	// Token: 0x06000061 RID: 97 RVA: 0x00003018 File Offset: 0x00001218
	private void UpdateUsingClusters()
	{
		this.ClusterManager.Update();
		for (int i = 0; i < this.ClusterManager.Clusters.Count; i++)
		{
			this.ProcessCluster(this.ClusterManager.Clusters[i]);
		}
		for (int j = 0; j < this.gestures.Count; j++)
		{
			T t = this.gestures[j];
			FingerClusterManager.Cluster cluster = this.ClusterManager.FindClusterById(t.ClusterId);
			FingerGestures.IFingerList fingerList;
			if (cluster != null)
			{
				FingerGestures.IFingerList fingers = cluster.Fingers;
				fingerList = fingers;
			}
			else
			{
				fingerList = GestureRecognizer.EmptyFingerList;
			}
			FingerGestures.IFingerList touches = fingerList;
			this.UpdateGesture(t, touches);
		}
	}

	// Token: 0x06000062 RID: 98 RVA: 0x000030D0 File Offset: 0x000012D0
	protected virtual void ProcessCluster(FingerClusterManager.Cluster cluster)
	{
		if (this.FindGestureByCluster(cluster) != null)
		{
			return;
		}
		if (cluster.Fingers.Count != this.RequiredFingerCount)
		{
			return;
		}
		T t = this.MatchActiveGestureToCluster(cluster);
		if (t != null)
		{
			t.ClusterId = cluster.Id;
		}
		else
		{
			t = this.FindFreeGesture();
			if (t == null)
			{
				return;
			}
			if (!this.CanBegin(t, cluster.Fingers))
			{
				return;
			}
			this.Begin(t, cluster.Id, cluster.Fingers);
		}
	}

	// Token: 0x06000063 RID: 99 RVA: 0x00003168 File Offset: 0x00001368
	private void ReleaseFingers(T gesture)
	{
		for (int i = 0; i < gesture.Fingers.Count; i++)
		{
			base.Release(gesture.Fingers[i]);
		}
	}

	// Token: 0x06000064 RID: 100 RVA: 0x000031B4 File Offset: 0x000013B4
	private void Begin(T gesture, int clusterId, FingerGestures.IFingerList touches)
	{
		gesture.ClusterId = clusterId;
		gesture.StartTime = Time.time;
		for (int i = 0; i < touches.Count; i++)
		{
			FingerGestures.Finger finger = touches[i];
			gesture.Fingers.Add(finger);
			base.Acquire(finger);
		}
		this.OnBegin(gesture, touches);
		gesture.PickStartSelection(this.Raycaster);
		gesture.State = GestureRecognitionState.Started;
	}

	// Token: 0x06000065 RID: 101 RVA: 0x00003244 File Offset: 0x00001444
	protected virtual FingerGestures.IFingerList GetTouches(T gesture)
	{
		if (this.SupportFingerClustering && this.ClusterManager)
		{
			FingerClusterManager.Cluster cluster = this.ClusterManager.FindClusterById(gesture.ClusterId);
			FingerGestures.IFingerList result;
			if (cluster != null)
			{
				FingerGestures.IFingerList fingers = cluster.Fingers;
				result = fingers;
			}
			else
			{
				result = GestureRecognizer.EmptyFingerList;
			}
			return result;
		}
		return FingerGestures.Touches;
	}

	// Token: 0x06000066 RID: 102 RVA: 0x000032A4 File Offset: 0x000014A4
	protected virtual void UpdateGesture(T gesture, FingerGestures.IFingerList touches)
	{
		if (gesture.State == GestureRecognitionState.Ready)
		{
			return;
		}
		if (gesture.State == GestureRecognitionState.Started)
		{
			gesture.State = GestureRecognitionState.InProgress;
		}
		switch (gesture.State)
		{
		case GestureRecognitionState.InProgress:
		{
			GestureRecognitionState gestureRecognitionState = this.OnRecognize(gesture, touches);
			if (gestureRecognitionState == GestureRecognitionState.FailAndRetry)
			{
				gesture.State = GestureRecognitionState.Failed;
				int clusterId = gesture.ClusterId;
				this.Reset(gesture);
				if (this.CanBegin(gesture, touches))
				{
					this.Begin(gesture, clusterId, touches);
				}
			}
			else
			{
				if (gestureRecognitionState == GestureRecognitionState.InProgress)
				{
					gesture.PickSelection(this.Raycaster);
				}
				gesture.State = gestureRecognitionState;
			}
			break;
		}
		case GestureRecognitionState.Failed:
		case GestureRecognitionState.Ended:
			if (gesture.PreviousState != gesture.State)
			{
				this.ReleaseFingers(gesture);
			}
			if (this.ResetMode == GestureResetMode.NextFrame || (this.ResetMode == GestureResetMode.EndOfTouchSequence && touches.Count == 0))
			{
				this.Reset(gesture);
			}
			break;
		default:
			Debug.LogError(string.Concat(new object[]
			{
				this,
				" - Unhandled state: ",
				gesture.State,
				". Failing gesture."
			}));
			gesture.State = GestureRecognitionState.Failed;
			break;
		}
	}

	// Token: 0x06000067 RID: 103 RVA: 0x00003428 File Offset: 0x00001628
	protected void RaiseEvent(T gesture)
	{
		if (this.OnGesture != null)
		{
			this.OnGesture(gesture);
			return;
		}
		FingerGestures.FireEvent(gesture);
		if (this.UseSendMessage && !string.IsNullOrEmpty(this.EventMessageName))
		{
			if (this.EventMessageTarget)
			{
				this.EventMessageTarget.SendMessage(this.EventMessageName, gesture, SendMessageOptions.DontRequireReceiver);
			}
			if (this.SendMessageToSelection != GestureRecognizer.SelectionType.None)
			{
				GameObject gameObject = null;
				switch (this.SendMessageToSelection)
				{
				case GestureRecognizer.SelectionType.Default:
					gameObject = this.GetDefaultSelectionForSendMessage(gesture);
					break;
				case GestureRecognizer.SelectionType.StartSelection:
					gameObject = gesture.StartSelection;
					break;
				case GestureRecognizer.SelectionType.CurrentSelection:
					gameObject = gesture.Selection;
					break;
				}
				if (gameObject && gameObject != this.EventMessageTarget)
				{
					gameObject.SendMessage(this.EventMessageName, gesture, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}

	// Token: 0x04000030 RID: 48
	private List<T> gestures;

	// Token: 0x04000031 RID: 49
	private static FingerGestures.FingerList tempTouchList = new FingerGestures.FingerList();

	// Token: 0x02000062 RID: 98
	// (Invoke) Token: 0x0600027B RID: 635
	public delegate void GestureEventHandler(T gesture);
}
