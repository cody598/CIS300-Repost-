/* UserInterface.cs
 * Author: Rod Howell
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Ksu.Cis300.FolderSizes
{
    /// <summary>
    /// A GUI for a program to find sizes of folders.
    /// </summary>
    public partial class UserInterface : Form
    {
        /// <summary>
        /// Constructs the GUI.
        /// </summary>
        public UserInterface()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Finds the total size of the given folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <returns>The total size of the given folder.</returns>
        private static long TotalSize(DirectoryInfo folder)
        {
            try
            {
                long size = 0;
                foreach (FileInfo f in folder.GetFiles())
                {
                    size += f.Length;
                }
                foreach (DirectoryInfo d in folder.GetDirectories())
                {
                    size += TotalSize(d);
                }
                return size;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Sets the currently-analyzed folder to the given path name.
        /// </summary>
        /// <param name="folder">The path name for the folder to analyze.</param>
        private void SetCurrentFolder(DirectoryInfo folder)
        {
            uxCurrentFolder.Text = folder.FullName;
            long size = TotalSize(folder);
            uxSize.Text = size.ToString("N0");
            uxFolderList.Items.Clear();
            uxUp.Enabled = (folder.Parent != null);
            try
            {
                foreach (DirectoryInfo d in folder.GetDirectories())
                {
                    uxFolderList.Items.Add(d);
                }
            }
            catch
            {
                // If we can't access the sub-folders, we can't add them to the list.
            }
        }

        /// <summary>
        /// Handles a Click event on the "Folder:" button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxSetFolder_Click(object sender, EventArgs e)
        {
            if (uxFolderBrowser.ShowDialog() == DialogResult.OK)
            {
                SetCurrentFolder(new DirectoryInfo(uxFolderBrowser.SelectedPath));
            }
        }

        /// <summary>
        /// Handles a SelectedIndexChanged event on the ListBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxFolderList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DirectoryInfo d = (DirectoryInfo)uxFolderList.SelectedItem;
            if (d != null)
            {
                SetCurrentFolder(d);
            }
        }

        /// <summary>
        /// Handles a Click event on the "Up one level" button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxUp_Click(object sender, EventArgs e)
        {
            DirectoryInfo d = new DirectoryInfo(uxCurrentFolder.Text);
            SetCurrentFolder(d.Parent);
        }
    }
}
