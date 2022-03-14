using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CefSharp.DevTools.Emulation;
using CefSharp.Structs;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;
using Message = FacebookWrapper.ObjectModel.Message;

namespace BasicFacebookFeatures
{
   
    public partial class FormMain : Form
    {
        FacebookLogic facebookLogic;
        private AppSettings m_AppSettings;
        
        public FormMain()
        {
            InitializeComponent();
            facebookLogic = new FacebookLogic();
            this.StartPosition = FormStartPosition.Manual;
            m_AppSettings =  AppSettings.LoadFromFile();
            this.Size = m_AppSettings.LastWindowSize;
            this.Location = m_AppSettings.LastWindowLocation;
            this.checkBoxRememberUser.Checked = m_AppSettings.RemmeberUser;

            if (m_AppSettings.RemmeberUser && !string.IsNullOrEmpty(m_AppSettings.LastAccessToken))
            {
                facebookLogic.Connect(m_AppSettings.LastAccessToken);
               
            }
            FacebookWrapper.FacebookService.s_CollectionLimit = 100;
        }
        //LoginResult m_LoginResult;
      
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (facebookLogic.Login() == true)
            {
                //buttonLogin.Text = $"Logged in as {m_LoginResult.LoggedInUser.Name}";
                pictureBoxProfile.LoadAsync(facebookLogic.SetUserPic());
            }
            else
            {
                MessageBox.Show( "Login Failed");
            }

        }

       
        private void buttonLogout_Click(object sender, EventArgs e)
        {
            if(facebookLogic.Logout()==true)
                  buttonLogin.Text = "Login";
            else
            {
                MessageBox.Show("logout failed");
            }
        }


        private void fetchByRangeYears(int i_yearTo, int i_yearFrom)
        {
            int count = 0;
            int countOfFemale = 0;
            int countOfMale = 0;
            //var t = m_LoggInUser.Friends;
            List<User> userList = new List<User>();
            foreach (User friend in m_LoggInUser.Friends)
            {
                string year = friend.Birthday.Substring(6, 4);
                if (int.TryParse(year, out int validateYear))
                {
                    if (validateYear >= i_yearTo && validateYear <= i_yearFrom)
                    {
                        userList.Add(friend);
                        if (friend.Gender == User.eGender.female)
                        {
                            countOfFemale++;
                        }
                        else if (friend.Gender == User.eGender.male)
                        {
                            countOfMale++;
                        }
                    }
                }
            }
            int countFriends = userList.Count; 
            string tempString = i_yearTo.ToString() + "-"+ i_yearFrom.ToString();
            if (countOfMale == 0 && countOfFemale == 0)
           {
               chart1.Series["Male"].Points.AddXY(tempString, countOfMale);
            }
           if (countOfMale > 0)
           {
               chart1.Series["Male"].Points.AddXY(tempString, countOfMale);
               chart1.Series["Male"].IsValueShownAsLabel = true;
            }
           if (countOfFemale > 0)
           {
               chart1.Series["Female"].Points.AddXY(tempString, countOfFemale);
               chart1.Series["Female"].IsValueShownAsLabel = true;
           }
        }
        private void calculation_Click(object sender, EventArgs e)
        {
            int input1 = Convert.ToInt32(textBox1.Text);
            int input2 = Convert.ToInt32(textBox2.Text);
            
            if (input1 < input2)
            { 
                fetchByRangeYears(input1, input2);
            }
            else
            {
                MessageBox.Show("The range is incorrect");
            }
        }

    }
}
