using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComPro.Interfaces;
using ComPro.Models;

namespace ComPro.Controllers
{

    [Authorize]
    public class ChatController : Controller
    {
        IChat _chat = new ChatManager();

        //GET: Chat
        public ActionResult ShowChat()
        {
            return View(_chat.All_reciever());
        }

                     
        public ActionResult NewMessage()
        {
            return PartialView("_NewChatPartialview");
        }

        [HttpPost]
        public ActionResult PostNewMessage(FormCollection frm)
        {
            if (ModelState.IsValid)
            {
                var invitees = new List<string>();
                if (!string.IsNullOrEmpty(frm["invitees"]))
                {
                    invitees = frm["invitees"].Split(',').ToList();
                }
                var result = _chat.CreateNewChat(frm["Message"], invitees);
            }
          

            return RedirectToAction("ShowChat");
        }


        public ActionResult Message(string Id)
        {
            
            return PartialView("_MessagePartialViewView", _chat.Chat(Id));
            
        }

        [HttpPost]
        public ActionResult ChatPost(Chat_Data_Pass model)
        {

            if (model != null)
            {

                var result = _chat.Save_Message(model);

                return Content(result.ToString());
            }
            return Content(Boolean.FalseString);
        }
        [Authorize]
        public ActionResult ChatPost()
        {

            return PartialView("_ChatPostPartialview");
        }
        [Authorize]
        public ActionResult ChatGet(string MessageThreadID)
        {
            var result = _chat.Chat_History(MessageThreadID);
            return PartialView("_ChatListPartial", result);
        }


    }
}