namespace PortProxy.ProxyServer
{
	using System;
	using System.Text.RegularExpressions;

	public class ServerConfig
	{
		public int RemoteServerPort { get; set; }

		public string RemoteServer { get; set; }

		public int BufferSize { get; set; }

		public int Port { get; set; }

		public bool Local { get; set; }

		public string LocalServer { get; set; }

		public bool GuiMode { get; set; }

		/// <summary>
		/// �Ƿ��Ƿ���ģʽ����Windows��
		/// </summary>
		public bool ServiceMode { get; set; } = false;

		/// <summary>
		/// ��������в���
		/// </summary>
		/// <returns></returns>
		public string GetCmdLine()
		{
			return $"{(Local ? "--local" : "")} {(ServiceMode ? "--service" : "")} {(GuiMode ? "--console" : "")} --server={RemoteServer} --sport={RemoteServerPort} --port={Port} --stat_server={LocalServer}";
		}

		public string CheckForConfigurationError()
		{
			if (RemoteServer.IsNullOrEmpty())
			{
				return "����ָ�����η�������ַ";
			}

			if (Port < 1025 || Port >= 65535)
			{
				return "��ָ����Ч�ı��ض˿ڷ�Χ��1025~65534��";
			}

			if (RemoteServerPort < 1025 || RemoteServerPort >= 65535)
			{
				return "��ָ����Ч�ı��ض˿ڷ�Χ��1025~65534��";
			}

			if (!LocalServer.IsNullOrEmpty()&&!Regex.IsMatch(LocalServer,@"^(\*|[a-z\d\.-_]+):\d+$", RegexOptions.IgnoreCase))
			{
				return "��������Ч��״̬������ǰ׺���磺127.0.0.1:7788��";
			}

			return null;
		}
	}
}
