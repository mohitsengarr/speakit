﻿
namespace SpeakifyAPI.DataModel
{
    public partial class UserContact
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactDescription { get; set; }
        public bool IsArchived { get; set; }

        public virtual User User { get; set; }
    }
}
