using AALife.Core.Configuration;

namespace AALife.Core.Domain.Configuration
{
    public class CommonSettings : ISettings
    {
        /// <summary>
        /// EncryptionKey
        /// </summary>
        public string EncryptionKey { get; set; }
    }
}
