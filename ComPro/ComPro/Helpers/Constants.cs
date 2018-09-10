using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComPro.Helpers
{
    public static class Constants
    {
        public const string NewLoginMessage = "Account is not approved by admin Or Email is not varified Or valid";
        public const string NewRegistrationMessage = "We will send you a confirmation email as soon as possible. Till then, Please wait to Login";
        public const string WarningToNoticeCreatorMessage = "Please fill up the required firld";

        public const string ModelMessage = "Hellow : This is an Auto Message";
        public const string NoticePosting = "Your Notice is successfully posted !! It is waiting for admin approval. ";
        public const string PostEdit = "Your Information is Successfully Modified";
        public const string PostEditFail = "Your Information Not Modified. Try Again ";
        public const string Delete = "User Profile is Successfully Deleted";
        public const string DeleteFail = "User Profile is Not Deleted. Try Again";


        public const string Verified = "your email is successfully verified. Please Log in ";
        public const string NotVerified = "your email is Not verified. Please please verify your email to login ";

        public const string Welcomesubject = "Welcome In BPID ";
        public const string NewUser = "New User Registration ";
        public const string  Emailsubject = "Approval Of Your Account In BPID ";
        public const string Email_Verification_Link = "https://bpid.dk/UserProfile/CheckLink?email=";
        public const string Send = "Email Sent Successfully.";
        public const string Fail = "Problem while sending email, Please check details.";
        public const string SmtpServer = "smtp.gmail.com" ;
        public const string SmtpPort = "587";

        public const string EmptyText = "Sorry Your Searching Text is empty !!!";
        public const string UserResult = "User Profile";
        public const string Start = ", ";
        public const string End = ": ";
        public const string Notice = "Notice";
        public const string Event = "Event";


        public const string EventFail = "Sorry !! Your Event is not Created . Please try again ";
        public const string EventSuccess = "Thank You !! Youe Event is waiting for Admin Approval .";
        public const string EventApprove = "Event Is approved .";
        public const string EventNotApprove = "Event Is Not approved .";
        public const string EventEditSuccess = "Event Is Successfully Modified  .";
        public const string EventEditFail = "Event Is failed to Modified please Try Again .";
        public const string EventEditMessage = "An Event is modified. Please check your Event List .";
        public const string EventDeleteMessage = "An Event is Deleted. Please check your Event List .";
        public const string EventEditPermission = "Sorry !! You are not Permitted to Edit This Event";
        public const string EventDeletePermission = "Sorry !! You are not Permitted to Delete This Event";
        public const string EventDeletetSuccess = "Event Is Successfully Deleted  .";
        public const string EventDeleteFail = "Event Is failed to Deleted please Try Again .";


        public const string User_Feedback = "User FeedBack from ComPro .";



    }
}