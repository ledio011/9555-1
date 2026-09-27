using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000012 RID: 18
[AddComponentMenu("FingerGestures/Components/Finger Cluster Manager")]
public class FingerClusterManager : MonoBehaviour
{
	// Token: 0x17000017 RID: 23
	// (get) Token: 0x0600007D RID: 125 RVA: 0x000037AC File Offset: 0x000019AC
	public FingerGestures.IFingerList FingersAdded
	{
		get
		{
			return this.fingersAdded;
		}
	}

	// Token: 0x17000018 RID: 24
	// (get) Token: 0x0600007E RID: 126 RVA: 0x000037B4 File Offset: 0x000019B4
	public FingerGestures.IFingerList FingersRemoved
	{
		get
		{
			return this.fingersRemoved;
		}
	}

	// Token: 0x17000019 RID: 25
	// (get) Token: 0x0600007F RID: 127 RVA: 0x000037BC File Offset: 0x000019BC
	public List<FingerClusterManager.Cluster> Clusters
	{
		get
		{
			return this.clusters;
		}
	}

	// Token: 0x06000080 RID: 128 RVA: 0x000037C4 File Offset: 0x000019C4
	public List<FingerClusterManager.Cluster> GetClustersPool()
	{
		return this.clusterPool;
	}

	// Token: 0x06000081 RID: 129 RVA: 0x000037CC File Offset: 0x000019CC
	private void Awake()
	{
		this.clusters = new List<FingerClusterManager.Cluster>();
		this.clusterPool = new List<FingerClusterManager.Cluster>();
		this.fingersAdded = new FingerGestures.FingerList();
		this.fingersRemoved = new FingerGestures.FingerList();
	}

	// Token: 0x06000082 RID: 130 RVA: 0x00003808 File Offset: 0x00001A08
	public void Update()
	{
		if (this.lastUpdateFrame == Time.frameCount)
		{
			return;
		}
		this.lastUpdateFrame = Time.frameCount;
		this.fingersAdded.Clear();
		this.fingersRemoved.Clear();
		for (int i = 0; i < FingerGestures.Instance.MaxFingers; i++)
		{
			FingerGestures.Finger finger = FingerGestures.GetFinger(i);
			if (finger.IsDown)
			{
				if (!finger.WasDown)
				{
					this.fingersAdded.Add(finger);
				}
			}
			else if (finger.WasDown)
			{
				this.fingersRemoved.Add(finger);
			}
		}
		for (int j = 0; j < this.fingersRemoved.Count; j++)
		{
			FingerGestures.Finger touch = this.fingersRemoved[j];
			for (int k = this.clusters.Count - 1; k >= 0; k--)
			{
				FingerClusterManager.Cluster cluster = this.clusters[k];
				if (cluster.Fingers.Remove(touch) && cluster.Fingers.Count == 0)
				{
					this.clusters.RemoveAt(k);
					this.clusterPool.Add(cluster);
				}
			}
		}
		for (int l = 0; l < this.fingersAdded.Count; l++)
		{
			FingerGestures.Finger finger2 = this.fingersAdded[l];
			FingerClusterManager.Cluster cluster2 = this.FindExistingCluster(finger2);
			if (cluster2 == null)
			{
				cluster2 = this.NewCluster();
				cluster2.StartTime = finger2.StarTime;
			}
			cluster2.Fingers.Add(finger2);
		}
	}

	// Token: 0x06000083 RID: 131 RVA: 0x000039A8 File Offset: 0x00001BA8
	public FingerClusterManager.Cluster FindClusterById(int clusterId)
	{
		return this.clusters.Find((FingerClusterManager.Cluster c) => c.Id == clusterId);
	}

	// Token: 0x06000084 RID: 132 RVA: 0x000039DC File Offset: 0x00001BDC
	private FingerClusterManager.Cluster NewCluster()
	{
		FingerClusterManager.Cluster cluster;
		if (this.clusterPool.Count == 0)
		{
			cluster = new FingerClusterManager.Cluster();
		}
		else
		{
			int index = this.clusterPool.Count - 1;
			cluster = this.clusterPool[index];
			cluster.Reset();
			this.clusterPool.RemoveAt(index);
		}
		cluster.Id = this.nextClusterId++;
		this.clusters.Add(cluster);
		return cluster;
	}

	// Token: 0x06000085 RID: 133 RVA: 0x00003A58 File Offset: 0x00001C58
	private FingerClusterManager.Cluster FindExistingCluster(FingerGestures.Finger finger)
	{
		FingerClusterManager.Cluster result = null;
		float num = float.MaxValue;
		float num2 = FingerGestures.Convert(this.ClusterRadius * this.ClusterRadius, this.DistanceUnit, DistanceUnit.Pixels);
		for (int i = 0; i < this.clusters.Count; i++)
		{
			FingerClusterManager.Cluster cluster = this.clusters[i];
			float num3 = finger.StarTime - cluster.StartTime;
			if (num3 <= this.TimeTolerance)
			{
				Vector2 averagePosition = cluster.Fingers.GetAveragePosition();
				float num4 = Vector2.SqrMagnitude(finger.Position - averagePosition);
				if (num4 < num && num4 < num2)
				{
					result = cluster;
					num = num4;
				}
			}
		}
		return result;
	}

	// Token: 0x04000046 RID: 70
	public DistanceUnit DistanceUnit;

	// Token: 0x04000047 RID: 71
	public float ClusterRadius = 250f;

	// Token: 0x04000048 RID: 72
	public float TimeTolerance = 0.5f;

	// Token: 0x04000049 RID: 73
	private int lastUpdateFrame = -1;

	// Token: 0x0400004A RID: 74
	private int nextClusterId = 1;

	// Token: 0x0400004B RID: 75
	private List<FingerClusterManager.Cluster> clusters;

	// Token: 0x0400004C RID: 76
	private List<FingerClusterManager.Cluster> clusterPool;

	// Token: 0x0400004D RID: 77
	private FingerGestures.FingerList fingersAdded;

	// Token: 0x0400004E RID: 78
	private FingerGestures.FingerList fingersRemoved;

	// Token: 0x02000013 RID: 19
	[Serializable]
	public class Cluster
	{
		// Token: 0x06000087 RID: 135 RVA: 0x00003B20 File Offset: 0x00001D20
		public void Reset()
		{
			this.Fingers.Clear();
		}

		// Token: 0x0400004F RID: 79
		public int Id;

		// Token: 0x04000050 RID: 80
		public float StartTime;

		// Token: 0x04000051 RID: 81
		public FingerGestures.FingerList Fingers = new FingerGestures.FingerList();
	}
}
