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

    public partial class tbl_DonationCauses
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_DonationCauses()
        {
            this.tbl_Donations = new HashSet<tbl_Donations>();
        }

        public int CauseID { get; set; }

        [Required(ErrorMessage = "Cause Name is required*")]
        [Display(Name = "Cause Title")]
        public string CauseName { get; set; }

        [Required(ErrorMessage = "description is required*")]
        [Display(Name = "Cause description")]
        public string CauseDiscription { get; set; }
        public string CauseImage { get; set; }
        public Nullable<int> AdminID { get; set; }


        [Required(ErrorMessage = "Image is required*")]
        [Display(Name = "Choose Image")]
        public HttpPostedFileBase CauseImgFile { get; set; }

        public virtual tbl_Admins tbl_Admins { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Donations> tbl_Donations { get; set; }
    }
}