using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200064D RID: 1613
	public class update_sex_mini_score
	{
		// Token: 0x0200064E RID: 1614
		public class request : SprotoTypeBase
		{
			// Token: 0x06002EB7 RID: 11959 RVA: 0x000BA728 File Offset: 0x000B8928
			public request() : base(update_sex_mini_score.request.max_field_count)
			{
			}

			// Token: 0x06002EB8 RID: 11960 RVA: 0x000BA738 File Offset: 0x000B8938
			public request(byte[] buffer) : base(update_sex_mini_score.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000DC1 RID: 3521
			// (get) Token: 0x06002EBA RID: 11962 RVA: 0x000BA754 File Offset: 0x000B8954
			// (set) Token: 0x06002EBB RID: 11963 RVA: 0x000BA75C File Offset: 0x000B895C
			public string id
			{
				get
				{
					return this._id;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._id = value;
				}
			}

			// Token: 0x17000DC2 RID: 3522
			// (get) Token: 0x06002EBC RID: 11964 RVA: 0x000BA774 File Offset: 0x000B8974
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000DC3 RID: 3523
			// (get) Token: 0x06002EBD RID: 11965 RVA: 0x000BA784 File Offset: 0x000B8984
			// (set) Token: 0x06002EBE RID: 11966 RVA: 0x000BA78C File Offset: 0x000B898C
			public long score
			{
				get
				{
					return this._score;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._score = value;
				}
			}

			// Token: 0x17000DC4 RID: 3524
			// (get) Token: 0x06002EBF RID: 11967 RVA: 0x000BA7A4 File Offset: 0x000B89A4
			public bool HasScore
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06002EC0 RID: 11968 RVA: 0x000BA7B4 File Offset: 0x000B89B4
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
							this.score = this.deserialize.read_integer();
						}
					}
					else
					{
						this.id = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x06002EC1 RID: 11969 RVA: 0x000BA82C File Offset: 0x000B8A2C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.id, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.score, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001F3B RID: 7995
			private static int max_field_count = 2;

			// Token: 0x04001F3C RID: 7996
			private string _id;

			// Token: 0x04001F3D RID: 7997
			private long _score;
		}
	}
}
