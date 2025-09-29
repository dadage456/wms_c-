using System;
using System.Collections.Generic;
using System.Text;

namespace PDA
{
    public class Message
    {
        static public void Alarm(string caption, string text)
        {
            System.Windows.Forms.MessageBox.Show(text, caption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation,
                System.Windows.Forms.MessageBoxDefaultButton.Button1);
        }
    }
}
