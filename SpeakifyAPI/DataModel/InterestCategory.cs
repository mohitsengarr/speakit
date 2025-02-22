﻿using System;
using System.Collections.Generic;

namespace SpeakifyAPI.DataModel
{
    public partial class InterestCategory
    {
        public InterestCategory()
        {
            InterestSubcategories = new HashSet<InterestSubcategory>();
            UserInterests = new HashSet<UserInterest>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<InterestSubcategory> InterestSubcategories { get; set; }
        public virtual ICollection<UserInterest> UserInterests { get; set; }
    }
}
