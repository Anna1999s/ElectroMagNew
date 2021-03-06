// Copyright © 2019 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using WebSite.Models;

namespace AspNetCoreContentLocalization.ViewModels.Shared
{
    public class Culture : ViewModelBase
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}