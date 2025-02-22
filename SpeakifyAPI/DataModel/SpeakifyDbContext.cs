﻿using Microsoft.EntityFrameworkCore;

namespace SpeakifyAPI.DataModel
{
    public partial class SpeakifyDbContext : DbContext
    {
        public SpeakifyDbContext()
        {
        }

        public SpeakifyDbContext(DbContextOptions<SpeakifyDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Follower> Followers { get; set; }
        public virtual DbSet<Hashtag> Hashtags { get; set; }
        public virtual DbSet<InterestCategory> InterestCategories { get; set; }
        public virtual DbSet<InterestSubcategory> InterestSubcategories { get; set; }
        public virtual DbSet<SystemUser> SystemUsers { get; set; }
        public virtual DbSet<Tweet> Tweets { get; set; }
        public virtual DbSet<TweetsHashtag> TweetsHashtags { get; set; }
        public virtual DbSet<TweetsMedia> TweetsMedia { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserContact> UserContacts { get; set; }
        public virtual DbSet<UserInterest> UserInterests { get; set; }
        public virtual DbSet<UserMention> UserMentions { get; set; }
        public virtual DbSet<UserSetting> UserSettings { get; set; }
        public virtual DbSet<UserTweetsFavorite> UserTweetsFavorites { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Follower>(entity =>
            {
                entity.ToTable("followers");

                entity.HasIndex(e => e.FollowedId, "followed_user_map_idx");

                entity.HasIndex(e => e.FollowerId, "follower_user_idx");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.FollowedId)
                    .IsRequired()
                    .HasColumnType("varchar(36)")
                    .HasColumnName("followed_id")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.FollowerId)
                    .IsRequired()
                    .HasColumnType("varchar(36)")
                    .HasColumnName("follower_id")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.IsArchived)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_archived")
                    .HasConversion<bool>();

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Followed)
                    .WithMany(p => p.FollowerFolloweds)
                    .HasForeignKey(d => d.FollowedId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("followed_user_map");

                entity.HasOne(d => d.FollowerNavigation)
                    .WithMany(p => p.FollowerFollowerNavigations)
                    .HasForeignKey(d => d.FollowerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("follower_user_map");
            });

            modelBuilder.Entity<Hashtag>(entity =>
            {
                entity.ToTable("hashtags");

                entity.HasIndex(e => e.Id, "id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Hashtag1)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasColumnName("hashtag")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.IsArchived)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_archived")
                    .HasConversion<bool>();
            });

            modelBuilder.Entity<InterestCategory>(entity =>
            {
                entity.ToTable("interest_category");

                entity.HasIndex(e => e.Id, "id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Description)
                    .HasColumnType("varchar(500)")
                    .HasColumnName("description")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.IsActive)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_active");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasColumnName("name")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<InterestSubcategory>(entity =>
            {
                entity.ToTable("interest_subcategory");

                entity.HasIndex(e => e.CategoryId, "category_subcategory_idx");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.CategoryId)
                    .HasColumnName("category_id")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Description)
                    .HasColumnType("varchar(500)")
                    .HasColumnName("description")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.IsActive)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_active");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasColumnName("name")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.InterestSubcategories)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("category_subcategory");
            });

            modelBuilder.Entity<SystemUser>(entity =>
            {
                entity.ToTable("system_users");

                entity.Property(e => e.Id)
                    .HasColumnType("varchar(36)")
                    .HasColumnName("id")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Address)
                    .HasColumnType("varchar(500)")
                    .HasColumnName("address")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasColumnName("email")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.FirstName)
                    .HasColumnType("varchar(100)")
                    .HasColumnName("first_name")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.IsArchived)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_archived")
                    .HasConversion<bool>();

                entity.Property(e => e.LastName)
                    .HasColumnType("varchar(100)")
                    .HasColumnName("last_name")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasColumnName("password_hash")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Phone)
                    .HasColumnType("varchar(12)")
                    .HasColumnName("phone")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnType("varchar(200)")
                    .HasColumnName("username")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Tweet>(entity =>
            {
                entity.ToTable("tweets");

                entity.HasIndex(e => e.UserId, "users_tweets_map_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("varchar(36)")
                    .HasColumnName("id")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.FavoriteCount).HasColumnName("favorite_count");

                entity.Property(e => e.InReplyToStatus)
                    .HasColumnType("varchar(36)")
                    .HasColumnName("in_reply_to_status")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.InReplyToUser)
                    .HasColumnType("varchar(36)")
                    .HasColumnName("in_reply_to_user")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.IsArchived)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_archived")
                    .HasConversion<bool>();

                entity.Property(e => e.PlaceCountry)
                    .HasColumnType("varchar(100)")
                    .HasColumnName("place_country")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ReplyCount).HasColumnName("reply_count");

                entity.Property(e => e.RetweetedFrom)
                    .HasColumnType("varchar(36)")
                    .HasColumnName("retweeted_from")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnType("varchar(200)")
                    .HasColumnName("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnType("varchar(36)")
                    .HasColumnName("user_id")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tweets)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("users_tweets_map");
            });

            modelBuilder.Entity<TweetsHashtag>(entity =>
            {
                entity.ToTable("tweets_hashtag");

                entity.HasIndex(e => e.HashtagId, "hashtag_tweets_map_idx");

                entity.HasIndex(e => e.Id, "id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.TweetsId, "tweets_hastag_map_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.HashtagId).HasColumnName("hashtag_id");

                entity.Property(e => e.IsArchived)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_archived")
                    .HasConversion<bool>();

                entity.Property(e => e.TweetsId)
                    .IsRequired()
                    .HasColumnType("varchar(36)")
                    .HasColumnName("tweets_id")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.Hashtag)
                    .WithMany(p => p.TweetsHashtags)
                    .HasForeignKey(d => d.HashtagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("hashtag_tweets_map");

                entity.HasOne(d => d.Tweets)
                    .WithMany(p => p.TweetsHashtags)
                    .HasForeignKey(d => d.TweetsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tweets_hastag_map");
            });

            modelBuilder.Entity<TweetsMedia>(entity =>
            {
                entity.ToTable("tweets_media");

                entity.HasIndex(e => e.Id, "id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.TweetsId, "tweets_media_map_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IsArchived)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_archived")
                    .HasConversion<bool>();

                entity.Property(e => e.Media)
                    .HasColumnType("varchar(300)")
                    .HasColumnName("media")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.MediaUrl)
                    .HasColumnType("varchar(200)")
                    .HasColumnName("media_url")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.MediaUrlHttps)
                    .HasColumnType("varchar(200)")
                    .HasColumnName("media_url_https")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TweetsId)
                    .IsRequired()
                    .HasColumnType("varchar(36)")
                    .HasColumnName("tweets_id")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Type)
                    .HasColumnType("varchar(100)")
                    .HasColumnName("type")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.Tweets)
                    .WithMany(p => p.TweetsMedia)
                    .HasForeignKey(d => d.TweetsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tweets_media_map");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id)
                    .HasColumnType("varchar(36)")
                    .HasColumnName("id")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Birthday)
                    .HasColumnType("varchar(100)")
                    .HasColumnName("birthday")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Country)
                    .HasColumnType("varchar(100)")
                    .HasColumnName("country")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CoverImage)
                    .HasColumnType("varchar(100)")
                    .HasColumnName("cover_image")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.DescriptionBio)
                    .HasColumnType("varchar(500)")
                    .HasColumnName("description_bio")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.DisplayBestTweetsFirst)
                    .HasColumnType("bit(1)")
                    .HasColumnName("display_best_tweets_first")
                    .HasConversion<bool>();

                entity.Property(e => e.DisplayNotifications)
                    .HasColumnType("bit(1)")
                    .HasColumnName("display_notifications")
                    .HasConversion<bool>();

                entity.Property(e => e.FollowRequestsSent).HasColumnName("follow_requests_sent");

                entity.Property(e => e.FollowersCount).HasColumnName("followers_count");

                entity.Property(e => e.FriendsCount).HasColumnName("friends_count");

                entity.Property(e => e.IsArchived)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_archived")
                    .HasConversion<bool>();

                entity.Property(e => e.IsVerified)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_verified")
                    .HasConversion<bool>();

                entity.Property(e => e.Link)
                    .HasColumnType("varchar(100)")
                    .HasColumnName("link")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Location)
                    .HasColumnType("varchar(100)")
                    .HasColumnName("location")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasColumnName("name")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ProfileImage)
                    .HasColumnType("varchar(100)")
                    .HasColumnName("profile_image")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ScreenName)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasColumnName("screen_name")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ThemeColor)
                    .HasColumnType("varchar(50)")
                    .HasColumnName("theme_color")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.VideoTweets)
                    .HasColumnType("bit(1)")
                    .HasColumnName("video_tweets")
                    .HasConversion<bool>();

                entity.Property(e => e.Website)
                    .HasColumnType("varchar(100)")
                    .HasColumnName("website")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.User)
                    .HasForeignKey<User>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("users_systemusers_map");
            });

            modelBuilder.Entity<UserContact>(entity =>
            {
                entity.ToTable("user_contacts");

                entity.HasIndex(e => e.UserId, "users_contacts_map_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("varchar(36)")
                    .HasColumnName("id")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ContactDescription)
                    .HasColumnType("varchar(500)")
                    .HasColumnName("contact_description")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ContactName)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasColumnName("contact_name")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ContactPhone)
                    .HasColumnType("varchar(100)")
                    .HasColumnName("contact_phone")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.IsArchived)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_archived")
                    .HasConversion<bool>();

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnType("varchar(36)")
                    .HasColumnName("user_id")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserContacts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("users_contacts_map");
            });

            modelBuilder.Entity<UserInterest>(entity =>
            {
                entity.ToTable("user_interest");

                entity.HasIndex(e => e.InterestCategoryId, "category_interest_idx");

                entity.HasIndex(e => e.InterestSubcategoryId, "sub_category_interest_idx");

                entity.HasIndex(e => e.UserId, "users_interest_map_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("varchar(36)")
                    .HasColumnName("id")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.InterestCategoryId)
                    .HasColumnName("interest_category_id")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.InterestSubcategoryId)
                    .HasColumnName("interest_subcategory_id")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.IsArchived)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_archived")
                    .HasConversion<bool>();

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnType("varchar(36)")
                    .HasColumnName("user_id")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.InterestCategory)
                    .WithMany(p => p.UserInterests)
                    .HasForeignKey(d => d.InterestCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("category_interest");

                entity.HasOne(d => d.InterestSubcategory)
                    .WithMany(p => p.UserInterests)
                    .HasForeignKey(d => d.InterestSubcategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("sub_category_interest");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserInterests)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("users_interest_map");
            });

            modelBuilder.Entity<UserMention>(entity =>
            {
                entity.ToTable("user_mentions");

                entity.HasIndex(e => e.TweetId, "tweets_mention_map_idx");

                entity.HasIndex(e => e.UserId, "user_mention_map_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("varchar(36)")
                    .HasColumnName("id")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.IsArchived)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_archived")
                    .HasConversion<bool>();

                entity.Property(e => e.TweetId)
                    .IsRequired()
                    .HasColumnType("varchar(36)")
                    .HasColumnName("tweet_id")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnType("varchar(36)")
                    .HasColumnName("user_id")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasColumnName("user_name")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.Tweet)
                    .WithMany(p => p.UserMentions)
                    .HasForeignKey(d => d.TweetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tweets_mention_map");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserMentions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_mention_map");
            });

            modelBuilder.Entity<UserSetting>(entity =>
            {
                entity.ToTable("user_settings");

                entity.HasIndex(e => e.Id, "id_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("varchar(36)")
                    .HasColumnName("id")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.EmailNewNotification)
                    .HasColumnType("bit(1)")
                    .HasColumnName("email_new_notification")
                     .HasConversion<bool>();

                entity.Property(e => e.EmailNotification)
                    .HasColumnType("bit(1)")
                    .HasColumnName("email_notification")
                     .HasConversion<bool>();

                entity.Property(e => e.IsArchived)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_archived")
                     .HasConversion<bool>();

                entity.Property(e => e.NotificationMuteNewAccount)
                    .HasColumnType("bit(1)")
                    .HasColumnName("notification_mute_new_account")
                     .HasConversion<bool>();

                entity.Property(e => e.NotificationMuteWhoDontFollow)
                    .HasColumnType("bit(1)")
                    .HasColumnName("notification_mute_who_dont_follow")
                     .HasConversion<bool>();

                entity.Property(e => e.NotificationMuteYouDontFollow)
                    .HasColumnType("bit(1)")
                    .HasColumnName("notification_mute_you_dont_follow")
                     .HasConversion<bool>();

                entity.Property(e => e.PrivacyPhotoTagging)
                    .HasColumnType("varchar(200)")
                    .HasColumnName("privacy_photo_tagging")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PrivacyTweetLocation)
                    .HasColumnType("bit(1)")
                    .HasColumnName("privacy_tweet_location")
                     .HasConversion<bool>();

                entity.Property(e => e.PrivacyTweetPrivacy)
                    .HasColumnType("bit(1)")
                    .HasColumnName("privacy_tweet_privacy")
                     .HasConversion<bool>();

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.UserSetting)
                    .HasForeignKey<UserSetting>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_settings_map");
            });

            modelBuilder.Entity<UserTweetsFavorite>(entity =>
            {
                entity.ToTable("user_tweets_favorite");

                entity.HasIndex(e => e.Id, "id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("varchar(36)")
                    .HasColumnName("id")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.IsArchived)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_archived")
                    .HasConversion<bool>();

                entity.Property(e => e.TweetsId)
                    .IsRequired()
                    .HasColumnType("varchar(36)")
                    .HasColumnName("tweets_id")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnType("varchar(36)")
                    .HasColumnName("user_id")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
