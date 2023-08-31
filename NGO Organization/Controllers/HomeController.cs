using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using NGO_Organization.Models;
using System.Web.Helpers;
using System.Threading.Tasks;
using System.Net.Mail;

namespace NGO_Organization.Controllers
{

    public class HomeController : Controller
    {
      dbNGOEntities6 db = new dbNGOEntities6();
        // GET: Home


        #region Index
        public ActionResult Index()
        {



            // Blogs section starts
            var BlogsList = db.tbl_Blogs.ToList();
            ViewBag.BlogList = BlogsList;
            // Blog section ends


            // Partners section starts
            var NgoList = db.tbl_AssociativeNGO.ToList();
            ViewBag.NgoList = NgoList;
            // Partners section Ends


            // Causes section starts
            var CausesList = db.tbl_DonationCauses.ToList();
            ViewBag.CausesList=CausesList;
            // Causes section Ends


            // Projects section starts
            var ProjectList = db.tbl_Projects.ToList();
            ViewBag.ProjectList = ProjectList;
            // projects section ends


            // Events section starts
            var EventsList = db.tbl_Events.ToList();
            ViewBag.EventList = EventsList;
            // Events section ends


            // Our Team section starts
            var teamlist = db.tbl_TeamMembers.ToList();
            ViewBag.tList = teamlist;

            var teamtitles = db.tbl_TeamMemberTitles.ToList();
            ViewBag.tTitles = teamtitles;
            // Our Team section ends


            return View();
        }
        #endregion

        #region Donatation

        public ActionResult Donate()
        {

            
            //for Causes list
            List<tbl_DonationCauses> Causes = db.tbl_DonationCauses.ToList();
            ViewBag.Causes = new SelectList(Causes, "CauseID", "CauseName");



            // for card name list
            var CardList = new List<SelectListItem>
             {
                new SelectListItem{ Text="CREDIT CARD", Value = "Credit Card", Selected = true},
                new SelectListItem{ Text="DEBIT CARD", Value = "Debit Card" },
                new SelectListItem{ Text="VISA CARD", Value = "VISA Card" },
             };
            ViewData["ForCardList"] = CardList;


            return View();
        }

        [HttpPost]
        public ActionResult Donate(tbl_Donations donate)
        {
           
            // for Causes List
            List<tbl_DonationCauses> Causes = db.tbl_DonationCauses.ToList();
            ViewBag.Causes = new SelectList(Causes, "CauseID", "CauseName");


            // for card name list
            var CardList = new List<SelectListItem>
             {
                new SelectListItem{ Text="CREDIT CARD", Value = "Credit Card", Selected = true},
                new SelectListItem{ Text="DEBIT CARD", Value = "Debit Card" },
               
             };
            ViewData["ForCardList"] = CardList;




            if (ModelState.IsValid == true)
            {
          

                db.tbl_Donations.Add(donate);
                int a = db.SaveChanges();
                if (a > 0)
                {
                    TempData["DonationMessage"] = "<script>alert('Thankyou for donation')</script>";
                  
                    return RedirectToAction("index");
                }
            }
        
            return View();


        }

        #endregion

        #region Gallery
        //Gallery section starts here
        public ActionResult GalleryImages()
        {

            var data = db.tbl_Gallaries.ToList();
            return View(data);
        }

        // Gallery section ends here

        #endregion

        #region ContactUs
        public ActionResult contactus()
        {
         
            return View();
        }
        [HttpPost]

        public ActionResult contactus(tbl_contactus contactus)
        {
            if(ModelState.IsValid== true)
            {
                db.tbl_contactus.Add(contactus);
                int a = db.SaveChanges();
                if (a > 0)
                {
                    TempData["ContactMessage"] = "<script>alert('Thankyou for Contacting us')</script>";

                    return RedirectToAction("index");
                }
            }
            return View();
        }
        #endregion

        #region Blogs

        public ActionResult Blogs()
        {
            var blogs = db.tbl_Blogs.ToList();
            return View(blogs);
        }

        public ActionResult BlogsDetails(int? id)
        {
     
            var blogsDetails = db.tbl_Blogs.Find(id);
            return View(blogsDetails);
        }



        #endregion

        #region AboutUs
        public ActionResult Aboutus()
        {
            var About = db.tbl_aboutus.ToList();
            ViewBag.Aboutus = About;



            // Our Team section starts
            var teamlist = db.tbl_TeamMembers.ToList();
            ViewBag.tList = teamlist;

            var teamtitles = db.tbl_TeamMemberTitles.ToList();
            ViewBag.tTitles = teamtitles;
            // Our Team section ends

            return View();
        }



        #endregion

        #region Achivements

        public ActionResult Achivements()
        {
            var achivements = db.tbl_Achivements.ToList();
            return View(achivements);
        }

        public ActionResult AchivementDetails(int? id)
        {

            var achivementDetails = db.tbl_Achivements.Find(id);
            return View(achivementDetails);
        }


        #endregion

        #region Volunteers
        public ActionResult Volunteers()
        {


            List<tbl_VolunteerTypes> types = db.tbl_VolunteerTypes.ToList();
     
            ViewBag.VolunteersType = new SelectList(types, "VolunteerTypeID", "VolunteerTypeName");
            return View();
        }

        [HttpPost]
        public ActionResult Volunteers(tbl_Volunteers volunteers)
        {

            List<tbl_VolunteerTypes> types = db.tbl_VolunteerTypes.ToList();
            ViewBag.VolunteersType = new SelectList(types, "VolunteerTypeID", "VolunteerTypeName");

            if (ModelState.IsValid == true)
            {
                db.tbl_Volunteers.Add(volunteers);
                int a = db.SaveChanges();
                if (a > 0)
                {


                    TempData["DonationMessage"] = "<script>alert('Thankyou For becoming our supporter we will contact you very soon')</script>";

                    return RedirectToAction("index");

                
                }
            }
    

            return View();
        }

        #endregion    

        #region Events

        public ActionResult Events()
        {
            var Events = db.tbl_Events.ToList();
            return View(Events);
        }

        public ActionResult EventDetails(int? id)
        {

            var EventDetails = db.tbl_Events.Find(id);
            return View(EventDetails);
        }



        #endregion

        #region Projects

        public ActionResult Projects()
        {
            var Project = db.tbl_Projects.ToList();
            return View(Project);
        }

        public ActionResult ProjectDetails(int? id)
        {

            var ProjectDetails = db.tbl_Projects.Find(id);
            return View(ProjectDetails);
        }

        #endregion

        #region Causes

        public ActionResult Causes()
        {

            var Causes = db.tbl_DonationCauses.ToList();
            return View(Causes);
        }


        #endregion

        #region OurTeam

        public ActionResult OurTeam()
        {
            var Team = db.tbl_TeamMembers.ToList();
            return View(Team);
        }

        public ActionResult MemberDetails(int? id)
        {

            var ProjectDetails = db.tbl_Projects.Find(id);
            return View(ProjectDetails);
        }


        #endregion

        #region Sending Invitation
        //public ActionResult Invitation()
        //{

        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Contact(tbl_invitation model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
        //        var message = new MailMessage();
        //        message.To.Add(new MailAddress("goharamin145@gmail.com")); //replace with valid value
        //        message.Subject = "Your email subject";
        //        message.Body = string.Format(body, model.InviteToMail, model.InviteMessage);
        //        message.IsBodyHtml = true;
        //        using (var smtp = new SmtpClient())
        //        {
        //            await smtp.SendMailAsync(message);
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return View(model);
        //}

        //[HttpPost]

        //public ActionResult Invitation(tbl_invitation invitation)
        //{


        //    WebMail.Send(invitation.InviteToMail, invitation.InviteMessage, null, null, null, null, true, null, null, null, null, null, null);
        //    return View();
        //}

        #endregion

        #region User Registration

        // sign up user starts here
        public ActionResult Signup()
        {

            return View();
        }
        [HttpPost]

        public ActionResult Signup(tbl_Users signup)
        {
            if (ModelState.IsValid == true)
            {

               
                    db.tbl_Users.Add(signup);
                    int a = db.SaveChanges();
                    if (a > 0)
                    {

                        ViewBag.signup = "Sign up successfully";
                    ModelState.Clear();
                        //TempData["SignupMessage"] = "<script>alert('Signup successfully')</script>";

                        return View();
                    }
            }

            return View();
        }
          

        

        // sign up user ends here


        // login user 
        public ActionResult LoginUser()
        {
        
                return View();
         }


        
        [HttpPost]
        public ActionResult LoginUser(tbl_Users user)
        {
            
        
               
                    var Credentials = db.tbl_Users.Where(model => model.UserName == user.UserName && model.UserPassword == user.UserPassword).FirstOrDefault();
                    if (Credentials == null )
                    {
                        ViewBag.ErrorMessage = "Invalid Username or Password. Please try again with valid Username and Password.";
                        return View();
                    }
                    else
                    {
                        Session["User"] = user.UserName;
                        return RedirectToAction("Index");
                    }

        }

        //login ends

        // Logout user 

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index");
 
        }

        #endregion
    }
}