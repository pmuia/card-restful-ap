﻿using Microsoft.AspNetCore.Identity;

namespace Cards.WEB.Identity
{
    public class ApplicationRole : IdentityRole<string>
    {
        public ApplicationRole() : base()
        {
            Id = Guid.NewGuid().ToString();
        }

        public byte RoleType { get; set; }

        public string Description { get; set; }

        public bool IsEnabled { get; set; }
    }
}
