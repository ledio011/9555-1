using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005E0 RID: 1504
	public class skill_level_up
	{
		// Token: 0x020005E1 RID: 1505
		public class request : SprotoTypeBase
		{
			// Token: 0x06002B67 RID: 11111 RVA: 0x000B3C40 File Offset: 0x000B1E40
			public request() : base(skill_level_up.request.max_field_count)
			{
			}

			// Token: 0x06002B68 RID: 11112 RVA: 0x000B3C50 File Offset: 0x000B1E50
			public request(byte[] buffer) : base(skill_level_up.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000C7F RID: 3199
			// (get) Token: 0x06002B6A RID: 11114 RVA: 0x000B3C6C File Offset: 0x000B1E6C
			// (set) Token: 0x06002B6B RID: 11115 RVA: 0x000B3C74 File Offset: 0x000B1E74
			public string skillId
			{
				get
				{
					return this._skillId;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._skillId = value;
				}
			}

			// Token: 0x17000C80 RID: 3200
			// (get) Token: 0x06002B6C RID: 11116 RVA: 0x000B3C8C File Offset: 0x000B1E8C
			public bool HasSkillId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000C81 RID: 3201
			// (get) Token: 0x06002B6D RID: 11117 RVA: 0x000B3C9C File Offset: 0x000B1E9C
			// (set) Token: 0x06002B6E RID: 11118 RVA: 0x000B3CA4 File Offset: 0x000B1EA4
			public long curLevel
			{
				get
				{
					return this._curLevel;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._curLevel = value;
				}
			}

			// Token: 0x17000C82 RID: 3202
			// (get) Token: 0x06002B6F RID: 11119 RVA: 0x000B3CBC File Offset: 0x000B1EBC
			public bool HasCurLevel
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000C83 RID: 3203
			// (get) Token: 0x06002B70 RID: 11120 RVA: 0x000B3CCC File Offset: 0x000B1ECC
			// (set) Token: 0x06002B71 RID: 11121 RVA: 0x000B3CD4 File Offset: 0x000B1ED4
			public long all
			{
				get
				{
					return this._all;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._all = value;
				}
			}

			// Token: 0x17000C84 RID: 3204
			// (get) Token: 0x06002B72 RID: 11122 RVA: 0x000B3CEC File Offset: 0x000B1EEC
			public bool HasAll
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x06002B73 RID: 11123 RVA: 0x000B3CFC File Offset: 0x000B1EFC
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.skillId = this.deserialize.read_string();
						break;
					case 1:
						this.curLevel = this.deserialize.read_integer();
						break;
					case 2:
						this.all = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002B74 RID: 11124 RVA: 0x000B3D90 File Offset: 0x000B1F90
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.skillId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.curLevel, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.all, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001E51 RID: 7761
			private static int max_field_count = 3;

			// Token: 0x04001E52 RID: 7762
			private string _skillId;

			// Token: 0x04001E53 RID: 7763
			private long _curLevel;

			// Token: 0x04001E54 RID: 7764
			private long _all;
		}
	}
}
