﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CGVakBooks.Models.ViewModels
{
    public class ProductVM
    {
        public products? Product { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? CategoryList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem>? CoverTypeList { get; set; }
    }
}
