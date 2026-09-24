using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x02000092 RID: 146
	[AddComponentMenu("UI/Toggle Group", 36)]
	public class ToggleGroup : UIBehaviour
	{
		// Token: 0x060004D2 RID: 1234 RVA: 0x000140F0 File Offset: 0x000122F0
		protected ToggleGroup()
		{
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x00014104 File Offset: 0x00012304
		// (set) Token: 0x060004D4 RID: 1236 RVA: 0x0001410C File Offset: 0x0001230C
		public bool allowSwitchOff
		{
			get
			{
				return this.m_AllowSwitchOff;
			}
			set
			{
				this.m_AllowSwitchOff = value;
			}
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00014118 File Offset: 0x00012318
		private void ValidateToggleIsInGroup(Toggle toggle)
		{
			if (toggle == null || !this.m_Toggles.Contains(toggle))
			{
				throw new ArgumentException(string.Format("Toggle {0} is not part of ToggleGroup {1}", toggle, this));
			}
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0001414C File Offset: 0x0001234C
		public void NotifyToggleOn(Toggle toggle)
		{
			this.ValidateToggleIsInGroup(toggle);
			for (int i = 0; i < this.m_Toggles.Count; i++)
			{
				if (!(this.m_Toggles[i] == toggle))
				{
					this.m_Toggles[i].isOn = false;
				}
			}
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x000141AC File Offset: 0x000123AC
		public void UnregisterToggle(Toggle toggle)
		{
			if (this.m_Toggles.Contains(toggle))
			{
				this.m_Toggles.Remove(toggle);
			}
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x000141CC File Offset: 0x000123CC
		public void RegisterToggle(Toggle toggle)
		{
			if (!this.m_Toggles.Contains(toggle))
			{
				this.m_Toggles.Add(toggle);
			}
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x000141EC File Offset: 0x000123EC
		public bool AnyTogglesOn()
		{
			return this.m_Toggles.Find((Toggle x) => x.isOn) != null;
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0001421C File Offset: 0x0001241C
		public IEnumerable<Toggle> ActiveToggles()
		{
			return from x in this.m_Toggles
			where x.isOn
			select x;
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00014248 File Offset: 0x00012448
		public void SetAllTogglesOff()
		{
			bool allowSwitchOff = this.m_AllowSwitchOff;
			this.m_AllowSwitchOff = true;
			for (int i = 0; i < this.m_Toggles.Count; i++)
			{
				this.m_Toggles[i].isOn = false;
			}
			this.m_AllowSwitchOff = allowSwitchOff;
		}

		// Token: 0x04000256 RID: 598
		[SerializeField]
		private bool m_AllowSwitchOff;

		// Token: 0x04000257 RID: 599
		private List<Toggle> m_Toggles = new List<Toggle>();
	}
}
