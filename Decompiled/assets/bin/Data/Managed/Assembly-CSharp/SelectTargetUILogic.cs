using System;
using UnityEngine;

// Token: 0x02000A45 RID: 2629
public class SelectTargetUILogic : SingletonUnity<SelectTargetUILogic>
{
	// Token: 0x06004CAE RID: 19630 RVA: 0x0019FD38 File Offset: 0x0019DF38
	private void Start()
	{
		this.screenWidth = Mathf.RoundToInt(480f * ((float)Screen.width / (float)Screen.height));
		this.mMainCamera = Camera.main;
	}

	// Token: 0x06004CAF RID: 19631 RVA: 0x0019FD64 File Offset: 0x0019DF64
	private void Update()
	{
		if (this.mMainPlayer == null)
		{
			this.mMainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
			if (this.mMainPlayer == null)
			{
				return;
			}
		}
		if (!UnityVersionUtil.IsActive(this.targetPic.gameObject))
		{
			if (this.mMainPlayer.SelectedTarget != null && !this.mMainPlayer.SelectedTarget.IsDie && UnityVersionUtil.IsActive(this.mMainPlayer.SelectedTarget.gameObject) && this.TargetDistance(this.mMainPlayer, this.mMainPlayer.SelectedTarget))
			{
				UnityVersionUtil.SetActiveRecursive(this.targetPic.gameObject, true);
			}
			if (this.mMainPlayer.SelectedTarget == null || this.mMainPlayer.SelectedTarget.IsDie || !UnityVersionUtil.IsActive(this.mMainPlayer.SelectedTarget.gameObject))
			{
				UnityVersionUtil.SetActiveRecursive(this.targetPic.gameObject, false);
				return;
			}
			Vector3 vector = this.mMainCamera.WorldToViewportPoint(this.mMainPlayer.SelectedTarget.Position + Vector3.up * 1.3f);
			if (vector.z < 0f)
			{
				UnityVersionUtil.SetActiveRecursive(this.targetPic.gameObject, false);
				return;
			}
			Vector3 localPosition;
			localPosition..ctor(vector.x * (float)this.screenWidth, vector.y * 480f, 0f);
			this.targetPic.transform.localPosition = localPosition;
			if (this.targetPic.transform.localPosition.x < -20f || this.targetPic.transform.localPosition.x > (float)(this.screenWidth + 20) || this.targetPic.transform.localPosition.y < -10f || this.targetPic.transform.localPosition.y > 490f)
			{
				this.mMainPlayer.SelectTarget(null);
			}
		}
		else
		{
			if (this.mMainPlayer.SelectedTarget == null || this.mMainPlayer.SelectedTarget.IsDie || !UnityVersionUtil.IsActive(this.mMainPlayer.SelectedTarget.gameObject))
			{
				UnityVersionUtil.SetActiveRecursive(this.targetPic.gameObject, false);
				return;
			}
			Vector3 vector2 = this.mMainCamera.WorldToViewportPoint(this.mMainPlayer.SelectedTarget.Position + Vector3.up * 1.3f);
			if (vector2.z < 0f)
			{
				UnityVersionUtil.SetActiveRecursive(this.targetPic.gameObject, false);
				return;
			}
			Vector3 localPosition2;
			localPosition2..ctor(vector2.x * (float)this.screenWidth, vector2.y * 480f, 0f);
			this.targetPic.transform.localPosition = localPosition2;
			if (this.targetPic.transform.localPosition.x < -20f || this.targetPic.transform.localPosition.x > (float)(this.screenWidth + 20) || this.targetPic.transform.localPosition.y < -10f || this.targetPic.transform.localPosition.y > 490f)
			{
				this.mMainPlayer.SelectTarget(null);
			}
		}
	}

	// Token: 0x06004CB0 RID: 19632 RVA: 0x001A0130 File Offset: 0x0019E330
	private bool TargetDistance(ObjMainPlayer mainPlayer, ObjCharacter obj)
	{
		return Vector3.SqrMagnitude(mainPlayer.Position - obj.Position) < 100f;
	}

	// Token: 0x04003A4F RID: 14927
	public UISprite targetPic;

	// Token: 0x04003A50 RID: 14928
	private int screenWidth;

	// Token: 0x04003A51 RID: 14929
	private Camera mMainCamera;

	// Token: 0x04003A52 RID: 14930
	private ObjMainPlayer mMainPlayer;
}
