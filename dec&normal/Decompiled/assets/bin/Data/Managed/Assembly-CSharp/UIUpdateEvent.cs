using System;

// Token: 0x02000A61 RID: 2657
public class UIUpdateEvent
{
	// Token: 0x04003C47 RID: 15431
	public static UIUpdateEvent.UpdateNoParamEvent UpdateMoneyEvent;

	// Token: 0x04003C48 RID: 15432
	public static UIUpdateEvent.UpdateNoParamEvent UpdateBackPackEvent;

	// Token: 0x04003C49 RID: 15433
	public static UIUpdateEvent.UpdateNoParamEvent UpdateBadgeBackPackEvent;

	// Token: 0x04003C4A RID: 15434
	public static UIUpdateEvent.UpdateNoParamEvent UpdateBadgeEquipPackEvent;

	// Token: 0x04003C4B RID: 15435
	public static UIUpdateEvent.UpdateNoParamEvent SyncBackPackEvent;

	// Token: 0x04003C4C RID: 15436
	public static UIUpdateEvent.OnStoryShowOverDelegate OnStoryShowOver;

	// Token: 0x04003C4D RID: 15437
	public static UIUpdateEvent.UpdateNoParamEvent LevelUpEvent;

	// Token: 0x04003C4E RID: 15438
	public static DelegateDefine.NoParamDelegate OnUIPageLoadFinished;

	// Token: 0x04003C4F RID: 15439
	public static DelegateDefine.NoParamDelegate OnChangeTeam;

	// Token: 0x04003C50 RID: 15440
	public static DelegateDefine.NoParamDelegate OnChangeGuild;

	// Token: 0x04003C51 RID: 15441
	public static UIUpdateEvent.UpdateNoParamEvent OnReshowBase;

	// Token: 0x02000B06 RID: 2822
	// (Invoke) Token: 0x060050A1 RID: 20641
	public delegate void UpdateNoParamEvent();

	// Token: 0x02000B07 RID: 2823
	// (Invoke) Token: 0x060050A5 RID: 20645
	public delegate void OnStoryShowOverDelegate(string storyId);
}
