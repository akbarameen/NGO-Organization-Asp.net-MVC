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

    public partial class tbl_Events
    {

        public int EventID { get; set; }

        [Required(ErrorMessage = "Event Title is required*")]
        [Display(Name = "Title")]
        public string EventTitle { get; set; }

        [Required(ErrorMessage = "Event Description is required*")]
        [Display(Name = "Description")]
        public string EventDescription { get; set; }

        [Required(ErrorMessage = "Event Date and Time is required*")]
        [Display(Name = "Date and Time")]
        public System.DateTime EventDateTime { get; set; }

        [Required(ErrorMessage = "Event Location is required*")]
        [Display(Name = "Location")]
        public string EventLocation { get; set; }
        public string EventImage { get; set; }
        public Nullable<int> AdminID { get; set; }


        [Required(ErrorMessage = "Event Image is required*")]
        [Display(Name = "Choose Image")]

        public Nullable<int> EventStatus { get; set; }

        public HttpPostedFileBase EventImgFile { get; set; }

        public virtual tbl_Admins tbl_Admins { get; set; }
    }
}
