namespace Fanex.N88.Core.Models
{
    using System;

    [Serializable]
    public class Setting
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public User Creator { get; set; }

        public DateTime CreatedDate { get; set; }

        public User Modifier { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}