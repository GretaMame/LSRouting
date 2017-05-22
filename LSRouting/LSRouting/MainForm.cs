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
        Network network;

        public MainForm()
        {
            InitializeComponent();
            HideMenuSelection();
            network = new Network(10);
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

        /// <summary>
        /// SELECTION 1 ADD ROUTER
        /// </summary>
        private void addRouterButton_Click(object sender, EventArgs e)
        {
            if (addRouterTextBox.Text.Equals(""))
            {
                MessageBox.Show("Please enter a router name");
            }
            else
            {
                if (network.AddRouter(addRouterTextBox.Text))
                {
                    MessageBox.Show("Router " + addRouterTextBox.Text + " added succesfully");
                };

            }
            addRouterTextBox.Text = "";
        }

        /// <summary>
        /// SELECTION 2 ADD LINK
        /// </summary>
        private void addLinkButton_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// SELECTION 3 SEND MESSAGE
        /// </summary>
        private void sendButton_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// SELECTION 4 VIEW ROUTING TABLE
        /// </summary>
        private void viewButton_Click(object sender, EventArgs e)
        {

        }

        private void viewAllButton_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// SELECTION 5 REMOVE ROUTER
        /// </summary>
        private void removeRouterButton_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// SELECTION 6 REMOVE LINK
        /// </summary>
        private void removeLinkButton_Click(object sender, EventArgs e)
        {

        }
    }
}
