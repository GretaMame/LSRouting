using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LSRouting
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            HideMenuSelection();
        }


        private void HideMenuSelection()
        {
            AddRouterGroupBox.Visible = false;
            AddLinkGroupBox.Visible = false;
            RemoveLinkGroupBox.Visible = false;
            RemoveRouterGroupBox.Visible = false;
            SendMessageGroupBox.Visible = false;
            ViewRoutingTableGroupBox.Visible = false;
        }

        private void selectItemButton_Click(object sender, EventArgs e)
        {
            HideMenuSelection();
            int selection;
            GroupBox current;
            if (int.TryParse(selectionTextBox.Text, out selection))
            {
                switch(selection)
                {
                    case (1):
                        AddRouterGroupBox.Visible = true;
                        current = AddRouterGroupBox;
                        break;
                    case (2):
                        AddLinkGroupBox.Visible = true;
                        current = AddLinkGroupBox;
                        break;
                    case (3):
                        SendMessageGroupBox.Visible = true;
                        current = SendMessageGroupBox;
                        break;
                    case (4):
                        ViewRoutingTableGroupBox.Visible = true;
                        current = ViewRoutingTableGroupBox;
                        break;
                    case (5):
                        RemoveRouterGroupBox.Visible = true;
                        current = RemoveRouterGroupBox;
                        break;
                    case (6):
                        RemoveLinkGroupBox.Visible = true;
                        current = RemoveLinkGroupBox;
                        break;
                }
            }
            else
            {
                selectionTextBox.Text = "";
                MessageBox.Show("Please enter a valid selection");
            }
            selectionTextBox.Text = "";
                
        }
    }
}
