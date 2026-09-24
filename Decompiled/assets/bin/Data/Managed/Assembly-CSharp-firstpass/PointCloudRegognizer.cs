using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000032 RID: 50
[AddComponentMenu("FingerGestures/Gestures/PointCloud Recognizer")]
public class PointCloudRegognizer : DiscreteGestureRecognizer<PointCloudGesture>
{
	// Token: 0x06000152 RID: 338 RVA: 0x00005B6C File Offset: 0x00003D6C
	protected override void Awake()
	{
		base.Awake();
		this.normalizer = new PointCloudRegognizer.GestureNormalizer();
		this.normalizedTemplates = new List<PointCloudRegognizer.NormalizedTemplate>();
		foreach (PointCloudGestureTemplate template in this.Templates)
		{
			this.AddTemplate(template);
		}
	}

	// Token: 0x06000153 RID: 339 RVA: 0x00005BF0 File Offset: 0x00003DF0
	private PointCloudRegognizer.NormalizedTemplate FindNormalizedTemplate(PointCloudGestureTemplate template)
	{
		return this.normalizedTemplates.Find((PointCloudRegognizer.NormalizedTemplate t) => t.Source == template);
	}

	// Token: 0x06000154 RID: 340 RVA: 0x00005C24 File Offset: 0x00003E24
	private List<PointCloudRegognizer.Point> Normalize(List<PointCloudRegognizer.Point> points)
	{
		return new List<PointCloudRegognizer.Point>(this.normalizer.Apply(points, 32));
	}

	// Token: 0x06000155 RID: 341 RVA: 0x00005C3C File Offset: 0x00003E3C
	public bool AddTemplate(PointCloudGestureTemplate template)
	{
		if (this.FindNormalizedTemplate(template) != null)
		{
			Debug.LogWarning("The PointCloud template " + template.name + " is already present in the list");
			return false;
		}
		List<PointCloudRegognizer.Point> list = new List<PointCloudRegognizer.Point>();
		for (int i = 0; i < template.PointCount; i++)
		{
			list.Add(new PointCloudRegognizer.Point(template.GetStrokeId(i), template.GetPosition(i)));
		}
		PointCloudRegognizer.NormalizedTemplate normalizedTemplate = new PointCloudRegognizer.NormalizedTemplate();
		normalizedTemplate.Source = template;
		normalizedTemplate.Points = this.Normalize(list);
		this.normalizedTemplates.Add(normalizedTemplate);
		return true;
	}

	// Token: 0x06000156 RID: 342 RVA: 0x00005CD0 File Offset: 0x00003ED0
	protected override void OnBegin(PointCloudGesture gesture, FingerGestures.IFingerList touches)
	{
		gesture.StartPosition = touches.GetAverageStartPosition();
		gesture.Position = touches.GetAveragePosition();
		gesture.RawPoints.Clear();
		gesture.RawPoints.Add(new PointCloudRegognizer.Point(0, gesture.Position));
	}

	// Token: 0x06000157 RID: 343 RVA: 0x00005D18 File Offset: 0x00003F18
	private bool RecognizePointCloud(PointCloudGesture gesture)
	{
		this.debugLastGesture = gesture;
		gesture.MatchDistance = 0f;
		gesture.MatchScore = 0f;
		gesture.RecognizedTemplate = null;
		gesture.NormalizedPoints.Clear();
		if (gesture.RawPoints.Count < 2)
		{
			return false;
		}
		gesture.NormalizedPoints.AddRange(this.normalizer.Apply(gesture.RawPoints, 32));
		float num = float.PositiveInfinity;
		for (int i = 0; i < this.normalizedTemplates.Count; i++)
		{
			PointCloudRegognizer.NormalizedTemplate normalizedTemplate = this.normalizedTemplates[i];
			float num2 = this.GreedyCloudMatch(gesture.NormalizedPoints, normalizedTemplate.Points);
			if (num2 < num)
			{
				num = num2;
				gesture.RecognizedTemplate = normalizedTemplate.Source;
				this.debugLastMatchedTemplate = normalizedTemplate;
			}
		}
		if (gesture.RecognizedTemplate != null)
		{
			gesture.MatchDistance = num;
			gesture.MatchScore = Mathf.Max((this.MaxMatchDistance - num) / this.MaxMatchDistance, 0f);
		}
		return gesture.MatchScore > 0f;
	}

	// Token: 0x06000158 RID: 344 RVA: 0x00005E28 File Offset: 0x00004028
	private float GreedyCloudMatch(List<PointCloudRegognizer.Point> points, List<PointCloudRegognizer.Point> refPoints)
	{
		float num = 0.5f;
		int num2 = Mathf.FloorToInt(Mathf.Pow((float)points.Count, 1f - num));
		float num3 = float.PositiveInfinity;
		for (int i = 0; i < points.Count; i += num2)
		{
			float num4 = PointCloudRegognizer.CloudDistance(points, refPoints, i);
			float num5 = PointCloudRegognizer.CloudDistance(refPoints, points, i);
			num3 = Mathf.Min(new float[]
			{
				num3,
				num4,
				num5
			});
		}
		return num3;
	}

	// Token: 0x06000159 RID: 345 RVA: 0x00005EA0 File Offset: 0x000040A0
	private static float CloudDistance(List<PointCloudRegognizer.Point> points1, List<PointCloudRegognizer.Point> points2, int startIndex)
	{
		int count = points1.Count;
		PointCloudRegognizer.ResetMatched(count);
		float num = 0f;
		int num2 = startIndex;
		do
		{
			int num3 = -1;
			float num4 = float.PositiveInfinity;
			for (int i = 0; i < count; i++)
			{
				if (!PointCloudRegognizer.matched[i])
				{
					float num5 = Vector2.Distance(points1[num2].Position, points2[i].Position);
					if (num5 < num4)
					{
						num4 = num5;
						num3 = i;
					}
				}
			}
			PointCloudRegognizer.matched[num3] = true;
			float num6 = (float)(1 - (num2 - startIndex + points1.Count) % points1.Count / points1.Count);
			num += num6 * num4;
			num2 = (num2 + 1) % points1.Count;
		}
		while (num2 != startIndex);
		return num;
	}

	// Token: 0x0600015A RID: 346 RVA: 0x00005F68 File Offset: 0x00004168
	private static void ResetMatched(int count)
	{
		if (PointCloudRegognizer.matched.Length < count)
		{
			PointCloudRegognizer.matched = new bool[count];
		}
		for (int i = 0; i < count; i++)
		{
			PointCloudRegognizer.matched[i] = false;
		}
	}

	// Token: 0x0600015B RID: 347 RVA: 0x00005FA8 File Offset: 0x000041A8
	protected override GestureRecognitionState OnRecognize(PointCloudGesture gesture, FingerGestures.IFingerList touches)
	{
		if (touches.Count == this.RequiredFingerCount)
		{
			gesture.Position = touches.GetAveragePosition();
			Vector2 position = gesture.RawPoints[gesture.RawPoints.Count - 1].Position;
			float num = Vector2.SqrMagnitude(gesture.Position - position);
			if (num > this.MinDistanceBetweenSamples * this.MinDistanceBetweenSamples)
			{
				int strokeId = 0;
				gesture.RawPoints.Add(new PointCloudRegognizer.Point(strokeId, gesture.Position));
			}
			return GestureRecognitionState.InProgress;
		}
		if (touches.Count >= this.RequiredFingerCount)
		{
			return GestureRecognitionState.Failed;
		}
		if (this.RecognizePointCloud(gesture))
		{
			return GestureRecognitionState.Ended;
		}
		return GestureRecognitionState.Failed;
	}

	// Token: 0x0600015C RID: 348 RVA: 0x00006058 File Offset: 0x00004258
	public override string GetDefaultEventMessageName()
	{
		return "OnCustomGesture";
	}

	// Token: 0x0600015D RID: 349 RVA: 0x00006060 File Offset: 0x00004260
	public void OnDrawGizmosSelected()
	{
		if (this.debugLastMatchedTemplate != null)
		{
			Gizmos.color = Color.yellow;
			this.DrawNormalizedPointCloud(this.debugLastMatchedTemplate.Points, 15f);
		}
		if (this.debugLastGesture != null)
		{
			Gizmos.color = Color.green;
			this.DrawNormalizedPointCloud(this.debugLastGesture.NormalizedPoints, 15f);
		}
	}

	// Token: 0x0600015E RID: 350 RVA: 0x000060C4 File Offset: 0x000042C4
	private void DrawNormalizedPointCloud(List<PointCloudRegognizer.Point> points, float scale)
	{
		if (points.Count > 0)
		{
			Gizmos.DrawWireSphere(scale * points[0].Position, 0.01f);
			for (int i = 1; i < points.Count; i++)
			{
				Gizmos.DrawLine(scale * points[i - 1].Position, scale * points[i].Position);
				Gizmos.DrawWireSphere(scale * points[i].Position, 0.01f);
			}
		}
	}

	// Token: 0x040000D2 RID: 210
	private const int NormalizedPointCount = 32;

	// Token: 0x040000D3 RID: 211
	private const float gizmoSphereRadius = 0.01f;

	// Token: 0x040000D4 RID: 212
	public float MinDistanceBetweenSamples = 5f;

	// Token: 0x040000D5 RID: 213
	public float MaxMatchDistance = 3.5f;

	// Token: 0x040000D6 RID: 214
	public List<PointCloudGestureTemplate> Templates;

	// Token: 0x040000D7 RID: 215
	private PointCloudRegognizer.GestureNormalizer normalizer;

	// Token: 0x040000D8 RID: 216
	private List<PointCloudRegognizer.NormalizedTemplate> normalizedTemplates;

	// Token: 0x040000D9 RID: 217
	private static bool[] matched = new bool[32];

	// Token: 0x040000DA RID: 218
	private PointCloudGesture debugLastGesture;

	// Token: 0x040000DB RID: 219
	private PointCloudRegognizer.NormalizedTemplate debugLastMatchedTemplate;

	// Token: 0x02000033 RID: 51
	[Serializable]
	public struct Point
	{
		// Token: 0x0600015F RID: 351 RVA: 0x00006178 File Offset: 0x00004378
		public Point(int strokeId, Vector2 pos)
		{
			this.StrokeId = strokeId;
			this.Position = pos;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00006188 File Offset: 0x00004388
		public Point(int strokeId, float x, float y)
		{
			this.StrokeId = strokeId;
			this.Position = new Vector2(x, y);
		}

		// Token: 0x040000DC RID: 220
		public int StrokeId;

		// Token: 0x040000DD RID: 221
		public Vector2 Position;
	}

	// Token: 0x02000034 RID: 52
	private class NormalizedTemplate
	{
		// Token: 0x040000DE RID: 222
		public PointCloudGestureTemplate Source;

		// Token: 0x040000DF RID: 223
		public List<PointCloudRegognizer.Point> Points;
	}

	// Token: 0x02000035 RID: 53
	private class GestureNormalizer
	{
		// Token: 0x06000162 RID: 354 RVA: 0x000061A8 File Offset: 0x000043A8
		public GestureNormalizer()
		{
			this.normalizedPoints = new List<PointCloudRegognizer.Point>();
			this.pointBuffer = new List<PointCloudRegognizer.Point>();
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000061C8 File Offset: 0x000043C8
		public List<PointCloudRegognizer.Point> Apply(List<PointCloudRegognizer.Point> inputPoints, int normalizedPointsCount)
		{
			this.normalizedPoints = this.Resample(inputPoints, normalizedPointsCount);
			PointCloudRegognizer.GestureNormalizer.Scale(this.normalizedPoints);
			PointCloudRegognizer.GestureNormalizer.TranslateToOrigin(this.normalizedPoints);
			return this.normalizedPoints;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00006200 File Offset: 0x00004400
		private List<PointCloudRegognizer.Point> Resample(List<PointCloudRegognizer.Point> points, int normalizedPointsCount)
		{
			float num = PointCloudRegognizer.GestureNormalizer.PathLength(points) / (float)(normalizedPointsCount - 1);
			float num2 = 0f;
			PointCloudRegognizer.Point item = default(PointCloudRegognizer.Point);
			this.normalizedPoints.Clear();
			this.normalizedPoints.Add(points[0]);
			this.pointBuffer.Clear();
			this.pointBuffer.AddRange(points);
			for (int i = 1; i < this.pointBuffer.Count; i++)
			{
				PointCloudRegognizer.Point point = this.pointBuffer[i - 1];
				PointCloudRegognizer.Point point2 = this.pointBuffer[i];
				if (point.StrokeId == point2.StrokeId)
				{
					float num3 = Vector2.Distance(point.Position, point2.Position);
					if (num2 + num3 > num)
					{
						item.Position = Vector2.Lerp(point.Position, point2.Position, (num - num2) / num3);
						item.StrokeId = point.StrokeId;
						this.normalizedPoints.Add(item);
						this.pointBuffer.Insert(i, item);
						num2 = 0f;
					}
					else
					{
						num2 += num3;
					}
				}
			}
			if (this.normalizedPoints.Count == normalizedPointsCount - 1)
			{
				this.normalizedPoints.Add(this.pointBuffer[this.pointBuffer.Count - 1]);
			}
			return this.normalizedPoints;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000635C File Offset: 0x0000455C
		private static float PathLength(List<PointCloudRegognizer.Point> points)
		{
			float num = 0f;
			for (int i = 1; i < points.Count; i++)
			{
				if (points[i].StrokeId == points[i - 1].StrokeId)
				{
					num += Vector2.Distance(points[i - 1].Position, points[i].Position);
				}
			}
			return num;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000063D8 File Offset: 0x000045D8
		private static void Scale(List<PointCloudRegognizer.Point> points)
		{
			Vector2 b = new Vector2(float.PositiveInfinity, float.PositiveInfinity);
			Vector2 vector = new Vector2(float.NegativeInfinity, float.NegativeInfinity);
			for (int i = 0; i < points.Count; i++)
			{
				PointCloudRegognizer.Point point = points[i];
				b.x = Mathf.Min(b.x, point.Position.x);
				b.y = Mathf.Min(b.y, point.Position.y);
				vector.x = Mathf.Max(vector.x, point.Position.x);
				vector.y = Mathf.Max(vector.y, point.Position.y);
			}
			float num = Mathf.Max(vector.x - b.x, vector.y - b.y);
			float d = 1f / num;
			for (int j = 0; j < points.Count; j++)
			{
				PointCloudRegognizer.Point value = points[j];
				value.Position = (value.Position - b) * d;
				points[j] = value;
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000651C File Offset: 0x0000471C
		private static void TranslateToOrigin(List<PointCloudRegognizer.Point> points)
		{
			Vector2 b = PointCloudRegognizer.GestureNormalizer.Centroid(points);
			for (int i = 0; i < points.Count; i++)
			{
				PointCloudRegognizer.Point value = points[i];
				value.Position -= b;
				points[i] = value;
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000656C File Offset: 0x0000476C
		private static Vector2 Centroid(List<PointCloudRegognizer.Point> points)
		{
			Vector2 a = Vector2.zero;
			for (int i = 0; i < points.Count; i++)
			{
				a += points[i].Position;
			}
			return a / (float)points.Count;
		}

		// Token: 0x040000E0 RID: 224
		private List<PointCloudRegognizer.Point> normalizedPoints;

		// Token: 0x040000E1 RID: 225
		private List<PointCloudRegognizer.Point> pointBuffer;
	}
}
