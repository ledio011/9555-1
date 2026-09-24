using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005C8 RID: 1480
	public class send_mail_box
	{
		// Token: 0x020005C9 RID: 1481
		public class request : SprotoTypeBase
		{
			// Token: 0x06002AAF RID: 10927 RVA: 0x000B24F8 File Offset: 0x000B06F8
			public request() : base(send_mail_box.request.max_field_count)
			{
			}

			// Token: 0x06002AB0 RID: 10928 RVA: 0x000B2508 File Offset: 0x000B0708
			public request(byte[] buffer) : base(send_mail_box.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000C37 RID: 3127
			// (get) Token: 0x06002AB2 RID: 10930 RVA: 0x000B2524 File Offset: 0x000B0724
			// (set) Token: 0x06002AB3 RID: 10931 RVA: 0x000B252C File Offset: 0x000B072C
			public string subject
			{
				get
				{
					return this._subject;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._subject = value;
				}
			}

			// Token: 0x17000C38 RID: 3128
			// (get) Token: 0x06002AB4 RID: 10932 RVA: 0x000B2544 File Offset: 0x000B0744
			public bool HasSubject
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000C39 RID: 3129
			// (get) Token: 0x06002AB5 RID: 10933 RVA: 0x000B2554 File Offset: 0x000B0754
			// (set) Token: 0x06002AB6 RID: 10934 RVA: 0x000B255C File Offset: 0x000B075C
			public string context
			{
				get
				{
					return this._context;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._context = value;
				}
			}

			// Token: 0x17000C3A RID: 3130
			// (get) Token: 0x06002AB7 RID: 10935 RVA: 0x000B2574 File Offset: 0x000B0774
			public bool HasContext
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000C3B RID: 3131
			// (get) Token: 0x06002AB8 RID: 10936 RVA: 0x000B2584 File Offset: 0x000B0784
			// (set) Token: 0x06002AB9 RID: 10937 RVA: 0x000B258C File Offset: 0x000B078C
			public string email
			{
				get
				{
					return this._email;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._email = value;
				}
			}

			// Token: 0x17000C3C RID: 3132
			// (get) Token: 0x06002ABA RID: 10938 RVA: 0x000B25A4 File Offset: 0x000B07A4
			public bool HasEmail
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x06002ABB RID: 10939 RVA: 0x000B25B4 File Offset: 0x000B07B4
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.subject = this.deserialize.read_string();
						break;
					case 1:
						this.context = this.deserialize.read_string();
						break;
					case 2:
						this.email = this.deserialize.read_string();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002ABC RID: 10940 RVA: 0x000B2648 File Offset: 0x000B0848
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.subject, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_string(this.context, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_string(this.email, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001E20 RID: 7712
			private static int max_field_count = 3;

			// Token: 0x04001E21 RID: 7713
			private string _subject;

			// Token: 0x04001E22 RID: 7714
			private string _context;

			// Token: 0x04001E23 RID: 7715
			private string _email;
		}
	}
}
