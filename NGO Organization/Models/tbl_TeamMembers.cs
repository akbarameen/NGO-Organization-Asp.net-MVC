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

    public partial class tbl_TeamMembers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_TeamMembers()
        {
            this.tbl_Projects = new HashSet<tbl_Projects>();
        }

        public int MemberID { get; set; }

        [Required(ErrorMessage = "Name is required*")]
        [Display(Name = "Name")]
        public string MemberName { get; set; }
        public string MemberImage { get; set; }

        [Required(ErrorMessage = "Description is required*")]
        [Display(Name = "Description")]
        public string MemberDescription { get; set; }

        [Required(ErrorMessage = "Title is required*")]
        [Display(Name = "Title ID")]
        public int TitleID { get; set; }
        public int AdminID { get; set; }

        [Required(ErrorMessage = "Image is required*")]
        [Display(Name = "Choose Image")]
        public HttpPostedFileBase MemberImgFile { get; set; }
        public virtual tbl_Admins tbl_Admins { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Projects> tbl_Projects { get; set; }
        public virtual tbl_TeamMemberTitles tbl_TeamMemberTitles { get; set; }
    }
}