using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Wcf_Chat
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "ServiceChat" в коде и файле конфигурации.
    public class ServiceChat : IServiceChat
    {
        List<ServerUser> users = new List<ServerUser>();
        int nextID = 1;

        public int Connect(string name)
        {
            ServerUser user = new ServerUser()
            {
                ID = nextID,
                Name = name,
                operationContext = OperationContext.Current
            };
            nextID += 1;
            SendMessage(0, ": " + user.Name+" connected");
            users.Add(user);
            return user.ID;
        }

        public void Disconnect(int id)
        {
            ServerUser user = users.FirstOrDefault(u => u.ID == id);
            if (user != null)
            {
                users.Remove(user);
                SendMessage(0, ": " + user.Name + " disconnected");
            }
            else { }
        }

       

        public void SendMessage(int id, string message)
        {
           foreach (ServerUser member in users)
            {
                string answer = DateTime.Now.ToShortTimeString();
                ServerUser user = users.FirstOrDefault(u => u.ID == id);
                if (user != null)
                {
                    answer += ": " + user.Name + " ";
                }
                answer += message;
                member.operationContext.GetCallbackChannel<IServerChatCallback>().MessageCallback(answer);
            }
        }

    }
}
