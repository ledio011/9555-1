using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200030F RID: 783
	public class attack_local_npc
	{
		// Token: 0x02000310 RID: 784
		public class request : SprotoTypeBase
		{
			// Token: 0x060015E1 RID: 5601 RVA: 0x000887AC File Offset: 0x000869AC
			public request() : base(attack_local_npc.request.max_field_count)
			{
			}

			// Token: 0x060015E2 RID: 5602 RVA: 0x000887BC File Offset: 0x000869BC
			public request(byte[] buffer) : base(attack_local_npc.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x1700044D RID: 1101
			// (get) Token: 0x060015E4 RID: 5604 RVA: 0x000887D8 File Offset: 0x000869D8
			// (set) Token: 0x060015E5 RID: 5605 RVA: 0x000887E0 File Offset: 0x000869E0
			public long damge
			{
				get
				{
					return this._damge;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._damge = value;
				}
			}

			// Token: 0x1700044E RID: 1102
			// (get) Token: 0x060015E6 RID: 5606 RVA: 0x000887F8 File Offset: 0x000869F8
			public bool HasDamge
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x1700044F RID: 1103
			// (get) Token: 0x060015E7 RID: 5607 RVA: 0x00088808 File Offset: 0x00086A08
			// (set) Token: 0x060015E8 RID: 5608 RVA: 0x00088810 File Offset: 0x00086A10
			public string effinfoId
			{
				get
				{
					return this._effinfoId;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._effinfoId = value;
				}
			}

			// Token: 0x17000450 RID: 1104
			// (get) Token: 0x060015E9 RID: 5609 RVA: 0x00088828 File Offset: 0x00086A28
			public bool HasEffinfoId
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x060015EA RID: 5610 RVA: 0x00088838 File Offset: 0x00086A38
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
							this.effinfoId = this.deserialize.read_string();
						}
					}
					else
					{
						this.damge = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x060015EB RID: 5611 RVA: 0x000888B0 File Offset: 0x00086AB0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.damge, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_string(this.effinfoId, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001875 RID: 6261
			private static int max_field_count = 2;

			// Token: 0x04001876 RID: 6262
			private long _damge;

			// Token: 0x04001877 RID: 6263
			private string _effinfoId;
		}
	}
}
