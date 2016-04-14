using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atentis.Connection;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class Accounts
    {
        #region propeties

        public static List<Accounts> accounts = new List<Accounts>();

        #endregion

        #region Save/Upload

        public static void load()
        {
        
        }

        #endregion

        #region Connections
        public Slot slot
        { get; private set; }

        public string name
        { get; private set; }

        private string login;

        private string password;

        private string server;

        private Int32 port;

        public Accounts(string name, string login, string password, string server, int port)
        {
            this.login = login;
            this.name = name;
            this.password = password;
            this.server = server;
            this.port = port;
        }

        public void connect()
        {
            this.slot.rqs = new RequestSocket(this.slot);
            this.slot.rqs.Init();
            this.slot.Start();
            this.setSlotEventHandlers(this.slot);
            this.setRSocketEventHandlers(this.slot);
        }

        public void disconnect()
        {
            this.slot.Disconnect();
            this.removeRSocketEventHandlers(this.slot);
            this.removeSlotEventHandlers(this.slot);
        }

        private void slot_evhSlotStateChanged(object sender, SlotEventArgs e)
        {
            if (e.State == SlotState.Denied)
            {
                (Application.OpenForms[0] as Form1).AddEvent(e.Slot.SlotID, "Denied");
            }
            if (e.State == SlotState.Disconnected)
            {
                (Application.OpenForms[0] as Form1).AddEvent(e.Slot.SlotID, "Disconnected");
            }
            if (e.State == SlotState.Connected)
            {
                (Application.OpenForms[0] as Form1).AddEvent(e.Slot.SlotID, "Connected");
            }
            if (e.State == SlotState.Failed)
            {
                (Application.OpenForms[0] as Form1).AddEvent(e.Slot.SlotID, "Failed");
            }
        }

        private void setSlotEventHandlers(Slot slot)
        {
            slot.evhSlotStateChanged += new SlotEventHandler(slot_evhSlotStateChanged);
        }

        private void removeSlotEventHandlers(Slot slot)
        {
            slot.evhSlotStateChanged -= slot_evhSlotStateChanged;
        }

        private void setRSocketEventHandlers(Slot slot)
        {
            slot.rqs.evhLoggedIn += rqs_evhServiceLoggedIn;
            slot.rqs.evhNewSession += rqs_evhNewSession;
            slot.rqs.evhNeedNewPassword += rqs_evhNeedNewPassword;
            slot.rqs.evhLogLine += rqs_evhLogLine;
        }

        private void removeRSocketEventHandlers(Slot slot)
        {
            slot.rqs.evhLoggedIn -= rqs_evhServiceLoggedIn;
            slot.rqs.evhNewSession -= rqs_evhNewSession;
            slot.rqs.evhNeedNewPassword -= rqs_evhNeedNewPassword;
            slot.rqs.evhLogLine -= rqs_evhLogLine;
        }

        private void rqs_evhNeedNewPassword(object sender, TableEventArgs e)
        {
            (Application.OpenForms[0] as Form1).AddEvent(e.RequestSocket.slot.SlotID, "Need to change your password");
        }

        private void rqs_evhLogLine(object sender, TableEventArgs e)
        {
            (Application.OpenForms[0] as Form1).AddEvent(e.RequestSocket.slot.SlotID, e.Message);
        }

        private void rqs_evhServiceLoggedIn(object sender, TableEventArgs e)
        {
            (Application.OpenForms[0] as Form1).AddEvent("EVENT", "LoggedIn");
        }

        private void rqs_evhNewSession(object sender, TableEventArgs e)
        {
            (Application.OpenForms[0] as Form1).AddEvent("EVENT", "New session required");
            System.Threading.ThreadPool.QueueUserWorkItem(reconnectSlotAsync, e);
        }

        private void reconnectSlotAsync(Object obj)
        {
            System.Threading.Thread.Sleep(1000);
            this.slot = cloneSlot(this.slot);
            connect();
        }

        private Slot cloneSlot(Slot cslot)
        {
            Slot newSlot = new Slot();
            newSlot.PublicKeyFile = "";
            newSlot.rqs = new RequestSocket(newSlot);
            newSlot.SlotID = this.name;
            newSlot.Server = this.server;
            newSlot.Port = this.port;
            newSlot.Login = this.login;
            newSlot.Password = this.password;
            setSlotEventHandlers(newSlot);
            setRSocketEventHandlers(newSlot);
            return newSlot;
        }

        #endregion
    }
}
