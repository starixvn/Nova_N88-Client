namespace Fanex.N88.Core.Configuration
{
    using System.Configuration;

    public class N88Settings : ConfigurationSection
    {
        private const string N88SettingsConfigs = "N88Settings";
        private const string N88Environment = "Environment";
        private const string N88EnableDirectLogin = "EnableDirectLogin";
        private const string N88HubUrl = "HubUrl";
        private const string N88SiteId = "SiteId";
        private const string N88EnableReCheckin = "EnableReCheckin";
        private const string N88UnauthorizeUrl = "UnauthorizeUrl";
        private const string N88ServiceUrls = "ServiceUrls";
        private const string N88AccessKey = "AccessKey";
        private const string N88Domain = "Domain";
        private const string N88DomainAdd = "add";
        private const string N88CacheDuration = "CacheDuration";

        private readonly static N88Settings _settings = ConfigurationManager.GetSection(N88SettingsConfigs) as N88Settings;

        public static N88Settings Settings
        {
            get
            {
                return _settings;
            }
        }

        public bool IsN88Hub
        {
            get { return SiteId == 0; }
        }

        public bool EnableDirectLogin
        {
            get
            {
                var result = false;

                if (!bool.TryParse(_EnableDirectLogin, out result))
                {
                    result = Equals(_EnableDirectLogin, "1");
                }

                return result;
            }

            set
            {
                _EnableDirectLogin = value.ToString();
            }
        }

        public bool EnableReCheckIn
        {
            get
            {
                var result = false;

                if (!bool.TryParse(_EnableReCheckIn, out result))
                {
                    result = Equals(_EnableReCheckIn, "1");
                }

                return result;
            }

            set
            {
                _EnableReCheckIn = value.ToString();
            }
        }

        [ConfigurationProperty(N88SiteId, IsRequired = true)]
        public int SiteId
        {
            get { return (int)this[N88SiteId]; }
            set { this[N88SiteId] = value; }
        }

        [ConfigurationProperty(N88HubUrl, IsRequired = true)]
        public string HubUrl
        {
            get { return (string)this[N88HubUrl]; }
            set { this[N88HubUrl] = value; }
        }

        [ConfigurationProperty(N88Environment, IsRequired = true)]
        public string Environment
        {
            get { return (string)this[N88Environment]; }
            set { this[N88Environment] = value; }
        }

        [ConfigurationProperty(N88UnauthorizeUrl, IsRequired = false)]
        public string UnauthorizeUrl
        {
            get { return (string)this[N88UnauthorizeUrl]; }
            set { this[N88UnauthorizeUrl] = value; }
        }

        [ConfigurationProperty(N88Domain, IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(DomainCollection), AddItemName = N88DomainAdd)]
        public DomainCollection Domains
        {
            get { return (DomainCollection)base[N88Domain]; }
        }

        [ConfigurationProperty(N88ServiceUrls, IsRequired = true)]
        public string ServiceUrls
        {
            get { return (string)this[N88ServiceUrls]; }
            set { this[N88ServiceUrls] = value; }
        }

        [ConfigurationProperty(N88AccessKey, IsRequired = true)]
        public string AccessKey
        {
            get { return (string)this[N88AccessKey]; }
            set { this[N88AccessKey] = value; }
        }

        [ConfigurationProperty(N88CacheDuration, IsRequired = false, DefaultValue = 0)]
        public int CacheDuration
        {
            get { return (int)this[N88CacheDuration]; }
            set { this[N88CacheDuration] = value; }
        }

        [ConfigurationProperty(N88EnableDirectLogin, IsRequired = true)]
        private string _EnableDirectLogin
        {
            get { return (string)this[N88EnableDirectLogin]; }
            set { this[N88EnableDirectLogin] = value; }
        }

        [ConfigurationProperty(N88EnableReCheckin, IsRequired = true)]
        private string _EnableReCheckIn
        {
            get { return (string)this[N88EnableReCheckin]; }
            set { this[N88EnableReCheckin] = value; }
        }
    }
}