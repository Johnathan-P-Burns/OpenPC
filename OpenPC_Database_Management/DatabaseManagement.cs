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
        private ContextMenu treeRightClick = new ContextMenu();
        private Building testbuilding = new Building()
        {
            Longitude = 1.0,
            Latitude = 1.0,
            Name = "MinKao",
            ID = 123
        };

        public DatabaseManagement()
        {
            InitializeComponent();
            //Implement add and delete buttons when right click on treeview nodes
            treeRightClick.MenuItems.Add("Add");
            treeRightClick.MenuItems.Add("Delete");
            foreach (MenuItem item in treeRightClick.MenuItems)
            {
                if (item.Text == "Add")
                    item.Click += new EventHandler(RightClickAdd_Click);
                else
                    item.Click += new EventHandler(RightClickDelete_Click);
            }

            //treeRightClick.MenuItems["Add"].Click += new EventHandler(RightClickAdd_Click);
            //treeRightClick.MenuItems["Delete"].Click += new EventHandler(RightClickDelete_Click);
        }
                
        private void SQLTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void GenerateTreeFromDB()
        {
            SQLTreeView.Nodes.Add("School");
            for (int i = 0; i < 5; i++)
                SQLTreeView.Nodes[0].Nodes.Add("Building" + i.ToString());
            for (int j = 0; j < SQLTreeView.Nodes[0].Nodes.Count; j++)
            {
                for (int k = 0; k < 5; k++)
                {
                    SQLTreeView.Nodes[0].Nodes[j].Nodes.Add("Room" + k.ToString());
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
            }
        }

        private void RightClickAdd_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You clicked \"Add\" option");
            SQLTreeView.SelectedNode.Nodes.Add(new TreeNode() { Text = "test", Tag = (object)testbuilding});
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
    }
}
