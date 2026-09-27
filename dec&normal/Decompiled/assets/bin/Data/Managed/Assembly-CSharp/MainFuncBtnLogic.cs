using System;
using UnityEngine;

// Token: 0x020009F9 RID: 2553
public class MainFuncBtnLogic : MonoBehaviour
{
	// Token: 0x06004909 RID: 18697 RVA: 0x00178780 File Offset: 0x00176980
	public void SetState(FUNCTION_TYPE functype, bool Islockshow = false)
	{
		this.curType = functype;
		if (!Islockshow)
		{
			if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(functype))
			{
				NGUITools.SetActive(base.gameObject, true);
				this.IconSp.alpha = 1f;
			}
			else
			{
				NGUITools.SetActive(base.gameObject, false);
			}
		}
		else
		{
			NGUITools.SetActive(base.gameObject, true);
			if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(functype))
			{
				if (this.LockFlag != null)
				{
					this.LockFlag.alpha = 0f;
				}
				this.IconSp.alpha = 1f;
			}
			else if (this.LockFlag != null)
			{
				this.LockFlag.alpha = 1f;
			}
		}
	}

	// Token: 0x0600490A RID: 18698 RVA: 0x0017885C File Offset: 0x00176A5C
	public void SetTips(bool istrue, FUNCTION_TYPE functype)
	{
		this.curType = functype;
		if (this.TipsFlag != null)
		{
			if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(functype))
			{
				this.TipsFlag.enabled = istrue;
			}
			else
			{
				this.TipsFlag.enabled = false;
			}
		}
	}

	// Token: 0x0600490B RID: 18699 RVA: 0x001788B4 File Offset: 0x00176AB4
	public void SetDirState(bool isopen)
	{
		if (isopen)
		{
			this.DirAnima.ResetToBeginning();
			this.DirAnima.enabled = false;
		}
		else
		{
			this.DirAnima.PlayForward();
			this.DirAnima.transform.localRotation = Quaternion.Euler(this.DirAnima.to);
			this.DirAnima.enabled = false;
		}
	}

	// Token: 0x0600490C RID: 18700 RVA: 0x0017891C File Offset: 0x00176B1C
	public void ShowDirAnima(bool isopen)
	{
		if (isopen)
		{
			this.DirAnima.PlayForward();
		}
		else
		{
			this.DirAnima.PlayReverse();
		}
	}

	// Token: 0x0400362C RID: 13868
	public UISprite IconSp;

	// Token: 0x0400362D RID: 13869
	public UISprite TipsFlag;

	// Token: 0x0400362E RID: 13870
	public UIWidget LockFlag;

	// Token: 0x0400362F RID: 13871
	public UILabel BtnLabel;

	// Token: 0x04003630 RID: 13872
	private FUNCTION_TYPE curType;

	// Token: 0x04003631 RID: 13873
	public TweenRotation DirAnima;
}
