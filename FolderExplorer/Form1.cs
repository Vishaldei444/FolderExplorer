using System;
using System.IO;
using System.Windows.Forms;

namespace FolderExplorer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Text = "Folder Explorer";
            listViewFiles.View = View.Details;
            listViewFiles.FullRowSelect = true;

            listViewFiles.Columns.Add("Name", 200);
            listViewFiles.Columns.Add("Size", 100, HorizontalAlignment.Right);
            listViewFiles.Columns.Add("Date Modified", 150);

            treeViewFolders.BeforeExpand += LoadSubfolders;
            treeViewFolders.AfterSelect += ShowFiles;

            treeViewFolders.Nodes.Add(CreateFolderNode(@"C:\", @"C:\"));
            treeViewFolders.Nodes.Add(CreateFolderNode(@"E:\", @"E:\"));
        }

        private TreeNode CreateFolderNode(string name, string path)
        {
            TreeNode node = new TreeNode(name) { Tag = path };

            // The placeholder gives the node an expand button.
            node.Nodes.Add("Loading...");
            return node;
        }

        private void LoadSubfolders(object? sender, TreeViewCancelEventArgs e)
        {
            TreeNode? node = e.Node;

            if (node?.Tag is not string path ||
                node.Nodes.Count != 1 || node.Nodes[0].Tag != null)
            {
                return;
            }

            try
            {
                DirectoryInfo[] folders = new DirectoryInfo(path).GetDirectories();
                node.Nodes.Clear();

                foreach (DirectoryInfo folder in folders)
                {
                    node.Nodes.Add(CreateFolderNode(folder.Name, folder.FullName));
                }
            }
            catch (UnauthorizedAccessException)
            {
                e.Cancel = true;
                ShowFolderError("You do not have permission to open this folder.");
            }
            catch (IOException)
            {
                e.Cancel = true;
                ShowFolderError("This drive or folder is unavailable.");
            }
        }

        private void ShowFiles(object? sender, TreeViewEventArgs e)
        {
            listViewFiles.BeginUpdate();

            try
            {
                listViewFiles.Items.Clear();

                if (e.Node?.Tag is not string path)
                {
                    return;
                }

                foreach (FileInfo file in new DirectoryInfo(path).GetFiles())
                {
                    ListViewItem row = new ListViewItem(file.Name);
                    row.SubItems.Add($"{file.Length:N0} B");
                    row.SubItems.Add(file.LastWriteTime.ToString("dd MMM yyyy HH:mm"));
                    listViewFiles.Items.Add(row);
                }
            }
            catch (UnauthorizedAccessException)
            {
                listViewFiles.Items.Clear();
                ShowFolderError("You do not have permission to view these files.");
            }
            catch (IOException)
            {
                listViewFiles.Items.Clear();
                ShowFolderError("This drive or folder could not be read.");
            }
            finally
            {
                listViewFiles.EndUpdate();
            }
        }

        private void ShowFolderError(string message)
        {
            MessageBox.Show(this, message, "Folder Explorer",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}