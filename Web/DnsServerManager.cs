using ARSoft.Tools.Net;
using ARSoft.Tools.Net.Dns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace VirtualRouterPlus.Web
{
    class DnsServerManager
    {
        private static DnsServer server = null;

        public static void Start()
        {
            DnsServer server = new DnsServer(IPAddress.Any, 10, 10);
            server.QueryReceived += OnQueryReceived;

            server.Start();
        }

        static async Task OnQueryReceived(object sender, QueryReceivedEventArgs e)
        {
            DnsMessage query = e.Query as DnsMessage;

            if (query == null)
                return;

            DnsMessage response = query.CreateResponseInstance();

            // check for valid query
            if ((query.Questions.Count == 1)
                && (query.Questions[0].RecordType == RecordType.A))
            {
                response.ReturnCode = ReturnCode.NoError;
                DomainName target = DomainName.Parse("hotspot.me");
                if (query.Questions[0].Name.Equals(target))
                {
                    response.AnswerRecords.Add(new ARecord(query.Questions[0].Name, 3600, IPAddress.Parse("192.168.137.1")));
                }
                else
                {
                    response.AnswerRecords.Add(new CNameRecord(query.Questions[0].Name, 3600, target));
                }
            }
            else
            {
                response.ReturnCode = ReturnCode.ServerFailure;
            }

            // set the response
            e.Response = response;
        }

        public static void Stop()
        {
            if (null != server)
            {
                try
                {
                    server.Stop();
                }
                catch
                {
                }
            }
        }
    }
}
