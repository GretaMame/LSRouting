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
            network = new Network();
            network.AddRouter("R1");
            network.AddRouter("R2");
            network.AddRouter("R3");
            network.AddRouter("R4");
            network.AddRouter("R5");
            network.AddRouter("R6");
            network.AddRouter("R7");

            network.AddLink("R1", "R2", 5);
            network.AddLink("R1", "R3", 2);
            network.AddLink("R3", "R2", 3);
            network.AddLink("R4", "R7", 5);
            network.AddLink("R3", "R4", 1);
            network.AddLink("R6", "R4", 10);
            network.AddLink("R4", "R5", 2);
            network.AddLink("R7", "R3", 2);
            network.printRouters();
        }


        private void HideMenuSelection()
        {
            AddRouterGroupBox.Visible = false;
            AddLinkGroupBox.Visible = false;
            RemoveLinkGroupBox.Visible = false;
            RemoveRouterGroupBox.Visible = false;
            SendMessageGroupBox.Visible = false;
            ViewRoutingTableGroupBox.Visible = false;
            messageRouteGroupBox.Visible = false;
        }

        private void selectItemButton_Click(object sender, EventArgs e)
        {
            HideMenuSelection();
            int selection;
            if (int.TryParse(selectionTextBox.Text, out selection))
            {
                switch(selection)
                {
                    case (1):
                        AddRouterGroupBox.Visible = true;
                        break;
                    case (2):
                        AddLinkGroupBox.Visible = true;
                        break;
                    case (3):
                        SendMessageGroupBox.Visible = true;
                        break;
                    case (4):
                        ViewRoutingTableGroupBox.Visible = true;
                        break;
                    case (5):
                        RemoveRouterGroupBox.Visible = true;
                        break;
                    case (6):
                        RemoveLinkGroupBox.Visible = true;
                        break;
                    default:
                        MessageBox.Show("PLease enter a selection from the menu(1-6) :)");
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
            int cost;
            if (addLinkRouterTextBox1.Text.Equals("") || addLinkRouterTextBox2.Text.Equals("")) MessageBox.Show("Enter router names");
            if (!int.TryParse(costTextBox.Text, out cost)) MessageBox.Show("Invalid cost value");
            if (network.AddLink(addLinkRouterTextBox1.Text, addLinkRouterTextBox2.Text, cost))
            {
                MessageBox.Show("Link added successfully");
            }
            else
            {
                MessageBox.Show("Please enter existing router names");
            }
            addLinkRouterTextBox1.Text = "";
            addLinkRouterTextBox2.Text = "";
            costTextBox.Text = "";
        }

        /// <summary>
        /// SELECTION 3 SEND MESSAGE
        /// </summary>
        private void sendButton_Click(object sender, EventArgs e)
        {
            if (msgRouterNameTextBox1.Text.Equals("") || msgRouterNameTextBox2.Text.Equals("") || msgTextBox.Text.Equals(""))
            {
                MessageBox.Show("Please enter router names and message");
                return;
            }
            messageRouteGroupBox.Visible = true;
            if (network.SendMessage(msgRouterNameTextBox1.Text, msgRouterNameTextBox2.Text, msgTextBox.Text))
            {
                MessageBox.Show("Message delivered successfully");
            }
            msgTextBox.Text = "";
            msgRouterNameTextBox1.Text = "";
            msgRouterNameTextBox2.Text = "";
        }

        /// <summary>
        /// SELECTION 4 VIEW ROUTING TABLE
        /// </summary>
        private void viewButton_Click(object sender, EventArgs e)
        {
            if (viewTableRouterTextBox.Text.Equals("")) MessageBox.Show("PLease enter router name");
            else
            {
                Dictionary<string, string> connections = network.GetList(viewTableRouterTextBox.Text);
                if (connections != null)
                {
                    foreach(string item in connections.Keys)
                    {
                        string next;
                        connections.TryGetValue(item, out next);
                        Label routingTable = new Label();
                        routingTable.Text = "D: " + item + " NH: " + next;
                        ViewRoutingTableGroupBox.Controls.Add(flowLayoutPanel1);
                        flowLayoutPanel1.Controls.Add(routingTable);

                        Console.WriteLine("Destination: " + item + " Next Hop: " + next);
                    }
                }
            }
        }

        private void viewAllButton_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// SELECTION 5 REMOVE ROUTER
        /// </summary>
        private void removeRouterButton_Click(object sender, EventArgs e)
        {
            if (removeRouterTextBox.Text.Equals("")) MessageBox.Show("Please enter router name");
            if (network.RemoveRouter(removeRouterTextBox.Text)) MessageBox.Show("Router successfully removed.");
            else MessageBox.Show("There has been an error while removing the router. Please check the routers name");
            removeRouterTextBox.Text = "";
        }

        /// <summary>
        /// SELECTION 6 REMOVE LINK
        /// </summary>
        private void removeLinkButton_Click(object sender, EventArgs e)
        {
            if (RemLinkNameTextBox1.Text.Equals("") || RemLinkNameTextBox2.Text.Equals("")) MessageBox.Show("Please enter router names");
            if (network.RemoveLink(RemLinkNameTextBox1.Text, RemLinkNameTextBox2.Text))
            {
                MessageBox.Show("Link successfully removed");
            }
            else MessageBox.Show("There has been a mistake while removing the link");
        }
    }
}
