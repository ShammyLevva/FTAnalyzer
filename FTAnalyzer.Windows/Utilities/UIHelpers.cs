using System;
using System.Configuration;
using System.IO;

namespace FTAnalyzer.Utilities
{
    public static class UIHelpers
    {
        public static int ShowYesNo(string message, string title)
        {
            DialogResult result = MessageBox.Show(Form.ActiveForm, message, title, MessageBoxButtons.YesNo);
            return (int)result;
        }

        public static int ShowMessage(string message) => ShowMessage(message, "FTAnalyzer");
        public static int ShowMessage(string message, string title) => (int)MessageBox.Show(Form.ActiveForm, message, title);

        public static int ShowMessage(Form form, string message) => (int)MessageBox.Show(form, message, string.Empty);
        public static int ShowMessage(Form form, string message, string title) => (int)MessageBox.Show(form, message, title);

        public static DialogResult ShowMessage(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon) =>
            MessageBox.Show(Form.ActiveForm, message, title, buttons, icon);

        public static int Yes => (int)DialogResult.Yes;
        public static int No => (int)DialogResult.No;
        public static int Cancel => (int)DialogResult.Cancel;

        public enum Direction { LEFT, RIGHT, ABOVE, BELOW };
        public static void MoveControl(Control ctrl, Control relativeTo, Direction direction)
        {
            if (direction == Direction.LEFT) // Control to be to the left of relativeTo
            {
                int newLeft = relativeTo.Left - relativeTo.Margin.Left - ctrl.Width;
                if (newLeft > 0)
                    ctrl.Left = newLeft;
                ctrl.Top = relativeTo.Top;
            }
            else if (direction == Direction.RIGHT) // Control to be to the left of relativeTo
            {
                int newLeft = relativeTo.Right + relativeTo.Margin.Right;
                ctrl.Left = newLeft;
                ctrl.Top = relativeTo.Top;
            }
            else if (direction == Direction.ABOVE) // Control to be above relativeTo
            {
                int newTop = relativeTo.Top - relativeTo.Margin.Top;
                if (newTop > 0)
                    ctrl.Top = newTop;
                ctrl.Left = relativeTo.Left;
            }
            else if (direction == Direction.BELOW) // Control to be below relativeTo
            {
                int newTop = relativeTo.Bottom + relativeTo.Margin.Bottom;
                ctrl.Top = newTop;
                ctrl.Left = relativeTo.Left;
            }
        }

        /// <summary>
        /// Safely saves application settings with proper error handling.
        /// </summary>
        /// <param name="settingsObject">The settings object to save (e.g., Settings.Default, GeneralSettings.Default)</param>
        /// <param name="showErrorToUser">Whether to display error message to user (default: true)</param>
        /// <returns>True if save succeeded, false if it failed</returns>
        public static bool SafeSaveSettings(ApplicationSettingsBase settingsObject, bool showErrorToUser = true)
        {
            try
            {
                settingsObject.Save();
                return true;
            }
            catch (ConfigurationErrorsException ex)
            {
                if (showErrorToUser)
                {
                    string message = $"Unable to save your settings.\n\n" +
                                   $"Error: {ex.Message}\n\n" +
                                   $"Possible solutions:\n" +
                                   $"1. Close FTAnalyzer completely and restart it\n" +
                                   $"2. Try running FTAnalyzer as Administrator\n" +
                                   $"3. Check if your AppData\\Local folder is accessible\n" +
                                   $"4. Check if any antivirus software is blocking file access\n\n" +
                                   $"FTAnalyzer will continue to work, but your settings may not be saved.";
                    ShowMessage(message, "Settings Save Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return false;
            }
            catch (IOException ex)
            {
                if (showErrorToUser)
                {
                    string message = $"Unable to save your settings due to a file access error.\n\n" +
                                   $"Error: {ex.Message}\n\n" +
                                   $"This may be caused by:\n" +
                                   $"1. Another instance of FTAnalyzer running\n" +
                                   $"2. Insufficient disk space\n" +
                                   $"3. File permissions issues\n\n" +
                                   $"FTAnalyzer will continue to work, but your settings may not be saved.";
                    ShowMessage(message, "Settings Save Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return false;
            }
            catch (Exception ex)
            {
                if (showErrorToUser)
                {
                    string message = $"An unexpected error occurred while saving your settings.\n\n" +
                                   $"Error: {ex.Message}\n\n" +
                                   $"FTAnalyzer will continue to work, but your settings may not be saved.";
                    ShowMessage(message, "Settings Save Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return false;
            }
        }
    }
}
