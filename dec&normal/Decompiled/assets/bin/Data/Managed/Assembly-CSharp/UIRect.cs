using System;
using UnityEngine;

// Token: 0x02000099 RID: 153
public abstract class UIRect : MonoBehaviour
{
	// Token: 0x17000077 RID: 119
	// (get) Token: 0x060003F7 RID: 1015 RVA: 0x0001CDDC File Offset: 0x0001AFDC
	public GameObject cachedGameObject
	{
		get
		{
			if (this.mGo == null)
			{
				this.mGo = base.gameObject;
			}
			return this.mGo;
		}
	}

	// Token: 0x17000078 RID: 120
	// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0001CE04 File Offset: 0x0001B004
	public Transform cachedTransform
	{
		get
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
			}
			return this.mTrans;
		}
	}

	// Token: 0x17000079 RID: 121
	// (get) Token: 0x060003F9 RID: 1017 RVA: 0x0001CE2C File Offset: 0x0001B02C
	public Camera anchorCamera
	{
		get
		{
			if (!this.mAnchorsCached)
			{
				this.ResetAnchors();
			}
			return this.mMyCam;
		}
	}

	// Token: 0x1700007A RID: 122
	// (get) Token: 0x060003FA RID: 1018 RVA: 0x0001CE48 File Offset: 0x0001B048
	public bool isFullyAnchored
	{
		get
		{
			return this.leftAnchor.target && this.rightAnchor.target && this.topAnchor.target && this.bottomAnchor.target;
		}
	}

	// Token: 0x1700007B RID: 123
	// (get) Token: 0x060003FB RID: 1019 RVA: 0x0001CEA8 File Offset: 0x0001B0A8
	public virtual bool isAnchoredHorizontally
	{
		get
		{
			return this.leftAnchor.target || this.rightAnchor.target;
		}
	}

	// Token: 0x1700007C RID: 124
	// (get) Token: 0x060003FC RID: 1020 RVA: 0x0001CEE0 File Offset: 0x0001B0E0
	public virtual bool isAnchoredVertically
	{
		get
		{
			return this.bottomAnchor.target || this.topAnchor.target;
		}
	}

	// Token: 0x1700007D RID: 125
	// (get) Token: 0x060003FD RID: 1021 RVA: 0x0001CF18 File Offset: 0x0001B118
	public virtual bool canBeAnchored
	{
		get
		{
			return true;
		}
	}

	// Token: 0x1700007E RID: 126
	// (get) Token: 0x060003FE RID: 1022 RVA: 0x0001CF1C File Offset: 0x0001B11C
	public UIRect parent
	{
		get
		{
			if (!this.mParentFound)
			{
				this.mParentFound = true;
				this.mParent = NGUITools.FindInParents<UIRect>(this.cachedTransform.parent);
			}
			return this.mParent;
		}
	}

	// Token: 0x1700007F RID: 127
	// (get) Token: 0x060003FF RID: 1023 RVA: 0x0001CF58 File Offset: 0x0001B158
	public UIRoot root
	{
		get
		{
			if (this.parent != null)
			{
				return this.mParent.root;
			}
			if (!this.mRootSet)
			{
				this.mRootSet = true;
				this.mRoot = NGUITools.FindInParents<UIRoot>(this.cachedTransform);
			}
			return this.mRoot;
		}
	}

	// Token: 0x17000080 RID: 128
	// (get) Token: 0x06000400 RID: 1024 RVA: 0x0001CFAC File Offset: 0x0001B1AC
	public bool isAnchored
	{
		get
		{
			return (this.leftAnchor.target || this.rightAnchor.target || this.topAnchor.target || this.bottomAnchor.target) && this.canBeAnchored;
		}
	}

	// Token: 0x17000081 RID: 129
	// (get) Token: 0x06000401 RID: 1025
	// (set) Token: 0x06000402 RID: 1026
	public abstract float alpha { get; set; }

	// Token: 0x06000403 RID: 1027
	public abstract float CalculateFinalAlpha(int frameID);

	// Token: 0x17000082 RID: 130
	// (get) Token: 0x06000404 RID: 1028
	public abstract Vector3[] localCorners { get; }

	// Token: 0x17000083 RID: 131
	// (get) Token: 0x06000405 RID: 1029
	public abstract Vector3[] worldCorners { get; }

	// Token: 0x06000406 RID: 1030 RVA: 0x0001D018 File Offset: 0x0001B218
	public virtual void Invalidate(bool includeChildren)
	{
		this.mChanged = true;
		if (includeChildren)
		{
			for (int i = 0; i < this.mChildren.size; i++)
			{
				this.mChildren.buffer[i].Invalidate(true);
			}
		}
	}

	// Token: 0x06000407 RID: 1031 RVA: 0x0001D064 File Offset: 0x0001B264
	public virtual Vector3[] GetSides(Transform relativeTo)
	{
		if (this.anchorCamera != null)
		{
			return this.anchorCamera.GetSides(relativeTo);
		}
		Vector3 position = this.cachedTransform.position;
		for (int i = 0; i < 4; i++)
		{
			UIRect.mSides[i] = position;
		}
		if (relativeTo != null)
		{
			for (int j = 0; j < 4; j++)
			{
				UIRect.mSides[j] = relativeTo.InverseTransformPoint(UIRect.mSides[j]);
			}
		}
		return UIRect.mSides;
	}

	// Token: 0x06000408 RID: 1032 RVA: 0x0001D108 File Offset: 0x0001B308
	protected Vector3 GetLocalPos(UIRect.AnchorPoint ac, Transform trans)
	{
		if (this.anchorCamera == null || ac.targetCam == null)
		{
			return this.cachedTransform.localPosition;
		}
		Vector3 vector = this.mMyCam.ViewportToWorldPoint(ac.targetCam.WorldToViewportPoint(ac.target.position));
		if (trans != null)
		{
			vector = trans.InverseTransformPoint(vector);
		}
		vector.x = Mathf.Floor(vector.x + 0.5f);
		vector.y = Mathf.Floor(vector.y + 0.5f);
		return vector;
	}

	// Token: 0x06000409 RID: 1033 RVA: 0x0001D1AC File Offset: 0x0001B3AC
	protected virtual void OnEnable()
	{
		this.mAnchorsCached = false;
		if (this.updateAnchors == UIRect.AnchorUpdate.OnEnable)
		{
			this.mUpdateAnchors = true;
		}
		if (this.mStarted)
		{
			this.OnInit();
		}
	}

	// Token: 0x0600040A RID: 1034 RVA: 0x0001D1E4 File Offset: 0x0001B3E4
	protected virtual void OnInit()
	{
		this.mChanged = true;
		this.mRootSet = false;
		this.mParentFound = false;
		if (this.parent != null)
		{
			this.mParent.mChildren.Add(this);
		}
	}

	// Token: 0x0600040B RID: 1035 RVA: 0x0001D220 File Offset: 0x0001B420
	protected virtual void OnDisable()
	{
		if (this.mParent)
		{
			this.mParent.mChildren.Remove(this);
		}
		this.mParent = null;
		this.mRoot = null;
		this.mRootSet = false;
		this.mParentFound = false;
	}

	// Token: 0x0600040C RID: 1036 RVA: 0x0001D26C File Offset: 0x0001B46C
	protected void Start()
	{
		this.mStarted = true;
		this.OnInit();
		this.OnStart();
	}

	// Token: 0x0600040D RID: 1037 RVA: 0x0001D284 File Offset: 0x0001B484
	public void Update()
	{
		if (!this.mAnchorsCached)
		{
			this.ResetAnchors();
		}
		int frameCount = Time.frameCount;
		if (this.mUpdateFrame != frameCount)
		{
			if (this.updateAnchors == UIRect.AnchorUpdate.OnUpdate || this.mUpdateAnchors)
			{
				this.mUpdateFrame = frameCount;
				this.mUpdateAnchors = false;
				bool flag = false;
				if (this.leftAnchor.target)
				{
					flag = true;
					if (this.leftAnchor.rect != null && this.leftAnchor.rect.mUpdateFrame != frameCount)
					{
						this.leftAnchor.rect.Update();
					}
				}
				if (this.bottomAnchor.target)
				{
					flag = true;
					if (this.bottomAnchor.rect != null && this.bottomAnchor.rect.mUpdateFrame != frameCount)
					{
						this.bottomAnchor.rect.Update();
					}
				}
				if (this.rightAnchor.target)
				{
					flag = true;
					if (this.rightAnchor.rect != null && this.rightAnchor.rect.mUpdateFrame != frameCount)
					{
						this.rightAnchor.rect.Update();
					}
				}
				if (this.topAnchor.target)
				{
					flag = true;
					if (this.topAnchor.rect != null && this.topAnchor.rect.mUpdateFrame != frameCount)
					{
						this.topAnchor.rect.Update();
					}
				}
				if (flag)
				{
					this.OnAnchor();
				}
			}
			this.OnUpdate();
		}
	}

	// Token: 0x0600040E RID: 1038 RVA: 0x0001D43C File Offset: 0x0001B63C
	public void UpdateAnchors()
	{
		if (this.isAnchored)
		{
			this.OnAnchor();
		}
	}

	// Token: 0x0600040F RID: 1039
	protected abstract void OnAnchor();

	// Token: 0x06000410 RID: 1040 RVA: 0x0001D450 File Offset: 0x0001B650
	public void SetAnchor(Transform t)
	{
		this.leftAnchor.target = t;
		this.rightAnchor.target = t;
		this.topAnchor.target = t;
		this.bottomAnchor.target = t;
		this.ResetAnchors();
		this.UpdateAnchors();
	}

	// Token: 0x06000411 RID: 1041 RVA: 0x0001D49C File Offset: 0x0001B69C
	public void SetAnchor(GameObject go)
	{
		Transform target = (!(go != null)) ? null : go.transform;
		this.leftAnchor.target = target;
		this.rightAnchor.target = target;
		this.topAnchor.target = target;
		this.bottomAnchor.target = target;
		this.ResetAnchors();
		this.UpdateAnchors();
	}

	// Token: 0x06000412 RID: 1042 RVA: 0x0001D500 File Offset: 0x0001B700
	public void SetAnchor(GameObject go, int left, int bottom, int right, int top)
	{
		Transform target = (!(go != null)) ? null : go.transform;
		this.leftAnchor.target = target;
		this.rightAnchor.target = target;
		this.topAnchor.target = target;
		this.bottomAnchor.target = target;
		this.leftAnchor.relative = 0f;
		this.rightAnchor.relative = 1f;
		this.bottomAnchor.relative = 0f;
		this.topAnchor.relative = 1f;
		this.leftAnchor.absolute = left;
		this.rightAnchor.absolute = right;
		this.bottomAnchor.absolute = bottom;
		this.topAnchor.absolute = top;
		this.ResetAnchors();
		this.UpdateAnchors();
	}

	// Token: 0x06000413 RID: 1043 RVA: 0x0001D5D4 File Offset: 0x0001B7D4
	public void ResetAnchors()
	{
		this.mAnchorsCached = true;
		this.leftAnchor.rect = ((!this.leftAnchor.target) ? null : this.leftAnchor.target.GetComponent<UIRect>());
		this.bottomAnchor.rect = ((!this.bottomAnchor.target) ? null : this.bottomAnchor.target.GetComponent<UIRect>());
		this.rightAnchor.rect = ((!this.rightAnchor.target) ? null : this.rightAnchor.target.GetComponent<UIRect>());
		this.topAnchor.rect = ((!this.topAnchor.target) ? null : this.topAnchor.target.GetComponent<UIRect>());
		this.mMyCam = NGUITools.FindCameraForLayer(this.cachedGameObject.layer);
		this.FindCameraFor(this.leftAnchor);
		this.FindCameraFor(this.bottomAnchor);
		this.FindCameraFor(this.rightAnchor);
		this.FindCameraFor(this.topAnchor);
		this.mUpdateAnchors = true;
	}

	// Token: 0x06000414 RID: 1044
	public abstract void SetRect(float x, float y, float width, float height);

	// Token: 0x06000415 RID: 1045 RVA: 0x0001D710 File Offset: 0x0001B910
	private void FindCameraFor(UIRect.AnchorPoint ap)
	{
		if (ap.target == null || ap.rect != null)
		{
			ap.targetCam = null;
		}
		else
		{
			ap.targetCam = NGUITools.FindCameraForLayer(ap.target.gameObject.layer);
		}
	}

	// Token: 0x06000416 RID: 1046 RVA: 0x0001D768 File Offset: 0x0001B968
	public virtual void ParentHasChanged()
	{
		this.mParentFound = false;
		UIRect uirect = NGUITools.FindInParents<UIRect>(this.cachedTransform.parent);
		if (this.mParent != uirect)
		{
			if (this.mParent)
			{
				this.mParent.mChildren.Remove(this);
			}
			this.mParent = uirect;
			if (this.mParent)
			{
				this.mParent.mChildren.Add(this);
			}
			this.mRootSet = false;
		}
	}

	// Token: 0x06000417 RID: 1047
	protected abstract void OnStart();

	// Token: 0x06000418 RID: 1048 RVA: 0x0001D7F0 File Offset: 0x0001B9F0
	protected virtual void OnUpdate()
	{
	}

	// Token: 0x040003AE RID: 942
	public UIRect.AnchorPoint leftAnchor = new UIRect.AnchorPoint();

	// Token: 0x040003AF RID: 943
	public UIRect.AnchorPoint rightAnchor = new UIRect.AnchorPoint(1f);

	// Token: 0x040003B0 RID: 944
	public UIRect.AnchorPoint bottomAnchor = new UIRect.AnchorPoint();

	// Token: 0x040003B1 RID: 945
	public UIRect.AnchorPoint topAnchor = new UIRect.AnchorPoint(1f);

	// Token: 0x040003B2 RID: 946
	public UIRect.AnchorUpdate updateAnchors = UIRect.AnchorUpdate.OnUpdate;

	// Token: 0x040003B3 RID: 947
	protected GameObject mGo;

	// Token: 0x040003B4 RID: 948
	protected Transform mTrans;

	// Token: 0x040003B5 RID: 949
	protected BetterList<UIRect> mChildren = new BetterList<UIRect>();

	// Token: 0x040003B6 RID: 950
	protected bool mChanged = true;

	// Token: 0x040003B7 RID: 951
	protected bool mStarted;

	// Token: 0x040003B8 RID: 952
	protected bool mParentFound;

	// Token: 0x040003B9 RID: 953
	protected bool mUpdateAnchors;

	// Token: 0x040003BA RID: 954
	[NonSerialized]
	public float finalAlpha = 1f;

	// Token: 0x040003BB RID: 955
	private UIRoot mRoot;

	// Token: 0x040003BC RID: 956
	private UIRect mParent;

	// Token: 0x040003BD RID: 957
	private Camera mMyCam;

	// Token: 0x040003BE RID: 958
	private int mUpdateFrame = -1;

	// Token: 0x040003BF RID: 959
	private bool mAnchorsCached;

	// Token: 0x040003C0 RID: 960
	private bool mRootSet;

	// Token: 0x040003C1 RID: 961
	private static Vector3[] mSides = new Vector3[4];

	// Token: 0x0200009A RID: 154
	[Serializable]
	public class AnchorPoint
	{
		// Token: 0x06000419 RID: 1049 RVA: 0x0001D7F4 File Offset: 0x0001B9F4
		public AnchorPoint()
		{
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0001D7FC File Offset: 0x0001B9FC
		public AnchorPoint(float relative)
		{
			this.relative = relative;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0001D80C File Offset: 0x0001BA0C
		public void Set(float relative, float absolute)
		{
			this.relative = relative;
			this.absolute = Mathf.FloorToInt(absolute + 0.5f);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0001D828 File Offset: 0x0001BA28
		public void SetToNearest(float abs0, float abs1, float abs2)
		{
			this.SetToNearest(0f, 0.5f, 1f, abs0, abs1, abs2);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0001D844 File Offset: 0x0001BA44
		public void SetToNearest(float rel0, float rel1, float rel2, float abs0, float abs1, float abs2)
		{
			float num = Mathf.Abs(abs0);
			float num2 = Mathf.Abs(abs1);
			float num3 = Mathf.Abs(abs2);
			if (num < num2 && num < num3)
			{
				this.Set(rel0, abs0);
			}
			else if (num2 < num && num2 < num3)
			{
				this.Set(rel1, abs1);
			}
			else
			{
				this.Set(rel2, abs2);
			}
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0001D8AC File Offset: 0x0001BAAC
		public void SetHorizontal(Transform parent, float localPos)
		{
			if (this.rect)
			{
				Vector3[] sides = this.rect.GetSides(parent);
				float num = Mathf.Lerp(sides[0].x, sides[2].x, this.relative);
				this.absolute = Mathf.FloorToInt(localPos - num + 0.5f);
			}
			else
			{
				Vector3 vector = this.target.position;
				if (parent != null)
				{
					vector = parent.InverseTransformPoint(vector);
				}
				this.absolute = Mathf.FloorToInt(localPos - vector.x + 0.5f);
			}
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0001D950 File Offset: 0x0001BB50
		public void SetVertical(Transform parent, float localPos)
		{
			if (this.rect)
			{
				Vector3[] sides = this.rect.GetSides(parent);
				float num = Mathf.Lerp(sides[3].y, sides[1].y, this.relative);
				this.absolute = Mathf.FloorToInt(localPos - num + 0.5f);
			}
			else
			{
				Vector3 vector = this.target.position;
				if (parent != null)
				{
					vector = parent.InverseTransformPoint(vector);
				}
				this.absolute = Mathf.FloorToInt(localPos - vector.y + 0.5f);
			}
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0001D9F4 File Offset: 0x0001BBF4
		public Vector3[] GetSides(Transform relativeTo)
		{
			if (this.target != null)
			{
				if (this.rect != null)
				{
					return this.rect.GetSides(relativeTo);
				}
				if (this.target.camera != null)
				{
					return this.target.camera.GetSides(relativeTo);
				}
			}
			return null;
		}

		// Token: 0x040003C2 RID: 962
		public Transform target;

		// Token: 0x040003C3 RID: 963
		public float relative;

		// Token: 0x040003C4 RID: 964
		public int absolute;

		// Token: 0x040003C5 RID: 965
		[NonSerialized]
		public UIRect rect;

		// Token: 0x040003C6 RID: 966
		[NonSerialized]
		public Camera targetCam;
	}

	// Token: 0x0200009B RID: 155
	public enum AnchorUpdate
	{
		// Token: 0x040003C8 RID: 968
		OnEnable,
		// Token: 0x040003C9 RID: 969
		OnUpdate
	}
}
