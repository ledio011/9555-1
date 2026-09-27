using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000022 RID: 34
	public class PointerEventData : BaseEventData
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x0000365C File Offset: 0x0000185C
		public PointerEventData(EventSystem eventSystem) : base(eventSystem)
		{
			this.eligibleForClick = false;
			this.pointerId = -1;
			this.position = Vector2.zero;
			this.delta = Vector2.zero;
			this.pressPosition = Vector2.zero;
			this.clickTime = 0f;
			this.clickCount = 0;
			this.scrollDelta = Vector2.zero;
			this.useDragThreshold = true;
			this.dragging = false;
			this.button = PointerEventData.InputButton.Left;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000036D4 File Offset: 0x000018D4
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x000036DC File Offset: 0x000018DC
		public GameObject pointerEnter { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000036E8 File Offset: 0x000018E8
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x000036F0 File Offset: 0x000018F0
		public GameObject lastPress { get; private set; }

		// Token: 0x1700002A RID: 42
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x000036FC File Offset: 0x000018FC
		public GameObject rawPointerPress
		{
			[CompilerGenerated]
			set
			{
				this.<rawPointerPress>k__BackingField = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00003708 File Offset: 0x00001908
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00003710 File Offset: 0x00001910
		public GameObject pointerDrag { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x0000371C File Offset: 0x0000191C
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00003724 File Offset: 0x00001924
		public RaycastResult pointerCurrentRaycast { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00003730 File Offset: 0x00001930
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00003738 File Offset: 0x00001938
		public RaycastResult pointerPressRaycast { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00003744 File Offset: 0x00001944
		// (set) Token: 0x060000AE RID: 174 RVA: 0x0000374C File Offset: 0x0000194C
		public bool eligibleForClick { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00003758 File Offset: 0x00001958
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00003760 File Offset: 0x00001960
		public int pointerId { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x0000376C File Offset: 0x0000196C
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00003774 File Offset: 0x00001974
		public Vector2 position { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00003780 File Offset: 0x00001980
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00003788 File Offset: 0x00001988
		public Vector2 delta { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00003794 File Offset: 0x00001994
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x0000379C File Offset: 0x0000199C
		public Vector2 pressPosition { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x000037A8 File Offset: 0x000019A8
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x000037B0 File Offset: 0x000019B0
		public float clickTime { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x000037BC File Offset: 0x000019BC
		// (set) Token: 0x060000BA RID: 186 RVA: 0x000037C4 File Offset: 0x000019C4
		public int clickCount { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000BB RID: 187 RVA: 0x000037D0 File Offset: 0x000019D0
		// (set) Token: 0x060000BC RID: 188 RVA: 0x000037D8 File Offset: 0x000019D8
		public Vector2 scrollDelta { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000037E4 File Offset: 0x000019E4
		// (set) Token: 0x060000BE RID: 190 RVA: 0x000037EC File Offset: 0x000019EC
		public bool useDragThreshold { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000037F8 File Offset: 0x000019F8
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x00003800 File Offset: 0x00001A00
		public bool dragging { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x0000380C File Offset: 0x00001A0C
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00003814 File Offset: 0x00001A14
		public PointerEventData.InputButton button { get; set; }

		// Token: 0x060000C3 RID: 195 RVA: 0x00003820 File Offset: 0x00001A20
		public bool IsPointerMoving()
		{
			return this.delta.sqrMagnitude > 0f;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003844 File Offset: 0x00001A44
		public bool IsScrolling()
		{
			return this.scrollDelta.sqrMagnitude > 0f;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00003868 File Offset: 0x00001A68
		public Camera enterEventCamera
		{
			get
			{
				return (!(this.pointerCurrentRaycast.module == null)) ? this.pointerCurrentRaycast.module.eventCamera : null;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000038A8 File Offset: 0x00001AA8
		public Camera pressEventCamera
		{
			get
			{
				return (!(this.pointerPressRaycast.module == null)) ? this.pointerPressRaycast.module.eventCamera : null;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x000038E8 File Offset: 0x00001AE8
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x000038F0 File Offset: 0x00001AF0
		public GameObject pointerPress
		{
			get
			{
				return this.m_PointerPress;
			}
			set
			{
				if (this.m_PointerPress == value)
				{
					return;
				}
				this.lastPress = this.m_PointerPress;
				this.m_PointerPress = value;
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003918 File Offset: 0x00001B18
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("<b>Position</b>: " + this.position);
			stringBuilder.AppendLine("<b>delta</b>: " + this.delta);
			stringBuilder.AppendLine("<b>eligibleForClick</b>: " + this.eligibleForClick);
			stringBuilder.AppendLine("<b>pointerEnter</b>: " + this.pointerEnter);
			stringBuilder.AppendLine("<b>pointerPress</b>: " + this.pointerPress);
			stringBuilder.AppendLine("<b>lastPointerPress</b>: " + this.lastPress);
			stringBuilder.AppendLine("<b>pointerDrag</b>: " + this.pointerDrag);
			stringBuilder.AppendLine("<b>Use Drag Threshold</b>: " + this.useDragThreshold);
			return stringBuilder.ToString();
		}

		// Token: 0x04000046 RID: 70
		private GameObject m_PointerPress;

		// Token: 0x02000023 RID: 35
		public enum FramePressState
		{
			// Token: 0x0400005B RID: 91
			Pressed,
			// Token: 0x0400005C RID: 92
			Released,
			// Token: 0x0400005D RID: 93
			PressedAndReleased,
			// Token: 0x0400005E RID: 94
			NotChanged
		}

		// Token: 0x02000024 RID: 36
		public enum InputButton
		{
			// Token: 0x04000060 RID: 96
			Left,
			// Token: 0x04000061 RID: 97
			Right,
			// Token: 0x04000062 RID: 98
			Middle
		}
	}
}
