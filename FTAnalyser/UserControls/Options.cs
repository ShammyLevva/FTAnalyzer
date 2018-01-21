using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

//TODO: Load option controls dynamically
//TODO: Add ability for option controls to have an icon next to their display name in tree view
namespace FTAnalyzer.UserControls
{
	/// <summary>
	/// Option display form.
	/// </summary>
	public partial class Options : Form
	{
		public const char MENU_DELIMETER = '|';

		private Dictionary<string, UserControl> _lookupTable;
		public Options()
		{
			InitializeComponent();
			_lookupTable = new Dictionary<string, UserControl>();
		}

		private void Options_Load(object sender, EventArgs e)
		{
			this.SuspendLayout();
			Type[] types = Assembly.GetExecutingAssembly().GetTypes();
			for (int i = 0; i < types.Length; i++)
			{
				if (types[i].GetInterface(typeof(IOptions).FullName) != null && types[i].BaseType == typeof(UserControl)) 
				{
					UserControl userControl = Activator.CreateInstance(types[i]) as UserControl;
					IOptions optionCast = userControl as IOptions;
					if (userControl != null && optionCast != null)
					{
						userControl.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));
						userControl.Location = new System.Drawing.Point(3, 3);
						userControl.Name = optionCast.TreePosition;
						userControl.Size = new System.Drawing.Size(243, 356);
						userControl.TabIndex = 0;
						userControl.Visible = false;
						panel1.Controls.Add(userControl);
						if (optionCast.MenuIcon != null)
						{
							OptionsMenuImageList.Images.Add(optionCast.TreePosition, optionCast.MenuIcon);
						}
						AddNodesToTree(optionCast.TreePosition);
						_lookupTable.Add(optionCast.TreePosition, userControl);
					}
				}
			}
			treeView1.ExpandAll();
			if (treeView1.Nodes.Count > 0)
			{
				treeView1.SelectedNode = treeView1.Nodes[0];
			}
			this.ResumeLayout();
		}

		private void AddNodesToTree(string treeNodePosition)
		{
			string[] splitPosition = treeNodePosition.Split(MENU_DELIMETER);
			SearchAndAddToSubTree(splitPosition, 0, treeView1.Nodes, treeNodePosition);
		}

		private void SearchAndAddToSubTree(string[] splitPosition,int startPositon, TreeNodeCollection nodes, string key)
		{
			TreeNode tempNode = null;
			if (splitPosition.Length > startPositon)
			{
				for (int i = 0; i < nodes.Count; i++)
				{
					if (nodes[i].Text == splitPosition[startPositon])
					{
						tempNode = nodes[i];
						//if a root node, set the tag to the first child node in the branch
						if (tempNode.Tag == null || String.IsNullOrEmpty(tempNode.Tag.ToString()))
						{
							tempNode.Tag = key;
						}
						break;
					}
				}
				if (tempNode == null)
				{
                    tempNode = new TreeNode(splitPosition[startPositon])
                    {
                        Tag = key,
                        ImageKey = key,
                        SelectedImageKey = key
                    };
                    nodes.Add(tempNode);
				}
				SearchAndAddToSubTree(splitPosition, startPositon + 1, tempNode.Nodes, key);
			}
		}

		private void OK_Click(object sender, EventArgs e)
		{
			List<string> controlErrors = new List<string>();
			foreach (Control control in panel1.Controls)
			{
                if (control is IOptions options)
                    if (options.HasValidationErrors())
                        controlErrors.Add(options.DisplayName);
            }
			if (controlErrors.Count > 0)
			{
				StringBuilder sb = new StringBuilder();
				sb.Append("The following tabs have validation errors on them:");
				sb.Append(Environment.NewLine);
				for (int i = 0; i < controlErrors.Count; i++)
				{
					sb.Append(controlErrors[i]);
					sb.Append(Environment.NewLine);
				}
				MessageBox.Show(sb.ToString(), "Validation Errors", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				foreach (Control control in panel1.Controls)
                    if (control is IOptions options)
                        options.Save();
				DialogResult = DialogResult.OK;
			}
		}

		private void Cancel_Click(object sender, EventArgs e)
		{
			foreach (Control control in panel1.Controls)
			{
                if (control is IOptions options)
                {
                    options.Cancel();
                }
            }
			DialogResult = DialogResult.Cancel;
		}

		private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (treeView1.SelectedNode != null)
			{
				this.SuspendLayout();
				for (int i = 0; i < panel1.Controls.Count; i++)
				{
					panel1.Controls[i].Visible = false;
				}
				_lookupTable[treeView1.SelectedNode.Tag.ToString()].Visible = true;
				this.ResumeLayout();
			}
		}
	}
}
