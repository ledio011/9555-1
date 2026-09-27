using System;
using System.Collections.Generic;

// Token: 0x020000E1 RID: 225
public class AIManager : SingletonUnity<AIManager>
{
	// Token: 0x17000166 RID: 358
	// (get) Token: 0x06000719 RID: 1817 RVA: 0x00031F00 File Offset: 0x00030100
	// (set) Token: 0x0600071A RID: 1818 RVA: 0x00031F08 File Offset: 0x00030108
	public List<ObjNPC> AIList
	{
		get
		{
			return this.mAIList;
		}
		set
		{
			this.mAIList = value;
		}
	}

	// Token: 0x0600071B RID: 1819 RVA: 0x00031F14 File Offset: 0x00030114
	private void Start()
	{
		this.Init();
	}

	// Token: 0x0600071C RID: 1820 RVA: 0x00031F1C File Offset: 0x0003011C
	public void Init()
	{
	}

	// Token: 0x0600071D RID: 1821 RVA: 0x00031F20 File Offset: 0x00030120
	public ObjNPC ObjCharacterToObjNPC(ObjCharacter obj)
	{
		return obj.gameObject.GetComponent<ObjNPC>();
	}

	// Token: 0x0600071E RID: 1822 RVA: 0x00031F30 File Offset: 0x00030130
	public void CheckAIState(AILogic ai)
	{
	}

	// Token: 0x0600071F RID: 1823 RVA: 0x00031F34 File Offset: 0x00030134
	private void Update()
	{
	}

	// Token: 0x0400062F RID: 1583
	private List<ObjNPC> mAIList;
}
