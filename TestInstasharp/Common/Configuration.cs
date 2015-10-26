using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestInstasharp.Common
{
    public class Configuration
    {
        private static readonly Lazy<Configuration> _instance = new Lazy<Configuration>(() => new Configuration());

        public static Configuration Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        public struct InstagramConfigSection
        {
            public string ClientId
            {
                get { return "bd42978994b84b69a9797539812d0c55"; }
            }

            public string ClientSecret
            {
                get { return "82add02207a6485bbe6497b410fd6215"; }
            }

            public string RedirectUri
            {
                get { return "http://localhost:5969/Home/OAuth"; }
            }
        }

        public InstagramConfigSection InstagramConfig;
    }
}
