//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NGO_Organization.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public partial class tbl_AssociativeNGO
    {
        public int NgoID { get; set; }

        [Required(ErrorMessage = "NGO name is required*")]
        [Display(Name = "Name")]
        public string NgoName { get; set; }

        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter Valid Email*")]
        [Required(ErrorMessage = "NGO Email is required*")]
        [Display(Name = "Email")]
        public string NgoEmail { get; set; }
        public string NgoLogo { get; set; }

        [Required(ErrorMessage = "You must provide a phone number")]
        [Display(Name = "Home Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{4})\)?[-. ]?([0-9]{7})$", ErrorMessage = "Not a valid phone number")]
        public string NgoPhone { get; set; }

        [RegularExpression(@"[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)",
                 ErrorMessage = "website format is not valid*.")]
        public string NgoWebsite { get; set; }

        [Required(ErrorMessage = "NGO Details are required*")]
        [Display(Name = "Description")]
        public string NgoDetails { get; set; }
        public Nullable<int> AdminID { get; set; }



        public Nullable<int> NGOStatus { get; set; }


        [Required(ErrorMessage = "Image is required*")]
        [Display(Name = "Choose Image")]
        public HttpPostedFileBase NgoLogoFile { get; set; }
        public virtual tbl_Admins tbl_Admins { get; set; }
    }
}

