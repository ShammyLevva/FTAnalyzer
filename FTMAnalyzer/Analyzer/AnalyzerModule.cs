using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.Services;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI.EventBroker;
using FTM.Shared.Constants;
using FTM.Data;
using MyFamily.Shared.Interfaces;

namespace FTM.Analyzer
{
    public class AnalyzerModule : ModuleInit
    {
        private WorkItem rootWorkItem;
        private IDataService dataService;

        [ServiceDependency]
        public WorkItem RootWorkItem
        {
            set
            {
                this.rootWorkItem = value;
            }
        }

        public override void Load()
        {
            base.Load();
            Command analyzeCommand = rootWorkItem.Commands["AnalyzeCommand"];

            ToolStripMenuItem analyzeMenu = new ToolStripMenuItem("Analyze");
            ToolStripMenuItem menuItem = new ToolStripMenuItem("FTM Analyzer");
            analyzeCommand.AddInvoker(menuItem, "Click");
            analyzeCommand.Status = CommandStatus.Disabled;

            analyzeMenu.DropDownItems.Add(menuItem);

            UIExtensionSite site = rootWorkItem.UIExtensionSites[UIExtensionSiteNames.MainMenu];
            site.Add<ToolStripMenuItem>(analyzeMenu);
        }

        [CommandHandler("AnalyzeCommand")]
        public void AnalyzeHandler(object sender, System.EventArgs e)
        {
            if (this.dataService != null)
            {
                MessageBox.Show("You have " + dataService.People.Count + " people in your tree.");
            }
        }

        [EventSubscription(EventTopicNames.FtmDatabaseOpened)]
        public void FtmDatabaseOpened(object sender, System.EventArgs e)
        {
            rootWorkItem.Commands["AnalyzeCommand"].Status = CommandStatus.Enabled;
            this.dataService = rootWorkItem.Services.Get<IDataService>();
        }

        [EventSubscription(EventTopicNames.FtmDatabaseClosed)]
        public void FtmDatabaseClosed(object sender, System.EventArgs e)
        {
            rootWorkItem.Commands["AnalyzeCommand"].Status = CommandStatus.Disabled;
            this.dataService = null;
        }

        [EventSubscription(EventTopicNames.PersonSelected)]
        public void PersonSelected(object sender, System.EventArgs e)
        {
            int x = 0;
        }
    }
}
