using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000425 RID: 1061
	public class notice
	{
		// Token: 0x02000426 RID: 1062
		public class request : SprotoTypeBase
		{
			// Token: 0x060020E2 RID: 8418 RVA: 0x0009F670 File Offset: 0x0009D870
			public request() : base(SprotoType.notice.request.max_field_count)
			{
			}

			// Token: 0x060020E3 RID: 8419 RVA: 0x0009F680 File Offset: 0x0009D880
			public request(byte[] buffer) : base(SprotoType.notice.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000919 RID: 2329
			// (get) Token: 0x060020E5 RID: 8421 RVA: 0x0009F69C File Offset: 0x0009D89C
			// (set) Token: 0x060020E6 RID: 8422 RVA: 0x0009F6A4 File Offset: 0x0009D8A4
			public string notice
			{
				get
				{
					return this._notice;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._notice = value;
				}
			}

			// Token: 0x1700091A RID: 2330
			// (get) Token: 0x060020E7 RID: 8423 RVA: 0x0009F6BC File Offset: 0x0009D8BC
			public bool HasNotice
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x1700091B RID: 2331
			// (get) Token: 0x060020E8 RID: 8424 RVA: 0x0009F6CC File Offset: 0x0009D8CC
			// (set) Token: 0x060020E9 RID: 8425 RVA: 0x0009F6D4 File Offset: 0x0009D8D4
			public bool repeate
			{
				get
				{
					return this._repeate;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._repeate = value;
				}
			}

			// Token: 0x1700091C RID: 2332
			// (get) Token: 0x060020EA RID: 8426 RVA: 0x0009F6EC File Offset: 0x0009D8EC
			public bool HasRepeate
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x060020EB RID: 8427 RVA: 0x0009F6FC File Offset: 0x0009D8FC
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
							this.repeate = this.deserialize.read_boolean();
						}
					}
					else
					{
						this.notice = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x060020EC RID: 8428 RVA: 0x0009F774 File Offset: 0x0009D974
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.notice, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_boolean(this.repeate, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001B96 RID: 7062
			private static int max_field_count = 2;

			// Token: 0x04001B97 RID: 7063
			private string _notice;

			// Token: 0x04001B98 RID: 7064
			private bool _repeate;
		}
	}
}
