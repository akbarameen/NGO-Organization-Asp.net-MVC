﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class dbNGOEntities6 : DbContext
    {
        public dbNGOEntities6()
            : base("name=dbNGOEntities6")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tbl_aboutus> tbl_aboutus { get; set; }
        public virtual DbSet<tbl_Achivements> tbl_Achivements { get; set; }
        public virtual DbSet<tbl_Admins> tbl_Admins { get; set; }
        public virtual DbSet<tbl_AskQuesions> tbl_AskQuesions { get; set; }
        public virtual DbSet<tbl_AssociativeNGO> tbl_AssociativeNGO { get; set; }
        public virtual DbSet<tbl_Blogs> tbl_Blogs { get; set; }
        public virtual DbSet<tbl_contactus> tbl_contactus { get; set; }
        public virtual DbSet<tbl_DonationCauses> tbl_DonationCauses { get; set; }
        public virtual DbSet<tbl_Donations> tbl_Donations { get; set; }
        public virtual DbSet<tbl_Events> tbl_Events { get; set; }
        public virtual DbSet<tbl_Gallaries> tbl_Gallaries { get; set; }
        public virtual DbSet<tbl_invitation> tbl_invitation { get; set; }
        public virtual DbSet<tbl_Projects> tbl_Projects { get; set; }
        public virtual DbSet<tbl_TeamMembers> tbl_TeamMembers { get; set; }
        public virtual DbSet<tbl_TeamMemberTitles> tbl_TeamMemberTitles { get; set; }
        public virtual DbSet<tbl_Users> tbl_Users { get; set; }
        public virtual DbSet<tbl_Volunteers> tbl_Volunteers { get; set; }
        public virtual DbSet<tbl_VolunteerTypes> tbl_VolunteerTypes { get; set; }
    }
}
