using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000639 RID: 1593
	public class update_game_server
	{
		// Token: 0x0200063A RID: 1594
		public class request : SprotoTypeBase
		{
			// Token: 0x06002E3E RID: 11838 RVA: 0x000B9870 File Offset: 0x000B7A70
			public request() : base(update_game_server.request.max_field_count)
			{
			}

			// Token: 0x06002E3F RID: 11839 RVA: 0x000B9880 File Offset: 0x000B7A80
			public request(byte[] buffer) : base(update_game_server.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x06002E41 RID: 11841 RVA: 0x000B9898 File Offset: 0x000B7A98
			protected override void decode()
			{
				while (this.deserialize.read_tag() != -1)
				{
					this.deserialize.read_unknow_data();
				}
			}

			// Token: 0x06002E42 RID: 11842 RVA: 0x000B98D4 File Offset: 0x000B7AD4
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				return this.serialize.close();
			}

			// Token: 0x04001F1D RID: 7965
			private static int max_field_count;
		}

		// Token: 0x0200063B RID: 1595
		public class response : SprotoTypeBase
		{
			// Token: 0x06002E43 RID: 11843 RVA: 0x000B98F0 File Offset: 0x000B7AF0
			public response() : base(update_game_server.response.max_field_count)
			{
			}

			// Token: 0x06002E44 RID: 11844 RVA: 0x000B9900 File Offset: 0x000B7B00
			public response(byte[] buffer) : base(update_game_server.response.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000D9B RID: 3483
			// (get) Token: 0x06002E46 RID: 11846 RVA: 0x000B991C File Offset: 0x000B7B1C
			// (set) Token: 0x06002E47 RID: 11847 RVA: 0x000B9924 File Offset: 0x000B7B24
			public List<game_server> game_server
			{
				get
				{
					return this._game_server;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._game_server = value;
				}
			}

			// Token: 0x17000D9C RID: 3484
			// (get) Token: 0x06002E48 RID: 11848 RVA: 0x000B993C File Offset: 0x000B7B3C
			public bool HasGame_server
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002E49 RID: 11849 RVA: 0x000B994C File Offset: 0x000B7B4C
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					int num2 = num;
					if (num2 != 2)
					{
						this.deserialize.read_unknow_data();
					}
					else
					{
						this.game_server = this.deserialize.read_obj_list<game_server>();
					}
				}
			}

			// Token: 0x06002E4A RID: 11850 RVA: 0x000B99A8 File Offset: 0x000B7BA8
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<game_server>(this.game_server, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001F1E RID: 7966
			private static int max_field_count = 2;

			// Token: 0x04001F1F RID: 7967
			private List<game_server> _game_server;
		}
	}
}
