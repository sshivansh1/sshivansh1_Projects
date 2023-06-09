﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using static System.Diagnostics.Trace;

namespace ConnLibrary
{
    public partial class ConnDialog : Form
    {
        public Socket Soc { get; private set; } 
        //Creating a client socket object for connection

        //Acts like a form Dialog for us
        public ConnDialog(bool enableMyAddress, bool enableMyPort)
        {
            InitializeComponent();
            label1.Text = "";

            if (enableMyAddress)
                tb_Address.ReadOnly = false;
            else
                tb_Address.ReadOnly = true;

            var enablePort = (enableMyPort)? tb_Port.ReadOnly = false: tb_Port.ReadOnly = true;
        }

        //When the connect button on the Form triggers, 
        //It creates a new socket object which attempt for an asychronous connection
        private void btn_Connect_Click(object sender, EventArgs e)
        {
            Soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            label1.Text = "Making Connection";
            //Soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); //transmission protocol);
            //For testing purposes, I have set the readonly property of the textbox to false
            try
            {
                //Socket connection attempt asynchronously 
                Soc.BeginConnect(
                    tb_Address.Text,            //The address to connect to
                    Convert.ToInt32(tb_Port.Text),    //The Port number (int)
                    CallBack_ConnectDone, //This is the call back function
                    42);                  //Passed in with the call back function
            }
            catch
            {
                //MessageBox.Show(err.Message, "Connection Failure...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Invoke(new Action(() => label1.Text = $"Connection error"));
            }
        }

        //If I begin the connect, I have to end it as well.
        //This will run on a different thread
        private void CallBack_ConnectDone(IAsyncResult ar)
        {
            //Trace.WriteLine("The fourth paramter passed is : " + (int)ar.AsyncState);
            try
            {
                Soc.EndConnect(ar);//doesn't return anything just end a pending connection request
                WriteLine("Connected");
                Invoke(new Action(() => this.DialogResult = DialogResult.OK)); //Perform the Ok Action

                //Invoke(new Action(() => this.Text = "Connected Successfully")); //A delegate can also be passed but this is fairly easy
                //WriteLine("I am in the try block above the dialog OK");
            }
            catch(Exception ex)
            {
                //Invoke(new Action(() => label1.Text = ex.Message.ToString())); //Perform the Ok Action
                MessageBox.Show($"{ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Invoke(new Action<string>((param) => this.Text = $"Not Connected! {param}"), ex.Message);
            }
        }
    }
}
