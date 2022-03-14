using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;

namespace BasicFacebookFeatures
{
    public class FacebookLogic
    {
        public string AccessToken { get; private set; }
        private FacebookWrapper.LoginResult m_LoginResult;
        private FacebookWrapper.ObjectModel.User m_LoggInUser;

        public bool Login()
        {
            bool flag = false;
            Clipboard.SetText("design.patterns20cc");

            FacebookWrapper.LoginResult m_LoginResult = FacebookService.Login("289790089925279", "email",
                "public_profile",
                "user_age_range",
                "user_birthday",
                "user_events",
                "user_friends",
                "user_gender",
                "user_hometown",
                "user_likes",
                "user_link",
                "user_location",
                "user_photos",
                "user_posts",
                "user_videos"
                );

            if (!string.IsNullOrEmpty(m_LoginResult.AccessToken))
            {
                m_LoggInUser = m_LoginResult.LoggedInUser;
                //FaceBookData();
                flag = true;
            }

            return flag;

        }

        public string SetUserPic()
        {
            string url=null;
            if (m_LoggInUser != null)
            {
                //userPic = new PictureBox();

                url= m_LoggInUser.PictureNormalURL;
            }
            return url;
        }

        public bool Logout()
        {
            bool flag = false;
            FacebookService.LogoutWithUI();
            if (string.IsNullOrEmpty(m_LoginResult.AccessToken))
            {
                flag = true;
            }

            return flag;
        }
        public void Connect(string i_AccessToken)
        {
            m_LoginResult=FacebookService.Connect(i_AccessToken);
        }
    }
}
