using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020008BC RID: 2236
[Serializable]
public class SceneAnimationObjData
{
	// Token: 0x04002779 RID: 10105
	public Animation AnimationObj;

	// Token: 0x0400277A RID: 10106
	public Animation SubAnimationObj;

	// Token: 0x0400277B RID: 10107
	public List<string> AnimationNameList;

	// Token: 0x0400277C RID: 10108
	public List<string> SubAniamtionNameList;

	// Token: 0x0400277D RID: 10109
	public List<float> AnimationSpeedList;

	// Token: 0x0400277E RID: 10110
	public Transform SubObjRoot;

	// Token: 0x0400277F RID: 10111
	public FakeObjLogic SubFakeObj;

	// Token: 0x04002780 RID: 10112
	public string SubModelId;

	// Token: 0x04002781 RID: 10113
	public CharacterModelData SubModelData;
}
