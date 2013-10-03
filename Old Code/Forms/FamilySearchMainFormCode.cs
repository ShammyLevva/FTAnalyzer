using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Old_Code.Forms
{
    class FamilySearchMainFormCode
    {
        //else if (tabSelector.SelectedTab == tabFamilySearch)
        //{
        //    btnCancelFamilySearch.Visible = false;
        //    btnViewResults.Visible = true;
        //    tsCountLabel.Text = "";
        //    btnFamilySearchChildrenSearch.Enabled = btnFamilySearchMarriageSearch.Enabled = ft.IndividualCount > 0;
        //    try
        //    {
        //        txtFamilySearchfolder.Text = (string)Application.UserAppDataRegistry.GetValue("FamilySearch Search Path");
        //    }
        //    catch (Exception)
        //    {
        //        txtFamilySearchfolder.Text = string.Empty;
        //    }
        //}


        //private void btnFamilySearchFolderBrowse_Click(object sender, EventArgs e)
        //{
        //    FolderBrowserDialog browse = new FolderBrowserDialog();
        //    browse.ShowNewFolderButton = true;
        //    browse.Description = "Please select a folder where the results of the FamilySearch search will be placed";
        //    browse.RootFolder = Environment.SpecialFolder.Desktop;
        //    if (txtFamilySearchfolder.Text != string.Empty)
        //        browse.SelectedPath = txtFamilySearchfolder.Text;
        //    if (browse.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {
        //        Application.UserAppDataRegistry.SetValue("FamilySearch Search Path", browse.SelectedPath);
        //        txtFamilySearchfolder.Text = browse.SelectedPath;
        //    }
        //}

        //private void btnFamilySearchMarriageSearch_Click(object sender, EventArgs e)
        //{
        //    HourGlass(true);
        //    btnCancelFamilySearch.Visible = true;
        //    btnViewResults.Visible = false;
        //    btnFamilySearchChildrenSearch.Enabled = false;
        //    btnFamilySearchMarriageSearch.Enabled = false;
        //    rtbFamilySearchResults.Text = "FamilySearch Marriage Search started.\n";
        //    int level = rbFamilySearchCountry.Checked ? FactLocation.COUNTRY : FactLocation.REGION;
        //    FamilySearchForm form = new FamilySearchNewSearchForm(rtbFamilySearchResults, FamilySearchDefaultCountry.Country, level, FamilySearchrelationTypes.Status, txtFamilySearchSurname.Text, webBrowser);
        //    IList<Family> families = ft.AllFamilies.ToList();
        //    int counter = 0;
        //    pbFamilySearch.Visible = true;
        //    pbFamilySearch.Maximum = families.Count;
        //    pbFamilySearch.Value = 0;
        //    stopProcessing = false;
        //    foreach (Family f in ft.AllFamilies)
        //    {
        //        form.SearchFamilySearch(f, txtFamilySearchfolder.Text, FamilySearchForm.MARRIAGESEARCH);
        //        pbFamilySearch.Value = counter++;
        //        Application.DoEvents();
        //        if (stopProcessing)
        //            break;
        //    }
        //    pbFamilySearch.Visible = false;
        //    btnCancelFamilySearch.Visible = false;
        //    btnViewResults.Visible = true;
        //    btnFamilySearchChildrenSearch.Enabled = true;
        //    btnFamilySearchMarriageSearch.Enabled = true;
        //    rtbFamilySearchResults.AppendText("\nFamilySearch Marriage Search finished.\n");
        //    HourGlass(false);
        //}

        //private void btnFamilySearchChildrenSearch_Click(object sender, EventArgs e)
        //{
        //    HourGlass(true);
        //    btnCancelFamilySearch.Visible = true;
        //    btnViewResults.Visible = false;
        //    btnFamilySearchChildrenSearch.Enabled = false;
        //    btnFamilySearchMarriageSearch.Enabled = false;
        //    rtbFamilySearchResults.Text = "FamilySearch Children Search started.\n";
        //    int level = rbFamilySearchCountry.Checked ? FactLocation.COUNTRY : FactLocation.REGION;
        //    FamilySearchForm form = new FamilySearchOldSearchForm(rtbFamilySearchResults, FamilySearchDefaultCountry.Country, level, FamilySearchrelationTypes.Status, txtFamilySearchSurname.Text);
        //    IList<Family> families = ft.AllFamilies.ToList();
        //    int counter = 0;
        //    pbFamilySearch.Visible = true;
        //    pbFamilySearch.Maximum = families.Count;
        //    pbFamilySearch.Value = 0;
        //    stopProcessing = false;
        //    foreach (Family f in families)
        //    {
        //        pbFamilySearch.Value = counter++;
        //        form.SearchFamilySearch(f, txtFamilySearchfolder.Text, FamilySearchForm.CHILDRENSEARCH);
        //        Application.DoEvents();
        //        if (stopProcessing)
        //            break;
        //    }
        //    pbFamilySearch.Visible = false;
        //    btnCancelFamilySearch.Visible = false;
        //    btnViewResults.Visible = true;
        //    btnFamilySearchChildrenSearch.Enabled = true;
        //    btnFamilySearchMarriageSearch.Enabled = true;
        //    rtbFamilySearchResults.AppendText("\nFamilySearch Children Search finished.\n");
        //    HourGlass(false);
        //}

        //private void censusCountry_CountryChanged(object sender, EventArgs e)
        //{
        //    cenDate.Country = censusCountry.Country;
        //}

        //private void btnViewResults_Click(object sender, EventArgs e)
        //{
        //    FamilySearchResultsViewer frmResults = new FamilySearchResultsViewer(txtFamilySearchfolder.Text);
        //    if (frmResults.ResultsPresent)
        //    {
        //        DisposeDuplicateForms(frmResults);
        //        frmResults.Show();
        //    }
        //    else
        //    {
        //        frmResults.Dispose();
        //        MessageBox.Show("Sorry there are no results files in the selected folder.");
        //    }
        //}

        //private void btnCancelFamilySearch_Click(object sender, EventArgs e)
        //{
        //    stopProcessing = true;
        //}

        //private void FamilySearchDefaultCountry_CountryChanged(object sender, EventArgs e)
        //{
        //    if (FamilySearchDefaultCountry.Country == Countries.SCOTLAND)
        //        rbFamilySearchCountry.Checked = true;
        //    else
        //        rbFamilySearchRegion.Checked = true;
        //}

        //private void rtbFamilySearchResults_TextChanged(object sender, EventArgs e)
        //{
        //    rtbFamilySearchResults.ScrollToBottom();
        //}

    }
}
