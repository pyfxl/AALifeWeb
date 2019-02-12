namespace AALife.Core.Domain.Media
{
    /// <summary>
    /// Represents a picture item type
    /// </summary>
    public enum PictureType
    {
        /// <summary>
        /// Entities (products, categories, manufacturers)
        /// </summary>
        Entity = 1,

        /// <summary>
        /// Avatar
        /// </summary>
        Avatar = 10,

        /// <summary>
        /// user picture
        /// </summary>
        Users = 2,

        /// <summary>
        /// user bg
        /// </summary>
        bg = 3,

        /// <summary>
        /// item picture
        /// </summary>
        Items = 4
    }
}
