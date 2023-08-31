using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.IO;
using System.Web.Mvc;
using NGO_Organization.Models;
using System.Web;

namespace NGO.Controllers
{
    public class AdminController : Controller
    {

        dbNGOEntities6 db = new dbNGOEntities6();
        // GET: Admin


        #region Dashboard

        // Dashborard section starts here
        public ActionResult Dashboard()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var users = db.tbl_Users.ToList();
                ViewBag.UsersCount = users.Count;

                var volunteers = db.tbl_Volunteers.ToList();
                ViewBag.VolunteersCount = volunteers.Count;

                var members = db.tbl_TeamMembers.ToList();
                ViewBag.MembersCount = members.Count;

                var Partners = db.tbl_AssociativeNGO.ToList();
                ViewBag.PartnersCount = Partners.Count;

                var membertitles = db.tbl_TeamMemberTitles.ToList();
                ViewBag.MemberTitlesCount = membertitles.Count;

                var Volunteertitles = db.tbl_VolunteerTypes.ToList();
                ViewBag.VolunteerTitlesCount = Volunteertitles.Count;



                var Blogs = db.tbl_Blogs.ToList();
                ViewBag.BlogsCount = Blogs.Count;

                var Events = db.tbl_Events.ToList();
                ViewBag.EventsCount = Events.Count;

                var Projects = db.tbl_Projects.ToList();
                ViewBag.ProjectsList = Projects;
                ViewBag.ProjectsCount = Projects.Count;

                var Causes = db.tbl_DonationCauses.ToList();
                ViewBag.CausesCount = Causes.Count;

                var Achivements = db.tbl_Achivements.ToList();
                ViewBag.AchivementsCount = Achivements.Count;


                var donation = db.tbl_Donations.ToList();
                ViewBag.TotalAmount = donation.Sum(x => x.DonationAmount);


                var contacts = db.tbl_contactus.ToList();
                ViewBag.ContactsList = contacts;
                ViewBag.ContactCount = contacts.Count;



              


                return View();
            }


        }
        // Dashboard section ends here
        #endregion

        #region Login

        // Login section starts here
        public ActionResult Login()
        {
            if (Session["Username"] != null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                HttpCookie cookie = Request.Cookies["user"];
                if (cookie != null)
                {
                    ViewBag.Username = cookie["Username"].ToString();

                    string EncryptedPassword = cookie["password"].ToString();
                    byte[] b = Convert.FromBase64String(EncryptedPassword);
                    string decryptedPassword = ASCIIEncoding.ASCII.GetString(b);
                    ViewBag.Password = decryptedPassword.ToString();

                }
                return View();
            }


        }

        [HttpPost]
        public ActionResult Login(tbl_Admins admin)
        {
            HttpCookie cookie = new HttpCookie("Admin"); // creating cookie object and naming it 

            if (ModelState.IsValid == true)
            {
                // code for remember me starts here



                if (admin.RememberMe == true)
                {

                    cookie["Username"] = admin.AdminUsername; // creating coolkie for Username


                    byte[] b = ASCIIEncoding.ASCII.GetBytes(admin.AdminPassword); // these two lines are for encrypting the password
                    string EncryptedPassword = Convert.ToBase64String(b);

                    cookie["Password"] = EncryptedPassword;// creating coolkie for password
                    cookie.Expires = DateTime.Now.AddDays(2);  // this cookie will be stored for two days
                    HttpContext.Response.Cookies.Add(cookie); // stores cookie in our current browser
                }
                else
                {
                    cookie.Expires = DateTime.Now.AddDays(-1); // these two lines wont let the cookie to be created if the remember me is not checked
                    HttpContext.Response.Cookies.Add(cookie);
                }

                //remember me ends here

                //code for login 
                var Credentials = db.tbl_Admins.Where(model => model.AdminUsername == admin.AdminUsername && model.AdminPassword == admin.AdminPassword).FirstOrDefault();
                if (Credentials == null)
                {
                    ViewBag.ErrorMessage = "Invalid Username or Password. Please try again with valid Username and Password.";
                    return View();
                }
                else
                {
                    Session["Username"] = admin.AdminUsername;
                    return RedirectToAction("Dashboard");
                }
            }
            return View();
        }

        // Login section ends here
        #endregion

        #region logout

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }
        #endregion

        #region AdminsList


        // Admin List section starts here
        public ActionResult Admins()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var Adminlist = db.tbl_Admins.ToList();
                return View(Adminlist);
            }

        }
        #endregion

        #region Lists from UserPanel Data

        // List of Volunteers 
        public ActionResult VolunteersList()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var Volunteers = db.tbl_Volunteers.ToList();
                return View(Volunteers);
            }

        }

        public ActionResult VolunteerDetails(int? id)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var selectedvolunteer = db.tbl_Volunteers.Find(id);
                return View(selectedvolunteer);
            }

        }






        // list of Users

        public ActionResult UsersList()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var Users = db.tbl_Users.ToList();
                return View(Users);
            }

        }

        public ActionResult UsersDetails(int? id)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var selectedUser = db.tbl_Users.Find(id);
                return View(selectedUser);
            }

        }





        // List of Donataions 

        public ActionResult Donations()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var Donation = db.tbl_Donations.ToList();
                return View(Donation);
            }

        }


        // List of Contacts us Messages
        public ActionResult ContactUs()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var Contacts = db.tbl_contactus.ToList();
                return View(Contacts);
            }

        }




        #endregion

        #region TeamTitles

        // Title List section starts here
        public ActionResult TeamMembersTitle()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var title = db.tbl_TeamMemberTitles.ToList();
                return View(title);
            }

        }

        // Title List section Ends here

        // Add Title section starts here

        public ActionResult AddMemberTitle()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }

        }

        [HttpPost]
        public ActionResult AddMemberTitle(tbl_TeamMemberTitles title)
        {


            if (ModelState.IsValid == true)
            {
                db.tbl_TeamMemberTitles.Add(title);
                int a = db.SaveChanges();
                if (a > 0)
                {
                    TempData["AddTitleMessage"] = "<script>alert('Title Added successfully')</script>";
                    return RedirectToAction("TeamMembersTitle");
                }
                else
                {
                    
                    return View();
                }


            }
            return View();




        }

        // Add Title section Ends here

        // Edit Title section starts here
        public ActionResult EditTitle(int? id)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var selectedCategory = db.tbl_TeamMemberTitles.Find(id);
                return View(selectedCategory);
            }

           
        }

        [HttpPost]
        public ActionResult EditTitle(tbl_TeamMemberTitles title)
        {
            if (ModelState.IsValid == true)
            {
                db.Entry(title).State = EntityState.Modified;
                int a = db.SaveChanges();
                if (a > 0)
                {
                    TempData["EditTitleMessage"] = "<script>alert('Title edited successfully')</script>";
                    return RedirectToAction("TeamMembersTitle");
                }
                else
                {
                    TempData["EditTitleMessage"] = "<script>alert('Title edited Failed')</script>";
                }
            }
            return View();
        }
        // Edit Title section ends here

        //public ActionResult DeleteTitle(int? id)
        //{
        //    if (Session["Username"] == null)
        //    {
        //        return RedirectToAction("Login");
        //    }
        //    else
        //    {
        //        var selectedCategory = db.tbl_TeamMemberTitles.Find(id);
        //        return View(selectedCategory);
        //    }


        //}

        //[HttpPost]
        //public ActionResult DeleteTitle(tbl_TeamMemberTitles title)
        //{
        //    if (ModelState.IsValid == true)
        //    {
        //        db.Entry(title).State = EntityState.Modified;
        //        int a = db.SaveChanges();
        //        if (a > 0)
        //        {
        //            TempData["DeleteProductMessage"] = "<script>alert('Product Deleted')</script>";
        //            return RedirectToAction("TeamMembersTitle");
        //        }
        //        else
        //        {
        //            TempData["DeleteProductMessage"] = "<script>alert('Product Not Deleted')</script>";
        //        }
        //    }
        //    return View();
        //}



        #endregion

        #region TeamMembers

                // Team members list section starts here
                public ActionResult TeamMembersList()
                {
                    if (Session["Username"] == null)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                
                        var team = db.tbl_TeamMembers.ToList();
                        return View(team);
                    }
           
                }

                // Team members list section ends here



                // Add Team members section starts here
                public ActionResult AddTeamMember()
                {
                    if (Session["Username"] == null)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        if(ModelState.IsValid == true)
                        {
                            var MemberTitleList = db.tbl_TeamMemberTitles.ToList();
                            ViewBag.TitleList = new SelectList(MemberTitleList, "TitleID", "TitleName");

                        }



                        return View();
                    }
                }
                [HttpPost]
                public ActionResult AddTeamMember(tbl_TeamMembers Member)
                {


                    if (ModelState.IsValid == true)
                    {
                        string FileName = Path.GetFileNameWithoutExtension(Member.MemberImgFile.FileName); // these three lines will store our image into content images Folder
                        string Extension = Path.GetExtension(Member.MemberImgFile.FileName);

                        // Making object of HttpPostedFileBase to get the lenght of image in bytes for size validation
                        HttpPostedFileBase PostedFile = Member.MemberImgFile;
                        int length = PostedFile.ContentLength;

                        //Validation For Image extension
                        if (Extension.ToLower() == ".jpg" || Extension.ToLower() == ".png" || Extension.ToLower() == ".jpeg")
                        {
                            // Validation for Image size
                            if (length <= 1000000)
                            {


                                FileName = FileName + Extension;
                                Member.MemberImage = "~/Content/images/MemberImages/" + FileName;   // This line will store Image into the database
                                FileName = Path.Combine(Server.MapPath("~/Content/images/MemberImages/"), FileName);  //  FileName will save in our Images folder
                                Member.MemberImgFile.SaveAs(FileName); // FileName is now saved in our Images folder


                                // now adding the rest data 
                                db.tbl_TeamMembers.Add(Member);
                                int a = db.SaveChanges();
                                if (a > 0)
                                {
                                    TempData["AddMemberMessage"] = "<script>alert('Member Added successfully')</script>";
                                    return RedirectToAction("TeamMembersList");
                                }



                            }
                            else
                            {
                                ViewBag.SizeErrorMessage = "Image shoud be less than 1MB"; 
                            }
                        }
                        else
                        {
                            ViewBag.ExtensionErrorMessage = "File Extension should be only of .jpg, .png, .jpeg";
                        }


                    }
           
                    return View();

                }

                // Add Team members section ends here


                // Edit Team members section Starts here
                public ActionResult EditTeamMember(int? id)
                {
                    if (Session["Username"] == null)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        var selectedMember = db.tbl_TeamMembers.Find(id);
                        return View(selectedMember);
                    }


                }

                [HttpPost]
                public ActionResult EditTeamMember(tbl_TeamMembers Member)
                {
                    if (ModelState.IsValid == true)
                    {
                        // In editing we will have to upload pic again so:

                        string FileName = Path.GetFileNameWithoutExtension(Member.MemberImgFile.FileName); // these three lines will store our image into content images Folder
                        string Extension = Path.GetExtension(Member.MemberImgFile.FileName);


                        // Making object of HttpPostedFileBase to get the lenght of image in bytes for size validation
                        HttpPostedFileBase PostedFile = Member.MemberImgFile;
                        int length = PostedFile.ContentLength;


                        //Validation For Image extension
                        if (Extension.ToLower()==".jpg" || Extension.ToLower()==".png" || Extension.ToLower() == ".jpeg")
                        {
                            // Validation for Image size
                            if(length<= 1000000)
                            {
                                FileName = FileName + Extension;
                                Member.MemberImage = "~/Content/images/MemberImages/" + FileName;   // This line will store Image into the database
                                FileName = Path.Combine(Server.MapPath("~/Content/images/MemberImages/"), FileName);  //  FileName will save in our Images folder
                                Member.MemberImgFile.SaveAs(FileName); // FileName is now saved in our Images folder


                                // Editing rest data

                                db.Entry(Member).State = EntityState.Modified;
                                int a = db.SaveChanges();
                                if (a > 0)
                                {
                                    TempData["EditMemberMessage"] = "<script>alert('Member updated successfully')</script>";
                                    return RedirectToAction("TeamMembersList");
                                }
                        
                            }
                            else
                            {
                                ViewBag.SizeErrorMessage = "Image shoud be less than 1MB";
                            }
                        }
                        else
                        {
                            ViewBag.ExtensionErrorMessage = "File Extension should be only of .jpg, .png or .jpeg";
                        }

                    }
                    return View();
                }

                // Edit Team members section Ends here


                // Team member Detials section Starts here

                public ActionResult TeamMemberDetails(int? id)
                {
                    if (Session["Username"] == null)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        var selectedMember = db.tbl_TeamMembers.Find(id);
                        return View(selectedMember);
                    }


                }


                // Team member Detials section Ends here


                // Remove Team Member section starts here

                public ActionResult RemoveTeamMember(int? id)
                {
                    if (Session["Username"] == null)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        var data = db.tbl_TeamMembers.Where(model => model.MemberID == id).FirstOrDefault();
                        return View(data);
                    }


                }

                [HttpPost]
                public ActionResult RemoveTeamMember(tbl_TeamMembers member)
                {



                                db.Entry(member).State = EntityState.Deleted;
                                int a = db.SaveChanges();
                                if (a > 0)
                                {
                                    TempData["DeleteMemberMessage"] = "<script>alert('Member Deleted successfully')</script>";
                                    return RedirectToAction("TeamMembersList");
                                }
                    else
                    {
                         TempData["DeleteMemberMessage"] = "<script>alert('Member Deleted Failed')</script>";
                    }





                    return View();

                }




                // Remove Team Member section ends here


                #endregion

        #region volunteerTypes

        // VolunteerTypes List section Starts here
        public ActionResult VolunteerTypesList()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var Types = db.tbl_VolunteerTypes.ToList();
                return View(Types);
            }

        }
        // VolunteerTypes List section Ends here


        // Add VolunteerTypes section Starts here

        public ActionResult AddVolunteerTypes()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }

        }

        [HttpPost]
        public ActionResult AddVolunteerTypes(tbl_VolunteerTypes Types)
        {


            if (ModelState.IsValid == true)
            {
                db.tbl_VolunteerTypes.Add(Types);
                int a = db.SaveChanges();
                if (a > 0)
                {
                    TempData["AddTypeMessage"] = "<script>alert('Volunteer Type Added successfully')</script>";
                    return RedirectToAction("VolunteerTypesList");
                }
                else
                {
                  
                    return View();
                }


            }
            return View();




        }
        // Add VolunteerTypes section Ends here


        // Edit VolunteerTypes section Starts here

        public ActionResult EditVolunteerTypes(int? id)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var selectedType = db.tbl_VolunteerTypes.Find(id);
                return View(selectedType);
            }


        }

        [HttpPost]
        public ActionResult EditVolunteerTypes(tbl_VolunteerTypes Types)
        {
            if (ModelState.IsValid == true)
            {
                db.Entry(Types).State = EntityState.Modified;
                int a = db.SaveChanges();
                if (a > 0)
                {
                    TempData["EditTypeMessage"] = "<script>alert('Type edited successfully')</script>";
                    return RedirectToAction("VolunteerTypesList");
                }
                else
                {
                    TempData["EditTypeMessage"] = "<script>alert('Title edited Failed')</script>";
                }
            }
            return View();
        }

        // Edit VolunteerTypes section Ends here
        #endregion

        #region blogs


        // For Blog List
        public ActionResult Blogs()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var blog = db.tbl_Blogs.ToList();
                return View(blog);
            }
           
        }
        // Blog List ends here


        // Add blog section starts here
        public ActionResult AddBlog()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {

                return View();
            }
        }

        [HttpPost]
        public ActionResult AddBlog(tbl_Blogs Blog)
        {


            if (ModelState.IsValid == true)
            {
                string FileName = Path.GetFileNameWithoutExtension(Blog.BlogImgFile.FileName); // these three lines will store our image into content images Folder
                string Extension = Path.GetExtension(Blog.BlogImgFile.FileName);

                // Making object of HttpPostedFileBase to get the lenght of image in bytes for size validation
                HttpPostedFileBase PostedFile = Blog.BlogImgFile;
                int length = PostedFile.ContentLength;

                //Validation For Image extension
                if (Extension.ToLower() == ".jpg" || Extension.ToLower() == ".png" || Extension.ToLower() == ".jpeg")
                {
                    // Validation for Image size
                    if (length <= 1000000)
                    {


                        FileName = FileName + Extension;
                        Blog.BlogImage = "~/Content/images/BlogImages/" + FileName;   // This line will store Image into the database
                        FileName = Path.Combine(Server.MapPath("~/Content/images/BlogImages/"), FileName);  //  FileName will save in our Images folder
                        Blog.BlogImgFile.SaveAs(FileName); // FileName is now saved in our Images folder


                        // now adding the rest data 
                        db.tbl_Blogs.Add(Blog);
                        int a = db.SaveChanges();
                        if (a > 0)
                        {
                            TempData["AddBlogMessage"] = "<script>alert('Blog Added successfully')</script>";
                            return RedirectToAction("Blogs");
                        }



                    }
                    else
                    {
                        ViewBag.SizeErrorMessage = "Image shoud be less than 1MB";
                    }
                }
                else
                {
                    ViewBag.ExtensionErrorMessage = "File Extension should be only of .jpg, .png, .jpeg";
                }


            }

            return View();

        }
        // AddBlog section ends here


        // Edit blogs section starts here
        public ActionResult Editblog(int? id)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var selectedBlog = db.tbl_Blogs.Find(id);
                return View(selectedBlog);
            }


        }

        [HttpPost]
        public ActionResult EditBlog(tbl_Blogs Blog)
        {


            if (ModelState.IsValid == true)
            {
                // By editing a blog we will have to upload image again so

                string FileName = Path.GetFileNameWithoutExtension(Blog.BlogImgFile.FileName); // these three lines will store our image into content images Folder
                string Extension = Path.GetExtension(Blog.BlogImgFile.FileName);

                // Making object of HttpPostedFileBase to get the lenght of image in bytes for size validation
                HttpPostedFileBase PostedFile = Blog.BlogImgFile;
                int length = PostedFile.ContentLength;

                //Validation For Image extension
                if (Extension.ToLower() == ".jpg" || Extension.ToLower() == ".png" || Extension.ToLower() == ".jpeg")
                {
                    // Validation for Image size
                    if (length <= 1000000)
                    {


                        FileName = FileName + Extension;
                        Blog.BlogImage = "~/Content/images/BlogImages/" + FileName;   // This line will store Image into the database
                        FileName = Path.Combine(Server.MapPath("~/Content/images/BlogImages/"), FileName);  //  FileName will save in our Images folder
                        Blog.BlogImgFile.SaveAs(FileName); // FileName is now saved in our Images folder


                        // Editing rest data

                        db.Entry(Blog).State = EntityState.Modified;
                        int a = db.SaveChanges();
                        if (a > 0)
                        {
                            TempData["EditBlogMessage"] = "<script>alert('Blog updated successfully')</script>";
                            return RedirectToAction("Blogs");
                        }




                    }
                    else
                    {
                        ViewBag.SizeErrorMessage = "Image shoud be less than 1MB";
                    }
                }
                else
                {
                    ViewBag.ExtensionErrorMessage = "File Extension should be only of .jpg, .png, .jpeg";
                }


            }

            return View();

        }

        // Edit blogs section ends here


        // Blogs Detials section Starts here

        public ActionResult BlogDetails(int? id)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var selectedBlog = db.tbl_Blogs.Find(id);
                return View(selectedBlog);
            }


        }


        // Blog Detials section Ends here







        #endregion

        #region Our Achivements

        // For Achivements List
        public ActionResult Achivements()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var achivements = db.tbl_Achivements.ToList();
                return View(achivements);
            }

        }
        // Achivements List ends here


        // Add Achivements section starts here
        public ActionResult AddAchivement()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {

                return View();
            }
        }

        [HttpPost]
        public ActionResult AddAchivement(tbl_Achivements achivement)
        {


            if (ModelState.IsValid == true)
            {
                string FileName = Path.GetFileNameWithoutExtension(achivement.AchivementImgFile.FileName); // these three lines will store our image into content images Folder
                string Extension = Path.GetExtension(achivement.AchivementImgFile.FileName);

                // Making object of HttpPostedFileBase to get the lenght of image in bytes for size validation
                HttpPostedFileBase PostedFile = achivement.AchivementImgFile;
                int length = PostedFile.ContentLength;

                //Validation For Image extension
                if (Extension.ToLower() == ".jpg" || Extension.ToLower() == ".png" || Extension.ToLower() == ".jpeg")
                {
                    // Validation for Image size
                    if (length <= 1000000)
                    {


                        FileName = FileName + Extension;
                        achivement.AchiveImage = "~/Content/images/AchivementImages/" + FileName;   // This line will store Image into the database
                        FileName = Path.Combine(Server.MapPath("~/Content/images/AchivementImages/"), FileName);  //  FileName will save in our Images folder
                        achivement.AchivementImgFile.SaveAs(FileName); // FileName is now saved in our Images folder


                        // now adding the rest data 
                        db.tbl_Achivements.Add(achivement);
                        int a = db.SaveChanges();
                        if (a > 0)
                        {
                            TempData["AddAchivementMessage"] = "<script>alert('Achivement Added successfully')</script>";
                            return RedirectToAction("Achivements");
                        }



                    }
                    else
                    {
                        ViewBag.SizeErrorMessage = "Image shoud be less than 1MB";
                    }
                }
                else
                {
                    ViewBag.ExtensionErrorMessage = "File Extension should be only of .jpg, .png, .jpeg";
                }


            }

            return View();

        }
        // Add Achivements section ends here


        // Edit Achivements section starts here
        public ActionResult EditAchivement(int? id)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {

                var selectedAchivements = db.tbl_Achivements.Find(id);
                return View(selectedAchivements);
            }
        }

        [HttpPost]
        public ActionResult EditAchivement(tbl_Achivements achivement)
        {


            if (ModelState.IsValid == true)
            {
                string FileName = Path.GetFileNameWithoutExtension(achivement.AchivementImgFile.FileName); // these three lines will store our image into content images Folder
                string Extension = Path.GetExtension(achivement.AchivementImgFile.FileName);

                // Making object of HttpPostedFileBase to get the lenght of image in bytes for size validation
                HttpPostedFileBase PostedFile = achivement.AchivementImgFile;
                int length = PostedFile.ContentLength;

                //Validation For Image extension
                if (Extension.ToLower() == ".jpg" || Extension.ToLower() == ".png" || Extension.ToLower() == ".jpeg")
                {
                    // Validation for Image size
                    if (length <= 1000000)
                    {


                        FileName = FileName + Extension;
                        achivement.AchiveImage = "~/Content/images/AchivementImages/" + FileName;   // This line will store Image into the database
                        FileName = Path.Combine(Server.MapPath("~/Content/images/AchivementImages/"), FileName);  //  FileName will save in our Images folder
                        achivement.AchivementImgFile.SaveAs(FileName); // FileName is now saved in our Images folder


                        // Editing rest data

                        db.Entry(achivement).State = EntityState.Modified;
                        int a = db.SaveChanges();
                        if (a > 0)
                        {
                            TempData["EditAchivementMessage"] = "<script>alert('Achivement updated successfully')</script>";
                            return RedirectToAction("Achivements");
                        }





                    }
                    else
                    {
                        ViewBag.SizeErrorMessage = "Image shoud be less than 1MB";
                    }
                }
                else
                {
                    ViewBag.ExtensionErrorMessage = "File Extension should be only of .jpg, .png, .jpeg";
                }


            }

            return View();

        }
        // Edit Achivements section ends here



        // Achivement Detials section Starts here

        public ActionResult AchivementDetails(int? id)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var selectedAchivement = db.tbl_Achivements.Find(id);
                return View(selectedAchivement);
            }


        }


        // Achivement Detials section Ends here


        // Remove Achivement section starts here

        public ActionResult RemoveAchivement(int? id)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var selectedAchivement = db.tbl_Achivements.Find(id);
                return View(selectedAchivement);
            }


        }



        // Remove Achivement section ends here



        #endregion

        #region DonationCauses
        // Causes List section Starts here
        public ActionResult CausesList()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var Causes = db.tbl_DonationCauses.ToList();
                return View(Causes);
            }

        }
        // Causes List section Ends here


        // Add Causes section Starts here

        public ActionResult AddCause()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {

                return View();
            }
        }

        [HttpPost]
        public ActionResult AddCause(tbl_DonationCauses Cause)
        {


            if (ModelState.IsValid == true)
            {
                string FileName = Path.GetFileNameWithoutExtension(Cause.CauseImgFile.FileName); // these three lines will store our image into content images Folder
                string Extension = Path.GetExtension(Cause.CauseImgFile.FileName);

                // Making object of HttpPostedFileBase to get the lenght of image in bytes for size validation
                HttpPostedFileBase PostedFile = Cause.CauseImgFile;
                int length = PostedFile.ContentLength;

                //Validation For Image extension
                if (Extension.ToLower() == ".jpg" || Extension.ToLower() == ".png" || Extension.ToLower() == ".jpeg")
                {
                    // Validation for Image size
                    if (length <= 1000000)
                    {


                        FileName = FileName + Extension;
                        Cause.CauseImage = "~/Content/images/CausesImages/" + FileName;   // This line will store Image into the database
                        FileName = Path.Combine(Server.MapPath("~/Content/images/CausesImages/"), FileName);  //  FileName will save in our Images folder
                        Cause.CauseImgFile.SaveAs(FileName); // FileName is now saved in our Images folder


                        // now adding the rest data 
                        db.tbl_DonationCauses.Add(Cause);
                        int a = db.SaveChanges();
                        if (a > 0)
                        {
                            //TempData["message"] = "Caused added successfully";
                            TempData["AddCauseMessage"] = "<script>alert('Cause Added successfully')</script>";
                            return RedirectToAction("CausesList");
                        }



                    }
                    else
                    {
                        ViewBag.SizeErrorMessage = "Image shoud be less than 1MB";
                    }
                }
                else
                {
                    ViewBag.ExtensionErrorMessage = "File Extension should be only of .jpg, .png, .jpeg";
                }


            }

            return View();
        }


        // Add Causes section Ends here





        // Edit Causes section Starts here
        public ActionResult EditCause(int? id)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var selectedCause = db.tbl_DonationCauses.Find(id);
                return View(selectedCause);
            }


        }

        [HttpPost]
        public ActionResult EditCause(tbl_DonationCauses Cause)
        {


            if (ModelState.IsValid == true)
            {
                string FileName = Path.GetFileNameWithoutExtension(Cause.CauseImgFile.FileName); // these three lines will store our image into content images Folder
                string Extension = Path.GetExtension(Cause.CauseImgFile.FileName);

                // Making object of HttpPostedFileBase to get the lenght of image in bytes for size validation
                HttpPostedFileBase PostedFile = Cause.CauseImgFile;
                int length = PostedFile.ContentLength;

                //Validation For Image extension
                if (Extension.ToLower() == ".jpg" || Extension.ToLower() == ".png" || Extension.ToLower() == ".jpeg")
                {
                    // Validation for Image size
                    if (length <= 1000000)
                    {


                        FileName = FileName + Extension;
                        Cause.CauseImage = "~/Content/images/CausesImages/" + FileName;   // This line will store Image into the database
                        FileName = Path.Combine(Server.MapPath("~/Content/images/CausesImages/"), FileName);  //  FileName will save in our Images folder
                        Cause.CauseImgFile.SaveAs(FileName); // FileName is now saved in our Images folder


                        // Editing rest data

                        db.Entry(Cause).State = EntityState.Modified;
                        int a = db.SaveChanges();
                        if (a > 0)
                        {
                            TempData["EditCauseMessage"] = "<script>alert('Cause updated successfully')</script>";
                            return RedirectToAction("CausesList");
                        }



                    }
                    else
                    {
                        ViewBag.SizeErrorMessage = "Image shoud be less than 1MB";
                    }
                }
                else
                {
                    ViewBag.ExtensionErrorMessage = "File Extension should be only of .jpg, .png, .jpeg";
                }


            }

            return View();
        }


        // Edit Causes section Ends here

        // Causes Detail section starts here

        public ActionResult CauseDetails(int? id)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var selectedBlog = db.tbl_DonationCauses.Find(id);
                return View(selectedBlog);
            }


        }

        // Causes Detail section Ends here





        #endregion

        #region Events

        // For Blog List
        public ActionResult Events()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var Event = db.tbl_Events.ToList();
                return View(Event);
            }

        }
        // Blog List ends here


        // Add blog section starts here
        public ActionResult AddEvent()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {

                return View();
            }
        }

        [HttpPost]
        public ActionResult AddEvent(tbl_Events Event)
        {


            if (ModelState.IsValid == true)
            {
                string FileName = Path.GetFileNameWithoutExtension(Event.EventImgFile.FileName); // these three lines will store our image into content images Folder
                string Extension = Path.GetExtension(Event.EventImgFile.FileName);

                // Making object of HttpPostedFileBase to get the lenght of image in bytes for size validation
                HttpPostedFileBase PostedFile = Event.EventImgFile;
                int length = PostedFile.ContentLength;

                //Validation For Image extension
                if (Extension.ToLower() == ".jpg" || Extension.ToLower() == ".png" || Extension.ToLower() == ".jpeg")
                {
                    // Validation for Image size
                    if (length <= 1000000)
                    {


                        FileName = FileName + Extension;
                        Event.EventImage = "~/Content/images/EventImages/" + FileName;   // This line will store Image into the database
                        FileName = Path.Combine(Server.MapPath("~/Content/images/EventImages/"), FileName);  //  FileName will save in our Images folder
                        Event.EventImgFile.SaveAs(FileName); // FileName is now saved in our Images folder


                        // now adding the rest data 
                        db.tbl_Events.Add(Event);
                        int a = db.SaveChanges();
                        if (a > 0)
                        {
                            TempData["AddEventMessage"] = "<script>alert('Event Added successfully')</script>";
                            return RedirectToAction("Events");
                        }



                    }
                    else
                    {
                        ViewBag.SizeErrorMessage = "Image shoud be less than 1MB";
                    }
                }
                else
                {
                    ViewBag.ExtensionErrorMessage = "File Extension should be only of .jpg, .png, .jpeg";
                }


            }

            return View();

        }
        // AddEvent section ends here

        // Edit Event section starts here
        public ActionResult EditEvent(int? id)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var selectedEvent = db.tbl_Events.Find(id);
                return View(selectedEvent);
            }


        }

        [HttpPost]
        public ActionResult EditEvent(tbl_Events Event)
        {


            if (ModelState.IsValid == true)
            {
                string FileName = Path.GetFileNameWithoutExtension(Event.EventImgFile.FileName); // these three lines will store our image into content images Folder
                string Extension = Path.GetExtension(Event.EventImgFile.FileName);

                // Making object of HttpPostedFileBase to get the lenght of image in bytes for size validation
                HttpPostedFileBase PostedFile = Event.EventImgFile;
                int length = PostedFile.ContentLength;

                //Validation For Image extension
                if (Extension.ToLower() == ".jpg" || Extension.ToLower() == ".png" || Extension.ToLower() == ".jpeg")
                {
                    // Validation for Image size
                    if (length <= 1000000)
                    {


                        FileName = FileName + Extension;
                        Event.EventImage = "~/Content/images/EventImages/" + FileName;   // This line will store Image into the database
                        FileName = Path.Combine(Server.MapPath("~/Content/images/EventImages/"), FileName);  //  FileName will save in our Images folder
                        Event.EventImgFile.SaveAs(FileName); // FileName is now saved in our Images folder


                        // Editing rest data

                        db.Entry(Event).State = EntityState.Modified;
                        int a = db.SaveChanges();
                        if (a > 0)
                        {
                            TempData["EditEventMessage"] = "<script>alert('Event updated successfully')</script>";
                            return RedirectToAction("Events");
                        }



                    }
                    else
                    {
                        ViewBag.SizeErrorMessage = "Image shoud be less than 1MB";
                    }
                }
                else
                {
                    ViewBag.ExtensionErrorMessage = "File Extension should be only of .jpg, .png, .jpeg";
                }


            }

            return View();

        }
        // Edit Event section ends here


        // Event Detials section Starts here

        public ActionResult EventDetails(int? id)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var selectedEvent = db.tbl_Events.Find(id);
                return View(selectedEvent);
            }


        }


        // Event Detials section Ends here


        // Remove Event section starts here

        public ActionResult RemoveEvent(int? id)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var selectedEvent = db.tbl_Achivements.Find(id);
                return View(selectedEvent);
            }


        }

        #endregion

        #region Projects

        // For Project List
        public ActionResult Projects()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var project = db.tbl_Projects.ToList();
                return View(project);
            }

        }
        // project List ends here


        // Add Project section starts here
        public ActionResult AddProject()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                if(ModelState.IsValid == true)
                {
                    var MembersList = db.tbl_TeamMembers.ToList();
                    ViewBag.MembersList = new SelectList(MembersList, "MemberID", "MemberName");
                }
           

                return View();
            }
        }

        [HttpPost]
        public ActionResult AddProject(tbl_Projects project)
        {


            if (ModelState.IsValid == true)
            {
                string FileName = Path.GetFileNameWithoutExtension(project.ProjectImgFIle.FileName); // these three lines will store our image into content images Folder
                string Extension = Path.GetExtension(project.ProjectImgFIle.FileName);

                // Making object of HttpPostedFileBase to get the lenght of image in bytes for size validation
                HttpPostedFileBase PostedFile = project.ProjectImgFIle;
                int length = PostedFile.ContentLength;

                //Validation For Image extension
                if (Extension.ToLower() == ".jpg" || Extension.ToLower() == ".png" || Extension.ToLower() == ".jpeg")
                {
                    // Validation for Image size
                    if (length <= 1000000)
                    {


                        FileName = FileName + Extension;
                        project.ProjectImage = "~/Content/images/ProjectImages/" + FileName;   // This line will store Image into the database
                        FileName = Path.Combine(Server.MapPath("~/Content/images/ProjectImages/"), FileName);  //  FileName will save in our Images folder
                        project.ProjectImgFIle.SaveAs(FileName); // FileName is now saved in our Images folder


                        // now adding the rest data 
                        db.tbl_Projects.Add(project);
                        int a = db.SaveChanges();
                        if (a > 0)
                        {
                            TempData["AddProjectMessage"] = "<script>alert('Project Added successfully')</script>";
                            return RedirectToAction("Projects");
                        }



                    }
                    else
                    {
                        ViewBag.SizeErrorMessage = "Image shoud be less than 1MB";
                    }
                }
                else
                {
                    ViewBag.ExtensionErrorMessage = "File Extension should be only of .jpg, .png, .jpeg";
                }


            }

            return View();

        }
        // Add project section ends here




        // Add Project section starts here
        public ActionResult EditProject(int? id)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                if (ModelState.IsValid == true)
                {
                    var MembersList = db.tbl_TeamMembers.ToList();
                    ViewBag.MembersList = new SelectList(MembersList, "MemberID", "MemberName");
                }
                var selectedProject = db.tbl_Projects.Find(id);
                return View(selectedProject);

              
            }
        }

        [HttpPost]
        public ActionResult EditProject(tbl_Projects project)
        {


            if (ModelState.IsValid == true)
            {
                string FileName = Path.GetFileNameWithoutExtension(project.ProjectImgFIle.FileName); // these three lines will store our image into content images Folder
                string Extension = Path.GetExtension(project.ProjectImgFIle.FileName);

                // Making object of HttpPostedFileBase to get the lenght of image in bytes for size validation
                HttpPostedFileBase PostedFile = project.ProjectImgFIle;
                int length = PostedFile.ContentLength;

                //Validation For Image extension
                if (Extension.ToLower() == ".jpg" || Extension.ToLower() == ".png" || Extension.ToLower() == ".jpeg")
                {
                    // Validation for Image size
                    if (length <= 1000000)
                    {


                        FileName = FileName + Extension;
                        project.ProjectImage = "~/Content/images/ProjectImages/" + FileName;   // This line will store Image into the database
                        FileName = Path.Combine(Server.MapPath("~/Content/images/ProjectImages/"), FileName);  //  FileName will save in our Images folder
                        project.ProjectImgFIle.SaveAs(FileName); // FileName is now saved in our Images folder


                        // Editing rest data

                        db.Entry(project).State = EntityState.Modified;
                        int a = db.SaveChanges();
                        if (a > 0)
                        {
                            TempData["EditProjectMessage"] = "<script>alert('Project updated successfully')</script>";
                            return RedirectToAction("Projects");
                        }




                    }
                    else
                    {
                        ViewBag.SizeErrorMessage = "Image shoud be less than 1MB";
                    }
                }
                else
                {
                    ViewBag.ExtensionErrorMessage = "File Extension should be only of .jpg, .png, .jpeg";
                }


            }

            return View();

        }


        // edit project section ends here

        // Team member Detials section Starts here

        public ActionResult ProjectDetails(int? id)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var selectedProject = db.tbl_Projects.Find(id);
                return View(selectedProject);
            }


        }


        // Team member Detials section Ends here


        // Remove Team Member section starts here

        public ActionResult RemoveProject(int? id)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var data = db.tbl_TeamMembers.Where(model => model.MemberID == id).FirstOrDefault();
                return View(data);
            }


        }

        [HttpPost]
        public ActionResult RemoveProject(tbl_TeamMembers member)
        {



            db.Entry(member).State = EntityState.Deleted;
            int a = db.SaveChanges();
            if (a > 0)
            {
                TempData["DeleteProjectMessage"] = "<script>alert('Member Deleted successfully')</script>";
                return RedirectToAction("TeamMembersList");
            }
            else
            {
                TempData["DeleteProjectMessage"] = "<script>alert('Member Deleted Failed')</script>";
            }





            return View();

        }




        // Remove Team Member section ends here

        #endregion

        #region AssociativeNGOs

        // NGO List section Starts here
        public ActionResult AssociativeNGOs()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var ngo = db.tbl_AssociativeNGO.ToList();
                return View(ngo);
            }

        }
        // NGO list  section Ends here


        // Add NGO section Starts here

        public ActionResult AddNGO()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {

                return View();
            }
        }

        [HttpPost]
        public ActionResult AddNGO(tbl_AssociativeNGO ngo)
        {


            if (ModelState.IsValid == true)
            {
                string FileName = Path.GetFileNameWithoutExtension(ngo.NgoLogoFile.FileName); // these three lines will store our image into content images Folder
                string Extension = Path.GetExtension(ngo.NgoLogoFile.FileName);

                // Making object of HttpPostedFileBase to get the lenght of image in bytes for size validation
                HttpPostedFileBase PostedFile = ngo.NgoLogoFile;
                int length = PostedFile.ContentLength;

                //Validation For Image extension
                if (Extension.ToLower() == ".jpg" || Extension.ToLower() == ".png" || Extension.ToLower() == ".jpeg")
                {
                    // Validation for Image size
                    if (length <= 1000000)
                    {


                        FileName = FileName + Extension;
                        ngo.NgoLogo = "~/Content/images/NGOLogos/" + FileName;   // This line will store Image into the database
                        FileName = Path.Combine(Server.MapPath("~/Content/images/NGOLogos/"), FileName);  //  FileName will save in our Images folder
                        ngo.NgoLogoFile.SaveAs(FileName); // FileName is now saved in our Images folder


                        // now adding the rest data 
                        db.tbl_AssociativeNGO.Add(ngo);
                        int a = db.SaveChanges();
                        if (a > 0)
                        {
                            //TempData["message"] = "Caused added successfully";
                            TempData["AddNgoMessage"] = "<script>alert('Partner Added successfully')</script>";
                            return RedirectToAction("AssociativeNGOs");
                        }



                    }
                    else
                    {
                        ViewBag.SizeErrorMessage = "Image shoud be less than 1MB";
                    }
                }
                else
                {
                    ViewBag.ExtensionErrorMessage = "File Extension should be only of .jpg, .png, .jpeg";
                }


            }

            return View();
        }


        // Add NGO section Ends here


        // Edit NGO section Starts here

        public ActionResult EditNGO(int? id)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var SelectedNGO = db.tbl_AssociativeNGO.Find(id);
                return View(SelectedNGO);
            }
        }

        [HttpPost]
        public ActionResult EditNGO(tbl_AssociativeNGO ngo)
        {


            if (ModelState.IsValid == true)
            {
                string FileName = Path.GetFileNameWithoutExtension(ngo.NgoLogoFile.FileName); // these three lines will store our image into content images Folder
                string Extension = Path.GetExtension(ngo.NgoLogoFile.FileName);

                // Making object of HttpPostedFileBase to get the lenght of image in bytes for size validation
                HttpPostedFileBase PostedFile = ngo.NgoLogoFile;
                int length = PostedFile.ContentLength;

                //Validation For Image extension
                if (Extension.ToLower() == ".jpg" || Extension.ToLower() == ".png" || Extension.ToLower() == ".jpeg")
                {
                    // Validation for Image size
                    if (length <= 1000000)
                    {


                        FileName = FileName + Extension;
                        ngo.NgoLogo = "~/Content/images/NGOLogos/" + FileName;   // This line will store Image into the database
                        FileName = Path.Combine(Server.MapPath("~/Content/images/NGOLogos/"), FileName);  //  FileName will save in our Images folder
                        ngo.NgoLogoFile.SaveAs(FileName); // FileName is now saved in our Images folder


                        // Editing rest data

                        db.Entry(ngo).State = EntityState.Modified;
                        int a = db.SaveChanges();
                        if (a > 0)
                        {
                            TempData["EditNGOMessage"] = "<script>alert('Partner NGO updated successfully')</script>";
                            return RedirectToAction("AssociativeNGOs");
                        }



                    }
                    else
                    {
                        ViewBag.SizeErrorMessage = "Image shoud be less than 1MB";
                    }
                }
                else
                {
                    ViewBag.ExtensionErrorMessage = "File Extension should be only of .jpg, .png, .jpeg";
                }


            }

            return View();
        }

        // Edit ngo section completed here


        // NGO detail section starts here

        public ActionResult NGODetails(int? id)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var selectedNGO = db.tbl_AssociativeNGO.Find(id);
                return View(selectedNGO);
            }


        }
        // NGO Detials section Ends here


        // Remove NGO section starts here

        public ActionResult RemoveNGO(int? id)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var selectedBlog = db.tbl_Blogs.Find(id);
                return View(selectedBlog);
            }


        }



        // Remove NGO section ends here


        #endregion

        #region QNA's



        #endregion

        #region Gallery


        // For GalleryImages List
        public ActionResult GalleryImagesList()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var GalleryImages = db.tbl_Gallaries.ToList();
                return View(GalleryImages);
            }

        }
        // GalleryImages List ends here


        // Add GalleryImages section starts here
        public ActionResult AddGalleryImage()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {

                return View();
            }
        }

        [HttpPost]
        public ActionResult AddGalleryImage(tbl_Gallaries Gallery)
        {


            if (ModelState.IsValid == true)
            {
                
                string FileName = Path.GetFileNameWithoutExtension(Gallery.GalleryImgFile.FileName); // these three lines will store our image into content images Folder
                string Extension = Path.GetExtension(Gallery.GalleryImgFile.FileName);

                // Making object of HttpPostedFileBase to get the lenght of image in bytes for size validation
                HttpPostedFileBase PostedFile = Gallery.GalleryImgFile;
                int length = PostedFile.ContentLength;

                //Validation For Image extension
                if (Extension.ToLower() == ".jpg" || Extension.ToLower() == ".png" || Extension.ToLower() == ".jpeg")
                {
                    // Validation for Image size
                    if (length <= 1000000)
                    {


                        FileName = FileName + Extension;
                        Gallery.GallaryImage = "~/Content/images/GalleryImages/" + FileName;   // This line will store Image into the database
                        FileName = Path.Combine(Server.MapPath("~/Content/images/GalleryImages/"), FileName);  //  FileName will save in our Images folder
                        Gallery.GalleryImgFile.SaveAs(FileName); // FileName is now saved in our Images folder


                        // now adding the rest data 
                        db.tbl_Gallaries.Add(Gallery);
                        int a = db.SaveChanges();
                        if (a > 0)
                        {
                            TempData["AddGalleryMessage"] = "<script>alert('Images Uploaded successfully')</script>";
                            return RedirectToAction("GalleryImagesList");
                        }



                    }
                    else
                    {
                        ViewBag.SizeErrorMessage = "Image shoud be less than 1MB";
                    }
                }
                else
                {
                    ViewBag.ExtensionErrorMessage = "File Extension should be only of .jpg, .png, .jpeg";
                }


            }

            return View();

        }
        // Add GalleryImages section ends here


        //Edit Gallery section starts here

        public ActionResult EditGalleryImage(int? id)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {

                var selectedIamge = db.tbl_Gallaries.Find(id);
                return View(selectedIamge);
            }
        }

        [HttpPost]
        public ActionResult EditGalleryImage(tbl_Gallaries Gallery)
        {


            if (ModelState.IsValid == true)
            {

                string FileName = Path.GetFileNameWithoutExtension(Gallery.GalleryImgFile.FileName); // these three lines will store our image into content images Folder
                string Extension = Path.GetExtension(Gallery.GalleryImgFile.FileName);

                // Making object of HttpPostedFileBase to get the lenght of image in bytes for size validation
                HttpPostedFileBase PostedFile = Gallery.GalleryImgFile;
                int length = PostedFile.ContentLength;

                //Validation For Image extension
                if (Extension.ToLower() == ".jpg" || Extension.ToLower() == ".png" || Extension.ToLower() == ".jpeg")
                {
                    // Validation for Image size
                    if (length <= 1000000)
                    {


                        FileName = FileName + Extension;
                        Gallery.GallaryImage = "~/Content/images/GalleryImages/" + FileName;   // This line will store Image into the database
                        FileName = Path.Combine(Server.MapPath("~/Content/images/GalleryImages/"), FileName);  //  FileName will save in our Images folder
                        Gallery.GalleryImgFile.SaveAs(FileName); // FileName is now saved in our Images folder

                        // Editing rest data

                        db.Entry(Gallery).State = EntityState.Modified;
                        int a = db.SaveChanges();
                        if (a > 0)
                        {
                            TempData["EditGalleryMessage"] = "<script>alert('Gallery image updated successfully')</script>";
                            return RedirectToAction("GalleryImagesList");
                        }


                    }
                    else
                    {
                        ViewBag.SizeErrorMessage = "Image shoud be less than 1MB";
                    }
                }
                else
                {
                    ViewBag.ExtensionErrorMessage = "File Extension should be only of .jpg, .png, .jpeg";
                }


            }

            return View();

        }

        //Edit Gallery secion ends here

        //// Delete gallery section starts here
        //public ActionResult RemoveImage(int? id)
        //{
        //    var Selectedimg = db.tbl_Gallaries.Find(id);
        //    return View(Selectedimg);
        //}

        //[HttpPost]
        //public ActionResult RemoveImage(tbl_Gallaries img)
        //{
        //    if (ModelState.IsValid == true)
        //    {
        //        db.Entry(img).State = EntityState.Modified;
        //        int a = db.SaveChanges();
        //        if (a > 0)
        //        {
        //            TempData["DeleteImgMessage"] = "<script>alert('Image Deleted')</script>";
        //            return RedirectToAction("GalleryImagesList");
        //        }
        //        else
        //        {
        //            TempData["DeleteImgMessage"] = "<script>alert('Image Not Deleted')</script>";
        //        }
        //    }
        //    return View();
        //}


        #endregion

        #region About us

        // About List section starts here
        public ActionResult Aboutus()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var aboutus = db.tbl_aboutus.ToList();
                return View(aboutus);
            }

        }

        // About List section Ends here


        // Edit About section starts here
        public ActionResult EditAboutus(int? id)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var aboutus = db.tbl_aboutus.Find(id);
                return View(aboutus);
            }


        }

        [HttpPost]
        public ActionResult EditAboutus(tbl_aboutus about)
        {
            if (ModelState.IsValid == true)
            {
                db.Entry(about).State = EntityState.Modified;
                int a = db.SaveChanges();
                if (a > 0)
                {
                    TempData["EditMessage"] = "<script>alert('about us updated successfully')</script>";
                    return RedirectToAction("Aboutus");
                }
                else
                {
                    TempData["EditMessage"] = "<script>alert('Title edited Failed')</script>";
                }
            }
            return View();
        }



        #endregion


    }
}