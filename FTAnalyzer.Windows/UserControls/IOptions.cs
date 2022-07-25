using System;
using System.Drawing;

namespace FTAnalyzer.UserControls
{
	internal interface IOptions
	{
		void Save();
		void Cancel();
        bool HasValidationErrors { get; }
        string DisplayName { get; }
		string TreePosition { get; }
		Image MenuIcon{get;}
	}
}
