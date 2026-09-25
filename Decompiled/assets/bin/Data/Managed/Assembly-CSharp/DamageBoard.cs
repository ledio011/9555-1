using System;
using System.Text;
using UnityEngine;

// Token: 0x020009DC RID: 2524
public class DamageBoard : MonoBehaviour
{
	// Token: 0x17000FB8 RID: 4024
	// (get) Token: 0x060047BA RID: 18362 RVA: 0x0016EC3C File Offset: 0x0016CE3C
	// (set) Token: 0x060047BB RID: 18363 RVA: 0x0016EC44 File Offset: 0x0016CE44
	public float ShowTime
	{
		get
		{
			return this.mShowTime;
		}
		set
		{
			this.mShowTime = value;
		}
	}

	// Token: 0x060047BC RID: 18364 RVA: 0x0016EC50 File Offset: 0x0016CE50
	private void Init()
	{
		if (this.mDamageBoadTransform == null)
		{
			this.mDamageBoadTransform = base.transform;
		}
		if (this.mDamageBoardLabel == null)
		{
			this.mDamageBoardLabel = base.gameObject.GetComponent<UILabel>();
			if (this.mDamageBoardLabel == null)
			{
				this.mDamageBoardLabel = base.gameObject.AddComponent<UILabel>();
			}
		}
		if (this.mCameraTransform == null)
		{
			this.mCameraTransform = Camera.mainCamera.transform;
		}
	}

	// Token: 0x060047BD RID: 18365 RVA: 0x0016ECE0 File Offset: 0x0016CEE0
	public void Reuse()
	{
	}

	// Token: 0x060047BE RID: 18366 RVA: 0x0016ECE4 File Offset: 0x0016CEE4
	public void ShowDamageBoard_01(int nType, string strValue, Vector3 pos)
	{
		if (DataManager.GetDamageBoardTypeDataById(nType) == null)
		{
			DamageBoardTypeData damageBoardTypeDataById = DataManager.GetDamageBoardTypeDataById(1);
		}
		this.mType = nType;
	}

	// Token: 0x060047BF RID: 18367 RVA: 0x0016ED0C File Offset: 0x0016CF0C
	public void ShowDamgeBoard(int nType, string strValue, Vector3 pos)
	{
		DamageBoardTypeData damageBoardTypeDataById = DataManager.GetDamageBoardTypeDataById(nType);
		if (damageBoardTypeDataById == null)
		{
			damageBoardTypeDataById = DataManager.GetDamageBoardTypeDataById(1);
		}
		this.Init();
		if (this.TweenArray == null)
		{
			this.TweenArray = base.gameObject.GetComponents<UITweener>();
		}
		pos.y += 2f;
		if (this.parent == null)
		{
			this.parent = new GameObject();
			this.parent.transform.parent = this.mDamageBoadTransform.parent;
		}
		this.parent.transform.localScale = Vector3.one;
		this.parent.transform.localEulerAngles = Vector3.zero;
		this.parent.transform.localPosition = Vector3.zero;
		this.mDamageBoadTransform.parent = this.parent.transform;
		this.mDamageBoadTransform.localPosition = Vector3.zero;
		this.mDamageBoadTransform.localScale = Vector3.one;
		this.mDamageBoadTransform.localEulerAngles = Vector3.zero;
		this.parent.transform.position = pos;
		for (int i = 0; i < this.TweenArray.Length; i++)
		{
			this.TweenArray[i].ResetToBeginning();
		}
		for (int j = 0; j < this.TweenArray.Length; j++)
		{
			this.TweenArray[j].enabled = true;
			this.TweenArray[j].Play();
		}
		this.parent.transform.localRotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
		Vector3 vector = this.mCameraTransform.rotation * Vector3.up;
		this.parent.transform.LookAt(this.mDamageBoadTransform.position + this.mCameraTransform.rotation * Vector3.back, vector);
		this.parent.transform.Rotate(Vector3.up * 180f);
		this.sb.Length = 0;
		this.sb.AppendFormat("{0}", strValue);
		this.mDamageBoardLabel.text = this.sb.ToString();
		this.mDamageBoardLabel.alpha = 1f;
		this.mShowTime = Time.time + damageBoardTypeDataById.ShowTime;
	}

	// Token: 0x060047C0 RID: 18368 RVA: 0x0016EF6C File Offset: 0x0016D16C
	private void Update()
	{
		if (this.parent != null && this.mDamageBoardLabel.alpha > 0.1f)
		{
			this.parent.transform.localRotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
			Vector3 vector = this.mCameraTransform.rotation * Vector3.up;
			this.parent.transform.LookAt(this.mDamageBoadTransform.position + this.mCameraTransform.rotation * Vector3.back, vector);
			this.parent.transform.Rotate(Vector3.up * 180f);
			float num = Vector3.Distance(base.transform.position, this.mCameraTransform.position);
			this.parent.transform.localScale = Vector3.Lerp(new Vector3(0.2f, 0.2f, 0.2f), Vector3.one, num / 10f);
		}
	}

	// Token: 0x060047C1 RID: 18369 RVA: 0x0016F07C File Offset: 0x0016D27C
	private void Awake()
	{
		this.Init();
	}

	// Token: 0x04003515 RID: 13589
	private UILabel mDamageBoardLabel;

	// Token: 0x04003516 RID: 13590
	private UITweener[] TweenArray;

	// Token: 0x04003517 RID: 13591
	private float mShowTime;

	// Token: 0x04003518 RID: 13592
	private Transform mDamageBoadTransform;

	// Token: 0x04003519 RID: 13593
	private int mType;

	// Token: 0x0400351A RID: 13594
	private Transform mCameraTransform;

	// Token: 0x0400351B RID: 13595
	private StringBuilder sb = new StringBuilder(512);

	// Token: 0x0400351C RID: 13596
	private GameObject parent;
}
