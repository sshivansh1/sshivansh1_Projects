using System;
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
using System.Threading;
using ConnLibrary;
using SocketFrames;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using static System.Diagnostics.Trace;

namespace Client
{
    public partial class Client : Form
    { 
        private Socket objSocket = null;
        private Thread myThread = null;
        private volatile bool isListening = false;
        
        public Client()
        {
            InitializeComponent();
        }

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            ConnDialog cd = new ConnDialog(true, true); //A new instance of the connection dialog
            
            //With all the fields in there
            //It displays for now, if acc to the received data
            //the connection is successful or not

            if(cd.ShowDialog() == DialogResult.OK)
            {
                objSocket = cd.Soc;

                if (objSocket.Connected)
                {
                    isListening = true;
                    tb_Status.Text = "Connected";
                    btn_Connect.Enabled = false;
                    btn_Disconnect.Enabled = true;
                    btn_Guess.Enabled = true;

                    myThread = new Thread(RXGuess);
                    myThread.IsBackground = true;
                    myThread.Start();
                }
            }
        }

        //I need to get a reply from the server 
        //To kind of have an idea where the random chosen value is at
        
        private void RXGuess()
        {
            while (isListening)
            {
                try
                {
                    byte[] buff = new byte[1024];
                    int iNumBytesRxED = objSocket.Receive(buff); //Receives int number of bytes and send into a buffer

                    // if zero bytes are received, the other side issued a shutdown
                    if (iNumBytesRxED == 0)
                    {
                        try
                        {
                            WriteLine("Softy disconnect");
                        }
                        catch (Exception ex)
                        {
                            WriteLine(ex.Message);
                        }
                        return;
                    }
                    //Continously receiveing data and deserializing
                    string data = Encoding.UTF8.GetString(buff);

                    CSocketFrame frame = JsonConvert.DeserializeObject<CSocketFrame>(data);//will have the random number

                    Invoke(new Action(() =>
                    {
                        if (frame.iData > 0)
                            isHigher();
                        else if (frame.iData < 0)
                            isLower();
                        else if (frame.iData == 0)
                            IsEqual();
                    }));
                }
                catch(Exception ex)
                {
                    Trace.WriteLine(ex);
                    Invoke(new Action(() => Disconnect()));
                }
            }
        }

        private void IsEqual()
        {
            tbar_Guess.Minimum = 1;
            tbar_Guess.Maximum = 1000;
            tbar_Guess.Value = tbar_Guess.Minimum;
            lbl_MinVal.Text = tbar_Guess.Minimum.ToString();
            lbl_MaxVal.Text = tbar_Guess.Maximum.ToString();
            lbl_GuessVal.Text = tbar_Guess.Value.ToString();
            tb_Status.Text = "Correct Guess!";
        }

        private void isHigher()
        {
            tbar_Guess.Maximum = tbar_Guess.Value - 1;
            tbar_Guess.Value = tbar_Guess.Maximum;
            lbl_MaxVal.Text = tbar_Guess.Maximum.ToString();
            lbl_GuessVal.Text = tbar_Guess.Value.ToString();
            tb_Status.Text = "Higher, try a smaller no.";
        }

        private void isLower()
        {
            tbar_Guess.Minimum = tbar_Guess.Value + 1;
            tbar_Guess.Value = tbar_Guess.Minimum;
            lbl_MinVal.Text = tbar_Guess.Minimum.ToString();
            lbl_GuessVal.Text = tbar_Guess.Value.ToString();
            tb_Status.Text = "Smaller, try a larger no.";
        }

        //When the guess button is clicked,
        //current track bar value has to be sent in the form of bytes array
        private void Btn_Guess_Click(object sender, EventArgs e)
        {
            var tbarValue = tbar_Guess.Value;

            if (objSocket != null)
            {
                try
                {
                    CSocketFrame frame = new CSocketFrame(tbarValue);
                    //JSON serilize and Unicode encode
                    string sframe = JsonConvert.SerializeObject(frame);
                    byte[] sbuff = Encoding.UTF8.GetBytes(sframe); //Will store the information as array of bytes

                    //send with socket to connected target
                    objSocket.Send(sbuff, sbuff.Count(), SocketFlags.None);
                }
                catch(Exception ex)
                {
                    Trace.WriteLine($"Send Error! : {ex}");
                }

                //byte[] data = new byte[5] { 42, 43, 4, 5, 2 }; //sample array of bytes
                //objSocket.Send(data, 0, 5, SocketFlags.None);
                //objSocket.Send(ms.GetBuffer(), (int)ms.Length, SocketFlags.None);
            }
        }


        private void tbar_Guess_Scroll(object sender, EventArgs e)
        {
            var tbarValue = tbar_Guess.Value;
            lbl_GuessVal.Text = tbarValue.ToString();
        }


        private void btn_Disconnect_Click(object sender, EventArgs e)
        {
            try
            {
                Disconnect();
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
            }
        }

        private void Disconnect()
        {
            try
            {
                isListening = false;
                objSocket.Shutdown(SocketShutdown.Both);//disable sends and/or receives on a socket. 
                tb_Status.Text = "Disconnected!";
                
                tbar_Guess.Minimum = 1;
                tbar_Guess.Maximum = 1000;
                tbar_Guess.Value = 1;
                lbl_MinVal.Text = tbar_Guess.Minimum.ToString();
                lbl_MaxVal.Text = tbar_Guess.Maximum.ToString();
                lbl_GuessVal.Text = tbar_Guess.Value.ToString();

                btn_Connect.Enabled = true;
                btn_Disconnect.Enabled = false;
                btn_Guess.Enabled = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error disconnecting, not connected: {ex.Message}");
            }
        }

        private void Client_Load(object sender, EventArgs e)
        {
            btn_Connect.Enabled = true;
            btn_Disconnect.Enabled = false; 
            btn_Guess.Enabled = false;
        }
    }
}


/******    Using The Binary Formatter in C# *******
           * 
           CSocketFrame frame = new CSocketFrame(tbarValue);
           MemoryStream ms = new MemoryStream();
           BinaryFormatter bf = new BinaryFormatter();
           bf.Serialize(ms, frame);
           objSocket.Send(ms.GetBuffer(), (int)ms.Length, SocketFlags.None);
           * ***********     End                *********
           */