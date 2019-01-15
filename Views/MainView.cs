// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainView.cs" company="CNC Software, Inc.">
//   Copyright (c) 2013 CNC Software, Inc.
// </copyright>
// <summary>
//   A main view.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ChainManagerExample.Views
{
    using System;
    using System.Linq;
    using System.Windows.Forms;

    using ChainManagerExample.Services;

    using Mastercam.Database;
    using Mastercam.Database.Types;
    using Mastercam.IO;
    using Mastercam.Math;

    /// <summary> A main view. </summary>
    public partial class MainView : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainView"/> class.
        /// </summary>
        public MainView()
        {
            this.InitializeComponent();
        }

        /// <summary> Raises the chain level event. </summary>
        ///
        /// <param name="sender"> Source of the event. </param>
        /// <param name="e">      Event information to send to registered event handlers. </param>
        private void OnChainLevel(object sender, EventArgs e)
        {
            const int Level = 5;

            UtilityService.Initialize();

            UtilityService.DrawBlock(new Point3D(0, 0, 0), new Point3D(10, 10, 0), -0.5, Level);

            var chains = ChainManager.ChainAll(true, false, ViewManager.CPlane, ChainDirectionType.Clockwise, Level);

            if (!chains.Any())
            {
                UtilityService.ShowMessage("No chains returned");
            }

            if (this.Contour(chains))
            {
                UtilityService.FitScreen();
            }
        }

        /// <summary> Raises the chain all selected event. </summary>
        ///
        /// <param name="sender"> Source of the event. </param>
        /// <param name="e">      Event information to send to registered event handlers. </param>
        private void OnChainAllSelected(object sender, EventArgs e)
        {
            UtilityService.Initialize();

            UtilityService.DrawPolygon();

            SelectionManager.SelectAllGeometry();

            var chains = ChainManager.ChainAllSelected(true);
            if (!chains.Any())
            {
                UtilityService.ShowMessage("No chains returned");
                return;
            }

            if (this.Contour(chains))
            {
                UtilityService.FitScreen();
            }
        }

        /// <summary> Raises the chain geometry event. </summary>
        ///
        /// <param name="sender"> Source of the event. </param>
        /// <param name="e">      Event information to send to registered event handlers. </param>
        private void OnChainGeometry(object sender, EventArgs e)
        {
            UtilityService.Initialize();

            var chains = ChainManager.ChainGeometry(UtilityService.DrawRectangle());
            if (!chains.Any())
            {
                UtilityService.ShowMessage("No chains returned");
                return;
            }

            if (this.Contour(chains))
            {
                UtilityService.FitScreen();
            }
        }

        /// <summary> Contours the given chains. </summary>
        ///
        /// <param name="chains"> The chains. </param>
        ///
        /// <returns> true if it succeeds, false if it fails. </returns>
        private bool Contour(Chain[] chains)
        {
            var endMill = UtilityService.CreateTool();
            if (endMill == null)
            {
                UtilityService.ShowMessage("Failed to create tool");
                return false;
            }

            var contourOp = UtilityService.CreateContourOperation(endMill, chains);
            if (contourOp == null)
            {
                UtilityService.ShowMessage("Failed to create contour operation");
                return false;
            }

            return true;
        }

        /// <summary> Raises the close view event. </summary>
        ///
        /// <param name="sender"> Source of the event. </param>
        /// <param name="e">      Event information to send to registered event handlers. </param>
        private void OnCloseView(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
