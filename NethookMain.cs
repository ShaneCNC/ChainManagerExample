// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NethookMain.cs" company="CNC Software, Inc.">
//   Copyright (c) 2013 CNC Software, Inc.
// </copyright>
// <summary>
//   Describes this class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ChainManagerExample
{
    using ChainManagerExample.Views;

    using Mastercam.App;
    using Mastercam.App.Types;

    /// <summary>
    /// Describes this class.
    /// </summary>
    public class NethookMain : NetHook3App
    {
        #region Public Override Methods

        /// <summary>
        /// The main entry point for your NETHook.
        /// </summary>
        /// <param name="param">System parameter.</param>
        /// <returns>A <c>MCamReturn</c> return type representing the outcome of your NetHook application.</returns>
        public override MCamReturn Run(int param)
        {
            using (var view = new MainView())
            {
                view.ShowDialog();
            }

            return MCamReturn.NoErrors;
        }

        #endregion
    }
}
