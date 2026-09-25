using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005E2 RID: 1506
	public class skill_use
	{
		// Token: 0x020005E3 RID: 1507
		public class request : SprotoTypeBase
		{
			// Token: 0x06002B76 RID: 11126 RVA: 0x000B3E28 File Offset: 0x000B2028
			public request() : base(skill_use.request.max_field_count)
			{
			}

			// Token: 0x06002B77 RID: 11127 RVA: 0x000B3E38 File Offset: 0x000B2038
			public request(byte[] buffer) : base(skill_use.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000C85 RID: 3205
			// (get) Token: 0x06002B79 RID: 11129 RVA: 0x000B3E54 File Offset: 0x000B2054
			// (set) Token: 0x06002B7A RID: 11130 RVA: 0x000B3E5C File Offset: 0x000B205C
			public long targetId
			{
				get
				{
					return this._targetId;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._targetId = value;
				}
			}

			// Token: 0x17000C86 RID: 3206
			// (get) Token: 0x06002B7B RID: 11131 RVA: 0x000B3E74 File Offset: 0x000B2074
			public bool HasTargetId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000C87 RID: 3207
			// (get) Token: 0x06002B7C RID: 11132 RVA: 0x000B3E84 File Offset: 0x000B2084
			// (set) Token: 0x06002B7D RID: 11133 RVA: 0x000B3E8C File Offset: 0x000B208C
			public string skillId
			{
				get
				{
					return this._skillId;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._skillId = value;
				}
			}

			// Token: 0x17000C88 RID: 3208
			// (get) Token: 0x06002B7E RID: 11134 RVA: 0x000B3EA4 File Offset: 0x000B20A4
			public bool HasSkillId
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000C89 RID: 3209
			// (get) Token: 0x06002B7F RID: 11135 RVA: 0x000B3EB4 File Offset: 0x000B20B4
			// (set) Token: 0x06002B80 RID: 11136 RVA: 0x000B3EBC File Offset: 0x000B20BC
			public bool combo
			{
				get
				{
					return this._combo;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._combo = value;
				}
			}

			// Token: 0x17000C8A RID: 3210
			// (get) Token: 0x06002B81 RID: 11137 RVA: 0x000B3ED4 File Offset: 0x000B20D4
			public bool HasCombo
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x17000C8B RID: 3211
			// (get) Token: 0x06002B82 RID: 11138 RVA: 0x000B3EE4 File Offset: 0x000B20E4
			// (set) Token: 0x06002B83 RID: 11139 RVA: 0x000B3EEC File Offset: 0x000B20EC
			public List<attack_list> attack_list
			{
				get
				{
					return this._attack_list;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._attack_list = value;
				}
			}

			// Token: 0x17000C8C RID: 3212
			// (get) Token: 0x06002B84 RID: 11140 RVA: 0x000B3F04 File Offset: 0x000B2104
			public bool HasAttack_list
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x17000C8D RID: 3213
			// (get) Token: 0x06002B85 RID: 11141 RVA: 0x000B3F14 File Offset: 0x000B2114
			// (set) Token: 0x06002B86 RID: 11142 RVA: 0x000B3F1C File Offset: 0x000B211C
			public long parm
			{
				get
				{
					return this._parm;
				}
				set
				{
					this.has_field.set_field(4, true);
					this._parm = value;
				}
			}

			// Token: 0x17000C8E RID: 3214
			// (get) Token: 0x06002B87 RID: 11143 RVA: 0x000B3F34 File Offset: 0x000B2134
			public bool HasParm
			{
				get
				{
					return this.has_field.has_field(4);
				}
			}

			// Token: 0x06002B88 RID: 11144 RVA: 0x000B3F44 File Offset: 0x000B2144
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.targetId = this.deserialize.read_integer();
						break;
					case 1:
						this.skillId = this.deserialize.read_string();
						break;
					case 2:
						this.combo = this.deserialize.read_boolean();
						break;
					case 3:
						this.attack_list = this.deserialize.read_obj_list<attack_list>();
						break;
					case 4:
						this.parm = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002B89 RID: 11145 RVA: 0x000B400C File Offset: 0x000B220C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.targetId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_string(this.skillId, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_boolean(this.combo, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_obj<attack_list>(this.attack_list, 3);
				}
				if (this.has_field.has_field(4))
				{
					this.serialize.write_integer(this.parm, 4);
				}
				return this.serialize.close();
			}

			// Token: 0x04001E55 RID: 7765
			private static int max_field_count = 5;

			// Token: 0x04001E56 RID: 7766
			private long _targetId;

			// Token: 0x04001E57 RID: 7767
			private string _skillId;

			// Token: 0x04001E58 RID: 7768
			private bool _combo;

			// Token: 0x04001E59 RID: 7769
			private List<attack_list> _attack_list;

			// Token: 0x04001E5A RID: 7770
			private long _parm;
		}
	}
}
