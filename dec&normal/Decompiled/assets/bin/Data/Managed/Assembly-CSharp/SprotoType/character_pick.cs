using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000348 RID: 840
	public class character_pick
	{
		// Token: 0x02000349 RID: 841
		public class request : SprotoTypeBase
		{
			// Token: 0x060018B3 RID: 6323 RVA: 0x0008E7DC File Offset: 0x0008C9DC
			public request() : base(character_pick.request.max_field_count)
			{
			}

			// Token: 0x060018B4 RID: 6324 RVA: 0x0008E7EC File Offset: 0x0008C9EC
			public request(byte[] buffer) : base(character_pick.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170005A1 RID: 1441
			// (get) Token: 0x060018B6 RID: 6326 RVA: 0x0008E808 File Offset: 0x0008CA08
			// (set) Token: 0x060018B7 RID: 6327 RVA: 0x0008E810 File Offset: 0x0008CA10
			public long id
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

			// Token: 0x170005A2 RID: 1442
			// (get) Token: 0x060018B8 RID: 6328 RVA: 0x0008E828 File Offset: 0x0008CA28
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x060018B9 RID: 6329 RVA: 0x0008E838 File Offset: 0x0008CA38
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					int num2 = num;
					if (num2 != 0)
					{
						this.deserialize.read_unknow_data();
					}
					else
					{
						this.id = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x060018BA RID: 6330 RVA: 0x0008E894 File Offset: 0x0008CA94
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.id, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x0400194E RID: 6478
			private static int max_field_count = 1;

			// Token: 0x0400194F RID: 6479
			private long _id;
		}

		// Token: 0x0200034A RID: 842
		public class response : SprotoTypeBase
		{
			// Token: 0x060018BB RID: 6331 RVA: 0x0008E8DC File Offset: 0x0008CADC
			public response() : base(character_pick.response.max_field_count)
			{
			}

			// Token: 0x060018BC RID: 6332 RVA: 0x0008E8EC File Offset: 0x0008CAEC
			public response(byte[] buffer) : base(character_pick.response.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170005A3 RID: 1443
			// (get) Token: 0x060018BE RID: 6334 RVA: 0x0008E908 File Offset: 0x0008CB08
			// (set) Token: 0x060018BF RID: 6335 RVA: 0x0008E910 File Offset: 0x0008CB10
			public long errno
			{
				get
				{
					return this._errno;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._errno = value;
				}
			}

			// Token: 0x170005A4 RID: 1444
			// (get) Token: 0x060018C0 RID: 6336 RVA: 0x0008E928 File Offset: 0x0008CB28
			public bool HasErrno
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x060018C1 RID: 6337 RVA: 0x0008E938 File Offset: 0x0008CB38
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					int num2 = num;
					if (num2 != 0)
					{
						this.deserialize.read_unknow_data();
					}
					else
					{
						this.errno = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x060018C2 RID: 6338 RVA: 0x0008E994 File Offset: 0x0008CB94
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.errno, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001950 RID: 6480
			private static int max_field_count = 1;

			// Token: 0x04001951 RID: 6481
			private long _errno;
		}
	}
}
