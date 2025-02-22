﻿
namespace SpeakifyAPI.DataModel
{
    public partial class UserSetting
    {
        public string Id { get; set; }
        public bool PrivacyTweetPrivacy { get; set; }
        public bool PrivacyTweetLocation { get; set; }
        public bool PrivacyPhotoTagging { get; set; }
        public bool EmailNotification { get; set; }
        public bool EmailNewNotification { get; set; }
        public bool NotificationMuteYouDontFollow { get; set; }
        public bool NotificationMuteWhoDontFollow { get; set; }
        public bool NotificationMuteNewAccount { get; set; }
        public bool IsArchived { get; set; }

        public virtual SystemUser IdNavigation { get; set; }
    }
}
