using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenPC_Database_Management
{
    public partial class DatabaseManagement : Form
    {
        Dictionary<int, string> hierarchy = new Dictionary<int, string>();
        private ContextMenu treeRightClick = new ContextMenu();

        public DatabaseManagement()
        {
            InitializeComponent();
            //Implement add and delete buttons when right click on treeview nodes
            treeRightClick.MenuItems.Add("Add");
            treeRightClick.MenuItems.Add("Edit");
            treeRightClick.MenuItems.Add("Delete");
            foreach (MenuItem item in treeRightClick.MenuItems)
            {
                switch (item.Text)
                {
                    case "Add":
                        item.Click += new EventHandler(RightClickAdd_Click);
                        break;
                    case "Delete":
                        item.Click += new EventHandler(RightClickDelete_Click);
                        break;
                    case "Edit":
                        item.Click += new EventHandler(RightClickEdit_Click);
                        break;
                    default:
                        break;
                }
            }
            hierarchy.Add(0, "School");
            hierarchy.Add(1, "Building");
            hierarchy.Add(2, "Room");
            hierarchy.Add(3, "Computer");
        }
                
        private void SQLTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void GenerateTreeFromDB()
        {
            SQLTreeView.Nodes.Add(new TreeNode() { Text = "UTK", Tag = new School() { Name = "UTK", Longitude = 1, Latitude = 1} });
            for (int i = 0; i < 5; i++)
                SQLTreeView.Nodes[0].Nodes.Add(new TreeNode() { Text = "Building" + i.ToString(), Tag = new Building() { Name = "Building" + i.ToString(), Longitude = 1 + i, Latitude = 1 + i } });
            for (int j = 0; j < SQLTreeView.Nodes[0].Nodes.Count; j++)
            {
                for (int k = 0; k < 5; k++)
                {
                    SQLTreeView.Nodes[0].Nodes[j].Nodes.Add(new TreeNode() { Text = "Room" + k.ToString(), Tag = new Room() { Name = "Room" + k.ToString(), Floor = 1 + k} });
                }
            }
        }

        private void DatabaseManagement_Load(object sender, EventArgs e)
        {
            GenerateTreeFromDB();
        }

        private void SQLTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            SQLTreeView.SelectedNode = e.Node;
            if (e.Button == MouseButtons.Right)
            {
                SQLTreeView.ContextMenu = treeRightClick;
            }
            else if(e.Button == MouseButtons.Left)
            {
                //Display Info about selected node
                InfoLabel.Text = GetNodeInfo(SQLTreeView.SelectedNode);
            }
        }

        private void RightClickAdd_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You clicked \"Add\" option");
            SQLTreeView.SelectedNode.Nodes.Add("T");
            //SQLTreeView.SelectedNode.Nodes.Add(new TreeNode() { Text = "test", Tag = (object)testbuilding});
            if (SQLTreeView.SelectedNode.Nodes.Count > 0 && !SQLTreeView.SelectedNode.IsExpanded)
                SQLTreeView.SelectedNode.Expand();
        }

        private void RightClickDelete_Click(object sender, EventArgs e)
        {
            DialogResult result =  MessageBox.Show($"You wish to delete \"{SQLTreeView.SelectedNode.Text}\" and all of its child nodes?", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                SQLTreeView.SelectedNode.Remove();
            }
        }

        private void RightClickEdit_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You clicked \"Edit\" option");
        }

        private string GetNodeInfo(TreeNode node)
        {
            string displayStr = String.Empty;
            switch (node.Level)
            {
                case 0:
                    displayStr += "Node type:\n" +
                                  "    School Node\n\n" +
                                  "Name:\n" +
                                 $"    {((School)node.Tag).Name}\n\n" +
                                  "Longitude:\n" +
                                 $"    {((School)node.Tag).Longitude}\n\n" +
                                  "Latitude:\n" +
                                 $"    {((School)node.Tag).Latitude}";
                    break;
                case 1:
                    displayStr += "Node type:\n" +
                                  "    Building Node\n\n" +
                                  "Name:\n" +
                                 $"    {((Building)node.Tag).Name}\n\n" +
                                  "Longitude:\n" +
                                 $"    {((Building)node.Tag).Longitude.ToString()}\n\n" +
                                  "Latitude:\n" +
                                 $"    {((Building)node.Tag).Latitude.ToString()}";
                    break;
                case 2:
                    displayStr += "Node type:\n" +
                                  "    Room Node\n\n" +
                                  "Name:\n" +
                                 $"    {((Room)node.Tag).Name}\n\n" +
                                  "Longitude:\n" +
                                 $"    {((Room)node.Tag).Floor.ToString()}";
                    break;
                case 3:
                    displayStr += "Node type:\n" +
                                  "    Computer Node\n\n" +
                                  "Name:\n" +
                                 $"    {((School)node.Tag).Name}\n\n" +
                                  "Longitude:\n" +
                                 $"    {((School)node.Tag).Longitude}\n\n" +
                                  "Latitude:\n" +
                                 $"    {((School)node.Tag).Latitude}\n\n";
                    break;
                default:
                    break;
            }
            return displayStr;
        }
    }
}
