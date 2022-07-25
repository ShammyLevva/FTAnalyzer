﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

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
		readonly Dictionary<string, UserControl> _lookupTable;
		public Options()
		{
			InitializeComponent();
            Properties.GeneralSettings.Default.ReloadRequired = false;
            _lookupTable = new Dictionary<string, UserControl>();
		}

		void Options_Load(object sender, EventArgs e)
		{
			SuspendLayout();
			try
			{
				Type[] types = Assembly.GetExecutingAssembly().GetTypes();
				for (int i = 0; i < types.Length; i++)
				{
					if (types[i].GetInterface(typeof(IOptions).FullName) != null && types[i].BaseType == typeof(UserControl)) 
					{
						if (Activator.CreateInstance(types[i]) is UserControl userControl && userControl is IOptions optionCast)
						{
							userControl.Anchor = (((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right);
							userControl.Location = new System.Drawing.Point(3, 3);
							userControl.Name = optionCast.TreePosition;
							userControl.Size = new System.Drawing.Size(243, 356);
							userControl.TabIndex = 0;
							userControl.Visible = false;
							userControl.Dock = DockStyle.Fill;
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
			}
			catch (ReflectionTypeLoadException ex)
			{
				StringBuilder sb = new StringBuilder();
				foreach (Exception exSub in ex.LoaderExceptions)
				{
					sb.AppendLine(exSub.Message);
					if (exSub is FileNotFoundException exFileNotFound)
					{
						if (!string.IsNullOrEmpty(exFileNotFound.FusionLog))
						{
							sb.AppendLine("Fusion Log:");
							sb.AppendLine(exFileNotFound.FusionLog);
						}
					}
					sb.AppendLine();
				}
				string errorMessage = sb.ToString();
				Console.WriteLine(errorMessage);
				//Display or log the error based on your application.
			}

			treeView1.ExpandAll();
			if (treeView1.Nodes.Count > 0)
			{
				treeView1.SelectedNode = treeView1.Nodes[0];
			}
			ResumeLayout();
		}

		void AddNodesToTree(string treeNodePosition)
		{
			string[] splitPosition = treeNodePosition.Split(MENU_DELIMETER);
			SearchAndAddToSubTree(splitPosition, 0, treeView1.Nodes, treeNodePosition);
		}

		void SearchAndAddToSubTree(string[] splitPosition,int startPositon, TreeNodeCollection nodes, string key)
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
						if (tempNode.Tag == null || string.IsNullOrEmpty(tempNode.Tag.ToString()))
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

		void OK_Click(object sender, EventArgs e)
		{
			List<string> controlErrors = new List<string>();
			foreach (Control control in panel1.Controls)
			{
                if (control is IOptions options)
                    if (options.HasValidationErrors)
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
                if(Properties.GeneralSettings.Default.ReloadRequired)
					OnReloadRequired();
				DialogResult = DialogResult.OK;
			}
		}

        public static event EventHandler ReloadRequired;
        protected static void OnReloadRequired() => ReloadRequired?.Invoke(null, EventArgs.Empty);

        void Cancel_Click(object sender, EventArgs e)
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

		void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (treeView1.SelectedNode != null)
			{
				this.SuspendLayout();
				for (int i = 0; i < panel1.Controls.Count; i++)
				{
					panel1.Controls[i].Visible = false;
				}
				_lookupTable[treeView1.SelectedNode.Tag.ToString()].Visible = true;
				ResumeLayout();
			}
		}
	}
}