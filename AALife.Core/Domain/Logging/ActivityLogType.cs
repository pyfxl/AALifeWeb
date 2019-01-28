namespace AALife.Core.Domain.Logging
{
    /// <summary>
    /// Represents an activity log type record
    /// </summary>
    public enum ActivityLogType
    {
        Default = 0,

        Query = 1,

        Insert = 2,

        Update = 3,

        Delete = 4
    }
}
