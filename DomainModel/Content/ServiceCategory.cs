﻿using DomainModel.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Content
{
    public class ServiceCategory: BaseEntity
    {
        public int? NameId { get; set; }
        public LocalizationSet Name { get; set; }

        public List<Service> Services { get; set; }
    }
}
