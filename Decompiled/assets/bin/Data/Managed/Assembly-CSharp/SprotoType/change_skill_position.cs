using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200033A RID: 826
	public class change_skill_position
	{
		// Token: 0x0200033B RID: 827
		public class request : SprotoTypeBase
		{
			// Token: 0x0600179F RID: 6047 RVA: 0x0008C220 File Offset: 0x0008A420
			public request() : base(change_skill_position.request.max_field_count)
			{
			}

			// Token: 0x060017A0 RID: 6048 RVA: 0x0008C230 File Offset: 0x0008A430
			public request(byte[] buffer) : base(change_skill_position.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000517 RID: 1303
			// (get) Token: 0x060017A2 RID: 6050 RVA: 0x0008C24C File Offset: 0x0008A44C
			// (set) Token: 0x060017A3 RID: 6051 RVA: 0x0008C254 File Offset: 0x0008A454
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

			// Token: 0x17000518 RID: 1304
			// (get) Token: 0x060017A4 RID: 6052 RVA: 0x0008C26C File Offset: 0x0008A46C
			public bool HasSkillId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000519 RID: 1305
			// (get) Token: 0x060017A5 RID: 6053 RVA: 0x0008C27C File Offset: 0x0008A47C
			// (set) Token: 0x060017A6 RID: 6054 RVA: 0x0008C284 File Offset: 0x0008A484
			public long indexPos
			{
				get
				{
					return this._indexPos;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._indexPos = value;
				}
			}

			// Token: 0x1700051A RID: 1306
			// (get) Token: 0x060017A7 RID: 6055 RVA: 0x0008C29C File Offset: 0x0008A49C
			public bool HasIndexPos
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x060017A8 RID: 6056 RVA: 0x0008C2AC File Offset: 0x0008A4AC
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					int num2 = num;
					if (num2 != 0)
					{
						if (num2 != 1)
						{
							this.deserialize.read_unknow_data();
						}
						else
						{
							this.indexPos = this.deserialize.read_integer();
						}
					}
					else
					{
						this.skillId = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x060017A9 RID: 6057 RVA: 0x0008C324 File Offset: 0x0008A524
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.skillId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.indexPos, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x040018F3 RID: 6387
			private static int max_field_count = 2;

			// Token: 0x040018F4 RID: 6388
			private string _skillId;

			// Token: 0x040018F5 RID: 6389
			private long _indexPos;
		}
	}
}
