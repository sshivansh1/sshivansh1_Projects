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
using System.Threading;
using SocketFrames;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using static System.Diagnostics.Trace;

namespace Server
{
    public partial class Server : Form
    {
        private Socket listSocket = null; //Initialization of a listening socket object to null
        private Socket connSocket = null; //everything is good, it gets initialized
        private Thread Rx_Thread= null;
        private volatile bool isListening = false;
        //A delegate reference
        //private delegate void delVoidByteData(byte[] buff_data);

        private static Random _rand = new Random();
        public Server()
        {
            InitializeComponent();
            this.Text = "Server";
            label1.Text = "-";
        }

        private void Server_Load(object sender, EventArgs e)
        {
            StartListening();
        }


        private void StartListening()
        {
            tb_RndGuessNum.ReadOnly = true;
            
            listSocket = new Socket(
            AddressFamily.InterNetwork,//uses IP V4
            SocketType.Stream, //streaming socket
            ProtocolType.Tcp //transmission protocol
            );

            try
            {
                // bind the socket to the endpoint (any interface, port 1666)
                listSocket.Bind(new IPEndPoint(IPAddress.Any, 1666));
            }
            catch(Exception ex)
            {
                WriteLine($"Unable to bind! {ex.Message}");
                MessageBox.Show($"Cannot create a new server while one is listening", "Bind Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
            try
            {
                // start listening
                listSocket.Listen(5);
            }
            catch (Exception ex)
            {
                WriteLine($"err listen! {ex.Message}");
                MessageBox.Show($"Listen Error! {ex.Message}", "Listen Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }

            label1.Text = "server is listening...";
            try
            { 
                // start waiting for a connection...
                listSocket.BeginAccept(CallBack_ConnectAccept, listSocket);
            }
            catch (Exception ex)
            {
                WriteLine($"err Accept! {ex.Message}");
                MessageBox.Show($"Accepting error! {ex.Message}", "Accept Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
        }


        private void CallBack_ConnectAccept(IAsyncResult ar)
        {
            //pull the listening socket from the args (state)
            //Socket lsok = (Socket)(ar.AsyncState);
            try
            {
                //also saving a new socket to a member
                connSocket = listSocket.EndAccept(ar); //create the connected socket (by terminating the accept process)
                
                //When it is connected, another server can be started because the listening socket is free
                listSocket.Close(); //client cannot make more connections to the listening server
                listSocket = null;
                isListening = true;
                //A seperate delegate method can be created as well
                Invoke(new Action(() => label1.Text = "Connected!")); //Changes the text of the form

                Rx_Thread = new Thread(RXBody);
                Rx_Thread.IsBackground = true;
                Rx_Thread.Start();
            }
            catch (SocketException err)
            {
                WriteLine($"Cannot accept {err.Message}!");
                Invoke(new Action(() => Text = "Bad!"));
            }
        }

        //private void DataProcessFunc(byte[] myDataBuffer)
        //{
        //    Text = myDataBuffer.Length.ToString(); //The size of the byte array sent to the server
        //    tb_RndGuessNum.Clear();
        //    foreach (var item in myDataBuffer)
        //    {
        //        tb_RndGuessNum.Text += item.ToString() + ", ";
        //    }
        //}


        //Connected Socket always in Rx State
        private void RXBody()
        {
            //Random server number picked on accept complete
            int randNum = _rand.Next(0, 1001);
            
            Invoke(new Action(() => tb_RndGuessNum.Text = randNum.ToString()));

            while (isListening)
            {
                try
                {
                    byte[] buff = new byte[1024];
                    byte[] srv_cl_Arr = new byte[1024];

                    //block on receive - will throw on hard disco
                    int iNumBytesRxED = connSocket.Receive(buff); //Receives int number of bytes and send into a buffer
                    //Trace.WriteLine(iNumBytesRxED + " number of bytes received");

                    if (iNumBytesRxED == 0)
                    {
                        // soft disco (user explicitly shut down socket)
                        // handle shutdown of socket
                        // bring listener back up

                        try
                        {
                            Invoke(new Action(() =>
                            {
                                WriteLine("Soft Disconn");
                                isListening = false;
                                StartListening();
                            }));
                        }
                        catch (Exception err)
                        {
                            Console.WriteLine(err.Message);
                        }
                        return;
                    }

                    /*************         [ Method One ]        ****************
                    * ------  Using the Binary Formatter to Deserialize the recieved data  ----
                    *   
                    *   BinaryFormatter bf = new BinaryFormatter();
                    *   MemoryStream m = new MemoryStream(buff);
                    *   CSocketFrame frame = (CSocketFrame)bf.Deserialize(m);
                    *   actually got data!
                    *   Invoke(new Action(() => this.Text = $"The tb bar value on the client side is : {frame.iData}"));
                    *   
                    * ************************************************************/
             
                    string sRX = Encoding.UTF8.GetString(buff);
                    CSocketFrame clientData = JsonConvert.DeserializeObject<CSocketFrame>(sRX);
                    Invoke(new Action(() => this.Text = $"The client trackbar value is : {clientData.iData}"));

                    try
                    {
                        CSocketFrame srvToClient = new CSocketFrame(clientData.iData.CompareTo(randNum));
                        string serverData = JsonConvert.SerializeObject(srvToClient);
                        srv_cl_Arr = Encoding.UTF8.GetBytes(serverData);
                        connSocket.Send(srv_cl_Arr, srv_cl_Arr.Count(), SocketFlags.None);
                    }
                    catch (Exception ex)
                    {
                        WriteLine(ex);
                    }

                    Invoke(new Action(() => {
                        if (randNum == clientData.iData)
                        {
                            randNum = _rand.Next(0, 1001);//Refresh the random number
                            tb_RndGuessNum.Text = randNum.ToString();
                            label1.Text = "New Game";
                        }
                    }));
                }

                catch (Exception ex)
                {
                    WriteLine($"Hard Disconnect {ex.Message}");
                    Invoke(new Action(() => {
                        StartListening();
                    }));
                }
            }
        }
    }
}

/*
*   ******     other ways of doing the same       *****
* 
* Invoke(new Action<int, string>((i, y) => Text = $"I got {i} bytes! from {y}"), iNumBytesRxED, "shivansh");
* Invoke(new Action(() => this.Text = $"recieved bytes : {iNumBytesRxED}"));
* Invoke(new Action(() => label1.Text = "Shivansh"));
* Invoke(new delVoidByteData(DataProcessFunc), buff);
* Invoke(new Action<byte[]>((x) => label1.Text = x.ToString()), buff);
* 
*   **************     End     ***************
*/