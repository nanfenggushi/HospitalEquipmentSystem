using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalEquipmentSystem.UI
{
    public static class Redirect
    {
        public static void NavigateTo<F>(UIPanel panel) where F : UIForm, new()
        {
            panel.Visible = true;
            panel.BringToFront();

            bool found = false;
            foreach (Control item in panel.Controls)
            {
                if (item is F)
                {
                    item.Show();
                    item.BringToFront();
                    found = true;
                }
                else
                {
                    item.Hide();
                }
            }

            if (!found)
            {
                F f = new F();
                f.TopLevel = false;
                f.ShowTitle = false;
                f.WindowState = FormWindowState.Maximized;
                panel.Controls.Add(f);
                f.Show();
            }
        }

    }
}

