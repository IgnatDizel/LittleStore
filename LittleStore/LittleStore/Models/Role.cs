﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LittleStore.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}