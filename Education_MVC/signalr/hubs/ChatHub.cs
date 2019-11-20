using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Education_MVC.Models;
using Education_MVC.Controllers;

namespace Education_MVC.signalr.hubs
{
    public class ChatHub : Hub
    {
        public void Send(string name,long hash, string message, string sendTo)
        {
            Clients.All.addNewMessageToPage(name, message, sendTo);
            using (GiaSuOnlineDB db = new GiaSuOnlineDB())
            {
                int chatid = db.Chats.SingleOrDefault(p => p.Hash == hash).ChatID;

                string content = StringCipher.Encrypt(message, name);

                db.ChatDetails.Add(new ChatDetail { ChatID = chatid, ChatDate = DateTime.Now, ChatUser = name, Content = content });
                db.SaveChanges();
            }
        }
    }
}